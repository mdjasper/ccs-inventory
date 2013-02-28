<%@ Page Title="" Language="C#" MasterPageFile="~/desktop/MasterPage.master" AutoEventWireup="true" CodeFile="selectdates.aspx.cs" Inherits="desktop_reports_incoming_SelectDates" %>

<%@ Register Src="~/NumericKeypad.ascx" TagPrefix="uc1" TagName="NumericKeypad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    &nbsp; Select Date 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Select Date</h1>
    <div id="Title">
       <asp:Label ID="lblQuest" runat="server" Text="Please Select the Dates you would like to run the report for:"></asp:Label>
</div>
    
<style>
   #Title {text-align:center; margin: 0px auto;}
</style>

    <div id="DateLable">
       
        
    <br />
</div>
    <style>
   #DateLable {text-align:center; margin: 0px auto;}
</style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>
    <table id="TableCenter">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Start Date"></asp:Label>
                <asp:Calendar ID="calStart" runat="server" CssClass="inline" />
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="End Date"></asp:Label>
                <asp:Calendar ID="calEnd" runat="server" />
            </td>
        </tr>
        <style>
    #Calendar{ text-align:center; margin: 0px auto; }
    </style>
    </table>
    </ContentTemplate>
</asp:UpdatePanel>

    <style>
    #TableCenter{ text-align:center;margin-left: auto;margin-right: auto;}
    </style>

    <br />
    <asp:Label ID="lblError" runat="server" ForeColor="Red" />
    <br />

    <div id="Button">
    <asp:Button ID="btnBack" CssClass="cancel" runat="server" Text="Back" OnClientClick="JavaScript: window.history.go(-1); return false;"/>
         &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:Button ID="btnNext" runat="server" CssClass="submit" Text="Next" OnClick="btnNext_Click" />
     </div>
    <style>
        #Button{ text-align:center; margin: 0px auto;}
    </style>
</asp:Content>




