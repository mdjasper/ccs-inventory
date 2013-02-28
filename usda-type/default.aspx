﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="USDACategoryManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../js/jquery-1.8.2.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-category.png" />USDA Category
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>
        USDA Category
    </h1>
    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
    <asp:GridView ID="gvUSDAType" CssClass="dataTable mediumFont" runat="server" AllowPaging="True" CellPadding="5" PageSize="5" Width="100%"
        OnPageIndexChanging="gvUSDAType_PageIndexChanging" AutoGenerateColumns="false" AllowSorting="true"
        OnSorting="TaskGridView_Sorting">
        <AlternatingRowStyle BackColor="#FFFFCC" />
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="USDAID"
                DataNavigateUrlFormatString="edit.aspx?id={0}"
                Text="Edit" ControlStyle-CssClass="button"/>

            <asp:TemplateField HeaderText="Description" SortExpression="Description">
                <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="USDA Number" SortExpression="Number">
                <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("USDANumber") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Button ID="btnAddNewUSDACat" runat="server" Text="Add USDA Category" CssClass="button submit" Width="100%" OnClick="btnAddNewUSDACat_Click"/>
</asp:Content>