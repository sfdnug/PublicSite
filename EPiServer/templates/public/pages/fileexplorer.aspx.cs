/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using System;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPiServer.Templates.Public.Pages
{
    /// <summary>
    /// Displays files in a tree structure. The root folder for the structure is 
    /// retrieved from the "RootFolder" page property.
    /// </summary>
    public partial class FileExplorer : TemplatePage
    {
        protected override void OnLoad(EventArgs e)
        {
            string rootFolder = CurrentPage.Property["RootFolder"].Value as String;
            try
            {
                if (!String.IsNullOrEmpty(rootFolder))
                {
                    HostingEnvironment.VirtualPathProvider.GetDirectory(rootFolder);
                }
            }
            catch (HttpException ex)
            {
                FileListControl.Visible = false;
                ErrorMessage.Visible = true;
                ErrorMessage.Text = ex.Message;
            }
        }
    }
}