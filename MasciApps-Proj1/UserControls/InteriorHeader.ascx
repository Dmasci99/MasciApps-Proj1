<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InteriorHeader.ascx.cs" Inherits="MasciApps_Proj1.UserControls.InteriorHeader" %>
<%--
/**
* Author : Daniel Masci - 200299037
* Class : Enterprise Computing
* Semester : 4
* Professor : Tom Tsiliopolous
* Purpose : Project 1 - Game Tracker
* Website Name : DanMasci.azurewebsites.net
* 
* The Following control is used as my Inteiror-Page Header.
* It is only called into my Interior.Master.
*/
--%>

<div class="interior-header" id="headerBackground" runat="server">
	<div class="container">
		<h1><%= Page.Title %></h1>
	</div>
</div><!-- interior-header -->