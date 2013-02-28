<%@ Page Title="" Language="C#" MasterPageFile="~/desktop/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="desktop_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    Report Type
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <style>
        #reports
        {
            width:300px;
            margin: 0 auto;
        }
    </style>
    <h1>Please Select a Report Type</h1>
    <div id="reports">
        <ul class="vertical-list">
         <li><a class="button text-center" href="shared/choosetemplate.aspx?reportType=<% Response.Write((int)ReportTemplate.ReportType.Incoming); %>">Incoming</a></li>
         <li><a class="button text-center" href="shared/choosetemplate.aspx?reportType=<% Response.Write((int)ReportTemplate.ReportType.Outgoing); %>">Outgoing</a></li>
         <li><a class="button text-center" href="shared/choosetemplate.aspx?reportType=<% Response.Write((int)ReportTemplate.ReportType.InOut); %>">In\Out</a></li>
         <li><a class="button text-center" href="shared/choosetemplate.aspx?reportType=<% Response.Write((int)ReportTemplate.ReportType.Inventory); %>">Inventory</a></li>
         <li><a class="button text-center" href="shared/choosetemplate.aspx?reportType=<% Response.Write((int)ReportTemplate.ReportType.GroceryRescue); %>">Grocery Rescue</a></li>
        </ul>
    </div>
</asp:Content>

