<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="confirm.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-category.png" />Confirmation
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    
    <h1>Save Changes?</h1>
   
    <div style ="margin-top:2em; margin-bottom: 3em; ">
        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" style ="font-size:12pt; font-weight:bold;"></asp:Label>
        <table class="form">
            <tr>
                <td>Old Donor Type:</td>
                <td><asp:Label ID="lblOldDonorType" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>New Donor Type:</td>
                <td><asp:Label ID="lblDonorType" runat="server" Text=""></asp:Label></td>
            </tr>
        </table>
    </div>

    <table class="buttonGroup cols-2">
        <tr>
            <td><asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="submit"  Width="100%" OnClick="btnSubmit_Click"/></td>
            <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="cancel"  Width="100%" OnClick="btnCancel_Click"/></td>
        </tr>
    </table>
    
</asp:Content>
