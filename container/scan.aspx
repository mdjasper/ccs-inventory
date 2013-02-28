<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="scan.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-container.png" /> Container
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Scan or Enter Bin ID</h1>
    <asp:Label ID="lblError" runat="server" ForeColor="Red" />
    <asp:TextBox ID="txtValue" runat="server" CssClass="scanner" type="number" ></asp:TextBox>
    <table class="buttonGroup cols-2">
        <tr>
            <td><a href="../default.aspx" class="cancel">Back</a></td>
            <td><asp:Button ID="btnSubmit" CssClass="submit fullWidth scanSubmit" runat="server" Text="Lookup Bin" OnClick="btnSubmit_Click" /></td>
        </tr>
    </table>
</asp:Content>