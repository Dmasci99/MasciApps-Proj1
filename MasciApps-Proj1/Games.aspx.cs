using MasciApps_Proj1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;

namespace MasciApps_Proj1
{
    public partial class Games : System.Web.UI.Page
    {
        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { //On first page render
                //test.InnerText = "!IsPostBack";
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                    PrivatePlaceHolder.Visible = true;
                GameCalendar.SelectedDate = DateTime.Today; //Set Calendar to Today
                this.GetMatches(); //Refresh ListView        
                Page.MaintainScrollPositionOnPostBack = false;
            }
        }

        #endregion

        #region Private Functions
        /**
         * <summary>
         * This method retrieves the matches found for the specified week.
         * </summary>
         * @method GetMatches
         * @returns {void}
         */
        private void GetMatches()
        {
            DateTime initialDate = GameCalendar.SelectedDate; //Grab Date from Calendar
            DateTime startDate = initialDate.AddDays(Convert.ToInt32(initialDate.DayOfWeek) * -1); //Set to the first day of the desired week
            DateTime endDate = startDate.AddDays(7); //Set to the last day of the current week

            CalendarValue.InnerText = "Week of: " + GameCalendar.SelectedDate.ToString("MM/dd/yyyy");

            try { 
                using (DefaultConnectionEF db = new DefaultConnectionEF())
                {
                    //query the Matches table using EF and Linq
                    var matches = (from allMatches in db.Matches
                                    join homeTeam in db.Teams on allMatches.HomeTeamID equals homeTeam.TeamID
                                    join awayTeam in db.Teams on allMatches.AwayTeamID equals awayTeam.TeamID
                                    join sport in db.Sports on homeTeam.SportID equals sport.SportID
                                    where allMatches.DateTime >= startDate && allMatches.DateTime <= endDate
                                    select new
                                    {
                                        allMatches.MatchID,
                                        allMatches.SportID,
                                        allMatches.HomeTeamID,
                                        allMatches.AwayTeamID,
                                        allMatches.Winner,
                                        MatchName = allMatches.Name,
                                        SportName = sport.Name,
                                        allMatches.DateTime,
                                        allMatches.SpecCount,
                                        allMatches.HomeTeamScore,
                                        HomeTeamName = homeTeam.Name,
                                        allMatches.AwayTeamScore,
                                        AwayTeamName = awayTeam.Name
                                    });
                    if (matches != null)
                    {
                        //bind the result to the GamesListView
                        GamesListView.DataSource = matches.ToList();
                        GamesListView.DataBind();
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
         * This is a Helper Method for GetMatch() by toggling the Edit Mode On or Off.
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
                //Reveal the edit form for the specified Match(ListViewItem)
                ((PlaceHolder)GamesListView.Items[itemID].FindControl("EditTemplate")).Visible = true;
                //Hide edit button and show delete button
                ((LinkButton)GamesListView.Items[itemID].FindControl("EditMatchLink")).Visible = false;
                ((LinkButton)GamesListView.Items[itemID].FindControl("DeleteMatchLink")).Visible = true;
            }
            else
            {
                //Hide the edit form
                ((PlaceHolder)GamesListView.Items[itemID].FindControl("EditTemplate")).Visible = false;
                //Hide delete button and show edit button
                ((LinkButton)GamesListView.Items[itemID].FindControl("DeleteMatchLink")).Visible = false;
                ((LinkButton)GamesListView.Items[itemID].FindControl("EditMatchLink")).Visible = true;
            }
        }
        /**
         * <summary>
         * This method populates the MatchType DropDown.
         * </summary>
         * 
         */
        protected void PopulateMatchType(DropDownList MatchType, int matchID)
        {
            try { 
                using (DefaultConnectionEF db = new DefaultConnectionEF())
                {
                    //Populate Dropdown with available values
                    var sports = (from sport in db.Sports
                                  select sport);
                    if (sports != null)
                    {
                        MatchType.DataSource = sports.ToList();
                        MatchType.DataBind();
                    }
                    //Select the correct sport of current ListViewItem
                    var currentMatch = (from match in db.Matches
                                       where match.MatchID == matchID
                                       select match).FirstOrDefault();
                    if (currentMatch != null)
                    {
                        MatchType.SelectedValue = Convert.ToString(currentMatch.SportID);
                        PopulateTeams(null, null);
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
         * This method populates the HomeTeam and AwayTeam DropDowns.
         * </summary>
         * @method PopulateTeams
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void PopulateTeams(object sender, EventArgs e)
        {
            int itemID = Convert.ToInt32(Request.QueryString["itemID"]); //ID of the specific Match(ListViewItem) we are editing
            int matchID = Convert.ToInt32(Request.QueryString["matchID"]); //ID of the Match as stored in database

            //Control References 
            DropDownList MatchTypeDropDownList = ((DropDownList)GamesListView.Items[itemID].FindControl("MatchTypeDropDownList"));
            DropDownList HomeTeamDropDownList = ((DropDownList)GamesListView.Items[itemID].FindControl("HomeTeamDropDownList"));
            DropDownList AwayTeamDropDownList = ((DropDownList)GamesListView.Items[itemID].FindControl("AwayTeamDropDownList"));

            try { 
                using (DefaultConnectionEF db = new DefaultConnectionEF())
                {
                    int sportID = Convert.ToInt32(MatchTypeDropDownList.SelectedItem.Value); //ID of the Current Sport

                    //Query all teams of specified Sport Type.
                    var allTeams = (from team in db.Teams
                                    where team.SportID == sportID
                                    select team);
                    //Populate HomeTeam and AwayTeam DropDowns with Teams
                    HomeTeamDropDownList.DataSource = allTeams.ToList();
                    HomeTeamDropDownList.DataBind();
                    AwayTeamDropDownList.DataSource = allTeams.ToList();
                    AwayTeamDropDownList.DataBind();
                    //Set Default Team Selections
                    if (sender == null && e == null)
                    { //if method is being called from PopulateMatchType()
                        var currentMatch = (from match in db.Matches
                                             where match.MatchID == matchID
                                             select match).FirstOrDefault();
                        if (currentMatch != null)
                        {
                            HomeTeamDropDownList.SelectedValue = Convert.ToString(currentMatch.HomeTeamID);
                            AwayTeamDropDownList.SelectedValue = Convert.ToString(currentMatch.AwayTeamID);
                        }
                        PopulateMatchWinner(null, null);
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
         * This method populated the MatchWinner DropDown.
         * </summary>
         * @method PopulateMatchWinner
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void PopulateMatchWinner(object sender, EventArgs e)
        {
            int itemID = Convert.ToInt32(Request.QueryString["itemID"]); //ID of the specific Match(ListViewItem) we are editing
            int matchID = Convert.ToInt32(Request.QueryString["matchID"]); //ID of the Match as stored in database
            //Control References 
            DropDownList HomeTeamDropDownList = ((DropDownList)GamesListView.Items[itemID].FindControl("HomeTeamDropDownList"));
            DropDownList AwayTeamDropDownList = ((DropDownList)GamesListView.Items[itemID].FindControl("AwayTeamDropDownList"));
            DropDownList MatchWinnerDropDownList = ((DropDownList)GamesListView.Items[itemID].FindControl("MatchWinnerDropDownList"));
            try
            {
                using (DefaultConnectionEF db = new DefaultConnectionEF())
                {
                    int homeTeamID = Convert.ToInt32(HomeTeamDropDownList.SelectedValue);
                    int awayTeamID = Convert.ToInt32(AwayTeamDropDownList.SelectedValue);
                    var currentTeams = (from team in db.Teams
                                        where team.TeamID == homeTeamID | team.TeamID == awayTeamID
                                        select team);
                    if (currentTeams != null)
                    {
                        //Assign Current Teams to MatchWinner DropDown
                        MatchWinnerDropDownList.DataSource = currentTeams.ToList();
                        MatchWinnerDropDownList.DataBind();
                        if (sender == null && e == null)
                        { //If being called from PopulateTeams()
                            var currentMatch = (from match in db.Matches
                                                where match.MatchID == matchID
                                                select match).FirstOrDefault();
                            MatchWinnerDropDownList.SelectedValue = Convert.ToString(currentMatch.Winner);
                        } 
                    }
                }
                this.PopulateMatchName(); //Dynamically create Name of Match based on Teams chosen
            }
            catch (Exception err)
            {
                ErrorLabel.Text = "Failed to connect to db";
                ErrorContainer.Visible = true;
            }
        }

        /**
         * <summary>
         * This method creates the Match name dynamically based on Teams Chosen.
         * </summary>
         * @method PopulateMatchName
         * @returns {void}
         */
        private void PopulateMatchName()
        {
            int itemID = Convert.ToInt32(Request.QueryString["itemID"]); //ID of the specific Match(ListViewItem) we are editing
            //Control References 
            DropDownList HomeTeamDropDownList = ((DropDownList)GamesListView.Items[itemID].FindControl("HomeTeamDropDownList"));
            DropDownList AwayTeamDropDownList = ((DropDownList)GamesListView.Items[itemID].FindControl("AwayTeamDropDownList"));
            TextBox MatchNameTextBox = ((TextBox)GamesListView.Items[itemID].FindControl("MatchNameTextBox"));
            //Check for Null values
            string homeTeam = HomeTeamDropDownList.SelectedValue == null ? HomeTeamDropDownList.Text = "N/A" : HomeTeamDropDownList.SelectedItem.Text;
            string awayTeam = AwayTeamDropDownList.SelectedValue == null ? AwayTeamDropDownList.Text = "N/A" : AwayTeamDropDownList.SelectedItem.Text;
            //Set MatchName
            MatchNameTextBox.Text = homeTeam + " vs. " + awayTeam;
        }

        #endregion

        #region Event Handlers
        /**
         * <summary>
         * This method redirects the user to Add a New Match.
         * </summary>
         * @method AddMatchButton_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void} 
         */
        protected void AddMatchButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/GamesAdd.aspx");
        }

        /**
         * <summary>
         * This method shows the EditTemplate and prepares for editing.
         * </summary>
         * @method EditMatchLink_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void EditMatchLink_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                int matchID = Convert.ToInt32(Request.QueryString["matchID"]); //ID of the Match as stored in database
                int itemID = Convert.ToInt32(Request.QueryString["itemID"]); //ID of the specific Match(ListViewItem) we are editing
                this.PopulateMatchType(((DropDownList)GamesListView.Items[itemID].FindControl("MatchTypeDropDownList")), matchID);
                this.ToggleEditMode("Edit", itemID);
            }            
        }

        /**
         * <summary>
         * This method deletes the chosen Match.
         * </summary>
         * @method DeleteMatchLink_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void} 
         */
        protected void DeleteMatchLink_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                try { 
                    using (DefaultConnectionEF db = new DefaultConnectionEF())
                    {
                        int matchID = Convert.ToInt32(Request.QueryString["matchID"]);
                        Match matchToDelete = (from match in db.Matches
                                               where match.MatchID == matchID
                                               select match).FirstOrDefault();
                        if (matchToDelete != null)
                        {
                            db.Matches.Remove(matchToDelete); //remove Match
                            db.SaveChanges(); //save db
                        }                    
                        this.GetMatches(); //Refresh ListView
                    }
                }
                catch (Exception err)
                {
                    ErrorLabel.Text = "Failed to connect to db";
                    ErrorContainer.Visible = true;
                }
            }         
        }

        /**
         * <summary>
         * This method cancels the process of Editing an Existing Match.
         * </summary>
         * @method EditMatchCancel_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void} 
         */
        protected void EditMatchCancel_Click(object sender, EventArgs e)
        {            
            this.ToggleEditMode("View", Convert.ToInt32(Request.QueryString["itemID"]));
        }

        /**
         * <summary>
         * This method takes the user input and updates an existing Match record.
         * </summary>
         * @method EditMatchUpdate_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void EditMatchUpdate_Click(object sender, EventArgs e)
        {
            int matchID = Convert.ToInt32(Request.QueryString["matchID"]); //ID of the Match as stored in database
            int itemID = Convert.ToInt32(Request.QueryString["itemID"]); //ID of the specific Match(ListViewItem) we are editing

            //Control References
            DropDownList MatchTypeDropDownList = ((DropDownList)GamesListView.Items[itemID].FindControl("MatchTypeDropDownList"));
            DropDownList HomeTeamDropDownList = ((DropDownList)GamesListView.Items[itemID].FindControl("HomeTeamDropDownList"));
            DropDownList AwayTeamDropDownList = ((DropDownList)GamesListView.Items[itemID].FindControl("AwayTeamDropDownList"));
            DropDownList MatchWinnerDropDownList = ((DropDownList)GamesListView.Items[itemID].FindControl("MatchWinnerDropDownList"));
            TextBox MatchNameTextBox = ((TextBox)GamesListView.Items[itemID].FindControl("MatchNameTextBox"));
            TextBox MatchDateTextBox = ((TextBox)GamesListView.Items[itemID].FindControl("MatchDateTextBox"));
            TextBox MatchTimeTextBox = ((TextBox)GamesListView.Items[itemID].FindControl("MatchTimeTextBox"));
            TextBox MatchSpecCountTextBox = ((TextBox)GamesListView.Items[itemID].FindControl("MatchSpecCountTextBox"));
            TextBox HomeTeamScoreTextBox = ((TextBox)GamesListView.Items[itemID].FindControl("HomeTeamScoreTextBox"));
            TextBox AwayTeamScoreTextBox = ((TextBox)GamesListView.Items[itemID].FindControl("AwayTeamScoreTextBox"));
            PlaceHolder EditTemplate = ((PlaceHolder)GamesListView.Items[itemID].FindControl("EditTemplate"));

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                try { 
                    using (DefaultConnectionEF db = new DefaultConnectionEF())
                    {
                        //Query db for specific Match and Teams
                        Match matchToEdit = (from match in db.Matches
                                             where match.MatchID == matchID
                                             select match).FirstOrDefault();
                        //Make appropriate changes to Match record
                        if (matchToEdit != null)
                        {
                            matchToEdit.SportID = Convert.ToInt32(MatchTypeDropDownList.SelectedValue); //Needs troubleshooting
                            matchToEdit.HomeTeamID = Convert.ToInt32(HomeTeamDropDownList.SelectedValue);
                            matchToEdit.AwayTeamID = Convert.ToInt32(AwayTeamDropDownList.SelectedValue);
                            matchToEdit.Winner = Convert.ToInt32(MatchWinnerDropDownList.SelectedValue);
                            matchToEdit.Name = MatchNameTextBox.Text;
                            matchToEdit.DateTime = Convert.ToDateTime(MatchDateTextBox.Text + " " + MatchTimeTextBox.Text);
                            matchToEdit.SpecCount = Convert.ToInt32(MatchSpecCountTextBox.Text);
                            matchToEdit.HomeTeamScore = Convert.ToInt32(HomeTeamScoreTextBox.Text);
                            matchToEdit.AwayTeamScore = Convert.ToInt32(AwayTeamScoreTextBox.Text);
                            db.SaveChanges();// save db - update match
                        }
                        this.ToggleEditMode("Edit", itemID); //Exit Edit Mode
                        this.GetMatches(); //Refresh ListView
                    }
                }
                catch (Exception err)
                {
                    ErrorLabel.Text = "Failed to connect to db";
                    ErrorContainer.Visible = true;
                }
            }
        }

        /**
         * <summary>
         * This method refreshes the ListView pulling the previous week's Matches.
         * </summary>
         * @method PreviousButton_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void PreviousButton_Click(object sender, EventArgs e)
        {

            GameCalendar.SelectedDate = GameCalendar.SelectedDate.AddDays(-7); //Set new Date 1 week behind            
            this.GetMatches(); //Refresh ListView with new Date
        }

        /**
         * <summary>
         * This method refreshes the ListView pulling the next week's Matches. 
         * </summary>
         * @method NextButton_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void NextButton_Click(object sender, EventArgs e)
        {
            GameCalendar.SelectedDate = GameCalendar.SelectedDate.AddDays(7); //Set new Date 1 week ahead            
            this.GetMatches(); //Refresh ListView with New Date
        }

        /**
         * <summary>
         * This method refreshes the ListView pulling the chosen week's Matches.
         * </summary>
         * @method GameCalendar_SelectionChanged
         * @param {object} sender
         * @param {EventArgs] e
         * @returns {void}
         */
        protected void GameCalendar_SelectionChanged(object sender, EventArgs e)
        {
            this.GetMatches(); //Refresh ListView
        }

        #endregion

    }
}