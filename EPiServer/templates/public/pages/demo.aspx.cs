/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

namespace EPiServer.Templates.Public.Pages
{
    /// <summary>
    /// The Demo page type gathers functions to show how different type of controls 
    /// and technics can interact with EPiServer and vice versa.
    /// </summary>
    public partial class Demo : TemplatePage
    {
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            string flashUrl = CurrentPage["EmbeddedURL"] as string;
            if (string.IsNullOrEmpty(flashUrl))
            {
                Flash.Visible = false;
            }
        }
    }
}