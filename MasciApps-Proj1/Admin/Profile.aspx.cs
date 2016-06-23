using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using MasciApps_Proj1.Models;

namespace MasciApps_Proj1.Admin
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetProfile();
            }
        }

        /**
         * <summary>
         * This method populates the profile from the db.
         * </summary>
         * @method GetProfile
         * @returns {void}
         */ 
        protected void GetProfile()
        {
            string userID = HttpContext.Current.User.Identity.GetUserId();

            using (UserConnection db = new UserConnection())
            {
                AspNetUser currentUser = (from user in db.AspNetUsers
                                   where user.Id == userID
                                   select user).FirstOrDefault();
                if (currentUser != null)
                {
                    UsernameTextBox.Text = currentUser.UserName;
                    EmailTextBox.Text = currentUser.Email;
                    PhoneTextBox.Text = currentUser.PhoneNumber;
                }
                else
                {
                    ErrorLabel.Text = "Failed to retreive Profile information";
                    ErrorContainer.Visible = true;
                }
            }

        }

        /**
         * <summary>
         * This method updates the current User's info upon request.
         * </summary>
         * @method ProfileSubmitButton_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */ 
        protected void ProfileSubmitButton_Click(object sender, EventArgs e)
        {
            string userID = HttpContext.Current.User.Identity.GetUserId();
            using (UserConnection db = new UserConnection())
            {
                AspNetUser currentUser = (from user in db.AspNetUsers
                                   where user.Id == userID
                                   select user).FirstOrDefault();
                if (currentUser != null)
                {
                    currentUser.Email = EmailTextBox.Text;
                    currentUser.PhoneNumber = PhoneTextBox.Text;
                }
                else
                {
                    ErrorLabel.Text = "Failed to retreive Profile information";
                    ErrorContainer.Visible = true;
                }
                db.SaveChanges(); //save db - update user info                
                Response.Redirect("~/Games.aspx"); //redirect to the Games page
            }            
        }

        /**
         * <summary>
         * This method cancel the process of editing a Profile.
         * </summary>
         * @method ProfileCancelButton_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */ 
        protected void ProfileCancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Games.aspx");
        }
    }
}