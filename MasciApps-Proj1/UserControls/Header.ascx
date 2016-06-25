<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="MasciApps_Proj1.UserControls.Header" %>
<%--
/**
* Author : Daniel Masci - 200299037
* Class : Enterprise Computing
* Semester : 4
* Professor : Tom Tsiliopolous
* Purpose : Project 1 - Game Tracker
* Website Name : MasciApps-GameTracker.azurewebsites.net
* 
* The Following control is used as my Header that is called into the Site.Master
* and Interior.Master. It is used as a consistent menu across the whole site.
*/
--%>

<header class="header">
	<div class="container">
        <!-- Mobile Menu -->
        <span id="moby-button"><i class="fa fa-bars"></i></span>
        <div class="clear-float"></div><!-- clear-float -->

        <!-- Logo -->
        <div class="logo">
			<img src="../Assets/Logos/masciapps(med).png">
		</div><!-- logo -->

        <!-- Main Menu -->
        <nav id="header-nav">
	        <ul class="menu">
                <li id="home"><a href="~/Default.aspx">Home</a></li>
		        <li><a runat="server" id="about" href="~/About.aspx">About</a></li>
		        <li><a runat="server" id="projects" href="~/Projects.aspx">Projects</a>
			        <ul class="sub-menu">
				        <li><a href="http://dekoningemc.ca/">Dekoning Mechanical</a></li>
				        <li><a href="http://www.turnkeybioscience.com/">Turnkey Bioscience</a></li>
				        <li><a href="http://www.liveinluxury.ca/">LiveInLuxury</a></li>
				        <li><a href="http://dogdayscamp.com/">DogDaysCamp</a></li>
			        </ul>
		        </li>						
		        <li><a runat="server" id="teams" href="~/Teams.aspx">Teams</a></li>	
                <li><a runat="server" id="games" href="~/Games.aspx">Games</a></li>	
		        <li><a runat="server" id="contact" href="~/Contact.aspx">Contact</a></li>	
		        <div class="clear-float"></div><!--clear-float-->				
	        </ul>
        </nav><!--.header-nav-->

        <!-- Account Menu -->
        <nav id="account-nav">        
            <asp:PlaceHolder runat="server" ID="PublicPlaceholder" Visible="true">
	            <ul class="menu">
		            <li><a runat="server" id="login" class="login" href="~/Login.aspx">Login</a></li>
		            <li><a runat="server" id="register" class="register" href="~/Register.aspx">Register</a></li>
	            </ul>
            </asp:PlaceHolder>
	        <asp:PlaceHolder runat="server" ID="PrivatePlaceholder" Visible="false">
                <ul class="menu loggedin">
		            <li><a runat="server" id="profile" class="profile" title="Profile" href="~/Admin/Profile.aspx"><i class="fa fa-user"></i></a></li>
		            <li><a runat="server" id="logout" class="logout" title="Sign out" href="~/Logout.aspx"><i class="fa fa-sign-out"></i></a></li>
		            <div class="clear-float"></div><!--clear-float-->
	            </ul>
            </asp:PlaceHolder>
        </nav><!-- account-nav -->
        
        <div class="clear-float"></div><!--clear-float-->
    </div>
</header>