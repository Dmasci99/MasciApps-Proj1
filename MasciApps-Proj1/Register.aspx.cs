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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                    Response.Redirect("~/Admin/Profile.aspx");
            }
        }

        protected void RegisterSubmitButton_Click(object sender, EventArgs e)
        {
            // create new userStore and userManager objects
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            
            // create new user obeject
            var user = new IdentityUser()
            {
                UserName = UsernameTextBox.Text,
                PhoneNumber = PhoneTextBox.Text,
                Email = EmailTextBox.Text
            };

            IdentityResult result = userManager.Create(user, ConfirmPasswordTextBox.Text);

            if (result.Succeeded)
            {
                // authenticate user and login
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication; // to manage authentications
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie); // create an Identity for user
                authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity); // Signin the Identity user

                // redirect to the Games page
                Response.Redirect("~/Games.aspx");
            }
            else
            {
                // display error in the div#AlertFlash
                ErrorLabel.Text = result.Errors.FirstOrDefault();
                ErrorContainer.Visible = true;
            }
        }

        protected void RegisterCancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Games.aspx");
        }
    }
}