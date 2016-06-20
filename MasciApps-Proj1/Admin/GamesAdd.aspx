<%@ Page Title="Add Game" Language="C#" MasterPageFile="~/Interior.Master" AutoEventWireup="true" CodeBehind="GamesAdd.aspx.cs" Inherits="MasciApps_Proj1.Admin.GamesAdd" %>

<asp:Content ID="AddGamePageContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="interior-page forms-page gametracker-page" id="gamesadd-page">
        <div class="container">

            <form runat="server" class="wide">
                <!-- STEP 1: Choose Match Type (Sport) -->
                <h3 class="step" id="step1">Step 1 - Match</h3>
                <div class="input-container matchType">
                    <asp:DropDownList runat="server" ID="MatchTypeDropDownList" DataValueField="SportID" DataTextField="Name"
                        OnSelectedIndexChanged="PopulateTeams" AutoPostBack="true"></asp:DropDownList>
                </div>

                <!-- STEP 2: Choose Match Teams (Home / Away) -->
                <h3 class="step" id="step2">Step 2 - Teams</h3>
                <div class="input-container team">
                    <asp:DropDownList runat="server" ID="HomeTeamDropDownList" DataValueField="TeamID" DataTextField="Name"
                        OnSelectedIndexChanged="PopulateMatchWinner" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="input-container teamScore">
                    <asp:TextBox runat="server" ID="HomeTeamScoreTextBox" TextMode="Number" Placeholder="Home Team Score"></asp:TextBox>
                </div>
                <div class="input-container team">
                    <asp:DropDownList runat="server" ID="AwayTeamDropDownList" DataValueField="TeamID" DataTextField="Name"
                        OnSelectedIndexChanged="PopulateMatchWinner" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="input-container teamScore">
                    <asp:TextBox runat="server" ID="AwayTeamScoreTextBox" TextMode="Number" Placeholder="Away Team Score"></asp:TextBox>
                </div>

                <!-- STEP 3: Set Match Details -->
                <h3 class="step" id="step3">Step 3 - Details</h3>                            
                <div class="input-container matchWinner">
                    <asp:DropDownList runat="server" ID="MatchWinnerDropDownList" DataValueField="TeamID" DataTextField="Name"></asp:DropDownList>
                </div>
                <div class="input-container matchName">
                    <asp:TextBox runat="server" ID="MatchNameTextBox" MaxLength="100" Placeholder="Name" Enabled="false"></asp:TextBox>
                </div>
                <div class="input-container matchDate">
                    <asp:TextBox runat="server" ID="MatchDateTextBox" TextMode="Date"></asp:TextBox>
                </div>
                <div class="input-container matchTime">                                        
                    <asp:TextBox runat="server" ID="MatchTimeTextBox" TextMode="Time"></asp:TextBox>
                </div>
                <div class="input-container matchSpecCount">
                    <asp:TextBox runat="server" ID="MatchSpecCountTextBox" TextMode="Number" Placeholder="Spec Count"></asp:TextBox>
                </div>                
                <div class="input-container buttons">
                    <asp:Button runat="server" ID="AddMatchCancel" CausesValidation="false" CommandName="Cancel" Text="Cancel" OnClick="AddMatchCancel_Click"/>
                    <asp:Button runat="server" ID="AddMatchSubmit" CausesValidation="true" CommandName="Save" Text="Save" OnClick="AddMatchSubmit_Click" />
                    <div class="clear-float"></div>
                </div>
            </form>

        </div>
    </div>

</asp:Content>
