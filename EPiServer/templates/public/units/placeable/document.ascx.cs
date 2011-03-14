/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using System;
using System.Web;
using EPiServer.Core;

namespace EPiServer.Templates.Public.Units.Placeable
{
    /// <summary>
    /// Shows a link to a single document, also displaying an icon corresponding to the file type.
    /// </summary>
    public partial class Document : UserControlBase
    {
        private string _filePath;
        private PageData _documentPage;

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                if (FilePath != null)
                {
                    DocumentLink.Visible = true;
                    DocumentLink.NavigateUrl = FilePath;
                    DocumentLink.CssClass = string.Format("{0}Extension", VirtualPathUtility.GetExtension(FilePath).Substring(1));
                    DocumentLink.Text = Server.HtmlEncode(VirtualPathUtility.GetFileName(FilePath));
                    DataBind();
                }
                else
                {
                    // Show the error message label and set the translated error message
                    ErrorMessage.Visible = true;
                    ErrorMessage.Text = Translate("/error/document");
                }
            }
        }

        /// <summary>
        /// Gets the encoded path to the document file.
        /// </summary>
        /// <value>The file path, HTTP path-encoded, since it comes from a PropertyUrlReference</value>
        public string FilePath
        {
            get 
            {
                if (_filePath == null)
                { 
                    _filePath = DocumentPage["DocumentInternalPath"] as string;
                }
                return _filePath;
            }
        }

        /// <summary>
        /// Gets or sets the document page.
        /// </summary>
        /// <value>The document page.</value>
        public PageData DocumentPage
        {
            get { return _documentPage ?? CurrentPage; }
            set { _documentPage = value; }
        }
    }
}