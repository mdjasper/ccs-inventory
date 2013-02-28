<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/NumericKeypad.ascx" TagPrefix="uc1" TagName="NumericKeypad" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    Main Menu
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <ul class="vertical-list">
        <li><a href="container/scan.aspx" class="button text-left"><img src="images/icon-container.png" />Scan Container</a></li>
        <li><a href="container/locate.aspx" class="button text-left"><img src="images/icon-locate.png" />Locate Container</a></li>
        <li><% if(Config.MOBILE()){ %>
                <a href="javascript:void(0)" class="button"><img src="images/icon-category.png" />Add Incoming Food Disabled on Tablet</a>
            <% } else { %>
                <a href="incoming-food/add.aspx" class="button text-left"><img src="images/icon-category.png" />Add Incoming Food</a>
            <% } %>
             
        </li>
        <li><a href="advanced" class="button text-left"><img src="images/gear.gif" />Advanced Settings</a></li>
        <li><a href="desktop/home.aspx" class="button text-left"><img src="images/icon-desktop.png" />Desktop Portal</a></li>
    </ul>
</asp:Content>