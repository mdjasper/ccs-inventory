<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="menu.aspx.cs" Inherits="container_menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-container.png" /> Container Menu
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">

    <h1>Container <asp:Label ID="lblBinNumber" runat="server" Text=""></asp:Label></h1>
    <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error"></asp:Label>
    <ul class="vertical-list">
        <li>
            <asp:HyperLink runat="server" ID="btnEdit" CssClass="button"><img src="../images/icon-container.png" /> Edit This Container</asp:HyperLink>
        </li>
        <li>
            <% if(Config.MOBILE()){ %>
                <a href="javascript:void(0)" class="button">Printing Disabled on Tablet</a>
            <% } else { %>
                <asp:HyperLink runat="server" ID="btnPrint" CssClass="button"><img src="../images/icon-print.png" />Print a Barcode Label</asp:HyperLink>
            <% } %>
        </li>
        <li>
            <asp:HyperLink runat="server" ID="btnMoveOut" CssClass="button"> &rarr; Move Out</asp:HyperLink>
        </li>
    </ul>
    <table class="buttonGroup">
        <tr><td>
            <a href="scan.aspx" class="cancel">Cancel</a>
            </td></tr>
    </table>
</asp:Content>

