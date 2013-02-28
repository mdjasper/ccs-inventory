<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="locations.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-locate.png" />Container Locations
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Containers labeled "<asp:Literal ID="ltrCategory" runat="server"></asp:Literal>"</h1>
    <asp:GridView ID="grdContainers" CssClass="dataTable mediumFont" runat="server" AutoGenerateColumns="false" AllowSorting="False">
        <Columns>
            <asp:BoundField DataField="RoomName" HeaderText="Location" />
            <asp:BoundField DataField="BinNumber" HeaderText="Bin Number" />
            <asp:CheckBoxField DataField="isUSDA" HeaderText="USDA?"  />
            <asp:BoundField DataField="Cases" HeaderText="Number of Cases (USDA)" />
        </Columns>
    </asp:GridView>
    <ul class="vertical-list">
        <li><a href="locate.aspx" class="submit">Search Again</a></li>
    </ul>
</asp:Content>

