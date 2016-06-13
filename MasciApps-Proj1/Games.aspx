<%@ Page Title="Games" Language="C#" MasterPageFile="~/Interior.Master" AutoEventWireup="true" CodeBehind="Games.aspx.cs" Inherits="MasciApps_Proj1.Games" %>
<%-- 
/**
* Author : Daniel Masci - 200299037
* Class : Enterprise Computing
* Semester : 4
* Professor : Tom Tsiliopolous
* Purpose : Project 1 - Game Tracker
* Website Name : MasciApps-GameTracker.azurewebsites.net/Games.aspx
* 
* The Following page is the Content for the "Games" page.
*/ 
--%>

<asp:Content ID="GamesPageContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="interior-page gametracker-page" id="games-page">
        <div class="container">
             
            <asp:ListView runat="server" ID="GamesListView" DataKeyNames="MatchID">
                <LayoutTemplate>
                    <div class="games-container">
                        <div runat="server" id="itemPlaceholder"></div>
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="game-container">
                        <div class="game-background"></div>

                        <div class="game-info">
                            <div class="date"><%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "Date")).ToString("dddd, MMM d @ hh:mmtt") %></div>
                            <div class="team left">
                                <div class="logo"><i class="fa fa-facebook"></i></div>
                                <div class="team-name"><%# DataBinder.Eval(Container.DataItem, "HomeTeamID") %></div>
                            </div>
                            <div class="team right">
                                <div class="logo"><i class="fa fa-twitter"></i></div>
                                <div class="team-name"><%# DataBinder.Eval(Container.DataItem, "AwayTeamID") %></div>
                            </div>
                            <div class="info left">
                                <div class="score"><%# DataBinder.Eval(Container.DataItem, "HomeTeamScore") %></div>
                                <div class="winner"><%# DataBinder.Eval(Container.DataItem, "Winner").Equals(DataBinder.Eval(Container.DataItem, "HomeTeamID")) ? 
                                                            "Win" : "Lose" %></div>
                            </div>
                            <div class="info right">                                 
                                <div class="score"><%# DataBinder.Eval(Container.DataItem, "AwayTeamScore") %></div>
                                <div class="winner"><%# DataBinder.Eval(Container.DataItem, "Winner").Equals(DataBinder.Eval(Container.DataItem, "AwayTeamID")) ? 
                                                            "Win" : "Lose" %></div>
                            </div>
                            <div class="spectators"><%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SpecCount")).ToString("N0") %> Spectators</div>
                        </div>

                    </div>
                </ItemTemplate>
            </asp:ListView> 

        </div>
    </div>

</asp:Content>
