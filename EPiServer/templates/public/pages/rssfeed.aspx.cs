/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using System;
using System.Xml;
using EPiServer.Core;
using EPiServer.Core.Html;
using System.Globalization;
using EPiServer.DataAbstraction;
using EPiServer.Personalization;
using EPiServer.Web;
using EPiServer.Filters;

namespace EPiServer.Templates.Public.Pages
{
    /// <summary>
    /// Renders an xml page representing an RSS source feed based on pages in 
    /// an RSS contianer root retrieved from the "RssSource" page property.
    /// </summary>
    public partial class RssFeed : TemplatePage
    {
        public RssFeed()
            : base(0, EPiServer.Web.PageExtensions.ContextMenu.OptionFlag)
        {         
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            Response.ContentType = "text/xml";
            Response.Clear();
            if (!PageReference.IsNullOrEmpty(CurrentPage["RssSource"] as PageReference))
            {
                WriteRssDocument();
            }
            Response.End();
        }

        private void WriteRssDocument()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode outerNode = doc.CreateElement("rss");
            XmlAttribute uriInfo = doc.CreateAttribute("xmlns:dc");
            uriInfo.Value = "http://purl.org/dc/elements/1.1/";
            outerNode.Attributes.Append(uriInfo);
            doc.AppendChild(outerNode);

            XmlAttribute versionInfo = doc.CreateAttribute("version");
            versionInfo.Value = "2.0";
            outerNode.Attributes.Append(versionInfo);

            XmlNode channel = doc.CreateElement("channel");
            outerNode.AppendChild(channel);

            XmlNode title = doc.CreateElement("title");
            title.InnerText = CurrentPage.PageName;
            channel.AppendChild(title);

            XmlNode link = doc.CreateElement("link");
            if (UrlRewriteProvider.IsFurlEnabled)
            {
                UrlBuilder url = new UrlBuilder(EPiServer.Configuration.Settings.Instance.SiteUrl);
                EPiServer.Global.UrlRewriteProvider.ConvertToExternal(url, null, System.Text.UTF8Encoding.UTF8);
                link.InnerText = url.ToString();
            }
            else
            {
                link.InnerText = EPiServer.Configuration.Settings.Instance.SiteUrl.ToString();
            }
            channel.AppendChild(link);

            XmlNode description = doc.CreateElement("description");
            description.InnerText = CurrentPage["Description"] as string;
            channel.AppendChild(description);

            PageReference listingContainer = CurrentPage["RssSource"] as PageReference;
            PageDataCollection children = GetChildren(listingContainer);
            FilterForVisitor.Filter(children);
            foreach (PageData page in children)
            {
                XmlNode item = doc.CreateNode(XmlNodeType.Element, "item", null);

                XmlNode itemTitle = doc.CreateElement("title");
                itemTitle.InnerText = page.PageName;
                item.AppendChild(itemTitle);

                UrlBuilder url = new UrlBuilder(UriSupport.Combine(EPiServer.Configuration.Settings.Instance.SiteUrl.ToString(), page.LinkURL).ToString());
                XmlNode itemLink = doc.CreateElement("link");
                if (UrlRewriteProvider.IsFurlEnabled)
                {
                    EPiServer.Global.UrlRewriteProvider.ConvertToExternal(url, null, System.Text.UTF8Encoding.UTF8);
                    itemLink.InnerText = url.ToString();
                }
                else
                {
                    itemLink.InnerText = url.ToString();

                }
                item.AppendChild(itemLink);

                XmlNode itemDescription = doc.CreateElement("description");
                itemDescription.InnerText = CreatePreviewText(page);
                item.AppendChild(itemDescription);

                XmlNode itemGuid = doc.CreateElement("guid");
                itemGuid.InnerText = url.ToString();
                item.AppendChild(itemGuid);
                
                XmlNode itemPubDate = doc.CreateElement("pubDate");
                itemPubDate.InnerText = page.Changed.ToString("r");
                item.AppendChild(itemPubDate);
                                
                foreach (int category in page.Category)
                {
                    XmlNode itemCategory = doc.CreateElement("category");
                    itemCategory.InnerText = page.Category.GetCategoryName(category);
                    item.AppendChild(itemCategory);
                }
                
                channel.AppendChild(item);
            }
            
            doc.Save(Response.OutputStream);
        }

        private string CreatePreviewText(PageData page)
        {   
            if (page.Property["MainIntro"] != null && page.Property["MainIntro"].ToString().Length > 0)
            {
                return page.Property["MainIntro"].ToWebString();
            }
            
            if (page.Property["MainBody"] == null || page.Property["MainBody"].ToString().Length == 0)
            {
                return String.Empty;
            }

            return TextIndexer.StripHtml(page.Property["MainBody"].ToWebString(), 400);
        }
    }
}
