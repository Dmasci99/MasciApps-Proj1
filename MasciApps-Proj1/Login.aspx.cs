﻿using System;
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
* Website Name : MasciApps-GameTracker.azurewebsites.net/Login.aspx
* 
* The Following page is Codebehind for the Login Page. 
* Users login to access Game Tracker Administrative rights.
* Logged-in users are redirected to their Profile.
* 
*/
namespace MasciApps_Proj1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                    Response.Redirect("~/Admin/Profile.aspx");
            }
        }

        protected void LoginSubmitButton_Click(object sender, EventArgs e)
        {
            // create new userStore and userManager
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);

            // retrieve user from db using credentials from login.aspx
            var user = userManager.Find(UsernameTextBox.Text, PasswordTextBox.Text);

            if (user != null)
            {// if we have a match
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                // signin the authenticated user
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);

                // redirect to MainMenu
                Response.Redirect("~/Games.aspx");
            }
            else
            {
                ErrorLabel.Text = "Invalid Username or Password";
                ErrorContainer.Visible = true;
            }
        }

        protected void LoginCancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Games.aspx");
        }
    }
}