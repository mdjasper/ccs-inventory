<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-container.png" />Manage Containers
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>View Containers</h1>

    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>

    <asp:GridView ID="grdContainers" CssClass="dataTable mediumFont" runat="server" CellPadding="5" Width="100%"
        AllowPaging="True" PageSize="5"
        OnPageIndexChanging="grdContainers_PageIndexChanging" 
        AllowSorting="True" OnSorting="grdContainers_Sorting" AutoGenerateColumns="false">
        <AlternatingRowStyle BackColor="#FFFFCC" />
        <Columns>

            <asp:HyperLinkField DataNavigateUrlFields="BinNumber"
                DataNavigateUrlFormatString="menu.aspx?id={0}"
                Text="Select" ControlStyle-CssClass="button"/>
            
            <asp:TemplateField HeaderText="Number">
                <ItemTemplate>
                    <asp:Label ID="lblCategoryName" runat="server" Text='<%# Bind("BinNumber") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Category" SortExpression="Category">
                <ItemTemplate>
                    <asp:Label ID="lblCategoryName" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Location" SortExpression="Location">
                <ItemTemplate>
                    <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("Location") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="USDA">
                <ItemTemplate>
                    <asp:CheckBox ID="chkUSDA" Checked='<%# Bind("isUSDA") %>' CssClass="checkBox" runat="server"/>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Weight" SortExpression="Weight">
                <ItemTemplate>
                    <asp:Label ID="lblWeight" runat="server" Text='<%# Bind("Weight") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
    <table class="buttonGroup cols-2">
        <tr>
            <td><a href="batch-print.aspx" class="cancel" target="_blank" title="Print Labels for All Containers">Batch Print</a>
</td>
            <td><a href="add.aspx" ID="btnAddContainer" class="submit" >Add Container</a></td>
        </tr>
    </table>
    
</asp:Content>

