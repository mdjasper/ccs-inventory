<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="container_add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        $(document).ready(function () { 
            $(".checkBox").click(function () {
                $('#<%= txtBinNumber.ClientID %>').toggleDisabled()
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-container.png" /> Add Container
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Add New Container</h1>
    <asp:Label ID="lblError" runat="server" Text="" CssClass="error" Visible="false"></asp:Label>
    <table class="form">
        <tr>
            <td>Auto Generate Bin Number</td>
            <td><asp:CheckBox ID="chkAutoGen" runat="server" CssClass="checkBox" /></td>
        </tr>
        <tr>
            <td>Bin number</td>
            <td><asp:TextBox ID="txtBinNumber" runat="server" type="number"></asp:TextBox></td>
        </tr>
    </table>

    <table class="buttonGroup cols-2">
        <tr>
            <td><a href="javascript:history.back()" class="cancel">Cancel</a></td>
            <td><asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="submit" OnClick="btnSave_Click"/></td>
        </tr>
    </table>
</asp:Content>

