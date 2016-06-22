﻿<%@ Page Title="Teams" Language="C#" MasterPageFile="~/Interior.Master" AutoEventWireup="true" CodeBehind="Teams.aspx.cs" Inherits="MasciApps_Proj1.Teams" %>
<%-- 
/**
* Author : Daniel Masci - 200299037
* Class : Enterprise Computing
* Semester : 4
* Professor : Tom Tsiliopolous
* Purpose : Project 1 - Game Tracker
* Website Name : MasciApps-GameTracker.azurewebsites.net/Teams.aspx
* 
* The Following page is the Content for the "Teams" page.
* Allows the user to view, add and edit Teams (depending on authentication).
*
*/ 
--%>

<asp:Content ID="TeamsPageContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="interior-page gametracker-page" id="teams-page">
        <div class="container">
            <h5 runat="server" id="test"></h5>
            
            <form runat="server">
                <div class="heading">
                    <asp:PlaceHolder runat="server" ID="PrivatePlaceHolder" Visible="false">
                        <div class="new-team">
                            <asp:HyperLink runat="server" ID="AddTeamButton" Text="<i class='fa fa-plus-circle'></i> New" NavigateUrl="~/Admin/TeamsAdd.aspx"></asp:HyperLink>
                        </div>
                    </asp:PlaceHolder>                        
                </div>

                <asp:ListView runat="server" ID="TeamsListView" DataKeyNames="TeamID">
                    <LayoutTemplate>
                        <div class="teams-container">
                            <div runat="server" id="itemPlaceholder"></div>
                            <div class="clear-float"></div>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="team-container" id="<%# "team" + Eval("TeamID") %>">   
                            
                            <asp:LinkButton runat="server" ID="EditTeamLink" CssClass="linkButton Edit" Text="<i class='fa fa-pencil'></i>" OnClick="EditTeamLink_Click"
                                PostBackUrl='<%# "~/Teams.aspx?teamID=" + Eval("TeamID") + "&itemID=" + Container.DataItemIndex + "#team" + Eval("TeamID") %>'></asp:LinkButton>                         
                            <asp:LinkButton runat="server" ID="DeleteTeamLink" CssClass="linkButton Delete" Visible="false" OnClick="DeleteTeamLink_Click"
                                Text="<i class='fa fa-trash-o'></i>" PostBackUrl='<%# "~/Teams.aspx?teamID=" + Eval("TeamID") %>'></asp:LinkButton>
                            <div class="team-background <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "SportName")).Trim().ToLower().Replace(" ", "-") %>"></div>
                            <asp:PlaceHolder runat="server" ID="ViewTemplate">
                                <div class="team-content">                                    
                                    <div class="name"><%# DataBinder.Eval(Container.DataItem, "TeamName") %></div>
                                    <div class="logo"></div>
                                    <div class="country"><%# DataBinder.Eval(Container.DataItem, "Country") %></div>
                                    <div class="city"><%# DataBinder.Eval(Container.DataItem, "City") %></div>
                                </div>                    
                            </asp:PlaceHolder>        
                            <asp:PlaceHolder runat="server" ID="EditTemplate" Visible="false">
                                <div class="team-content">
                                    <div class="input-container">
                                        <asp:DropDownList runat="server" ID="TeamTypeDropDownList" DataValueField="SportID" DataTextField="Name"></asp:DropDownList>
                                    </div>
                                    <div class="input-container">
                                        <asp:TextBox runat="server" ID="TeamNameTextBox" PlaceHolder="Team Name" Required="true"></asp:TextBox>
                                    </div>
                                    <div class="input-container">
                                        <asp:TextBox runat="server" ID="CountryTextBox" PlaceHolder="Country" Required="true"></asp:TextBox>
                                    </div>
                                    <div class="input-container">
                                        <asp:TextBox runat="server" ID="CityTextBox" PlaceHolder="City" Required="true"></asp:TextBox>
                                    </div>
                                    <div class="input-container buttons">                                        
                                        <asp:Button runat="server" ID="EditTeamCancel" CausesValidation="false" Text="Cancel" OnClick="EditTeamCancel_Click" 
                                            PostBackUrl='<%# "~/Teams.aspx?itemID=" + Container.DataItemIndex %>'/>
                                        <asp:Button runat="server" ID="EditTeamUpdate" CausesValidation="true" Text="Update" OnClick="EditTeamUpdate_Click" 
                                            PostBackUrl='<%# "~/Teams.aspx?itemID=" + Container.DataItemIndex %>' />
                                    </div>
                                </div>
                            </asp:PlaceHolder>
                        </div>
                    </ItemTemplate>
                </asp:ListView>

            </form>
        </div>
    </div>

</asp:Content>