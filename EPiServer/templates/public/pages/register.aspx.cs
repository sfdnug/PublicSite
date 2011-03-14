/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using System.Web.Security;
using EPiServer.Security;

namespace EPiServer.Templates.Public.Pages
{
    /// <summary>
    /// A registration form page used to create users with the default Membership provider.
    /// </summary>
    public partial class Register : TemplatePage
    {
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            //Checks if provider supports creating users.
            if (!ProviderCapabilities.IsSupported(ProviderFacade.GetDefaultMembershipProviderName(), ProviderCapabilities.Action.Create))
            {
                RegistrationWizard.Visible = false;
                ProviderDoNotSupportCreateUser.Visible = true;
                ProviderDoNotSupportCreateUser.Text = string.Format("<br />" +Translate("/admin/secedit/providerdonotsupportcreateuser"), ProviderFacade.GetDefaultMembershipProviderName());
            }

        }

    }
}