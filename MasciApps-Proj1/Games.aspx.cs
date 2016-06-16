using MasciApps_Proj1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MasciApps_Proj1
{
    public partial class Games : System.Web.UI.Page
    {
        #region Global Variables


        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { //On first page render
                //test.InnerText = "!IsPostBack";
                GameCalendar.SelectedDate = DateTime.Today; //Set Calendar to Today
                this.GetMatches(); //Refresh ListView                
            }
            else
            { //On postback
                //test.InnerText = "IsPostBack";
                if (Request.QueryString.Count > 0)
                { //If querystring provided (true when editing)
                    this.GetMatch();
                }
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
            try
            {
                DateTime initialDate = GameCalendar.SelectedDate; //Grab Date from Calendar
                DateTime startDate = initialDate.AddDays(Convert.ToInt32(initialDate.DayOfWeek) * -1); //Set to the first day of the desired week
                DateTime endDate = startDate.AddDays(7); //Set to the last day of the current week

                CalendarValue.InnerText = "Week of: " + GameCalendar.SelectedDate.ToString("MM/dd/yyyy");

                using (DefaultConnection db = new DefaultConnection())
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
                                       HomeTeamLogo = homeTeam.Logo,
                                       allMatches.AwayTeamScore,
                                       AwayTeamName = awayTeam.Name,
                                       AwayTeamLogo = awayTeam.Logo
                                   });

                    //bind the result to the GamesListView
                    GamesListView.DataSource = matches.ToList();
                    GamesListView.DataBind();
                }
            }
            catch (Exception e)
            {
                //Invalid Date Selection
                //Or Temporary Failure with Connection (no idea why it sometimes does this. Only has happened twice.)
            }        
        }

        /**
         * <summary>
         * This method retrieves a match for editing.
         * </summary>
         * @method GetMatch
         * @returns {void}
         */
        protected void GetMatch()
        {

            try
            {
                int matchID = Convert.ToInt32(Request.QueryString["matchID"]);
                using (DefaultConnection db = new DefaultConnection())
                {
                    //Query db for specific Match 
                    var matchToEdit = (from match in db.Matches
                                         join homeTeam in db.Teams on match.HomeTeamID equals homeTeam.TeamID
                                         join awayTeam in db.Teams on match.AwayTeamID equals awayTeam.TeamID
                                         join sport in db.Sports on homeTeam.SportID equals sport.SportID
                                         where match.MatchID == matchID
                                         select new
                                         {
                                             match.MatchID,
                                             match.HomeTeamID,
                                             match.AwayTeamID,
                                             match.HomeTeamScore,
                                             match.AwayTeamScore,
                                             MatchName = match.Name,
                                             match.DateTime,
                                             match.Winner,
                                             match.SpecCount,
                                             HomeTeamLogo = homeTeam.Logo,
                                             HomeTeamName = homeTeam.Name,
                                             AwayTeamLogo = awayTeam.Logo,
                                             AwayTeamName = awayTeam.Name,
                                             match.SportID,
                                             SportName = sport.Name
                                         }).FirstOrDefault();

                    int itemID = Convert.ToInt32(Request.QueryString["itemID"]); //ID of the specific Match(ListViewItem) we are editing
                    /**
                     * Queries for populating DropDown Selections/Options
                     */
                    var allTeams = (from team in db.Teams
                                    where team.SportID == matchToEdit.SportID
                                    select team);
                    ((DropDownList)GamesListView.Items[itemID].FindControl("HomeTeamDropDownList")).DataSource = allTeams.ToList();
                    ((DropDownList)GamesListView.Items[itemID].FindControl("HomeTeamDropDownList")).DataBind();

                    ((DropDownList)GamesListView.Items[itemID].FindControl("AwayTeamDropDownList")).DataSource = allTeams.ToList();
                    ((DropDownList)GamesListView.Items[itemID].FindControl("AwayTeamDropDownList")).DataBind();

                    var currentTeams = (from team in db.Teams
                                        where team.TeamID == matchToEdit.HomeTeamID | team.TeamID == matchToEdit.AwayTeamID
                                        select team);
                    ((DropDownList)GamesListView.Items[itemID].FindControl("MatchWinnerDropDownList")).DataSource = currentTeams.ToList();
                    ((DropDownList)GamesListView.Items[itemID].FindControl("MatchWinnerDropDownList")).DataBind();

                    var sports = (from sport in db.Sports
                                  select sport);
                    ((DropDownList)GamesListView.Items[itemID].FindControl("MatchTypeDropDownList")).DataSource = sports.ToList();
                    ((DropDownList)GamesListView.Items[itemID].FindControl("MatchTypeDropDownList")).DataBind();

                    /**
                     * Fill Edit Forms with appropriate data
                     */
                    /* Text Boxes */
                    //Home Team
                    ((DropDownList)GamesListView.Items[itemID].FindControl("HomeTeamDropDownList")).SelectedValue = Convert.ToString(matchToEdit.HomeTeamID);
                    ((TextBox)GamesListView.Items[itemID].FindControl("HomeTeamScoreTextBox")).Text = Convert.ToString(matchToEdit.HomeTeamScore);
                    //Away Team
                    ((DropDownList)GamesListView.Items[itemID].FindControl("AwayTeamDropDownList")).SelectedValue = Convert.ToString(matchToEdit.AwayTeamID);
                    ((TextBox)GamesListView.Items[itemID].FindControl("AwayTeamScoreTextBox")).Text = Convert.ToString(matchToEdit.AwayTeamScore);
                    //Match
                    ((DropDownList)GamesListView.Items[itemID].FindControl("MatchTypeDropDownList")).SelectedValue = Convert.ToString(matchToEdit.SportID);
                    ((TextBox)GamesListView.Items[itemID].FindControl("MatchNameTextBox")).Text = matchToEdit.MatchName;
                    ((TextBox)GamesListView.Items[itemID].FindControl("MatchDateTextBox")).Text = Convert.ToDateTime(matchToEdit.DateTime).ToString("yyyy-MM-dd");
                    ((TextBox)GamesListView.Items[itemID].FindControl("MatchTimeTextBox")).Text = Convert.ToDateTime(matchToEdit.DateTime).ToString("HH:mm");
                    ((DropDownList)GamesListView.Items[itemID].FindControl("MatchWinnerDropDownList")).SelectedValue = Convert.ToString(matchToEdit.Winner);
                    ((TextBox)GamesListView.Items[itemID].FindControl("MatchSpecCountTextBox")).Text = Convert.ToString(matchToEdit.SpecCount);

                    //Reveal the edit form for the specified Match(ListViewItem)
                    ((HtmlControl)GamesListView.Items[itemID].FindControl("editTemplate")).Attributes.Add("class", "game-edit active");                    

                }
            }
            catch (Exception e)
            {
                //test.InnerText = e.StackTrace;
            }

        }

        #endregion

        #region Event Handlers
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
            this.GetMatches();
        }

        #endregion

        /**
         * <summary>
         * This method handles the pre-edit of a Match: show edit form and populate with data.
         * </summary>
         * @method GamesListView_ItemEditing
         * @param {object} sender
         * @param {ListViewEditEventArgs} e
         * @returns {void}
         */ 
        protected void GamesListView_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            try
            {
                //Reverse engineer to work with data before updating database
                List<Match> matches = (List<Match>)GamesListView.DataSource; //List of Matches
                Match match = matches[GamesListView.EditItem.DataItemIndex];

                GamesListView.EditItem.FindControl("HomeTeamNameTextBox"); //Null - need item[rowindex]
                GamesListView.FindControl("HomeTeamNameTextBox"); //Null - need item[rowindex]
            }
            catch (Exception)
            {
                //
            }
            



            //Populate Edit Form with values

            //Query the Sports table for Match Type Drop Down List

            //Populate Match Type Selection

            //Update the Item

            //Show the Edit Form


        }

        protected void GamesListView_ItemUpdated(object sender, ListViewUpdatedEventArgs e)
        {

        }
    }
}