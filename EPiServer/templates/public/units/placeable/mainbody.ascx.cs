/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using System;

namespace EPiServer.Templates.Public.Units.Placeable
{
    /// <summary>
    /// Gets the main body of text and a heading from either the "Heading" page property
    /// or simply the page name.
    /// </summary>
    public partial class MainBody : UserControlBase
    {
        /// <summary>
        /// Returns the property Heading if set; otherwise the PageName is returned.
        /// </summary>
        protected string Heading 
        {
            get 
            {
                if (CurrentPage["Heading"] != null)
                {
                    return CurrentPage.Property["Heading"].ToWebString();
                }
                else
                {
                    return CurrentPage.Property["PageName"].ToWebString();
                }
            }
        }
    }
}