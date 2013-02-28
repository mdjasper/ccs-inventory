<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-person.png" />Incoming Food
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    
    <h1>Incoming Food 
        <!--<asp:Button ID="btnAddIncomingFood1" runat="server" Text="+" CssClass="submit right" OnClick="btnAddIncomingFood_Click" />-->
    </h1>

    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>

    <asp:GridView ID="grdFoodIn" CssClass="dataTable mediumFont" runat="server" AllowPaging="True" CellPadding="5" 
        OnPageIndexChanging="grdFoodIn_PageIndexChanging" PageSize="6" Width="100%" AllowSorting="True" AutoGenerateColumns="false" 
        OnRowDataBound="grdFoodIn_RowDataBound" OnSorting="TaskGridView_Sorting">
        <AlternatingRowStyle BackColor="#FFFFCC" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="printInKind" runat="server" NavigateUrl='<%# Eval("Id", "print.aspx?id={0}") %>'
                        Text="Print" CssClass="button"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Donor" SortExpression="Donor">
                <ItemTemplate>
                    <asp:Label ID="lblDonor" runat="server" Text='<%# Bind("Donor") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Category" SortExpression="Category">
                <ItemTemplate>
                    <asp:Label ID="lblFoodCategory" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Weight" SortExpression="Weight">
                <ItemTemplate>
                    <asp:Label ID="lblWeight" runat="server" Text='<%# Bind("Weight") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                      
            <asp:TemplateField HeaderText="Time Stamp" SortExpression="TimeStamp">
                <ItemTemplate>
                    <asp:Label ID="lblTimeStamp" runat="server" Text='<%# Bind("TimeStamp") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="DonorType" SortExpression="DonorType">
                <ItemTemplate>
                    <asp:Label ID="lblDonorType" runat="server" Text='<%# Bind("DonorType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Button ID="btnAddIncomingFood" runat="server" Text="Add Incoming Food" CssClass="button submit" Width="100%" OnClick="btnAddIncomingFood_Click"/>
    
</asp:Content>