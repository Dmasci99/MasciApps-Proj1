<%@ Page Title="Projects" Language="C#" MasterPageFile="~/Interior.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="MasciApps_Proj1.Projects" %>
<%-- 
/**
* Author : Daniel Masci - 200299037
* Class : Enterprise Computing
* Semester : 4
* Professor : Tom Tsiliopolous
* Purpose : Assignment 1 - ASP.NET Portfolio
* Website Name : DanMasci.azurewebsites.net/Projects.aspx
* 
* The Following page is the Content for the "Projects" page.
*/ 
--%>
<asp:Content ID="ProjectsPageContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="interior-page" id="projects-page">

	    <div class="container">
		    <div class="gallery content">
			    <h2>Static Websites</h2>
			    <ul class="projects">
				    <li class="project">					
					    <div class="image image-scale">
						    <img src="Assets/dekoning.jpg" alt="">
						    <div class="text">
							    <p>Dekoning</p>
							    <div class="button"><a href="http://dekoningemc.ca/">Take Me Here</a></div>
						    </div>
					    </div><!-- image -->
				    </li>
				    <li class="project">					
					    <div class="image image-scale">
						    <img src="Assets/turnkey.jpg" alt="">
						    <div class="text">
							    <p>Turnkey Bioscience</p>
							    <div class="button"><a href="http://www.turnkeybioscience.com/">Take Me Here</a></div>
						    </div>
					    </div><!-- image -->
				    </li>
				    <li class="project">					
					    <div class="image image-scale">
						    <img src="Assets/liveinluxury.jpg" alt="">
						    <div class="text">
							    <p>LiveInLuxury</p>
							    <div class="button"><a href="http://www.liveinluxury.ca/">Take Me Here</a></div>
						    </div>
					    </div><!-- image -->
				    </li>
				    <li class="project">					
					    <div class="image image-scale">
						    <img src="Assets/dogdayscamp.jpg" alt="">
						    <div class="text">
							    <p>DogDaysCamp</p>
							    <div class="button"><a href="http://dogdayscamp.com/">Take Me Here</a></div>
						    </div>
					    </div><!-- image -->
				    </li>
			    </ul><!-- projects -->
		    </div><!-- gallery -->
	    </div><!-- container -->

    </div><!-- #projects-page -->

</asp:Content>
