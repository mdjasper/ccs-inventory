<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-category.png" />Edit Donor Type
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    
    <h1>Edit Donor Type:</h1>
    <h1><asp:Label runat="server" ID="lblDonorTypeName"></asp:Label></h1>
    
    <div style ="margin-top:2em; margin-bottom: 3em; ">
        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" style ="font-size:12pt; font-weight:bold;"></asp:Label>
        <table class="form">
            <tr>
                <td>Donor Type:</td>
                <td><asp:TextBox runat="server" ID="txtDonorType"></asp:TextBox></td>
            </tr>
        </table>
    </div>

    <table class="buttonGroup cols-2">
        <tr>
            <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="cancel" OnClick="btnCancel_Click" Width="100%"/></td>
            <td><asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="submit confirm" OnClick="btnSubmit_Click" Width="100%"/></td>
        </tr>
    </table>
    
</asp:Content>