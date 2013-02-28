<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="AddUSDACategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    Add USDA Category
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Add a New USDA Category</h1>
    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
    <table class="form">
        <tr>
            <td>Description:</td>
            <td><asp:TextBox ID="txtUSDADescription" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>USDA Category Number:</td>
            <td><asp:TextBox ID="txtUSDANumber" runat="server"></asp:TextBox></td> 
        </tr>
    </table>

     <table class="buttonGroup cols-2">
        <tr>
            <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="cancel"  Width="100%" OnClick="btnCancel_Click" /></td>
            <td><asp:LinkButton ID="btnAddUSDATypeSubmit" runat="server" Text="Save" CssClass="submit confirm" OnClick="btnAddUSDATypeSubmit_Click" /></td>
        </tr>
    </table>

</asp:Content>

