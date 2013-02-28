<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-category.png" />Add Donor Types
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    
    <h1>Add Donor Types </h1>
    <br/>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" style ="font-size:12pt; font-weight:bold;"></asp:Label>
    
    <table class="form">
        <tr>
            <td>Donor Type:</td>
            <td><asp:TextBox ID="txtNewDonorType" runat="server"></asp:TextBox></td>
        </tr>
            
    </table>

    <br/><br/><br/>
    <table class="buttonGroup cols-2">
        <tr>
            <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="cancel"  Width="100%" OnClick="btnCancel_Click" /></td>
            <td><asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="submit confirm" OnClick="btnSave_Click" /></td>
        </tr>
    </table>
</asp:Content>