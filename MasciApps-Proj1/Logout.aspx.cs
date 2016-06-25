using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
/**
* Author : Daniel Masci - 200299037
* Class : Enterprise Computing
* Semester : 4
* Professor : Tom Tsiliopolous
* Purpose : Project 1 - Game Tracker
* Website Name : MasciApps-GameTracker.azurewebsites.net/Logout.aspx
* 
* The Following page is the Logout Page. Logs the user out and redirects to Games.aspx.
*/
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