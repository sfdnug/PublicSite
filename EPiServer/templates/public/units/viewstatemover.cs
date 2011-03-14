#region Copyright © 1997-2007 EPiServer AB. All Rights Reserved.
/*
This code may only be used according to the EPiServer License Agreement.
The use of this code outside the EPiServer environment, in whole or in
parts, is forbidden without prior written permission from EPiServer AB.

EPiServer is a registered trademark of EPiServer AB. For more information 
see http://www.episerver.com/license or request a copy of the EPiServer 
License Agreement by sending an email to info@episerver.com
*/
#endregion
using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

using EPiServer.Core;
using EPiServer.PlugIn;
using EPiServer.Web;

namespace EPiServerSample.templates.Units
{
    /// <summary>
    /// Move ViewState from the start of the form element, to the end of it. View state looks like this:
    /// input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="..."
    /// All input elements of where name begins with "__VIEWSTATE" are treated as view state.
    /// Scripts that follow any found view state inside the form element are also moved to follow after the moved
    /// view state to enable scripts that depend on the existance of view state to work as well.
    /// ViewState may be large, and cause search engines to miss significant data or rank pages lower.
    /// To disable it, remove this file from the project, or implement a Web.Config setting or similar mechanism
    /// to control it's activity.
    /// </summary>
    [PagePlugIn()]
    public class ViewStateMover
    {
        private static bool _enableWhitespaceRemoval = false;
        /// <summary>
        /// Called by the EPiServer framework during startup, due to the PagePlugIn attribute. This is not
        /// a PagePlugIn, we're just piggybacking the mechanism to ensure we get called once (and only once).
        /// </summary>
        /// <param name="optionFlag"></param>
        public static void Initialize(int optionFlag)
        {
            string enableViewStateMover = System.Web.Configuration.WebConfigurationManager.AppSettings["enableViewStateMover"];
            if (!String.IsNullOrEmpty(enableViewStateMover))
            {
                bool isEnableViewStateMover;
                if (Boolean.TryParse(enableViewStateMover, out isEnableViewStateMover))
                {
                    if (!isEnableViewStateMover)
                    {
                        return;
                    }
                }
            }

            HtmlRewriteToExternal.HtmlRewriteInit += HtmlRewriteToExternal_HtmlRewriteInit;

            // Pick-up optional setting from Web.Config
            string enableWhitespaceRemoval = System.Web.Configuration.WebConfigurationManager.AppSettings["enableViewStateMoverWhitespaceRemoval"];
            if (!String.IsNullOrEmpty(enableWhitespaceRemoval))
            {
                Boolean.TryParse(enableWhitespaceRemoval, out _enableWhitespaceRemoval);
            }
        }

        /// <summary>
        /// Init the HtmlRewrite-engines event handlers. This is called every time a HtmlRewritePipe object
        /// is instantiated by the UrlRewriteModule
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EPiServer.Web.HtmlRewriteEventArgs"/> instance containing the event data.</param>
        static private void HtmlRewriteToExternal_HtmlRewriteInit(object sender, HtmlRewriteEventArgs e)
        {
            // We need an instance of ourselves, to keep track of our state
            ViewStateMover viewStateMover = new ViewStateMover();

            // There are two major events from the HtmlRewrite-engine, which allow us to rewrite
            // names and values of the content. The exact definition depends on the XmlNodeType
            // that is being processed.
            e.RewritePipe.HtmlRewriteName += viewStateMover.HtmlRewriteNameEventHandler;
            e.RewritePipe.HtmlRewriteValue += viewStateMover.HtmlRewriteValueEventHandler;
        }

        /// <summary>
        /// The states of our state-machine as we find what we're looking for.
        /// </summary>
        private enum State { WaitingForForm, InFormElement, BufferingInput, BufferingScript, SkipToEnd };
        private State _state;

        /// <summary>
        /// Helper class to group the value and quote char used for attribute values
        /// </summary>
        private class AttributeValue
        {
            /// <summary>
            /// The Value of an attribute
            /// </summary>
            public string Value;
            /// <summary>
            /// The quote char used
            /// </summary>
            public char QuoteChar;
            /// <summary>
            /// Initialize an instance
            /// </summary>
            /// <param name="value"></param>
            /// <param name="quoteChar"></param>
            public AttributeValue(string value, char quoteChar)
            {
                Value = value;
                QuoteChar = quoteChar;
            }
        }

        /// <summary>
        /// Buffer attributes of the "input" element here
        /// </summary>
        private Dictionary<string, AttributeValue> _inputAttributes = new Dictionary<string, AttributeValue>();

        /// <summary>
        /// Keep the list of view state elements here
        /// </summary>
        private List<Dictionary<string, AttributeValue>> _viewState = new List<Dictionary<string, AttributeValue>>();

        /// <summary>
        /// Marker to determine wether in fact we've seen the ViewState
        /// </summary>
        private bool _viewStateFound = false;

        /// <summary>
        /// Handle rewrite name
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EPiServer.Web.HtmlRewriteEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// The name event is raised before an associated value event. Check the e.NodeType and other properties to determine
        /// course of action.
        /// </remarks>
        private void HtmlRewriteNameEventHandler(object sender, HtmlRewriteEventArgs e)
        {
            switch (_state)
            {
                case State.WaitingForForm:
                    if (e.NodeType == XmlNodeType.Element && String.Compare(e.Name, "form", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        _state = State.InFormElement;
                    }
                    break;

                case State.InFormElement:
                    if (e.NodeType != XmlNodeType.Element)
                    {
                        break;
                    }
                    if (String.Compare(e.Name, "input", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        _inputAttributes.Clear();
                        e.IsHoldingOutput = true;
                        _state = State.BufferingInput;
                        break;
                    }

                    // If we've seen ViewState, we either buffer script, or emit previously buffered script because
                    // we're not at the end of the form element yet.
                    if (_viewState.Count > 0)
                    {
                        if (String.Compare(e.Name, "script", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            _scripts.Append("<script");
                            e.IsHoldingOutput = true;
                            _state = State.BufferingScript;
                            break;
                        }

                        // If we find an element inside the form element and we have scripts buffered, we change our
                        // mind and output it anyway, since we want to ensure that only the last scripts are output
                        // after the ViewState (which should be the ones registred as startup script).
                        if (_scripts.Length > 0)
                        {
                            e.ValueBuilder.Length = 0;
                            e.ValueBuilder.Append(_scripts);
                            _scripts.Length = 0;
                            e.ValueBuilder.Append("<").Append(e.Name);
                        }
                    }
                    break;
            }
        }

        private StringBuilder _scripts = new StringBuilder();

        /// <summary>
        /// Handle rewrite value
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EPiServer.Web.HtmlRewriteEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// The value event is raised after an associated name event. Check the e.NodeType and other properties to determine
        /// course of action.
        /// </remarks>
        private void HtmlRewriteValueEventHandler(object sender, HtmlRewriteEventArgs e)
        {
            // Remove all insignificant whitespace
            if (_enableWhitespaceRemoval && e.NodeType == XmlNodeType.Whitespace)
            {
                e.ValueBuilder.Length = 0;
                e.ValueBuilder.Append(" ");
                return;
            }
            switch (_state)
            {
                // We're buffering the contents of input element attributes, while checking for if it is a ViewState field
                case State.BufferingInput:
                    switch (e.NodeType)
                    {
                        // Another attribute, add it to the collection and check if it's a ViewState
                        case XmlNodeType.Attribute:
                            _inputAttributes.Add(e.Name, new AttributeValue(e.Value, e.QuoteChar));
                            _viewStateFound |= String.Compare(e.Name, "name", StringComparison.OrdinalIgnoreCase) == 0 && e.Value.StartsWith("__VIEWSTATE");
                            e.IsHoldingOutput = true;
                            break;

                        // End of the input element start tag. Determine if we're to output and continue waiting, or if it's the ViewState,
                        // in which case we switch state, and output nothing here.
                        case XmlNodeType.Element:
                            if (_viewStateFound && e.IsEmptyElement)
                            {
                                e.IsHoldingOutput = true;
                                _viewState.Add(_inputAttributes);
                                _inputAttributes = new Dictionary<string, AttributeValue>();
                                _state = State.InFormElement;
                            }
                            else
                            {
                                e.ValueBuilder.Length = 0;
                                AppendInputElement(e.ValueBuilder, _inputAttributes, e.IsEmptyElement);
                                _inputAttributes.Clear();
                                _state = State.InFormElement;
                            }
                            _viewStateFound = false;
                            break;
                    }
                    break;

                case State.BufferingScript:
                    bool isHoldingOutput = true;
                    switch (e.NodeType)
                    {
                        case XmlNodeType.Attribute:
                            _scripts.AppendFormat(" {0}={2}{1}{2}", e.Name, HttpUtility.HtmlAttributeEncode(e.Value), e.QuoteChar);
                            break;

                        case XmlNodeType.Comment:
                        case XmlNodeType.SignificantWhitespace:
                        case XmlNodeType.Text:
                        case XmlNodeType.CDATA:
                            _scripts.Append(e.Value);
                            break;

                        case XmlNodeType.Element:
                            _scripts.Append(e.Value);
                            if (e.IsEmptyElement)
                            {
                                _state = State.InFormElement;
                            }
                            break;

                        case XmlNodeType.EndElement:
                            _scripts.Append(e.Value);
                            _state = State.InFormElement;
                            break;

                        default:
                            isHoldingOutput = false;
                            break;
                    }
                    if (isHoldingOutput)
                    {
                        e.IsHoldingOutput = true;
                    }
                    break;

                case State.InFormElement:
                    string value;
                    switch (e.NodeType)
                    {
                        case XmlNodeType.EndElement:
                            if (String.Compare(e.Name, "form", StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                value = e.Value;
                                e.ValueBuilder.Length = 0;
                                if (_viewState.Count > 0)
                                {
                                    // Setting display:none because the div will under some circumstances 
                                    // take up place in IE6 even though it shouldn't.
                                    e.ValueBuilder.Append("<div style=\"display:none\">");
                                    foreach (Dictionary<string, AttributeValue> viewState in _viewState)
                                    {
                                        AppendInputElement(e.ValueBuilder, viewState, e.IsEmptyElement);
                                    }
                                    e.ValueBuilder.Append("</div>");
                                }
                                _inputAttributes.Clear();
                                _viewState.Clear();
                                e.ValueBuilder.Append(_scripts);
                                _scripts.Length = 0;
                                e.ValueBuilder.Append(value);
                                _state = State.SkipToEnd;
                            }
                            break;
                    }
                    break;

                case State.SkipToEnd:
                    break;
            }
        }

        /// <summary>
        /// Appends the input element data to the ValueBuilderResult
        /// </summary>
        /// <param name="e">The <see cref="EPiServer.Web.HtmlRewriteEventArgs"/> instance containing the event data.</param>
        /// <param name="_inputAttributes">The _input attributes.</param>
        private void AppendInputElement(StringBuilder sb, Dictionary<string, AttributeValue> _inputAttributes, bool isEmptyElement)
        {
            sb.AppendFormat("<input");
            foreach (KeyValuePair<string, AttributeValue> kvp in _inputAttributes)
            {
                sb.AppendFormat(" {0}={2}{1}{2}", kvp.Key, HttpUtility.HtmlAttributeEncode(kvp.Value.Value), kvp.Value.QuoteChar);
            }
            sb.Append(" />");
        }
    }
}
