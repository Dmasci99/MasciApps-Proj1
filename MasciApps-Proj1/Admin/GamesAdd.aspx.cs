using MasciApps_Proj1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MasciApps_Proj1.Admin
{
    public partial class GamesAdd : System.Web.UI.Page
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
         * This method populates the MatchType DropDown for Step 1.
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
         * This method populates the HomeTeam and AwayTeam DropDowns for Step 2.
         * </summary>
         * @method PopulateTeams
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void PopulateTeams(object sender, EventArgs e)
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                int sportID = Convert.ToInt32(MatchTypeDropDownList.SelectedItem.Value);

                //Query all teams of specified Sport Type.
                var allTeams = (from team in db.Teams
                                where team.SportID == sportID
                                select team);
                //Assign Sport Teams to HomeTeam and AwayTeam DropDowns
                HomeTeamDropDownList.DataSource = allTeams.ToList();
                HomeTeamDropDownList.DataBind();
                AwayTeamDropDownList.DataSource = allTeams.ToList();
                AwayTeamDropDownList.DataBind();
                //Start with no selection
                //MatchTypeDropDownList.ClearSelection();
            }
        }

        /**
         * <summary>
         * This method populated the MatchWinner DropDown for Step 3.
         * </summary>
         * @method PopulateMatchWinner
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void PopulateMatchWinner(object sender, EventArgs e)
        {
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                int sportID = Convert.ToInt32(MatchTypeDropDownList.SelectedItem.Value);

                //Query 2 teams specified in Step 2.
                var currentTeams = (from team in db.Teams
                                    where team.TeamID == sportID | team.TeamID == sportID
                                    select team);
                if (currentTeams != null)
                {
                    //Assign Current Teams to MatchWinner DropDown
                    MatchWinnerDropDownList.DataSource = currentTeams.ToList();
                    MatchWinnerDropDownList.DataBind();
                    //Start with no selection
                    MatchWinnerDropDownList.ClearSelection();
                }                
            }
            PopulateMatchName(); //Dynamically create Name of Match based on Teams chosen
        }

        /**
         * <summary>
         * This method creates the Match name dynamically based on Teams Chosen from Step 2.
         * </summary>
         * @method PopulateMatchName
         * @returns {void}
         */
         private void PopulateMatchName()
        {
            //Check for Null values
            string homeTeam = HomeTeamDropDownList.SelectedValue == null ? HomeTeamDropDownList.Text = "N/A" : HomeTeamDropDownList.SelectedItem.Text;
            string awayTeam = AwayTeamDropDownList.SelectedValue == null ? AwayTeamDropDownList.Text = "N/A" : AwayTeamDropDownList.SelectedItem.Text;
            //Set MatchName
            MatchNameTextBox.Text = homeTeam + " vs. " + awayTeam;
        } 

        /**
         * <summary>
         * This method adds a new Match to the Matches table using EF.
         * </summary>
         * @method AddMatchSubmit_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */ 
        protected void AddMatchSubmit_Click(object sender, EventArgs e)
        {
            //use EF to connect to the Server
            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                //use the student model to save a new record
                Match newMatch = new Match()
                {
                    SportID = Convert.ToInt32(MatchTypeDropDownList.SelectedValue),
                    HomeTeamID = Convert.ToInt32(HomeTeamDropDownList.SelectedValue),
                    AwayTeamID = Convert.ToInt32(AwayTeamDropDownList.SelectedValue),
                    Winner = Convert.ToInt32(MatchWinnerDropDownList.SelectedValue),
                    Name = MatchNameTextBox.Text,
                    DateTime = Convert.ToDateTime(MatchDateTextBox.Text + " " + MatchTimeTextBox.Text),
                    SpecCount = Convert.ToInt32(MatchSpecCountTextBox.Text),
                    HomeTeamScore = Convert.ToInt32(HomeTeamScoreTextBox.Text),
                    AwayTeamScore = Convert.ToInt32(AwayTeamScoreTextBox.Text)
                };

                //adding the new student to the collection
                db.Matches.Add(newMatch);
                db.SaveChanges();

                Response.Redirect("~/Games.aspx");
            }
        }

        /**
         * <summary>
         * This method cancels the process to add a Match and returns the user to the Games Page.
         * </summary>
         * @method AddMatchCancel_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void AddMatchCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Games.aspx");
        }
    }
}