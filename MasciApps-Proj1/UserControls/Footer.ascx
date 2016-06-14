<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="MasciApps_Proj1.UserControls.Footer" %>
<%--
/**
* Author : Daniel Masci - 200299037
* Class : Enterprise Computing
* Semester : 4
* Professor : Tom Tsiliopolous
* Purpose : Project 1 - Game Tracker
* Website Name : MasciApps-GameTracker.azurewebsites.net
* 
* The Following control is used as my Footer that is called into the Site.Master
* and Interior.Master. It is used as a consistent footer across the whole site.
*/
--%>

<div class="clear-float"></div>				
<footer class="section">
	<div class="container">			
		<a class="home" href="/">Home</a>			
	</div><!-- container -->

	<div class="copyright">
		<div class="container">
			<p class="" id="copyright">Copyright &copy; 2016 MasciApps. All Rights Reserved.</p>
            <p class="" id="author">Website by Dan Masci</p>
		</div><!-- container -->				
	</div><!--div.copyright-->
</footer>

<!-- JS -->
<script src="Scripts/jquery-2.2.3.min.js"></script>
<script src="Scripts/moby.js"></script>
<script src="Scripts/custom.js"></script>
<script src="Scripts/gametracker.js"></script>
