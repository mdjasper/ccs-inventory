<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="AddFoodCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-category.png" />Add Food Category
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Add a New Food Category</h1>
    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
    <table class="form">
        <tr>
            <td>Food Type:</td>
            <td><asp:TextBox ID="txtAddFoodType" runat="server"></asp:TextBox></td> 
        </tr>
        <tr>
            <td>Perishable:</td>
            <td>
                <asp:CheckBox ID="cbPerishable" CssClass="checkBox" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Non Food:</td>
            <td>
                <asp:CheckBox ID="cbNonFood" CssClass="checkBox" runat="server" /></td>
        </tr>
        <tr>
            <td></td>
        </tr>
    </table>

    <table class="buttonGroup cols-2">
        <tr>
            <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="cancel"  Width="100%" OnClick="btnCancel_Click" /></td>
            <td><asp:LinkButton ID="btnAddFoodTypeSubmit" runat="server" Text="Save" CssClass="submit confirm" OnClick="btnAddFoodTypeSubmit_Click" /></td>
        </tr>
    </table>
</asp:Content>

