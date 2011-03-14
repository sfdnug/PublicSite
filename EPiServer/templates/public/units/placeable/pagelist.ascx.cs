/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using System;
using System.Globalization;
using System.Web;
using EPiServer.Core;
using EPiServer.Core.Html;
using EPiServer.DynamicContent;
using System.Text;
using System.Text.RegularExpressions;

namespace EPiServer.Templates.Public.Units.Placeable
{
    /// <summary>
    /// An advanded page list commonly used for rendering news, event and article list.
    /// Can be customized to show/hide a heading, thumbnails, preview text, date and time and a link to
    /// the list container page.
    /// </summary>
    public partial class PageList : UserControlBase
    {
        private string _pageLinkProperty;
        private string _heading;
        private string _dateProperty;
        private string _thumbnailProperty;
        private string _seeMoreText;
        private string _maxCountProperty;
        private int _previewTextLength = 0;
        private bool _showTopRuler = false;

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                epiPageList.PageLinkProperty = PageLinkProperty;
                if (MaxCountProperty != null && CurrentPage[MaxCountProperty] != null)
                {
                    epiPageList.MaxCount = (int) CurrentPage[MaxCountProperty];
                }
                epiPageList.DataBind();
            }
        }

        /// <summary>
        /// Gets or sets the name of the page property pointing out the parent page of this page list
        /// </summary>
        /// <returns>The name of the page property pointing out the parent page.</returns>
        public string PageLinkProperty
        {
            get { return _pageLinkProperty; }
            set { _pageLinkProperty = value; }
        }

        /// <summary>
        /// Gets or sets the heading above the page list
        /// </summary>
        /// <returns>The heading of the page list</returns>
        /// <remarks>
        /// If this property hasn't been set, then either the Heading property or PageName 
        /// property of the target page will be used instead.
        /// </remarks>
        public string Heading
        {
            get
            {
                // if Heading has been set then return it as is.
                if(_heading != null)
                {
                    return _heading;
                }

                PageReference listReference = null;

                if (!String.IsNullOrEmpty(PageLinkProperty))
                { 
                    listReference = CurrentPage[PageLinkProperty] as PageReference;
                }

                if (listReference != null)
                {
                    // get the page that is being pointed out by the page property
                    PageData listRootPage = GetPage(listReference);

                    // return the Heading property if exists on the page pointed out by "PageLinkProperty" otherwise return its page name
                    return _heading = (listRootPage["Heading"] as string ?? listRootPage.PageName);
                }
                return string.Empty;
            }
            set { _heading = value; }
        }


        /// <summary>
        /// Gets or sets the visibility of the top hr separator.
        /// </summary>
        /// <returns>A boolen indicating if the top horisontal ruler is visible or not</returns>
        public bool ShowTopRuler
        {
            get { return _showTopRuler; }
            set { _showTopRuler = value; }
        }


        /// <summary>
        /// Gets or sets the number of characters that should be shown in the preview text (default = 0)
        /// </summary>
        /// <returns>The character count of the preview text</returns>
        /// <remarks>
        /// The preview text is primarily created from the MainIntro property if exists, 
        /// otherwise parts of the MainBody property are being used. If neither a MainIntro nor a 
        /// MainBody property exists, the preview will not be shown.
        /// </remarks>
        public int PreviewTextLength
        {
            get { return _previewTextLength; }
            set { _previewTextLength = value; }
        }

        /// <summary>
        /// Gets or sets the name of the page property that should be used to generate the date
        /// </summary>
        /// <returns>The name of the page property used for generating the date</returns>
        /// <remarks>
        /// Several built-in page properties could be used in this case, for instance PageSaved, 
        /// PageChanged, PageStartPublish, PageCreated.
        /// </remarks>
        public string DateProperty
        {
            get { return _dateProperty; }
            set { _dateProperty = value; }
        }

        /// <summary>
        /// Gets or sets the name of the property pointing out the thumbnail image for the current item in the PageList.
        /// </summary>
        /// <returns>
        /// The name of the property pointing out the thumbnail image for the current item in the PageList.
        /// </returns>
        public string ThumbnailProperty
        {
            get { return _thumbnailProperty; }
            set { _thumbnailProperty = value; }
        }

        /// <summary>
        /// Gets or sets the text for the 'read more' link
        /// </summary>
        /// <returns>The text for the 'read more' link</returns>
        /// <remarks>
        /// The 'read more' link points to the parent page of the PageList
        /// </remarks>
        public string SeeMoreText
        {
            get { return _seeMoreText; }
            set { _seeMoreText = value; }
        }

        /// <summary>
        /// Gets or sets the name of the page property indicating the amount of items in the PageList
        /// </summary>
        /// <returns>The name of the page property which points out the amount of items in the PageList</returns>
        public string MaxCountProperty
        {
            get { return _maxCountProperty; }
            set { _maxCountProperty = value; }
        }

        protected string GetImage(PageData page)
        {
            if(ThumbnailProperty != null)
            {
                string _imgTag = "<img src=\"{0}\" alt=\"{1}\" />";
                return string.Format(_imgTag, page[ThumbnailProperty], page.PageName);
            }
            return string.Empty;
        }

        /// <returns>
        /// Returns a string representation of the date for the specified PageData using 
        /// standard formating
        /// </returns>
        /// <remarks>
        /// In this case the DateTime object is formated to show only the date according 
        /// to the websites CultureInfo before it is returned as a string
        /// </remarks>
        protected string GetFormattedDate(PageData page)
        {
            return GetFormattedDate(page, "d");
        }

        /// <returns>Returns a string representation of the date for the specified PageData </returns>
        /// <remarks>
        /// In this case the DateTime object is formated according to the websites CultureInfo
        /// and a given DateTime format before it is returned as a string
        /// </remarks>
        protected string GetFormattedDate(PageData page, string format)
        {
            if(DateProperty != null && page[DateProperty] is DateTime)
            {
                // Format the date according to the threads culture info
                return ((DateTime)page[DateProperty]).ToString(format);
            }
            return string.Empty;
        }

        /// <returns>Returns the preview text for the specified PageData</returns>
        /// <remarks>The preview text is primarily created from the MainIntro property if it exists, 
        /// otherwise parts of the MainBody property are being used. 
        /// If neither a MainIntro nor a MainBody property exists, the preview will not be shown. 
        /// The length of the preview text is defined in <code>PreviewTextLength</code></remarks>
        protected string GetPreviewText(PageData page)
        {
            if(PreviewTextLength <= 0)
            {
                return string.Empty;
            }

            if (page.Property["MainIntro"] != null && page.Property["MainIntro"].ToString().Length > 0)
            {
                return StripPreviewText(page.Property["MainIntro"].ToWebString(), PreviewTextLength);
            }
            
            string previewText = String.Empty;
            
            if (page.Property["MainBody"] != null)
            {
                previewText = page.Property["MainBody"].ToWebString();
            }
             
            if (String.IsNullOrEmpty(previewText))
            {
                return string.Empty;
            }

            //If the MainBody contains DynamicContents, replace those with an empty string
            StringBuilder regexPattern = new StringBuilder(@"<span[\s\W\w]*?classid=""");
            regexPattern.Append(DynamicContentFactory.Instance.DynamicContentId.ToString());
            regexPattern.Append(@"""[\s\W\w]*?</span>");
            previewText = Regex.Replace(previewText, regexPattern.ToString(), string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return TextIndexer.StripHtml(previewText, PreviewTextLength);
        }

        /// <returns>A link to the contanier of the PageList</returns>
        /// <remarks>Both SeeMoreText and PageLinkProperty must be set in order for the link to function correctly.</remarks>
        protected string GetContainerLink()
        {
            if (SeeMoreText != null && PageLinkProperty != null)
            {
                string _linkTag = "<a href=\"{0}\">{1}</a>";
                
                PageReference containerPage = CurrentPage[PageLinkProperty] as PageReference;
                if (containerPage != null)
                {
                    PageData page = GetPage(containerPage);
                    return string.Format(_linkTag, HttpUtility.UrlPathEncode(page.LinkURL), SeeMoreText);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Strips a text to a given length without splitting the last word.
        /// </summary>
        /// <param name="previewText">The string to shorten</param>
        /// <returns>A shortened version of the given string</returns>
        private static string StripPreviewText(string previewText, int maxLength)
        {
            if (previewText.Length <= maxLength)
            {
                return previewText;
            }
            previewText = previewText.Substring(0, maxLength);
            // The maximum number of characters to cut from the end of the string.
            int maxCharCut = (previewText.Length > 15 ? 15 : previewText.Length - 1);
            int previousWord = previewText.LastIndexOfAny(new char[] { ' ', '.', ',', '!', '?' }, previewText.Length - 1, maxCharCut);
            if (previousWord <= 0)
            {
                previewText = previewText.Substring(0, previousWord);
            }
            return  previewText + " ...";
        }
    }
}