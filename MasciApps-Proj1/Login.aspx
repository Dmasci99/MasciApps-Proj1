<%@ Page Title="Login" Language="C#" MasterPageFile="~/Interior.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MasciApps_Proj1.Login" %>
<%-- 
/**
* Author : Daniel Masci - 200299037
* Class : Enterprise Computing
* Semester : 4
* Professor : Tom Tsiliopolous
* Purpose : Project 1 - Game Tracker
* Website Name : MasciApps-GameTracker.azurewebsites.net/About.aspx
* 
* The Following page is the Login Page. 
* Users login to access Game Tracker Administrative rights.
*/ 
--%>

<asp:Content ID="LoginPageContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="interior-page" id="login-page">
        <div class="container">

            <form runat="server">
                <h2>Login</h2>
                <div class="error-container">
                    <span runat="server" id="Error">Testing error message</span>
                </div>
                <div class="cancel">
                    <asp:LinkButton runat="server" ID="LoginCancelButton" OnClick="LoginCancelButton_Click"><i class="fa fa-times"></i></asp:LinkButton>
                </div>                
                <div class="input-container">
                    <asp:TextBox runat="server" ID="UsernameTextBox" Placeholder="Username" TextMode="SingleLine" required="true" TabIndex="1"/>
                </div>
                <div class="input-container">
                    <asp:TextBox runat="server" ID="PasswordTextBox" Placeholder="Password" TextMode="Password" required="true" TabIndex="2"/>
                </div>                
				<div class="submit">
                    <asp:Button runat="server" CssClass="" ID="LoginSubmitButton" Text="Login" CausesValidation="true" OnClick="LoginSubmitButton_Click" />
				</div>
            </form>

        </div>
    </div>

</asp:Content>
