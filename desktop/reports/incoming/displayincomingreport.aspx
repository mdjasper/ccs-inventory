﻿<%@ Page Title="" Language="C#" MasterPageFile="~/desktop/MasterPage.master" AutoEventWireup="true" CodeFile="displayincomingreport.aspx.cs" Inherits="desktop_reports_incoming_DisplayIncomingReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
        <style>
        .center
        {
            display:block;
            margin: 0px auto;
            border: 1px solid black;
        }
    </style>
       <asp:scriptmanager ID="Scriptmanager1" runat="server"></asp:scriptmanager>
        <h1>Grocery Rescue Report</h1>
        <rsweb:ReportViewer  CssClass="center" Width="1000px" Height="1000px" ID="reportViewer" ShowPrintButton="true" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="desktop\reports\incoming\incomingreport.rdlc">
        </LocalReport>
        </rsweb:ReportViewer>
</asp:Content>

