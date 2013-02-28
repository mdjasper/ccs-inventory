<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../js/jquery-ui-1.10.0.custom.min.js" type="text/javascript"></script>
    <script src="DonorService.ashx" type="text/javascript"></script>
    <link href="../css/ui-lightness/jquery-ui-1.10.0.custom.min.css" rel="stylesheet" /> 
    <script>
        $(document).ready(function () {
            $('#<%= txtDonorName.ClientID %>').autocomplete({ source: donorNames });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-person.png" />View Donors
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    
    <h1>View Donors 
       <!-- <input type="button" class="submit right" value="+" onclick="$('.addSection').toggle('fast');"/> -->
        <!--<asp:Button ID="btnAddDonor1" runat="server" Text="+" CssClass="submit right" OnClick="btnAddDonor_Click" />-->
    </h1>

    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" style ="font-size:12pt; font-weight:bold;"></asp:Label>

    <table class="form">
        <tr>
            <td>Search by Donor:</td>
            <td><asp:TextBox runat="server" ID="txtDonorName" class="donorInput"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnFind" runat="server" Text="Find" CssClass="button submit right" 
                    style="font-size: 14pt;" OnClick="btnFind_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button cancel right" 
                    style="font-size: 14pt;  margin-right: 10px;" OnClick="btnReset_Click" />

            </td>
        </tr>
    </table>

    <asp:GridView ID="grdDonors" CssClass="dataTable mediumFont" runat="server" AllowPaging="True" CellPadding="5" 
        OnPageIndexChanging="grdDonors_PageIndexChanging" PageSize="5" Width="100%" AllowSorting="True" AutoGenerateColumns="false"
        OnSorting="TaskGridView_Sorting">
        <AlternatingRowStyle BackColor="#FFFFCC" />
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="Id"
                DataNavigateUrlFormatString="edit.aspx?id={0}"
                Text="Edit" ControlStyle-CssClass="button"/>

            <asp:TemplateField HeaderText="Donor" SortExpression="Donor">
                <ItemTemplate>
                    <asp:Label ID="lblDonor" runat="server" Text='<%# Bind("Donor") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Donor Type" SortExpression="DonorType">
                <ItemTemplate>
                    <asp:Label ID="lblDonorType" runat="server" Text='<%# Bind("DonorType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Street Addr" SortExpression="Address">
                <ItemTemplate>
                    <asp:Label ID="lblStreetAddr" runat="server" Text='<%# Bind("StreetAddr") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Zipcode" SortExpression="Zipcode">
                <ItemTemplate>
                    <asp:Label ID="lblZipcode" runat="server" Text='<%# Bind("Zipcode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Button ID="btnAddDonor" runat="server" Text="Add Donor" CssClass="button submit" Width="100%" OnClick="btnAddDonor_Click" />

</asp:Content>