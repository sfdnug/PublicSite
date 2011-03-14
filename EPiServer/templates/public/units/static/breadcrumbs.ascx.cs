/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using System;
using EPiServer;
using EPiServer.Configuration;
using EPiServer.Core;
using System.Text;

namespace EPiServer.Templates.Public.Units.Static
{
    /// <summary>
    /// Provide links back to each previous page that the user navigated through in order 
    /// to get to the current page. In the case of the trail being too long, only the start page
    /// parent pages below the <see cref="MaxLength"/> threshold are shown.
    /// </summary>
    public partial class BreadCrumbs : UserControlBase
    {
        private const string _link = "<a href=\"{0}\" title=\"{1}\">{2}</a>";
        private string _separator = " / ";
        private int _maxLength = 60;

        /// <summary>
        /// In case the breadcrumb string is longer then the specified MaxLength, 
        /// a shorter alternative is used where only the start page, parent page
        /// and the current page are shown.
        /// </summary>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            Breadcrumbs.Text = GenerateBreabCrumbs(CurrentPage);

        }

        /// <summary>
        /// Creates the bread crumb link string from the start page of the site to the supplied page
        /// </summary>
        /// <param name="page">The last page in the bread crumb string.</param>
        /// <returns>A bread crumb string with anchors to parent pages.</returns>
        private string GenerateBreabCrumbs(PageData page)
        {
            // Initiate a string builder based on max length. The generated html is considerably longer than the visible text.
            StringBuilder breadCrumbsText = new StringBuilder(8 * MaxLength);

            // Initiate a counter holding the visible length of the bread crumbs with the length of the start page link text.
            int breadCrumbsLength = Translate("/navigation/startpage").Length;

            while (page != null && page.PageLink != PageReference.StartPage)
            {
                breadCrumbsLength += page.PageName.Length + Separator.Length;
                if (breadCrumbsLength > MaxLength)
                {
                    breadCrumbsText.Insert(0, Separator + "...");
                    break;
                }

                // Insert the link at beginning of the bread crumbs string 
                breadCrumbsText.Insert(0, Separator + GetLink(page));

                // Get next visible parent
                page = GetParentPageData(page);
            }

            // Generate the start page link 
            string startPageLinkUrl = Server.UrlPathEncode(GetPage(PageReference.StartPage).LinkURL);
            string startPageLink = String.Format(_link, startPageLinkUrl, Translate("/navigation/startpagelinktitle"), Translate("/navigation/startpage"));

            // Insert the startpage link and return
            return breadCrumbsText.Insert(0, startPageLink).ToString();
        }

        /// <summary>
        /// Get the next visible parent page of the supplied <see cref="PageData"/>. 
        /// </summary>
        /// <param name="page"></param>
        /// <returns>The <see cref="PageData"/> object or    </returns>
        private PageData GetParentPageData(PageData pageData) 
        {
            // Don't return a PageData object for start page or root page.
            if (pageData == null || pageData.ParentLink == PageReference.StartPage || pageData.ParentLink == PageReference.RootPage)
            {
                return null;
            }

            // Get Page data for parent page
            pageData = GetPage(pageData.ParentLink);
            if (pageData != null && pageData.VisibleInMenu)
            {
                return pageData;
            }
            // Step up to next parent
            return GetParentPageData(pageData);
        }


        /// <summary>
        /// Returns a anchor based on a <see cref="PageData"/> object.
        /// </summary>
        private string GetLink(PageData page)
        {
            string pageName = page.Property["PageName"].ToWebString();
            return string.Format(_link, Server.UrlPathEncode(page.LinkURL), pageName, pageName);
        }

        /// <summary>
        /// A string used to separate the page links (default = " / ")
        /// </summary>
        /// <returns>The breadcrumbs separator string</returns>
        public string Separator
        {
            get { return _separator; }
            set { _separator = value; }
        }

        /// <summary>
        /// Sets the max length on the visible breadcrumb text (default = 60)
        /// </summary>
        public int MaxLength
        {
            get { return _maxLength; }
            set { _maxLength = value; }
        }
    }
}