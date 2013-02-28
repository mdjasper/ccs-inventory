<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="confirm.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-container.png" />Confirmation
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Save Changes?</h1>
    <table>
        <tr>
            <td>ID:</td><td><asp:Label ID="lblID" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Weight:</td><td><asp:Label ID="lblWeight" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Food Type:</td><td><asp:Label ID="lblType" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>Location:</td><td><asp:Label ID="lblLocation" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>

    <ul class="vertical-list">
        <li><asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="submit" OnClick="btnSave_Click" /></li>
        <li><a href="default.aspx" class="cancel">Cancel</a></li>
    </ul>
    
</asp:Content>

