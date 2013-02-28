<%@ Page Title="" Language="C#" MasterPageFile="~/desktop/MasterPage.master" AutoEventWireup="true" CodeFile="locations.aspx.cs" Inherits="desktop_reports_incoming_Locations"  %>

<%@ Register Src="~/NumericKeypad.ascx" TagPrefix="uc1" TagName="NumericKeypad" %>

<%@ Register src="../shared/ListSelectionControl.ascx" tagname="ListSelectionControl" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    &nbsp;Locations 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <h1>Locations</h1>
    <div id="Title">
       <asp:Label ID="lblQuest" runat="server" Text="Please Select the Locations you would like to show up on the Report"></asp:Label>
</div>

<style>
   #Title {text-align:center; margin: 0px auto;}
</style>
    
    <br />
    <div id="InputForm">
    
        <br />
        <uc2:ListSelectionControl ID="lstlocations" runat="server" />
        <br />
    </div>
    <style>
   #InputForm {text-align:center; margin: 0px auto;}
</style>
    <asp:Label ID="lblError" runat="server" ForeColor="Red" />
    <br />
    <div id="Button">
    <asp:Button ID="btnBack" CssClass="cancel" runat="server" Text="Back" OnClientClick="JavaScript: window.history.go(-1); return false;" />
         &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:Button ID="btnNext" runat="server" CssClass="submit" Text="Next" OnClick="btnNext_Click" />
     </div>
    <style>
        #Button{ text-align:center; margin: 0px auto;}
    </style>
</asp:Content>



