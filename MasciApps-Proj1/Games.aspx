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
* Allows the user (depending on authentication) to add new games, 
* edit existing games and filter by Calendar Week.
* 
*/ 
--%>

<asp:Content ID="GamesPageContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="interior-page gametracker-page" id="games-page">
        <div class="container">
            <h5 runat="server" id="test"></h5>
            
            <form runat="server">
                <div class="games-heading">
                    <asp:PlaceHolder runat="server" ID="PrivatePlaceHolder" Visible="false">
                        <div class="new-game">
                            <asp:HyperLink runat="server" ID="AddMatchButton" Text="<i class='fa fa-plus-circle'></i> New" NavigateUrl="~/Admin/GamesAdd.aspx"></asp:HyperLink>
                        </div>
                    </asp:PlaceHolder>                        
                    <div class="pagination">
                        <asp:LinkButton runat="server" CSSClass="prev" OnClick="PreviousButton_Click" Text="<i class='fa fa-chevron-left'></i>" 
                            PostBackUrl="~/Games.aspx"></asp:LinkButton>
                        <asp:LinkButton runat="server" CssClass="next" OnClick="NextButton_Click" Text="<i class='fa fa-chevron-right'></i>" 
                            PostBackUrl="~/Games.aspx"></asp:LinkButton>
                    </div>
                    <div class="calender">
                        <div class="calendar-trigger" runat="server" id="CalendarValue">Week of: mm/dd/yyyy</div>
                        <asp:Calendar runat="server" ID="GameCalendar" CssClass="GameCalendar" OnSelectionChanged="GameCalendar_SelectionChanged" 
                            SelectionMode="Day" SelectedDayStyle-CssClass="SelectedWeek"></asp:Calendar>                        
                    </div>                            
                </div>

                <asp:ListView runat="server" ID="GamesListView" DataKeyNames="MatchID" 
                    OnItemEditing="GamesListView_ItemEditing" OnItemUpdated="GamesListView_ItemUpdated">
                    <LayoutTemplate>
                        <div class="games-container">
                            <div runat="server" id="itemPlaceholder"></div>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="game-container" id="<%# "match" + Eval("MatchID") %>">
                            <asp:LinkButton runat="server" ID="EditMatchLink" CssClass="editMatch" Text="<i class='fa fa-pencil'></i>"
                                PostBackUrl='<%# "~/Games.aspx?matchID=" + Eval("MatchID") + "&itemID=" + Container.DataItemIndex + "#match" + Eval("MatchID") %>'></asp:LinkButton>
                            <div class="game-content">
                                <div class="game-background <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "SportName")).Trim().ToLower().Replace(" ", "-") %>"></div>
                                <div class="date"><%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateTime")).ToString("dddd, MMM d @ hh:mmtt") %></div>
                                <div class="teams">
                                    <div class="team left">
                                        <div class="logo"><%# DataBinder.Eval(Container.DataItem, "HomeTeamLogo") %></div>
                                        <div class="team-name"><%# DataBinder.Eval(Container.DataItem, "HomeTeamName") %></div>
                                    </div>
                                    <div class="team right">
                                        <div class="logo"><%# DataBinder.Eval(Container.DataItem, "AwayTeamLogo") %></div>
                                        <div class="team-name"><%# DataBinder.Eval(Container.DataItem, "AwayTeamName") %></div>
                                    </div>
                                    <div class="clear-float"></div>
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
                                    <div class="clear-float"></div>       
                                    <div class="spectators"><%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SpecCount")).ToString("N0") %> Spectators</div>
                                    <div class="hidden MatchID"><%# DataBinder.Eval(Container.DataItem, "MatchID") %></div>
                                    <div class="hidden HomeTeamID"><%# DataBinder.Eval(Container.DataItem, "HomeTeamID") %></div>
                                    <div class="hidden AwayTeamID"><%# DataBinder.Eval(Container.DataItem, "AwayTeamID") %></div>
                                </div>                            
                            </div>

                            <div class="game-edit" runat="server" id="EditTemplate">
                                <div class="home-team col-3">
                                    <div class="input-container teamLogo">
                                        <asp:FileUpload runat="server" ID="HomeTeamLogoUpload" Enabled="false" />
                                        <asp:RegularExpressionValidator runat="server" ID="HomeTeamLogoValidator" ControlToValidate="HomeTeamLogoUpload"
                                            ErrorMessage="Only .jpg, .jpeg & .png formats are allowed" 
                                            ValidationExpression="(.+\.([Jj][Pp][Gg])|.+\.([Pp][Nn][Gg])|.+\.([Jj][Pp][Ee][Gg])|.+\.([Jj][Pp][Ee][Gg]))"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="input-container team">
                                        <asp:DropDownList runat="server" ID="HomeTeamDropDownList" DataValueField="TeamID" DataTextField="Name"></asp:DropDownList>
                                    </div>
                                    <div class="input-container teamScore">
                                        <asp:TextBox runat="server" ID="HomeTeamScoreTextBox" TextMode="Number" Placeholder="Home Team Score"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="match col-3">
                                    <div class="input-container matchType">
                                        <asp:DropDownList runat="server" ID="MatchTypeDropDownList" DataValueField="SportID" DataTextField="Name"></asp:DropDownList>
                                    </div>
                                    <div class="input-container matchName">
                                        <asp:TextBox runat="server" ID="MatchNameTextBox" MaxLength="100" Placeholder="Name"></asp:TextBox>
                                    </div>
                                    <div class="input-container matchDate">
                                        <asp:TextBox runat="server" ID="MatchDateTextBox" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="input-container matchTime">                                        
                                        <asp:TextBox runat="server" ID="MatchTimeTextBox" TextMode="Time"></asp:TextBox>
                                    </div>
                                    <div class="input-container matchWinner">
                                        <asp:DropDownList runat="server" ID="MatchWinnerDropDownList" DataValueField="TeamID" DataTextField="Name"></asp:DropDownList>
                                    </div>
                                    <div class="input-container matchSpecCount">
                                        <asp:TextBox runat="server" ID="MatchSpecCountTextBox" TextMode="Number" Placeholder="Spec Count"></asp:TextBox>
                                    </div>
                                    <div class="input-container buttons">
                                        <asp:Button runat="server" ID="EditMatchCancel" CausesValidation="false" CommandName="Cancel" Text="Cancel" 
                                            OnClick="EditMatchCancel_Click" PostBackUrl="~/Games.aspx"/>
                                        <asp:Button runat="server" ID="EditMatchUpdate" CausesValidation="true" CommandName="Update" Text="Update" 
                                            OnClick="EditMatchUpdate_Click" PostBackUrl="~/Games.aspx"/>
                                    </div>
                                </div>
                                <div class="away-team col-3">                                    
                                    <div class="input-container teamLogo">
                                        <asp:FileUpload runat="server" ID="AwayTeamLogoUpload" Enabled="false" />
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="HomeTeamLogoUpload"
                                            ErrorMessage="Only .jpg, .jpeg & .png formats are allowed" 
                                            ValidationExpression="(.+\.([Jj][Pp][Gg])|.+\.([Pp][Nn][Gg])|.+\.([Jj][Pp][Ee][Gg])|.+\.([Jj][Pp][Ee][Gg]))"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="input-container team">
                                        <asp:DropDownList runat="server" ID="AwayTeamDropDownList" DataValueField="TeamID" DataTextField="Name"></asp:DropDownList>
                                    </div>
                                    <div class="input-container teamScore">
                                        <asp:TextBox runat="server" ID="AwayTeamScoreTextBox" TextMode="Number" Placeholder="Away Team Score"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="clear-float"></div>
                            </div>

                        </div>
                    </ItemTemplate>
                </asp:ListView>

            </form>
        </div>
    </div>

</asp:Content>
