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
    public partial class TeamsAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PopulateMatchType();
            }
        }

        /**
         * <summary>
         * This method populates the MatchType DropDown for Step 2.
         * </summary>
         * @method PopulateMatchType
         * @returns {void}
         */
        protected void PopulateMatchType()
        {
            try
            {
                using (DefaultConnectionEF db = new DefaultConnectionEF())
                {
                    var sports = (from sport in db.Sports
                                  select sport);
                    if (sports != null)
                    {
                        MatchTypeDropDownList.DataSource = sports.ToList();
                        MatchTypeDropDownList.DataBind();
                        //Start with no selection
                        MatchTypeDropDownList.ClearSelection();
                    }
                }
            }
            catch (Exception err)
            {
                ErrorLabel.Text = "Failed to connect to db";
                ErrorContainer.Visible = true;
            }
        }

        /**
         * <summary>
         * This method cancels the process of adding a New Team by refreshing the page.
         * </summary>
         * @method AddTeamCancel_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void AddTeamCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Games.aspx");
        }

        /**
         * <summary>
         * This method submits the New Team to the database.
         * </summary>
         * @method AddTeamSave_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void AddTeamSave_Click(object sender, EventArgs e)
        {
            try { 
                using (DefaultConnectionEF db = new DefaultConnectionEF())
                {
                    Team newTeam = new Team()
                    {
                        SportID = Convert.ToInt32(MatchTypeDropDownList.SelectedValue),
                        Name = TeamNameTextBox.Text,
                        Country = CountryTextBox.Text,
                        City = CityTextBox.Text
                    };
                    db.Teams.Add(newTeam);//insert into db
                    db.SaveChanges();//save db
                    Response.Redirect("~/Admin/TeamsAdd.aspx"); //Refresh Page
                }
            }
            catch (Exception err)
            {
                ErrorLabel.Text = "Failed to connect to db";
                ErrorContainer.Visible = true;
            }
        }
    }
}