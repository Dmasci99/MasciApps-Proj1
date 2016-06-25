using MasciApps_Proj1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
/**
* Author : Daniel Masci - 200299037
* Class : Enterprise Computing
* Semester : 4
* Professor : Tom Tsiliopolous
* Purpose : Project 1 - Game Tracker
* Website Name : MasciApps-GameTracker.azurewebsites.net/Teams.aspx
* 
* The Following page is the Codebehind for the "Teams" page.
* Allows the user to view, add and edit Teams (depending on authentication).
*
*/
namespace MasciApps_Proj1
{
    public partial class Teams : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                    PrivatePlaceHolder.Visible = true;
                this.GetTeams();
            }
        }

        /**
         * <summary>
         * This method retrieves the all the teams found in our db.
         * </summary>
         * @method GetTeams
         * @returns {void}
         */
        protected void GetTeams()
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                //query the Teams table using EF and Linq
                var teams = (from team in db.Teams
                               join sport in db.Sports on team.SportID equals sport.SportID
                               select new
                               {
                                   team.TeamID,
                                   team.SportID,
                                   TeamName = team.Name,
                                   SportName = sport.Name,
                                   team.Country,
                                   team.City
                               });
                if (teams != null)
                {
                    //bind the result to the TeamsListView
                    TeamsListView.DataSource = teams.AsQueryable().OrderBy("SportID ASC").ToList();
                    TeamsListView.DataBind();
                }                
            }
        }
        
        /**
         * <summary>
         * This method populates the TeamTypeDropDownList.
         * </summary>
         * @method PopulateTeamType
         * @param {DropDownList} TeamType
         * @returns {void}
         */
        protected void PopulateTeamType(DropDownList TeamType, int teamID)
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                //Populate Dropdown with available values
                var sports = (from sport in db.Sports
                              select sport);
                if (sports != null)
                {
                    TeamType.DataSource = sports.ToList();
                    TeamType.DataBind();                    
                }
                //Select the correct sport of current ListViewItem
                var currentTeam = (from team in db.Teams
                                     where team.TeamID == teamID
                                     select team).FirstOrDefault();
                if (currentTeam != null)
                {
                    TeamType.SelectedValue = Convert.ToString(currentTeam.SportID);
                }
            }
        }

        /**
         * <summary>
         * This is a Helper Method for GetTeam() by toggling the Edit Mode On or Off.
         * </summary>
         * @method ToggleEditMode
         * @param {string} mode
         * @param {int} itemID
         * @returns {void}
         */
        protected void ToggleEditMode(string mode, int itemID)
        {
            if (mode == "Edit")
            {
                //Hide the view template and show the edit template
                ((PlaceHolder)TeamsListView.Items[itemID].FindControl("ViewTemplate")).Visible = false;
                ((PlaceHolder)TeamsListView.Items[itemID].FindControl("EditTemplate")).Visible = true;
                //Hide edit button and show delete button
                ((LinkButton)TeamsListView.Items[itemID].FindControl("EditTeamLink")).Visible = false;
                ((LinkButton)TeamsListView.Items[itemID].FindControl("DeleteTeamLink")).Visible = true;
            }
            else
            {
                //Hide the edit template and show the view template
                ((PlaceHolder)TeamsListView.Items[itemID].FindControl("EditTemplate")).Visible = false;
                ((PlaceHolder)TeamsListView.Items[itemID].FindControl("ViewTemplate")).Visible = true;
                //Hide delete button and show edit button
                ((LinkButton)TeamsListView.Items[itemID].FindControl("DeleteTeamLink")).Visible = false;
                ((LinkButton)TeamsListView.Items[itemID].FindControl("EditTeamLink")).Visible = true;
            }
        }

        /**
         * <summary>
         * This method shows the EditTemplate and prepares for editing.
         * </summary>
         * @method EditTeamLink_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void} 
         */
        protected void EditTeamLink_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                int itemID = Convert.ToInt32(Request.QueryString["itemID"]); //ID of the specific Team(ListViewItem) we are editing
                this.ToggleEditMode("Edit", itemID); //Enter Edit Mode
                this.PopulateTeamType(((DropDownList)TeamsListView.Items[itemID].FindControl("TeamTypeDropDownList")),
                                        Convert.ToInt32(Request.QueryString["teamID"]));
            }
        }
        /**
         * <summary>
         * This method deletes the chosen Team.
         * </summary>
         * @method DeleteTeamLink_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void} 
         */
        protected void DeleteTeamLink_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                using (DefaultConnectionEF db = new DefaultConnectionEF())
                {
                    int teamID = Convert.ToInt32(Request.QueryString["teamID"]);
                    Team teamToDelete = (from team in db.Teams
                                           where team.TeamID == teamID
                                           select team).FirstOrDefault();
                    if (teamToDelete != null)
                    {
                        db.Teams.Remove(teamToDelete); //remove Team
                        db.SaveChanges(); //save db
                    }
                    this.GetTeams(); //Refresh ListView
                }
            }
        }

        /**
         * <summary>
         * This method cancels the process of Editing an Existing Team.
         * </summary>
         * @method EditTeamCancel_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void} 
         */
        protected void EditTeamCancel_Click(object sender, EventArgs e)
        {
            this.ToggleEditMode("View", Convert.ToInt32(Request.QueryString["itemID"])); //Exit Edit Mode
        }

        /**
         * <summary>
         * This method takes the user input and updates an existing Team record.
         * </summary>
         * @method EditTeamSave_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void EditTeamSave_Click(object sender, EventArgs e)
        {            
            int teamID = Convert.ToInt32(Request.QueryString["teamID"]); //ID of the Team as stored in database
            int itemID = Convert.ToInt32(Request.QueryString["itemID"]); //ID of the specific Team(ListViewItem) we are editing
            //Control References
            DropDownList TeamTypeDropDownList = ((DropDownList)TeamsListView.Items[itemID].FindControl("TeamTypeDropDownList"));
            TextBox TeamNameTextBox = ((TextBox)TeamsListView.Items[itemID].FindControl("TeamNameTextBox"));
            TextBox CountryTextBox = ((TextBox)TeamsListView.Items[itemID].FindControl("CountryTextBox"));
            TextBox CityTextBox = ((TextBox)TeamsListView.Items[itemID].FindControl("CityTextBox"));

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                using (DefaultConnectionEF db = new DefaultConnectionEF())
                {
                    //Query db for specific Team
                    Team teamToEdit = (from team in db.Teams
                                       where team.TeamID == teamID
                                       select team).FirstOrDefault();
                    //Make appropriate changes to Team record
                    if (teamToEdit != null)
                    {
                        teamToEdit.SportID = Convert.ToInt32(TeamTypeDropDownList.SelectedItem.Value); //Needs troubleshooting
                        teamToEdit.Name = TeamNameTextBox.Text;
                        teamToEdit.Country = CountryTextBox.Text;
                        teamToEdit.City = CityTextBox.Text;
                        db.SaveChanges();
                    }
                    this.ToggleEditMode("View", itemID);//Exit Edit Mode                
                    this.GetTeams();//Refresh ListView
                }
            }            
        }
    }
}