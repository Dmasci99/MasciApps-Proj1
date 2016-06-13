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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetMatches();
            }            
        }

        /**
         * <summary>
         * This method retrieves all the matches found in the DB using EF.
         * </summary>
         * @method GetGames
         * @returns {void}
         */ 
        protected void GetMatches()
        {
            using (DefaultConnection db = new DefaultConnection())
            {
                //query the Matches table using EF and Linq
                var matches = (from allMatches in db.Matches
                               select new
                               {
                                   allMatches.MatchID,
                                   allMatches.HomeTeamID,
                                   allMatches.AwayTeamID,
                                   allMatches.HomeTeamScore,
                                   allMatches.AwayTeamScore,
                                   allMatches.Name,
                                   allMatches.Date,
                                   allMatches.Winner,
                                   allMatches.SpecCount
                               });

                //bind the result to the GamesListView
                GamesListView.DataSource = matches.ToList();
                GamesListView.DataBind();                
            }
        }

    }
}