<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="locate.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-locate.png" /> Locate Container
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Locate containers containing:</h1>
    <%-- Databind ddlType to type table in database --%>
    <asp:DropDownList ID="ddlType" runat="server"   DataTextField="CategoryType" DataValueField ="FoodCategoryID">
        <asp:ListItem Value="1">Select Food Type</asp:ListItem>
    </asp:DropDownList>

    <table class="buttonGroup cols-2">
        <tr>
            <td><a href="../default.aspx" class="cancel">Cancel</a></td>
            <td><asp:LinkButton ID="btnSave" runat="server" Text="Find" CssClass="submit" OnClick="btnSave_Click"/></td>
        </tr>
    </table>
</asp:Content>

