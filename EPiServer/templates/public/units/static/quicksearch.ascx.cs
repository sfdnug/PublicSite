/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using System;
using EPiServer;
using EPiServer.Core;

namespace EPiServer.Templates.Public.Units.Static
{
    /// <summary>
    /// Used for quick searching from anywhere on the website. 
    /// Always presents the results on the search page.
    /// The search can be restricted to a page branch by 
    /// setting the "SearchRoot" property.
    /// </summary>
    public partial class QuickSearch : UserControlBase
    {
        private static string _searchUrl = "{0}&quicksearchquery={1}";
        private string _searchPageUrl;

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
        }
	    
        /// <summary>
        /// Redirects to the search result page
        /// </summary>
        protected void Search_Click(object sender, EventArgs e)
        {
            if(SearchPageUrl == null)
            {
                return;
            }
            string searchText = Server.UrlEncode(SearchText.Text.Trim());
            Response.Redirect(string.Format(_searchUrl, SearchPageUrl, searchText));
        }

        /// <summary>
        /// Gets or sets the search page url
        /// </summary>
        /// <remarks>By default the search page url is intialized using the dynamic 
        /// property SearchPage which should point to the PageReference of the search page</remarks>
        public string SearchPageUrl
        {
            get
            {
                if(_searchPageUrl == null)
                {
                	PageReference searchPageRef = CurrentPage["SearchPage"] as PageReference;
                	if (searchPageRef != null) 
                	{
                    	PageData searchPage = GetPage(searchPageRef);
                    	_searchPageUrl = searchPage.LinkURL;
                	}
                }
                return _searchPageUrl;
            }
            set { _searchPageUrl = value; }
        }
    }
}