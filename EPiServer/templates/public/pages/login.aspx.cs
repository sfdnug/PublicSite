/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/
using System;
using EPiServer.Security;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;

namespace EPiServer.Templates.Public.Pages
{
    /// <summary>
    /// A login form page. 
    /// </summary>
    public partial class Login : TemplatePage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Sets up which input control to focus and which button to use on enter key.
            WebControl control = LoginPanel.FindControl("LoginView$LoginControl$UserName") as WebControl;
            if (control != null)
            {
                Form.DefaultFocus = control.ClientID;
            }
            control = LoginPanel.FindControl("LoginView$LoginControl$LoginBtn") as WebControl;
            if (control != null)
            {
                Form.DefaultButton = control.UniqueID;
            }
        }
    }
}