/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using System;
using EPiServer.Core;

namespace EPiServer.Templates.Public.Units.Static
{
    /// <summary>
    /// A common top area for the whole website, where the logotype is usually presented.
    /// </summary>
    public partial class PageHeader : UserControlBase
    {
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            PageData startPage = DataFactory.Instance.GetPage(PageReference.StartPage);

            lnkLogotype.NavigateUrl = startPage.LinkURL;
            lnkLogotype.ImageUrl = startPage["Logotype"] as string ?? string.Empty;
            lnkLogotype.ToolTip = Translate("/navigation/startpagelinktitle");
            lnkLogotype.Text = Translate("/navigation/startpagelinktitle");
        }
    }
}