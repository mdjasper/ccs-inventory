<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="FoodCategoryEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-category.png" />Edit Food Category
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Edit Food Category <!--<asp:Label runat="server" ID="lblID"></asp:Label>--></h1>
    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
    <table class="form">
        <tr>
            <td>Name:</td>
            <td><asp:TextBox runat="server" ID="txtName"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Perishable:</td>
            <td><asp:CheckBox ID="cbPerishable" CssClass="checkBox" runat="server" /></td>
        </tr>
        <tr>
            <td>Non Food:</td>
            <td><asp:CheckBox ID="cbNonFood" CssClass="checkBox" runat="server" /></td>
        </tr>
    </table>

    <table class="buttonGroup cols-3">
        <tr>
            <td><a href="default.aspx" ID="btnCancel" class="cancel">Cancel</a></td>
            <td><asp:LinkButton ID="btnDelete" runat="server" Text="Delete" CssClass="delete" OnClick="btnDelete_Click" /></td>
            <td><asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="submit" OnClick="btnSave_Click" /></td>
        </tr>
     </table>
</asp:Content>