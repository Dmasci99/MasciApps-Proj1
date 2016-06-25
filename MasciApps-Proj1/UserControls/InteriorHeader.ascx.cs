using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/**
* Author : Daniel Masci - 200299037
* Class : Enterprise Computing
* Semester : 4
* Professor : Tom Tsiliopolous
* Purpose : Project 1 - Game Tracker
* Website Name : MasciApps-GameTracker.azurewebsites.net
* 
* The Following control is the Codebehind for my Inteiror-Page Header.
* It is only called into my Interior.Master.
*/
namespace MasciApps_Proj1.UserControls
{
    public partial class InteriorHeader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            setHeaderImage();
        }

        /**
         * This method sets the Interior Header to the appropriate Background Image.
         *  
         * @method setHeaderImage
         * @return void
         */
        private void setHeaderImage()
        {
            string backgroundImage = "/Assets/" + Page.Title.ToLower().Replace(" ", "") + "-header.jpg";
            //headerBackground.Attributes.Add("style", "background-image: url(" + backgroundImage + ")");
            headerBackground.Attributes.Add("style", "background-image: url(/Assets/dashboard.jpg");
        }
    }
}