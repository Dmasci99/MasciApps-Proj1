using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MasciApps_Proj1.UserControls
{
    public partial class Header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showLinks();
                setActivePageLink();
            }
        }

        /**
         * This method sets the appropriate navbar link to "active". 
         *  
         * @method setActivePageLink
         * @return void
         */
        private void setActivePageLink()
        {
            switch (Page.Title)
            {
                case "About Me": about.Attributes.Add("class", "active"); break;
                case "Projects": projects.Attributes.Add("class", "active"); break;
                case "Services": services.Attributes.Add("class", "active"); break;
                case "Games": games.Attributes.Add("class", "active"); break;
                case "Contact Me": contact.Attributes.Add("class", "active"); break;
                case "Login": login.Attributes.Add("class", "active login"); break;
                case "Register": register.Attributes.Add("class", "active register"); break;
                case "Profile": profile.Attributes.Add("class", "active profile"); break;
            }
        }

        /**
         * <summary>
         * This method determines what Links to show in our Header: dependant on User Authorization.
         * </summary>
         * @method showLinks
         * @returns {void}
         */
        private void showLinks()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                PrivatePlaceholder.Visible = true;
                PublicPlaceholder.Visible = false;
            }
            else
            {
                PrivatePlaceholder.Visible = false;
                PublicPlaceholder.Visible = true;
            }
        }
    }
}