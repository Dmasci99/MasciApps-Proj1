<%@ Page Title="Register" Language="C#" MasterPageFile="~/Interior.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MasciApps_Proj1.Register" %>

<asp:Content ID="RegisterPageContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="interior-page" id="register-page">
        <div class="container">

            <form runat="server">
                <h2>Register</h2>
                <div class="error-container">
                    <div class="error" runat="server" id="Error"></div>
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
                    <asp:TextBox runat="server" ID="FirstNameTextBox" Placeholder="First Name" TextMode="SingleLine" required="true" TabIndex="4"/>
                </div>                
                <div class="input-container">
                    <asp:TextBox runat="server" ID="LastNameTextBox" Placeholder="Last Name" TextMode="SingleLine" required="true" TabIndex="5"/>
                </div>                
                <div class="input-container">
                    <asp:TextBox runat="server" ID="EmailTextBox" Placeholder="Email Address" TextMode="Email" required="true" TabIndex="6"/>
                </div>                
				<div class="submit">
                    <asp:Button runat="server" CssClass="" ID="RegisterSubmitButton" Text="Register" CausesValidation="true" OnClick="RegisterSubmitButton_Click" />
				</div>
            </form>

        </div>
    </div>

</asp:Content>
