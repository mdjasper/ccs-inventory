<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="agency_add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    Add new Agency
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">

    <h1>Add Agency</h1>

    <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
    
    <h2>Information</h2>
    
    <table class="form">
        <tr>
            <td>Name:</td>
            <td><asp:TextBox runat="server" ID="txtAgencyName"></asp:TextBox></td>
        </tr>  
    </table>

    <h2>Address</h2>
    
    <table class="form">
        <tr>
            <td>Street Address 1:</td>
            <td><asp:TextBox runat="server" ID="txtStreet1"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Street Address 2:</td>
            <td><asp:TextBox runat="server" ID="txtStreet2"></asp:TextBox></td>
        </tr>
        <tr>
            <td>City:</td>
            <td><asp:TextBox runat="server" ID="txtCity"></asp:TextBox></td>
        </tr>
        <tr>
            <td>State:</td>
            <td><asp:DropDownList ID="ddlState" 
                    runat="server"   
                    DataTextField="StateFullName" 
                    DataValueField ="StateID" 
                    AppendDataBoundItems="True">
                        <asp:ListItem Value="0"> &#60; Select a State &#62;</asp:ListItem>
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Zip:</td>
            <td><asp:TextBox runat="server" ID="txtZip"></asp:TextBox></td>
        </tr>
    </table>

    <table class="buttonGroup cols-2">
        <tr>
            <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="cancel"  Width="100%" OnClick="btnCancel_Click" /></td>
            <td><asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="submit confirm" OnClick="btnSave_Click" /></td>
        </tr>
    </table>

</asp:Content>

