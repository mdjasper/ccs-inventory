<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    Whoops!
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <center>
        <h1 style="color:#0094ff">Whoops!</h1>
        <p><h2>An error has occured.</h2></p>
        <p><em>It has been logged in the system for improvement.</em></p>
        <!--<p>The error occured on: <asp:Label ID="lblErrorPath" runat="server" Text=""></asp:Label></p>-->
    </center>
</asp:Content>

