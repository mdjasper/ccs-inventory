<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    Manage Locations
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">

    <h1>
        Locations
        <input type="button" class="submit right" value="+" onclick="$('.addSection').toggle('fast');"/>
    </h1>

    <div class="addSection">
        <table class="form">
            <tr>
                <td>Location:</td>
                <td><asp:TextBox ID="txtAddLocation" runat="server"></asp:TextBox></td>
                
            </tr>
            <tr>
                <td><asp:LinkButton ID="btnAddLocationSubmit" runat="server" Text="Save" CssClass="submit" OnClick="btnAddLocationSubmit_Click"/></td>
            </tr>
        </table>
    </div>

    <asp:GridView ID="gvLocation" CssClass="dataTable  removeColumn" runat="server" AllowPaging="True" CellPadding="5" PageIndex="10" Width="100%">
        <AlternatingRowStyle BackColor="#FFFFCC" />
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="LocationId"
                DataNavigateUrlFormatString="edit.aspx?id={0}"
                Text="Edit"/>
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
</asp:Content>

