/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using EPiServer.Personalization;

namespace EPiServer.Templates.Public.Pages
{
    /// <summary>
    /// Creates a subsription, which listens for changes in the website for the current user
    /// </summary>
    /// <remarks>Pages that one can subscribe to must be activated for subscription</remarks>
    public partial class SubscriptionPage : TemplatePage
    {
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                if(!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return;
                }
                SubscriptionArea.Visible = true;
                string email = EPiServerProfile.Current.Email;
                if (!string.IsNullOrEmpty(email))
                {
                    Email.Text = email;
                    Email.Enabled = false;
                }

                foreach (ListItem item in Interval.Items)
                {
                    item.Selected = Int32.Parse(item.Value) == Subscription.Interval;
                }
                SubscriptionList.DataBind();
            }
        }

        protected void Subscribe_Click(object sender, EventArgs e)
        {
            EPiServerProfile user = EPiServerProfile.Current;
            
            user.SubscriptionInfo.Interval = Int32.Parse(Interval.SelectedItem.Value);
            
            if (Email.Enabled)
            {
                user.Email = Email.Text;
            }

            user.Save();
        }
    }
}
