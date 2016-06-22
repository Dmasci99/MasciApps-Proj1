using MasciApps_Proj1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MasciApps_Proj1
{
    public partial class Teams : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetTeams();
            }
            else
            {
                if (Request.QueryString.Count > 1)
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                        this.GetTeam();
                    else
                        Response.Redirect("~/Login.aspx");
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
                                   team.Logo,
                                   team.Country,
                                   team.City
                               });

                //bind the result to the TeamsListView
                TeamsListView.DataSource = teams.ToList();
                TeamsListView.DataBind();
            }
        }

        /**
         * <summary>
         * This method retrieves a team for editing.
         * </summary>
         * @method GetTeam
         * @returns {void}
         */
        protected void GetTeam()
        {
            int teamID = Convert.ToInt32(Request.QueryString["teamID"]); //ID of the Team as stored in database
            int itemID = Convert.ToInt32(Request.QueryString["itemID"]); //ID of the specific Team(ListViewItem) we are editing

            //Control References
            DropDownList TeamTypeDropDownList = ((DropDownList)TeamsListView.Items[itemID].FindControl("TeamTypeDropDownList"));
            TextBox TeamNameTextBox = ((TextBox)TeamsListView.Items[itemID].FindControl("TeamNameTextBox"));
            TextBox CountryTextBox = ((TextBox)TeamsListView.Items[itemID].FindControl("CountryTextBox"));
            TextBox CityTextBox = ((TextBox)TeamsListView.Items[itemID].FindControl("CityTextBox"));

            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                //Query db for specific Team 
                var teamToEdit = (from team in db.Teams
                                    join sport in db.Sports on team.SportID equals sport.SportID
                                    where team.TeamID == teamID
                                    select new
                                    {
                                        team.TeamID,
                                        team.SportID,
                                        TeamName = team.Name,
                                        SportName = sport.Name,
                                        team.Logo,
                                        team.Country,
                                        team.City
                                    }).FirstOrDefault();
                /**
                * Populate TeamTypeDropDownList
                */
                PopulateTeamType(TeamTypeDropDownList);
                /**
                * Fill Edit Forms with appropriate data
                */
                TeamTypeDropDownList.SelectedValue = Convert.ToString(teamToEdit.SportID);
                TeamNameTextBox.Text = teamToEdit.TeamName;
                CountryTextBox.Text = teamToEdit.Country;
                CityTextBox.Text = teamToEdit.City;

                this.ToggleEditMode("Edit", itemID);
            }
        }

        /**
         * <summary>
         * This method populates the TeamTypeDropDownList.
         * </summary>
         * @method PopulateTeamType
         * @returns {void}
         */
        protected void PopulateTeamType(DropDownList TeamType)
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                var sports = (from sport in db.Sports
                              select sport);
                TeamType.DataSource = sports.ToList();
                TeamType.DataBind();
                //Start with no selection
                TeamType.ClearSelection();
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
         * This method shows the edit Template (using ToggleEditMode()).
         * </summary>
         * @method EditTeamLink_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void} 
         */
        protected void EditTeamLink_Click(object sender, EventArgs e)
        {
            this.ToggleEditMode("Edit", Convert.ToInt32(Request.QueryString["itemID"]));
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
                    db.Teams.Remove(teamToDelete); //remove Team
                    db.SaveChanges(); //save db
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
            this.ToggleEditMode("View", Convert.ToInt32(Request.QueryString["itemID"]));
        }

        /**
         * <summary>
         * This method takes the user input and updates an existing Team record.
         * </summary>
         * @method EditTeamUpdate_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void EditTeamUpdate_Click(object sender, EventArgs e)
        {            
            int teamID = Convert.ToInt32(Request.QueryString["teamID"]); //ID of the Team as stored in database
            int itemID = Convert.ToInt32(Request.QueryString["itemID"]); //ID of the specific Team(ListViewItem) we are editing

            //Control References
            DropDownList TeamTypeDropDownList = ((DropDownList)TeamsListView.Items[itemID].FindControl("TeamTypeDropDownList"));
            TextBox TeamNameTextBox = ((TextBox)TeamsListView.Items[itemID].FindControl("TeamNameTextBox"));
            TextBox CountryTextBox = ((TextBox)TeamsListView.Items[itemID].FindControl("CountryTextBox"));
            TextBox CityTextBox = ((TextBox)TeamsListView.Items[itemID].FindControl("CityTextBox"));

            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                //Query db for specific Match and Teams
                Team teamToEdit = (from team in db.Teams
                                     where team.TeamID == teamID
                                     select team).FirstOrDefault();

                //Make appropriate changes to Match record
                teamToEdit.SportID = Convert.ToInt32(TeamTypeDropDownList.SelectedItem.Value);
                teamToEdit.Name = TeamNameTextBox.Text;
                teamToEdit.Country = CountryTextBox.Text;
                teamToEdit.City = CityTextBox.Text;
                //Exit Edit Mode
                this.ToggleEditMode("Edit", itemID);
                //Refresh ListView
                this.GetTeams(); 
            }
        }
    }
}