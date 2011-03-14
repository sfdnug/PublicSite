/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/
using System;
using EPiServer.Core;
using EPiServer.XForms;
using EPiServer.XForms.WebControls;

namespace EPiServer.Templates.Public.Units.Placeable
{
	/// <summary>
	/// This user control creates a XForm based on a XForm page property.
	/// </summary>
    /// <remarks>The XForm functionality relies on logic that has been moved to
    /// the Global.asax.cs file in order to work better with forms that have been
    /// inserted as dynamic data.</remarks>
	public partial class XFormControl : UserControlBase
	{
        private XForm _form;
        private string _xFormProperty;
        private string _heading;
        private string _headingProperty;
        private bool _showStatistics = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            SwitchButton.Visible = EnableStatistics && Form != null;

            if (Form == null)
            {
                return;
            }

            SetupForm();
        }

        public void FormControl_AfterSubmitPostedData(object sender, SaveFormDataEventArgs e)
        {
            if (EnableStatistics)
            {
                SwitchView(null, null);
                SwitchButton.Visible = false;
            }
        }

        /// <summary>
        /// Toggles the form and statistics views
        /// </summary>
        protected void SwitchView(object sender, System.EventArgs e)
        {
            if (StatisticsPanel.Visible)
            {
                StatisticsPanel.Visible = false;
                SwitchButton.Text = Translate("/form/showstat");
            }
            else
            {
                Statistics.DataBind();
                NumberOfVotes.Text = String.Format(Translate("/form/numberofvotes"), Statistics.NumberOfVotes);
                StatisticsPanel.Visible = true;
                SwitchButton.Text = Translate("/form/showform");
            }
            FormPanel.Visible = !StatisticsPanel.Visible;
        }

        private void SetupForm()
        {
            FormControl.FormDefinition = Form;
            FormControl.AfterSubmitPostedData += new SaveFormDataEventHandler(FormControl_AfterSubmitPostedData);
        }

        /// <summary>
        /// Gets or sets the XForm property
        /// </summary>
        public XForm Form
        {
            get
            {
                if (_form == null)
                {
                    string formGuid = CurrentPage[XFormProperty] as string;
                    if (!String.IsNullOrEmpty(formGuid))
                    {
                        _form = XForm.CreateInstance(new Guid(formGuid));
                    }
                }
                return _form;
            }
            set
            {
                _form = value;
            }
        }

        /// <summary>
        /// Name of the page property pointing out the current XForm
        /// </summary>
        /// <value>The name of the page property used for the XForm</value>
        public string XFormProperty
        {
            get { return _xFormProperty; }
            set { _xFormProperty = value; }
        }

        /// <summary>
        /// The heading above the XForm
        /// </summary>
        /// <value>The heading that should be shown above the XForm</value>
        /// <remarks>If this property hasn't been set the page property pointed out by HeadingProperty will be used instead.</remarks>
        public string Heading
        {
            get
            {
                if (_heading == null)
                {
                    if (HeadingProperty != null)
                    {
                        _heading = CurrentPage[HeadingProperty] as string;
                    }
                }
                return _heading;
            }
            set { _heading = value; }
        }

        /// <summary>
        /// Name of the page property that should be used to generate the form heading
        /// </summary>
        /// <value>The page property name that should be used to generate the form heading</value>
        /// <remarks>If neither Heading or HeadingProperty are set, the form will be shown without any heading</remarks>
        public string HeadingProperty
        {
            get { return _headingProperty; }
            set { _headingProperty = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether XForm statistics should be shown.
        /// </summary>
        /// <value><c>true</c> if statistics should be shown otherwise, <c>false</c>.</value>
        public bool ShowStatistics
        {
            get { return _showStatistics; }
            set { _showStatistics = value; }
        }

        /// <summary>
        /// Enables XForm statistics
        /// </summary>
        protected bool EnableStatistics
        {
            get
            {
                return CurrentPage["EnableStatistics"] == null
                    ? false
                    : (bool)CurrentPage["EnableStatistics"];
            }
        }
	}
}