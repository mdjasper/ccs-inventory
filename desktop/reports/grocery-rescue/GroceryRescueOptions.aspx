<%@ Page Title="" Language="C#" MasterPageFile="~/desktop/MasterPage.master" AutoEventWireup="true" CodeFile="groceryrescueoptions.aspx.cs" Inherits="desktop_reports_incoming_GroceryRescueOptions" %>

<%@ Register Src="~/NumericKeypad.ascx" TagPrefix="uc1" TagName="NumericKeypad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    &nbsp;Grocery Rescue
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Grocery Rescue Report Options</h1>
    <div id="Title">
       <asp:Label ID="lblQuest" runat="server" Text="Please fill out the fields that will appear on the report"></asp:Label>
</div>

<style>
   #Title {text-align:center; margin: 0px auto;}
</style>
    
    <br />
    <div id="InputForm">


        <table align="center" class="auto-style1" style="width: 30%">
            <tr>
                <td><asp:Label ID="lblAgency" runat="server" Text="Agency:"></asp:Label>
                <asp:TextBox ID="txtAgency" runat="server"  Width="166px"></asp:TextBox></td>
                <td><asp:Label ID="lblPhone" runat="server" Text="Phone:"></asp:Label>
                <asp:TextBox ID="txtPhone" runat="server" Width="166px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblContact" runat="server" Text="Contact:"></asp:Label>
                     <asp:TextBox ID="txtContact" runat="server" Width="166px"></asp:TextBox></td>
                <td>    <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                        &nbsp;
                        <asp:TextBox ID="txtEmail" runat="server" Width="166px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>    <asp:Label ID="lblMC" runat="server" Text="MC#:"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtMC" runat="server" Width="166px"></asp:TextBox></td>
                <td><asp:Label ID="lblRe" runat="server" Text="RE#:"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtRE" runat="server" Width="166px"></asp:TextBox></td>
            </tr>
        </table>
    <br />
    <asp:Label ID="lblAdditional" runat="server" Text="Additional Comments"></asp:Label>
    <br />
    <asp:TextBox TextMode=MultiLine  ID="txtComment" runat="server" Height="142px" Width="553px"></asp:TextBox>
        <br />
        <asp:Label ID="lblError" runat="server" ForeColor="Red" />
    </div>
    <style>
   #InputForm {text-align:center; margin: 0px auto;}
</style>
    <br />
    
    <br />
    <div id="Button">
    <asp:Button ID="btnBack" CssClass="cancel" runat="server" Text="Back"  OnClientClick="JavaScript: window.history.go(-1); return false;" />
         &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:Button ID="btnNext" runat="server" CssClass="submit" Text="Next" OnClick="btnNext_Click" />
     </div>
    <style>
        #Button{ text-align:center; margin: 0px auto;}
        .auto-style1
        {
            width: 100%;
        }
    </style>
</asp:Content>



