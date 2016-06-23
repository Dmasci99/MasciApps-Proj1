<%@ Page Title="Register" Language="C#" MasterPageFile="~/Interior.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MasciApps_Proj1.Register" %>
<%-- 
/**
* Author : Daniel Masci - 200299037
* Class : Enterprise Computing
* Semester : 4
* Professor : Tom Tsiliopolous
* Purpose : Project 1 - Game Tracker
* Website Name : MasciApps-GameTracker.azurewebsites.net/Register.aspx
* 
* The Following page is the Content for the "Register" page.
* Allows an anonymous user to Register. 
* Logged-in users are redirected to their Profile.
*
*/ 
--%>

<asp:Content ID="RegisterPageContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="interior-page forms-page" id="register-page">
        <div class="container">

            <form runat="server">
                <h2>Register</h2>
                <div runat="server" id="ErrorContainer" class="error-container" visible="false">
                    <asp:Label runat="server" ID="ErrorLabel"></asp:Label>
                </div>
                <div class="cancel">
                    <asp:LinkButton runat="server" ID="RegisterCancelButton" OnClick="RegisterCancelButton_Click"><i class="fa fa-times"></i></asp:LinkButton>
                </div>
                <div class="input-container">
                    <asp:TextBox runat="server" ID="UsernameTextBox" Placeholder="Username" TextMode="SingleLine" required="true" TabIndex="1"/>
                </div>
                <div class="input-container">
                    <asp:TextBox runat="server" ID="PasswordTextBox" Placeholder="Password" TextMode="Password" required="true" TabIndex="2"/>
                </div>                
                <div class="input-container">
                    <asp:TextBox runat="server" ID="ConfirmPasswordTextBox" Placeholder="Confirm Password" TextMode="Password" required="true" TabIndex="3"/>   
                    <asp:CompareValidator runat="server" ID="PasswordCompareValidator" ErrorMessage="Your passwords must match."
                        ControlToValidate="ConfirmPasswordTextBox" ControlToCompare="PasswordTextBox"></asp:CompareValidator>
                </div>                
                <div class="input-container">
                    <asp:TextBox runat="server" ID="EmailTextBox" Placeholder="Email Address" TextMode="Email" required="true" TabIndex="6"/>
                </div>                
                <div class="input-container">
                    <asp:TextBox runat="server" ID="PhoneTextBox" Placeholder="Phone#" TextMode="Phone" TabIndex="6"/>
                </div>                
				<div class="submit">
                    <asp:Button runat="server" ID="RegisterSubmitButton" Text="Register" CausesValidation="true" OnClick="RegisterSubmitButton_Click" />
				</div>
            </form>

        </div>
    </div>

</asp:Content>
