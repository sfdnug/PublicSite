/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using System;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using EPiServer.Core;
using EPiServer.Core.Html;

namespace EPiServer.Templates.Public.Units.Static
{
    /// <summary>
    /// The header of the website. Generates metadata tags, rss and css links.
    /// </summary>
    public partial class Header : UserControlBase
    {
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            CreateMetaData();
        }

        /// <summary>
        /// Gets the link to a RSS source page
        /// </summary>
        /// <param name="page">The RSS source page</param>
        /// <returns>A string representation of a link</returns>
        protected string GetRss(PageData page)
        {
            string rssLinkTag = "<link rel=\"alternate\" type=\"application/rss+xml\" href=\"{0}\" title=\"{1}\" />";
            return string.Format(rssLinkTag, Server.HtmlEncode(page.LinkURL), page.PageName);
        }


        /// <summary>
        /// Creates the metadata tags for the website
        /// </summary>
        private void CreateMetaData()
        {
            // Description - use MetaDescription property if it exists, otherwise use contents 
            // from MainIntro property. Do not display the meta tag if none of these options return 
            // values.
            string metaDescription = GetPropertyString("MetaDescription", CurrentPage);
            if (metaDescription.Equals(string.Empty))
            {
                metaDescription = GetPropertyString("MainIntro", CurrentPage);
                if (!metaDescription.Equals(string.Empty))
                {
                    metaDescription = TextIndexer.StripHtml(metaDescription, 0);
                    if (metaDescription.Length > 255)
                        metaDescription = metaDescription.Substring(0, 252) + "...";
                }
            }
            if (!metaDescription.Equals(string.Empty))
            {
                CreateMetaTag("description", metaDescription, false);
            }

            // Keywords
            CreateMetaTag("keywords", "MetaKeywords", CurrentPage, false);

            // Author
            CreateMetaTag("author", "MetaAuthor", CurrentPage, false);

            // Rating
            CreateMetaTag("rating", "General", false);

            // Revisit each month
            CreateMetaTag("revisit-after", "4 weeks", false);

            // Generator
            CreateMetaTag("generator", "EPiServer", false);

            // Robots
            CreateMetaTag("robots", "all", false);

            // Charset
            CreateMetaTag("Content-Type", string.Format("text/html; charset={0}", "UTF-8"), true);

            // Created - GMT format
            if (CurrentPage.Created != DateTime.MinValue)
            {
                CreateMetaTag("creation_date", CurrentPage.Created.ToString("R"), false);
            }
            // Last modified data, in GMT format - Note, same as revised
            if (CurrentPage.Changed != DateTime.MinValue)
            {
                CreateMetaTag("last-modified", CurrentPage.Changed.ToString("R"), false);
            }
            // Revised - GMT format
            if (CurrentPage.Changed != DateTime.MinValue)
            {
                CreateMetaTag("revised", CurrentPage.Changed.ToString("R"), false);
            }
            CreateMetaTag("Content-Language", CurrentPage.LanguageBranch, true);
        }

        /// <summary>
        /// Adds a meta tag control to the page header
        /// </summary>
        /// <param name="name">The name of the meta tag</param>
        /// <param name="content">The content of the meta tag</param>
        /// <param name="httpEquiv">True if the meta tag should be HTTP-EQUIV</param>
        private void CreateMetaTag(string name, string content, bool httpEquiv)
        {
            HtmlMeta tag = new HtmlMeta();
            if(httpEquiv)
            {
                tag.HttpEquiv = name;
            }
            else
            {
                tag.Name = name;
            }
            tag.Content = content;
            plhMetaDataArea.Controls.Add(tag);
        }

        /// <summary>
        /// Adds a meta tag control to the page header
        /// </summary>
        /// <param name="name">The name of the meta tag</param>
        /// <param name="propertyName">The name of the me tag page property</param>
        /// <param name="pageData">The page from where to get the property</param>
        /// <param name="httpEquiv">True if the meta tag should be HTTP-EQUIV</param>
        private void CreateMetaTag(string name, string propertyName, PageData pageData, bool httpEquiv)
        {
            string property = pageData[propertyName] as string;
            if(property != null)
            {
                CreateMetaTag(name, property, httpEquiv);
            }
        }

        /// <summary>
        /// Gets a page property as a string
        /// </summary>
        /// <param name="PropertyName">The name of the property to get</param>
        /// <param name="pageData">The page from where to get the property</param>
        /// <returns>A string representation of the page property</returns>
        private static string GetPropertyString(string PropertyName, PageData pageData)
        {
            return pageData[PropertyName] as string ?? String.Empty;
        }
    }
}