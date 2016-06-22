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
    public partial class TeamsEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PopulateTeams();
                this.PopulateMatchType();
            }
            Page.MaintainScrollPositionOnPostBack = true;//Maintain Page position
        }

        /**
         * <summary>
         * This method populates the Team DropDown for Step 1.
         * </summary>
         * @method PopulateTeams
         * @returns {void}
         */
        protected void PopulateTeams()
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                //Query all teams of specified Sport Type.
                var allTeams = (from team in db.Teams
                                select team);
                //Assign Teams to DropDown
                TeamDropDownList.DataSource = allTeams.ToList();
                TeamDropDownList.DataBind();                

                //Start with no selection
                TeamDropDownList.ClearSelection();
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
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                var sports = (from sport in db.Sports
                              select sport);
                MatchTypeDropDownList.DataSource = sports.ToList();
                MatchTypeDropDownList.DataBind();
                //Start with no selection
                MatchTypeDropDownList.ClearSelection();
            }
        }

        /**
         * <summary>
         * This method will populate the details for the chosen Team.
         * </summary>
         * @method PopulateDetails
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void PopulateDetails(object sender, EventArgs e)
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                int teamID = Convert.ToInt32(TeamDropDownList.SelectedItem.Value);
                var teamToEdit = (from team in db.Teams
                                  where team.TeamID == teamID
                                  select team).FirstOrDefault();
                MatchTypeDropDownList.SelectedValue = Convert.ToString(teamToEdit.SportID);
                TeamNameTextBox.Text = teamToEdit.Name;
                CountryTextBox.Text = teamToEdit.Country;
                CityTextBox.Text = teamToEdit.City;
            }
        }

        /**
         * <summary>
         * This method will swap the form to: add a new Team.
         * </summary>
         * @method SwapFunction
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void SwapFunction(object sender, EventArgs e)
        {
            if (FunctionDropDownList.SelectedItem.Text == "Add New Team")
                Response.Redirect("~/Admin/TeamsAdd.aspx");
            else
                Response.Redirect("~/Admin/TeamsEdit.aspx");
        }

        /**
         * <summary>
         * This method cancels the process of editing an existing Team.
         * </summary>
         * @method EditTeamCancel_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void} 
         */ 
        protected void EditTeamCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Games.aspx");
        }

        /**
         * <summary>
         * This method submits the changes to the chosen existing Team.
         * </summary>
         * @method EditTeamSave_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */ 
        protected void EditTeamSave_Click(object sender, EventArgs e)
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                int teamID = Convert.ToInt32(TeamDropDownList.SelectedValue);
                Team teamToEdit = (from team in db.Teams
                                  where team.TeamID == teamID
                                  select team).FirstOrDefault();
                teamToEdit.SportID = Convert.ToInt32(MatchTypeDropDownList.SelectedValue);
                teamToEdit.Name = TeamNameTextBox.Text;
                teamToEdit.Country = CountryTextBox.Text;
                teamToEdit.City = CityTextBox.Text;
                db.SaveChanges(); // save db - update Team
                this.PopulateTeams(); //Refresh TeamDropDownList
            }
        }

        /**
         * <summary>
         * This method deletes the chosen existing Team.
         * </summary>
         * @method EditTeamDelete_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void} 
         */
        protected void EditTeamDelete_Click(object sender, EventArgs e)
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                int teamID = Convert.ToInt32(TeamDropDownList.SelectedValue);
                Team teamToDelete = (from team in db.Teams
                                     where team.TeamID == teamID
                                     select team).FirstOrDefault();
                db.Teams.Remove(teamToDelete); //remove Team
                db.SaveChanges(); //save db
                this.PopulateTeams(); //Refresh TeamDropDownList
            }
        }
    }
}