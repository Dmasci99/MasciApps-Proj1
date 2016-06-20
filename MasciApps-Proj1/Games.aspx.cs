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
            }
            else
            { //On postback
                //test.InnerText = "IsPostBack";
                if (Request.QueryString.Count > 0) //If querystring provided (true when editing)
                    if (HttpContext.Current.User.Identity.IsAuthenticated) //If user is logged in - enter edit mode
                        this.GetMatch();
                    else
                        Response.Redirect("~/Login.aspx");                
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
                HtmlControl EditTemplate = ((HtmlControl)GamesListView.Items[itemID].FindControl("EditTemplate"));

                using (DefaultConnectionEF db = new DefaultConnectionEF())
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

                    /**
                     * Queries for populating DropDown Selections/Options
                     */
                    //MatchSelect
                    var sports = (from sport in db.Sports
                                  select sport);
                    MatchTypeDropDownList.DataSource = sports.ToList();
                    MatchTypeDropDownList.DataBind();
                    //TeamSelect
                    var allTeams = (from team in db.Teams
                                    where team.SportID == matchToEdit.SportID
                                    select team);
                    HomeTeamDropDownList.DataSource = allTeams.ToList();
                    HomeTeamDropDownList.DataBind();
                    AwayTeamDropDownList.DataSource = allTeams.ToList();
                    AwayTeamDropDownList.DataBind();
                    //WinnerSelect
                    var currentTeams = (from team in db.Teams
                                        where team.TeamID == matchToEdit.HomeTeamID | team.TeamID == matchToEdit.AwayTeamID
                                        select team);
                    MatchWinnerDropDownList.DataSource = currentTeams.ToList();
                    MatchWinnerDropDownList.DataBind();

                    /**
                     * Fill Edit Forms with appropriate data
                     */
                    /* Text Boxes */
                    //Home Team
                    HomeTeamDropDownList.SelectedValue = Convert.ToString(matchToEdit.HomeTeamID);
                    HomeTeamScoreTextBox.Text = Convert.ToString(matchToEdit.HomeTeamScore);
                    //Away Team
                    AwayTeamDropDownList.SelectedValue = Convert.ToString(matchToEdit.AwayTeamID);
                    AwayTeamScoreTextBox.Text = Convert.ToString(matchToEdit.AwayTeamScore);
                    //Match
                    MatchTypeDropDownList.SelectedValue = Convert.ToString(matchToEdit.SportID);
                    MatchNameTextBox.Text = matchToEdit.MatchName;
                    MatchDateTextBox.Text = Convert.ToDateTime(matchToEdit.DateTime).ToString("yyyy-MM-dd");
                    MatchTimeTextBox.Text = Convert.ToDateTime(matchToEdit.DateTime).ToString("HH:mm");
                    MatchWinnerDropDownList.SelectedValue = Convert.ToString(matchToEdit.Winner);
                    MatchSpecCountTextBox.Text = Convert.ToString(matchToEdit.SpecCount);

                    //Reveal the edit form for the specified Match(ListViewItem)
                    EditTemplate.Attributes.Add("class", "game-edit active");
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
            this.GetMatches(); //Refresh ListVIew
        }
                

        protected void AddMatchButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/GamesAdd.aspx");
        }

        protected void EditMatchCancel_Click(object sender, EventArgs e)
        {
            int itemID = Convert.ToInt32(Request.QueryString["itemID"]); //ID of the specific Match(ListViewItem) we are editing
            HtmlControl EditTemplate = ((HtmlControl)GamesListView.Items[itemID].FindControl("EditTemplate"));
            //Hide edit form - remove class "active"
            EditTemplate.Attributes.Add("class", "game-edit");
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
            HtmlControl EditTemplate = ((HtmlControl)GamesListView.Items[itemID].FindControl("EditTemplate"));

            using (DefaultConnectionEF db = new DefaultConnectionEF())
            {
                //Query db for specific Match and Teams
                Match matchToEdit = (from match in db.Matches
                                     where match.MatchID == matchID
                                     select match).FirstOrDefault();
                
                //Make appropriate changes to Match record
                matchToEdit.SportID = Convert.ToInt32(MatchTypeDropDownList.SelectedValue);
                matchToEdit.HomeTeamID = Convert.ToInt32(HomeTeamDropDownList.SelectedValue);
                matchToEdit.AwayTeamID = Convert.ToInt32(AwayTeamDropDownList.SelectedValue);
                matchToEdit.Winner = Convert.ToInt32(MatchWinnerDropDownList.SelectedValue);
                matchToEdit.Name = MatchNameTextBox.Text;
                matchToEdit.DateTime = Convert.ToDateTime(MatchDateTextBox.Text + " " + MatchTimeTextBox.Text);
                matchToEdit.SpecCount = Convert.ToInt32(MatchSpecCountTextBox.Text);
                matchToEdit.HomeTeamScore = Convert.ToInt32(HomeTeamScoreTextBox.Text);
                matchToEdit.AwayTeamScore = Convert.ToInt32(AwayTeamScoreTextBox.Text);

                db.SaveChanges();// save db - update match
                //Hide edit form - remove class "active"
                EditTemplate.Attributes.Add("class", "game-edit");
            }
        }

        #endregion
    }
}