<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    Edit Location
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Edit Location</h1>
    <table class="form">
        <tr>
            <td>Location Id:</td>
            <td><asp:TextBox runat="server" ID="txtLocationId" Enabled="false"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Location:</td>
            <td><asp:TextBox runat="server" ID="txtLocation"></asp:TextBox></td>
        </tr>
    </table>

    <ul class="vertical-list">
        <li><asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="submit" OnClick="btnSave_Click" /></li>
        <li><asp:LinkButton ID="btnCancel" runat="server" Text="Remove" CssClass="cancel" OnClick="btnCancel_Click" /></li>
    </ul>
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
</asp:Content>

