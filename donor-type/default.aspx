<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-category.png" />Donor Types
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    
    <h1>Donor Types 
       <!--<input type="button" class="submit right" value="+" onclick="$('.addSection').toggle('fast');"/>-->
    </h1>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" style ="font-size:12pt; font-weight:bold;"></asp:Label>

    <asp:GridView ID="grdDonorTypes" CssClass="dataTable" runat="server" AllowPaging="True" CellPadding="5" 
        OnPageIndexChanging="grdDonorTypes_PageIndexChanging" PageSize="10" Width="100%" AutoGenerateColumns="false"
        AllowSorting="true" OnSorting="TaskGridView_Sorting">
        
        <AlternatingRowStyle BackColor="#FFFFCC" />
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="FoodSourceTypeID"
                DataNavigateUrlFormatString="edit.aspx?id={0}"
                Text="Edit" ControlStyle-CssClass="button"/>

             <asp:TemplateField HeaderText="Donor Type" SortExpression="DonorType">
                <ItemTemplate>
                    <asp:Label ID="lblDonorType" runat="server" Text='<%# Bind("DonorType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
   
    <asp:Button ID="btnAddDonorType" runat="server" Text="Add Donor Type" CssClass="button submit" Width="100%" OnClick="btnAddDonorType_Click" />
    
    
</asp:Content>