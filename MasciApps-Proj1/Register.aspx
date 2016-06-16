<%@ Page Title="Register" Language="C#" MasterPageFile="~/Interior.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MasciApps_Proj1.Register" %>

<asp:Content ID="RegisterPageContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="interior-page" id="register-page">
        <div class="container">

            <form runat="server">
                <h2>Register</h2>
                <div class="input-container">
                    <asp:TextBox runat="server" ID="UsernameTextBox" Placeholder="Username" />
                </div>
                <div class="input-container">
                    <asp:TextBox runat="server" ID="PasswordTextBox" Placeholder="Password" />
                </div>                
                <div class="input-container">
                    <asp:TextBox runat="server" ID="ConfirmPasswordTextBox" Placeholder="Confirm Password" />
                </div>                
                <div class="input-container">
                    <asp:TextBox runat="server" ID="FirstNameTextBox" Placeholder="First Name" />
                </div>                
                <div class="input-container">
                    <asp:TextBox runat="server" ID="LastNameTextBox" Placeholder="Last Name" />
                </div>                
                <div class="input-container">
                    <asp:TextBox runat="server" ID="EmailTextBox" Placeholder="Email Address" />
                </div>                
				<div class="submit">
                    <asp:Button runat="server" CssClass="" ID="RegisterSubmitButton" Text="Send" CausesValidation="true" OnClick="RegisterSubmitButton_Click" />
				</div>
            </form>

        </div>
    </div>

</asp:Content>
