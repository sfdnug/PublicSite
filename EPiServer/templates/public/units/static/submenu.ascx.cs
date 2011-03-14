/*
Copyright (c) 2007 EPiServer AB.  All rights reserved.

This code is released by EPiServer AB under the Source Code File - Specific License Conditions, published in 20 August 2007. 
See http://www.episerver.com/Specific_License_Conditions for details. 
*/

using System;
using EPiServer;
using EPiServer.Web.WebControls;

namespace EPiServer.Templates.Public.Units.Static
{
    /// <summary>
    /// Lists the pages in the submenu. The root page for the submenu is 
    /// the current page in the main menu.
    /// </summary>
    public partial class SubMenu : UserControlBase
    {
        private MenuList _menuList;

        /// <summary>
        /// Gets or sets the data source for this control
        /// </summary>
        public MenuList MenuList
        {
            get { return _menuList; }
            set { _menuList = value; }
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            if (MenuList == null)
            {
                return;
            }
            Menu.PageLink = MenuList.OpenTopPage;
            Menu.DataBind();
        }

    }
}