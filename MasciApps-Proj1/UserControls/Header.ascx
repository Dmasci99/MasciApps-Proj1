<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="MasciApps_Proj1.UserControls.Header" %>
<%--
/**
* Author : Daniel Masci - 200299037
* Class : Enterprise Computing
* Semester : 4
* Professor : Tom Tsiliopolous
* Purpose : Project 1 - Game Tracker
* Website Name : DanMasci.azurewebsites.net
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

        <!-- Main Menu -->
        <nav id="header-nav">
	        <ul class="menu">
                <li id="home"><a href="../Default.aspx">Home</a></li>
		        <li><a runat="server" id="about" href="../About.aspx">About</a></li>
		        <li><a runat="server" id="projects" href="../Projects.aspx">Projects</a>
			        <ul class="sub-menu">
				        <li><a href="http://dekoningemc.ca/">Dekoning Mechanical</a></li>
				        <li><a href="http://www.turnkeybioscience.com/">Turnkey Bioscience</a></li>
				        <li><a href="http://www.liveinluxury.ca/">LiveInLuxury</a></li>
				        <li><a href="http://dogdayscamp.com/">DogDaysCamp</a></li>
			        </ul>
		        </li>						
		        <li><a runat="server" id="services" href="../Services.aspx">Services</a></li>	
		        <li><a runat="server" id="contact" href="../Contact.aspx">Contact</a></li>	
		        <div class="clear-float"></div><!--clear-float-->				
	        </ul>
        </nav><!--.header-nav-->

        <div class="clear-float"></div><!--clear-float-->
    </div>
</header>