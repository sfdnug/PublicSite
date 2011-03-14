/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using EPiServer.Core;
using EPiServer.Web.WebControls;
using System.Web.UI.WebControls;

namespace EPiServer.Templates.Public.Units.Static
{
    /// <summary>
    /// A common footer for the website where links common for the whole site are listed. 
    /// </summary>
    public partial class PageFooter : UserControlBase
    {
        private PageData _startPage;

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            SetPageReference(Privacy, "PrivacyPage");
            SetPageReference(Contact, "ContactPage");
            SetPageReference(Sitemap, "SitemapPage");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyControl"></param>
        /// <param name="property"></param>
        private void SetPageReference(Property propertyControl, string propertyName)
        {
            PageReference pageRef = StartPage[propertyName] as PageReference;

            if (pageRef != null)
            {
                propertyControl.Parent.Visible = true;
                propertyControl.PageLink = pageRef;
            }
        }

        /// <summary>
        /// Gets the start page of the website
        /// </summary>
        public PageData StartPage
        {
            get
            {
                if (_startPage == null)
                {
                    _startPage = DataFactory.Instance.GetPage(PageReference.StartPage);
                }
                return _startPage;
            }
        }
    }
}