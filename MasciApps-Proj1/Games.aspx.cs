using MasciApps_Proj1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
            {
                GameCalendar.SelectedDate = DateTime.Today; //Set Calendar to Today
                this.GetMatches(); //Refresh ListView
            }
        }

        #endregion

        #region Private Functions
        /**
         * <summary>
         * This method retrieves the matches found for the specified week.
         * </summary>
         * @method GetGames
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
                                   where allMatches.Date >= startDate && allMatches.Date <= endDate
                                   select new
                                   {
                                       allMatches.MatchID,
                                       allMatches.HomeTeamID,
                                       allMatches.AwayTeamID,
                                       allMatches.HomeTeamScore,
                                       allMatches.AwayTeamScore,
                                       MatchName = allMatches.Name,
                                       allMatches.Date,
                                       allMatches.Winner,
                                       allMatches.SpecCount,
                                       HomeTeamLogo = homeTeam.Logo,
                                       HomeTeamName = homeTeam.Name,
                                       AwayTeamLogo = awayTeam.Logo,
                                       AwayTeamName = awayTeam.Name,
                                       SportName = sport.Name
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

    }
}