/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using System;
using EPiServer.Core;
using EPiServer.Web.WebControls;

namespace EPiServer.Templates.Public.Units.Placeable
{
    /// <summary>
    /// The DocumentList template is used to display a list of document pages 
    /// with document name, a short summary and a link to download the document.
    /// </summary>
    public partial class DocumentList : UserControlBase
    {

        private string _pageLinkProperty;
        private bool _showText = true;
        private string _maxCountProperty;
        private string _heading;

        /// <summary>
        /// Sets up Document list properties and databinds the document list.
        /// </summary>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                DocList.PageLinkProperty = PageLinkProperty;
                if (MaxCountProperty != null && CurrentPage[MaxCountProperty] != null)
                {
                    DocList.MaxCount = (int)CurrentPage[MaxCountProperty];
                }
                DocList.DataBind();
            }
        }

        /// <summary>
        /// Returns the preview text if based on the document list property <see cref="ShowText"/>.
        /// </summary>
        /// <param name="container">The container of the currently rendered PageList template.</param>
        /// <returns>Preview text for the document if <see cref="ShowText"/> is set to true, otherwise <see cref="String.Empty"/>.</returns>
        protected string GetPreviewText(PageTemplateContainer container)
        {
            if (ShowText)
            {
                return container.PreviewText;
            }
            return string.Empty;
        }

        /// <summary>
        /// Name of the page property pointing out the parent page of this document list.
        /// </summary>
        /// <returns>The name of the page property pointing out the parent page.</returns>
        public string PageLinkProperty
        {
            get { return _pageLinkProperty; }
            set { _pageLinkProperty = value; }
        }

        /// <summary>
        /// Decides whether the preview text is shown or not.
        /// </summary>
        /// <returns>If set to true the preview text will be shown; otherwise not.</returns>
        public bool ShowText
        {
            get { return _showText; }
            set { _showText = value; }
        }

        /// <summary>
        /// The name of the page property indicating the maximum number of items shown in the Document list.
        /// </summary>
        /// <returns>The number of ducuments to show in the list</returns>
        public string MaxCountProperty
        {
            get { return _maxCountProperty; }
            set { _maxCountProperty = value; }
        }

        /// <summary>
        /// The heading of the document list
        /// </summary>
        /// <returns>The name of the page where the list is used</returns>
        protected string Heading
        {
            get { return _heading ?? (_heading = GetPage(DocList.PageLink).PageName); }
        }
    }
}