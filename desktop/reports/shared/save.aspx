<%@ Page Title="" Language="C#" MasterPageFile="~/desktop/MasterPage.master" AutoEventWireup="true" CodeFile="save.aspx.cs" Inherits="desktop_reports_incoming_Default" %>

<%@ Register Src="~/NumericKeypad.ascx" TagPrefix="uc1" TagName="NumericKeypad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    &nbsp;
    Save Template
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Save as Template?</h1>
    <div id="Title">
       <asp:Label ID="lblQuest" runat="server" Text="Would you like to save this report as a template for later use?"></asp:Label>
</div>

<style>
   #Title {text-align:center; margin: 0px auto;}
</style>
    
    <br />
    <br />
    <div id="TempletInput">
       <asp:Label ID="Label1" runat="server" Text="Template Name" Font-Size="11pt"></asp:Label>
&nbsp;<asp:TextBox ID="txtTitle" runat="server" Height="22px" Width="751px"></asp:TextBox>
        <br />
</div>

<style>
   #TempletInput {text-align:center; margin: 0px auto;}
</style>
        <div id="Submit">
    <asp:Button ID="btnSave" runat="server" CssClass="submit" Text="Save" OnClick="btnNext_Click" />
</div>
<style>
   #Submit {text-align:center; margin: 0px auto;}
</style>
    <br />
    
    <br />
    <div id="response">
        <asp:Label ID="lblResponse" runat="server" ForeColor="Green" />
    </div>
     <style>
        #response{ text-align:center; margin: 0px auto;}
    </style>
    <br />
    <div id="Button">
    <asp:Button ID="btnBack" CssClass="cancel" runat="server" Text="Back" OnClientClick="JavaScript: window.history.go(-1); return false;"/>
         &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:Button ID="btnNext" runat="server" CssClass="submit" Text="Next" OnClick="btnNext_Click" />
     </div>
    <style>
        #Button{ text-align:center; margin: 0px auto;}
        .submit
        {}
    </style>
</asp:Content>


