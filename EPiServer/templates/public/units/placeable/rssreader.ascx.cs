/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using System;
using System.Data;
using System.Net;
using System.Xml;
using System.Web.UI.WebControls;
using System.Web;

namespace EPiServer.Templates.Public.Units.Placeable
{
    /// <summary>
    /// Reads an RSS feed from a RssUrl page property and displays it in a repeater templated control.
    /// </summary>
    public partial class RssReader : UserControlBase
    {
        // Rss feed cache time in seconds
        private const uint rssCacheTime = 300; 
        
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            XmlDocument feedSource = GetFeedSource();
            if (feedSource != null)
            {
                Rss.DataSource = feedSource.SelectNodes("//item");
                Rss.DataBind();
            }
            else
            {
                // If no RssUrl is set then hide the repeater
                Rss.Visible = false;
            }
        }

        /// <summary>
        /// Returns the feed data in an xml document, or null if the page property "RssUrl" is not set. 
        /// </summary>
        /// <remarks>
        /// Implements caching of retreived data for the specified amount of time.
        /// </remarks>
        private XmlDocument GetFeedSource()
        {
            string rssUrl = CurrentPage["RssUrl"] as string;

            if (String.IsNullOrEmpty(rssUrl))
            {
                return null;
            }

            // Create a unique cache key based on the feed url
            string cacheKey = "RssReader-" + rssUrl;

            // Try to get the feed data from cache
            object cacheData = Cache[cacheKey];

            XmlDocument xmlDocument;

            if (cacheData == null) 
            {
                // Load the feed if it wasn't in cache
                xmlDocument = new XmlDocument();
                try
                {
                    WebRequest webRequest = WebRequest.Create(rssUrl);
                    webRequest.Timeout = 5000;
                    using (WebResponse response = webRequest.GetResponse())
                    {
                        // Keep in mind that it's always a bad idea to perform blocking remote calls on a busy 
                        // page. If the RSS doesn't respond in a timely fashion many users will wait for the 
                        // document to load and in the meantime lock the whole site.
                        xmlDocument.Load(response.GetResponseStream());
                        // Store the feed data in cache with expiration
                        InsertCacheData(cacheKey, xmlDocument, rssCacheTime);
                    }
                }
                catch (Exception e)
                {
                    ShowErrorMessage(e.Message);
                    InsertCacheData(cacheKey, e.Message, rssCacheTime);
                }
            }
            else 
            {
                xmlDocument = cacheData as XmlDocument;
                if (xmlDocument == null) 
                {
                    ShowErrorMessage(cacheData as string);
                }
            }

            return xmlDocument;
        }

        private void InsertCacheData(string cacheKey, object cacheData, uint cacheTime)
        {
            Cache.Insert(cacheKey, cacheData, null, DateTime.Now.AddSeconds(cacheTime), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// Display a custom error message for logged in users
        /// </summary>
        /// <param name="message">The error message to show.</param>
        private void ShowErrorMessage(string message)
        {
            // Only display the error message for logged in users, other users won't see the feed at all.
            if (EPiServer.Security.PrincipalInfo.Current.Principal.Identity.IsAuthenticated)
            {
                ErrorMessage.Visible = true;
                ErrorMessage.Text = "Error when loading RSS data: <br />" +
                    HttpUtility.HtmlEncode(message.Replace(Environment.NewLine, "<br />" + Environment.NewLine));
            }
        }

    }
}