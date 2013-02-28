<%@ Page Title="" Language="C#" MasterPageFile="~/desktop/MasterPage.master" AutoEventWireup="true" CodeFile="selectdate.aspx.cs" Inherits="desktop_reports_incoming_SelectDate" %>

<%@ Register Src="~/NumericKeypad.ascx" TagPrefix="uc1" TagName="NumericKeypad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    &nbsp; Select Date 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Select Date</h1>
        <div id="center">
            <br />
      <asp:Label ID="lblQuest" runat="server" Text="Please Select the Date you would like to run the report for:"></asp:Label>
</div><!-- end #center -->

<style>
   #center {text-align:center; margin: 0px auto;}
</style>
    
    <br />
            <div id="DivSeclectDate">
       <asp:Label ID="Label1" runat="server" Text="Select Date"></asp:Label>
</div>

<style>
   #DivSeclectDate {text-align:center; margin: 0px auto;}
</style>
   
<br />

<div id="Calender">
       <asp:Calendar ID="calStart" runat="server" CssClass="center-text"></asp:Calendar>
</div>
<style>
   #calStart {margin-left:auto;margin-right:auto; text-align:center; margin: 0px auto;}
</style>
    

    <br />
    <asp:Label ID="lblError" runat="server" ForeColor="Red" />
    <div id="Button">
    <asp:Button ID="btnBack" CssClass="cancel" runat="server" Text="Back" OnClientClick="JavaScript: window.history.go(-1); return false;"/>
         &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:Button ID="btnNext" runat="server" CssClass="submit" Text="Next" OnClick="btnNext_Click" />
     </div>
    <style>
        #Button{ text-align:center; margin: 0px auto;}
    </style>
</asp:Content>




