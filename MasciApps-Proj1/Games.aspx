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

            <div class="games-heading">
                <form runat="server">
                    <div class="pagination">
                        <asp:LinkButton runat="server" CSSClass="prev" OnClick="PreviousButton_Click" Text="<i class='fa fa-chevron-left'></i>"></asp:LinkButton>
                        <asp:LinkButton runat="server" CssClass="next" OnClick="NextButton_Click" Text="<i class='fa fa-chevron-right'></i>"></asp:LinkButton>
                    </div>
                    <div class="calender">
                        <div class="calendar-trigger" runat="server" id="CalendarValue">Week of: mm/dd/yyyy</div>
                        <asp:Calendar runat="server" ID="GameCalendar" CssClass="GameCalendar" OnSelectionChanged="GameCalendar_SelectionChanged" 
                        SelectionMode="Day" SelectedDayStyle-CssClass="SelectedWeek"></asp:Calendar>                        
                    </div>                
                </form>                            
            </div>

            <asp:ListView runat="server" ID="GamesListView" DataKeyNames="MatchID">
                <LayoutTemplate>
                    <div class="games-container">
                        <div runat="server" id="itemPlaceholder"></div>
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="game-container">
                        <div class="game-background <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "SportName")).Trim().ToLower().Replace(" ", "-") %>"></div>

                        <div class="game-content">
                            <div class="date"><%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "Date")).ToString("dddd, MMM d @ hh:mmtt") %></div>
                            <div class="teams">
                                <div class="team left">
                                    <div class="logo"><%# DataBinder.Eval(Container.DataItem, "HomeTeamLogo") %></div>
                                    <div class="team-name"><%# DataBinder.Eval(Container.DataItem, "HomeTeamName") %></div>
                                </div>
                                <div class="team right">
                                    <div class="logo"><%# DataBinder.Eval(Container.DataItem, "AwayTeamLogo") %></div>
                                    <div class="team-name"><%# DataBinder.Eval(Container.DataItem, "AwayTeamName") %></div>
                                </div>
                            </div>
                            <div class="game-info">
                                <div class="info left">
                                    <div class="score"><%# DataBinder.Eval(Container.DataItem, "HomeTeamScore") %></div>
                                    <div class="<%# DataBinder.Eval(Container.DataItem, "Winner").Equals(DataBinder.Eval(Container.DataItem, "HomeTeamID")) ? 
                                                                "winner" : "loser" %>"></div>
                                </div>
                                <div class="info right">                                 
                                    <div class="score"><%# DataBinder.Eval(Container.DataItem, "AwayTeamScore") %></div>
                                    <div class="<%# DataBinder.Eval(Container.DataItem, "Winner").Equals(DataBinder.Eval(Container.DataItem, "AwayTeamID")) ? 
                                                                "winner" : "loser" %>"></div>
                                </div>                                
                                <div class="spectators"><%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SpecCount")).ToString("N0") %> Spectators</div>
                            </div>                            
                        </div>

                    </div>
                </ItemTemplate>
            </asp:ListView> 

        </div>
    </div>

</asp:Content>
