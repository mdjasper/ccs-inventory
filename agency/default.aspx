<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="agency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    Manage Agency List
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">

    <h1>Agency List</h1>

    <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>

    <asp:GridView ID="grdAgency" CssClass="dataTable" runat="server" AllowPaging="True" CellPadding="5" 
        OnPageIndexChanging="grdAgency_PageIndexChanging" PageSize="10" Width="100%" AutoGenerateColumns="false"
        AllowSorting="true" OnSorting="grdAgency_Sorting">
        
        <AlternatingRowStyle BackColor="#FFFFCC" />
        <Columns>

            <asp:HyperLinkField DataNavigateUrlFields="ID"
                DataNavigateUrlFormatString="edit.aspx?id={0}"
                Text="Edit" ControlStyle-CssClass="button"/>

             <asp:TemplateField HeaderText="Donor Type" SortExpression="agency">
                <ItemTemplate>
                    <asp:Label ID="lblDonorType" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="City" SortExpression="agency">
                <ItemTemplate>
                    <asp:Label ID="lblDonorType" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
   
    <asp:Button ID="btnAddAgency" runat="server" Text="Add Agency" CssClass="button submit" Width="100%" OnClick="btnAddAgency_Click" />
    

</asp:Content>

