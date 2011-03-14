using System;
using System.Web.UI.WebControls;
using EPiServer.Core;
using EPiServer.XForms.WebControls;

using Label = System.Web.UI.WebControls.Label;
using System.Web.UI;
using EPiServer.Web;

namespace EPiServer.Templates
{
	public class Global : EPiServer.Global
	{
		protected void Application_Start(Object sender, EventArgs e)
		{
            XFormControl.ControlSetup += new EventHandler(XForm_ControlSetup);
        }

        #region Global XForm Events

        /// <summary>
        /// Sets up events for each new instance of the XFormControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>As the ControlSetup event is triggered for each instance of the XFormControl control
        /// you need to take into consideration that any event handlers will affect all XForms for the entire
        /// application. If the EPiServer UI is running in the same application this might also be affected depending
        /// on which events you attach to and what is done in the event handlers.</remarks>
        public void XForm_ControlSetup(object sender, EventArgs e)
        {
            XFormControl control = (XFormControl)sender;

            control.BeforeLoadingForm += new LoadFormEventHandler(XForm_BeforeLoadingForm);
            control.ControlsCreated += new EventHandler(XForm_ControlsCreated);
            control.BeforeSubmitPostedData += new SaveFormDataEventHandler(XForm_BeforeSubmitPostedData);
            control.AfterSubmitPostedData += new SaveFormDataEventHandler(XForm_AfterSubmitPostedData);
        }

        /// <summary>
        /// Handles the BeforeLoadingForm event of the XFormControl.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EPiServer.XForms.WebControls.LoadFormEventArgs"/> instance containing the event data.</param>
        public void XForm_BeforeLoadingForm(object sender, LoadFormEventArgs e)
        {
            XFormControl formControl = (XFormControl)sender;

            //We set the validation group of the form to match our global validation group in the master page.
            formControl.ValidationGroup = "XForm";
        }

        /// <summary>
        /// Handles the ControlsCreated event of the XFormControl.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void XForm_ControlsCreated(object sender, EventArgs e)
        {
            XFormControl formControl = (XFormControl)sender;

            //We set the inline error validation text to "*" as we use a
            //validation summary in the master page to display the detailed error message.
            foreach (Control control in formControl.Controls)
            {
                if (control is BaseValidator)
                {
                    ((BaseValidator)control).Text = "*";
                }
            }
            
            if (formControl.Page.User.Identity.IsAuthenticated)
            {
                formControl.Data.UserName = formControl.Page.User.Identity.Name;
            }
        }

        /// <summary>
        /// Handles the BeforeSubmitPostedData event of the XFormControl.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EPiServer.XForms.WebControls.SaveFormDataEventArgs"/> instance containing the event data.</param>
        public void XForm_BeforeSubmitPostedData(object sender, SaveFormDataEventArgs e)
        {
            XFormControl control = (XFormControl)sender;

            PageBase currentPage = control.Page as PageBase;

            if (currentPage == null)
            {
                return;
            }

            //We set the current page that the form has been posted from
            //This might differ from the actual page that the form property exists on.
            e.FormData.PageGuid = currentPage.CurrentPage.PageGuid;
        }

        /// <summary>
        /// Handles the AfterSubmitPostedData event of the XFormControl.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EPiServer.XForms.WebControls.SaveFormDataEventArgs"/> instance containing the event data.</param>
        public void XForm_AfterSubmitPostedData(object sender, SaveFormDataEventArgs e)
        {
            XFormControl control = (XFormControl)sender;

            if (control.FormDefinition.PageGuidAfterPost != Guid.Empty)
            {
                PermanentPageLinkMap pageMap = PermanentLinkMapStore.Find(control.FormDefinition.PageGuidAfterPost) as PermanentPageLinkMap;
                if (pageMap != null)
                {
                    control.Page.Response.Redirect(pageMap.MappedUrl.ToString());
                    return;
                }
            }

            //After the form has been posted we remove the form elements and add a "thank you message".
            control.Controls.Clear();
            Label label = new Label();
            label.CssClass = "thankyoumessage";
            label.Text = LanguageManager.Instance.Translate("/form/postedmessage");
            control.Controls.Add(label);
        }

        #endregion

        protected void Application_End(Object sender, EventArgs e)
		{
		}

        /// <summary>
        /// Raises the <see cref="E:ValidateUIAccess"/> event. Override this in inheriting classes to customize behavior,
        /// always calling the base-class implementation as well. Check for e.Cancel == true and do an early exit if so.
        /// </summary>
        /// <remarks>
        /// This is only a code sample on how you can utilize this event.
        /// </remarks>
        /// <param name="e">The <see cref="EPiServer.ValidateUIAccessEventArgs"/> instance containing the event data.</param>
        protected override void OnValidateRequestAccess(ValidateRequestAccessEventArgs e)
        {
            base.OnValidateRequestAccess(e);
            if (e.Cancel)
            {
                return;
            }
        }

        /// <summary>
        /// Gets a list of default documents. Overide if you need to change which documents are actually
        /// tried.
        /// </summary>
        /// <remarks>
        /// This is only a code sample on how you can utilize this method.
        /// </remarks>
        /// <param name="url">The URL of the request that is determined to need a default document</param>
        /// <returns>null or a list of default documents to try</returns>
        protected override string[] GetDefaultDocuments(Uri url)
        {
            return base.GetDefaultDocuments(url);
        }
    }
}