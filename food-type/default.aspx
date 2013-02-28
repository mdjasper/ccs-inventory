<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="FoodCateogryManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../js/jquery-1.8.2.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-category.png" />Food Category
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>
        Food Category
    </h1>
    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
    <asp:GridView ID="gvFoodType" CssClass="dataTable mediumFont" runat="server" AllowPaging="True" CellPadding="5" PageSize="5" Width="100%"
        OnPageIndexChanging="gvFoodType_PageIndexChanging" AutoGenerateColumns="false" AllowSorting="true"
        OnSorting="TaskGridView_Sorting">
        <AlternatingRowStyle BackColor="#FFFFCC" />
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="FoodCategoryID"
                DataNavigateUrlFormatString="edit.aspx?id={0}"
                Text="Edit" ControlStyle-CssClass="button"/>
        
            <asp:TemplateField HeaderText="Category Name" SortExpression="CategoryType">
                <ItemTemplate>
                    <asp:Label ID="lblCategoryName" runat="server" Text='<%# Bind("CategoryType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Perishable?">
                <ItemTemplate>
                    <asp:CheckBox ID="cbPerishable" runat="server" Enabled="false" Checked='<%# Bind("Perishable") %>'></asp:CheckBox>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="NonFood?">
                <ItemTemplate>
                    <asp:CheckBox ID="cbNonFood" runat="server" Enabled="false" Checked='<%# Bind("NonFood") %>'></asp:CheckBox>
                </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>

     <asp:Button ID="btnAddFoodCat" runat="server" Text="Add Food Category" CssClass="button submit" Width="100%" OnClick="btnAddFoodCat_Click" />

</asp:Content>