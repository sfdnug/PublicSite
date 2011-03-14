/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using System;
using System.Globalization;
using System.Web.UI.HtmlControls;

using EPiServer.Core;
using EPiServer.Web.WebControls;
using EPiServer.DataAbstraction;


namespace EPiServer.Templates.Public.Units.Static
{
    /// <summary>
    /// A list of general functions such as login/logout, rss and language
    /// </summary>
	public partial class Functions : UserControlBase
	{
        private PageData _startPage;

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                SetPageReference(Rss, "RssContainer");
                SetPageReference(Sitemap, "SitemapPage");

                if (Configuration.Settings.Instance.UIShowGlobalizationUserInterface)
                {
                    SetLanguage();
                }
            }
	    }

        /// <summary>
        /// </summary>
        /// <param name="propertyControl"></param>
        /// <param name="property"></param>
        private void SetPageReference(Property propertyControl, string property)
        {
            PageReference pageRef = StartPage[property] as PageReference;

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

        /// <summary>
        /// Initializes the language link.
        /// Checks the number of available and enabled languages. If more than two, 
        /// populates a dropdown-menu with the available and enabled languages.
        /// Otherwise sets the link to the not currently active language.
        /// </summary>
        private void SetLanguage()
        {
            PageDataCollection languageBranches = DataFactory.Instance.GetLanguageBranches(CurrentPage.PageLink);
            if (languageBranches == null) { return; }

            if (languageBranches.Count > 2)
            {     
                LanguageList.Visible = LanguageListLabel.Visible = LanguageButton.Visible = LanguageList.Parent.Visible = true;

                foreach (PageData languageBranch in languageBranches)
                {
                    if (languageBranch.LanguageID != CurrentPage.LanguageID && LanguageBranch.Load(languageBranch.LanguageID).Enabled)                    
                    {
                        LanguageList.Items.Add(new System.Web.UI.WebControls.ListItem(new CultureInfo(languageBranch.LanguageID).NativeName, languageBranch.LanguageID));
                    }
                }
            }
            else
            {
                foreach (PageData languageBranch in languageBranches)
                {
                    if (languageBranch.LanguageID != CurrentPage.LanguageID && LanguageBranch.Load(languageBranch.LanguageID).Enabled)
                    {
                        Language.Visible = Language.Parent.Visible = true;
                        Language.NavigateUrl = EPiServer.UriSupport.AddLanguageSelection(languageBranch.LinkURL, languageBranch.LanguageID);
                        Language.Text = Translate(new CultureInfo(languageBranch.LanguageID).NativeName);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Redirects to the selected language.
        /// </summary>
        public void ChangeLanguage(object sender, EventArgs e)
        {
            if (LanguageList.SelectedValue != "noLangSelected")
            {
                Response.Redirect(EPiServer.UriSupport.AddLanguageSelection(CurrentPage.LinkURL, LanguageList.SelectedValue));
            }
        }

        /// <summary>
        /// Gets the link to the login page
        /// </summary>
        protected string GetLoginUrl()
        {
            PageReference loginPageRef = CurrentPage["LoginPage"] as PageReference;
            if (loginPageRef != null)
            {
                return Server.HtmlEncode(DataFactory.Instance.GetPage(loginPageRef).LinkURL);
            }
            LoginView.Visible = false;
            return string.Empty;
        }
	}
}