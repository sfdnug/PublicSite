/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using EPiServer.Core;

namespace EPiServer.Templates.Public.Pages
{
    /// <summary>
    /// The Site Map page displays the page structure beneath a specific page, the IndexRoot parameter. 
    /// If If no page has been set the start page of the site is used as root.
    /// </summary>
    public partial class SiteMap : Page
    {
        private PageReference _indexRoot;

		/// <summary>
		/// Gets the page used as the root for the site map
		/// </summary>
		/// <remarks>If the IndexRoot page property is not set the start page will be used instead</remarks>
        public PageReference IndexRoot
        {
            get 
            { 
                if(PageReference.IsNullOrEmpty(_indexRoot))
                {
					_indexRoot = CurrentPage["IndexRoot"] as PageReference ?? PageReference.StartPage;
                }
                return _indexRoot;
            }
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            SiteMapTree.DataBind();
        }
    }
}