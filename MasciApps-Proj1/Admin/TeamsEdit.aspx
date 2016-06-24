<%@ Page Title="Edit Team" Language="C#" MasterPageFile="~/Interior.Master" AutoEventWireup="true" CodeBehind="TeamsEdit.aspx.cs" Inherits="MasciApps_Proj1.Admin.TeamsEdit" %>

<asp:Content ID="EditTeamPageContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="interior-page forms-page gametracker-page" id="teamsadd-page">
        <div class="container">

            <form runat="server" class="wide">
                <div class="navigation special">
                    <a href="/Admin/TeamsAdd.aspx" class="option">New</a>
                    <a href="/Admin/TeamsEdit.aspx" class="option active">Edit</a>
                    <div class="clear-float"></div>
                </div>             
                <div runat="server" id="ErrorContainer" class="error-container" visible="false">
                    <asp:Label runat="server" ID="ErrorLabel"></asp:Label>
                </div>

                <h3 class="step" id="step1">Step 1 - Team</h3>
                <div class="input-container team">
                    <asp:DropDownList runat="server" ID="TeamDropDownList" DataValueField="TeamID" DataTextField="Name"
                        OnSelectedIndexChanged="PopulateDetails" AutoPostBack="true" TabIndex="1"></asp:DropDownList>                    
                    <asp:LinkButton runat="server" ID="EditTeamDelete" CssClass="Delete" CausesValidation="false" 
                        Text="<i class='fa fa-trash-o'></i>" OnClick="EditTeamDelete_Click"/>
                </div>
                
                <h3 class="step" id="step2">Step 2 - Details</h3>
                <div class="input-container teamType">
                    <asp:DropDownList runat="server" ID="TeamTypeDropDownList" DataValueField="SportID" DataTextField="Name" TabIndex="2"></asp:DropDownList>
                </div>
                <div class="input-container teamName">
                    <asp:TextBox runat="server" ID="TeamNameTextBox" Placeholder="Team Name" TabIndex="3" Required="true"></asp:TextBox>
                </div>
                <div class="input-container country">
                    <asp:TextBox runat="server" ID="CountryTextBox" Placeholder="Country" TabIndex="4" Required="true"></asp:TextBox>
                </div>
                <div class="input-container city">
                    <asp:TextBox runat="server" ID="CityTextBox" Placeholder="City" TabIndex="5" Required="true"></asp:TextBox>
                </div>
                <div class="input-container buttons">
                    <asp:Button runat="server" ID="EditTeamCancel" CausesValidation="false" CommandName="Cancel" Text="Cancel" OnClick="EditTeamCancel_Click"/>
                    <asp:Button runat="server" ID="EditTeamSave" CausesValidation="true" CommandName="Save" Text="Save" OnClick="EditTeamSave_Click"/>
                    <div class="clear-float"></div>
                </div>
            </form>

        </div>
    </div>

</asp:Content>
