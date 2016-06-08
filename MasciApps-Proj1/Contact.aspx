<%@ Page Title="Contact Me" Language="C#" MasterPageFile="~/Interior.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="MasciApps_Proj1.Contact" %>
<%-- 
/**
* Author : Daniel Masci - 200299037
* Class : Enterprise Computing
* Semester : 4
* Professor : Tom Tsiliopolous
* Purpose : Project 1 - Game Tracker
* Website Name : DanMasci.azurewebsites.net/Contact.aspx
* 
* The Following page is the Content for the "Contact" page.
*/ 
--%>
<asp:Content ID="ContactPageContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="interior-page" id="contact-page">

	    <div class="content container">
		    <div class="left">
			    <div class="text">
				    <p>I would love for you to Contact Me! Please fill out the info below and I will get back to you asap!</p>
			    </div><!-- text -->
                <form runat="server">
				    <div class="input-container left-half">
					    <asp:TextBox runat="server" CssClass="input" ID="FullNameTextBox" Placeholder="Full Name:"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="form-error" ID="FullNameValidator" runat="server" 
                            ErrorMessage="Full Name is required" ControlToValidate="FullNameTextBox"></asp:RequiredFieldValidator>
				    </div><!-- input -->
				    <div class="input-container right-half">
					    <asp:TextBox runat="server" CssClass="input" ID="EmailAddressTextBox" Placeholder="Email Address:"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="form-error" ID="EmailAddressValidator" runat="server" 
                            ErrorMessage="Email Address is required" ControlToValidate="EmailAddressTextBox"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="form-error" runat="server" ID="EmailRegexValidator" ControlToValidate="EmailAddressTextBox"
                            ErrorMessage="Must enter a valid email type" ValidationExpression=".{1,}@.{1,}\.[a-z]{2,8}"></asp:RegularExpressionValidator>
				    </div><!-- input -->
				    <div class="input-container">
					    <asp:TextBox runat="server" CssClass="input" ID="SubjectTextBox" Placeholder="Subject:"></asp:TextBox>
				    </div><!-- input -->
				    <div class="input-container textarea">
					    <asp:TextBox runat="server" CssClass="input" ID="MessageTextBox" Placeholder="Type your message here..." TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" CssClass="form-error" ID="MessageValidator" runat="server" 
                            ErrorMessage="Message is required" ControlToValidate="MessageTextBox"></asp:RequiredFieldValidator>
				    </div><!-- input -->
				    <div class="submit">
                        <asp:Button runat="server" CssClass="" ID="ContactSubmitButton" Text="Send" CausesValidation="true" OnClick="ContactSubmitButton_Click" 
                             />
				    </div><!-- submit -->
			    </form>
		    </div><!-- left -->
		    <div class="divider">
			    <div class="social">
				    <a href="https://ca.linkedin.com/hp/"><i class="fa fa-linkedin"></i></a>
				    <a href="https://www.facebook.com/"><i class="fa fa-facebook"></i></a>
				    <a href="https://www.youtube.com/"><i class="fa fa-youtube"></i></a>				
			    </div><!-- social -->
		    </div>
		    <div class="clear-float"></div><!-- clear-float -->
	    </div><!-- container -->

    </div><!-- #contact-page -->

</asp:Content>

