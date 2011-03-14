/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using System;
using System.IO;
using System.Web.Hosting;
using System.Web.UI.WebControls;
using EPiServer.Core;
using EPiServer.Web.PropertyControls;

namespace EPiServer.Templates.Public.Units.Placeable
{
    /// <summary>
    /// Used to display files in a tree structure. The root folder for the structure is 
    /// retrieved from the "RootFolder" page property.
    /// </summary>
    public partial class FileList : UserControlBase
    {
        /// <summary>
        /// Set up the root of the FileTreeDataSource control
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                string rootFolder = CurrentPage["RootFolder"] as string;
                if (!String.IsNullOrEmpty(rootFolder))
                {
                    FileDataSource.Root = rootFolder;
                }

                if (CurrentPage["SortFilesBy"] != null)
                {
                    FileDataSource.SortOrder = (FileSortOrder)CurrentPage["SortFilesBy"];
                }
            }
        }

        protected void FileTree_TreeNodeDataBound(object sender, System.Web.UI.WebControls.TreeNodeEventArgs e)
        {
            VirtualFileBase virtualFile = e.Node.DataItem as VirtualFileBase;
            if (virtualFile.IsDirectory)
            {
                e.Node.SelectAction = TreeNodeSelectAction.Expand;
            }
            else
            {
                e.Node.NavigateUrl = e.Node.DataPath;
            }
            e.Node.Text = Server.HtmlEncode(virtualFile.Name);
        }

    }
}