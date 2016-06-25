<%@ Page Title="Add Team" Language="C#" MasterPageFile="~/Interior.Master" AutoEventWireup="true" CodeBehind="TeamsAdd.aspx.cs" Inherits="MasciApps_Proj1.Admin.TeamsAdd" %>
<%--
/**
* Author : Daniel Masci - 200299037
* Class : Enterprise Computing
* Semester : 4
* Professor : Tom Tsiliopolous
* Purpose : Project 1 - Game Tracker
* Website Name : MasciApps-GameTracker.azurewebsites.net/Admin/TeamsAdd.aspx

* The Following page is the Content for the "TeamsAdd" page.
* Allows the user to add a new Team.
*/ 
--%>

<asp:Content ID="AddTeamPageContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="interior-page forms-page gametracker-page" id="teamsadd-page">
        <div class="container">

            <form runat="server" class="wide">   
                <div class="navigation special">
                    <a href="/Admin/TeamsAdd.aspx" class="option active">New</a>
                    <a href="/Admin/TeamsEdit.aspx" class="option">Edit</a>
                    <div class="clear-float"></div>
                </div>             
                <div runat="server" id="ErrorContainer" class="error-container" visible="false">
                    <asp:Label runat="server" ID="ErrorLabel"></asp:Label>
                </div>            
                
                <h3 class="step" id="step1">New Team</h3>
                <div class="input-container matchType">
                    <asp:DropDownList runat="server" ID="MatchTypeDropDownList" DataValueField="SportID" DataTextField="Name" TabIndex="1"></asp:DropDownList>
                </div>
                <div class="input-container teamName">
                    <asp:TextBox runat="server" ID="TeamNameTextBox" Placeholder="Team Name" TabIndex="2" Required="true"></asp:TextBox>
                </div>
                <div class="input-container country">
                    <asp:TextBox runat="server" ID="CountryTextBox" Placeholder="Country" TabIndex="3" Required="true"></asp:TextBox>
                </div>
                <div class="input-container city">
                    <asp:TextBox runat="server" ID="CityTextBox" Placeholder="City" TabIndex="4" Required="true"></asp:TextBox>
                </div>
                <div class="input-container buttons">
                    <asp:Button runat="server" ID="AddTeamCancel" CausesValidation="false" CommandName="Cancel" Text="Cancel" OnClick="AddTeamCancel_Click"/>
                    <asp:Button runat="server" ID="AddTeamSave" CausesValidation="true" CommandName="Save" Text="Save" OnClick="AddTeamSave_Click"/>
                    <div class="clear-float"></div>
                </div>
            </form>

        </div>
    </div>

</asp:Content>
