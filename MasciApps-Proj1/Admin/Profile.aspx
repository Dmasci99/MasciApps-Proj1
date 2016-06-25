<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Interior.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="MasciApps_Proj1.Admin.Profile" %>
<%--
/**
* Author : Daniel Masci - 200299037
* Class : Enterprise Computing
* Semester : 4
* Professor : Tom Tsiliopolous
* Purpose : Project 1 - Game Tracker
* Website Name : MasciApps-GameTracker.azurewebsites.net/Admin/Profile.aspx

* The Following page is the Form for the "Profile" page.
* Allows a registered user to edit their existing profile.
*/ 
--%>

<asp:Content ID="ProfilePageContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="interior-page forms-page" id="profile-page">
        <div class="container">

            <form runat="server">
                <h2>Profile</h2>
                <div runat="server" id="ErrorContainer" class="error-container" visible="false">
                    <asp:Label runat="server" ID="ErrorLabel"></asp:Label>
                </div>
                <div class="cancel">
                    <asp:LinkButton runat="server" ID="ProfileCancelButton" OnClick="ProfileCancelButton_Click"><i class="fa fa-times"></i></asp:LinkButton>
                </div>
                <div class="input-container">
                    <asp:TextBox runat="server" ID="UsernameTextBox" Placeholder="Username" Enabled="false" required="true" TabIndex="0"/>
                </div>  
                <div class="input-container">
                    <asp:TextBox runat="server" ID="EmailTextBox" Placeholder="Email Address" TextMode="Email" required="true" TabIndex="1"/>
                </div>                
                <div class="input-container">
                    <asp:TextBox runat="server" ID="PhoneTextBox" Placeholder="Phone#" TextMode="Phone" TabIndex="2"/>
                </div>                
				<div class="submit">
                    <asp:Button runat="server" ID="ProfileSubmitButton" Text="Update" CausesValidation="true" OnClick="ProfileSubmitButton_Click" />
				</div>
            </form>

        </div>
    </div>

</asp:Content>
