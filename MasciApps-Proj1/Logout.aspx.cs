using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace MasciApps_Proj1
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Use existing Session and Cookie info
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            // SignOut the currently-logged-in-user
            authenticationManager.SignOut();

            Response.Redirect("~/Login.aspx");
        }
    }
}