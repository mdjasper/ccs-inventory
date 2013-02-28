<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="AuditManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    View Audits
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
    <asp:GridView ID="gvAudits" CssClass="dataTable" runat="server" AllowPaging="True" CellPadding="5" PageSize="5" Width="100%"
        OnPageIndexChanging="gvAudits_PageIndexChanging" AutoGenerateColumns="false"
        AllowSorting="true"
        OnSorting="TaskGridView_Sorting">
        <AlternatingRowStyle BackColor="#FFFFCC" />
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="AuditID"
                DataNavigateUrlFormatString="view.aspx?id={0}"
                Text="Adjustments" ControlStyle-CssClass="button"
                />

            <asp:TemplateField HeaderText="Date Peformed" SortExpression="Date">
                <ItemTemplate>
                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Performed By" SortExpression="UserName">
                <ItemTemplate>
                    <asp:Label ID="lblUserID" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <table class="buttonGroup cols-2">
        <tr>
            <td><a href=".." class="cancel">Back</a></td>
            <td>
                <a href="perform.aspx" class="submit">Perform Audit</a>
            </td>
        </tr>
    </table>
</asp:Content>

