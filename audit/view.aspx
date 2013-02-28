<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="view.aspx.cs" Inherits="View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    View Adjustments
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
    <asp:GridView ID="gvAdjustments" CssClass="dataTable  mediumFont" runat="server" AllowPaging="True" CellPadding="5" PageSize="5" Width="100%" AutoGenerateColumns="false"
        OnPageIndexChanging="gvAdjustments_PageIndexChanging">
        <AlternatingRowStyle BackColor="#FFFFCC" />
        <Columns>
            <asp:TemplateField HeaderText="Weight">
                <ItemTemplate>
                    <asp:Label ID="lblWeight" runat="server" Text='<%# Bind("Weight") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Food Category">
                <ItemTemplate>
                    <asp:Label ID="lblFoodCategory" runat="server" Text='<%# Bind("FoodCategory") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Location">
                <ItemTemplate>
                    <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("Location") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="USDA?">
                <ItemTemplate>
                    <asp:Label ID="lblIsUSDA" runat="server" Text='<%# Bind("IsUSDA") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="USDA Number">
                <ItemTemplate>
                    <asp:Label ID="lblUSDANumber" runat="server" Text='<%# Bind("USDANumber") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Units">
                <ItemTemplate>
                    <asp:Label ID="lblUnits" runat="server" Text='<%# Bind("Cases") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <a href="default.aspx" class="cancel">Back</a>
</asp:Content>

