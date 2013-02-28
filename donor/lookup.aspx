<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="lookup.aspx.cs" Inherits="Default2" %>


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
    <img src="../images/icon-person.png" /> Find Donor
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    
    <h1>Find Donor</h1>

    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" style ="font-size:12pt; font-weight:bold;"></asp:Label>
    
    <h2>Information</h2>
    
    <br/>
    <table class="form">
        <tr>
            <td>Name:</td>
            <td><asp:TextBox runat="server" ID="txtDonorName" class="donorInput"></asp:TextBox></td>
        </tr>
        
    </table>

    <asp:GridView ID="grdDonors" CssClass="dataTable mediumFont" runat="server" AllowPaging="True" CellPadding="5" 
        OnPageIndexChanging="grdDonors_PageIndexChanging" PageSize="5" Width="100%" AllowSorting="False" AutoGenerateColumns="false">
        <AlternatingRowStyle BackColor="#FFFFCC" />
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="Id"
                DataNavigateUrlFormatString="edit.aspx?id={0}"
                Text="Edit" ControlStyle-CssClass="button"/>

            <asp:TemplateField HeaderText="Donor">
                <ItemTemplate>
                    <asp:Label ID="lblDonor" runat="server" Text='<%# Bind("Donor") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Donor Type">
                <ItemTemplate>
                    <asp:Label ID="lblDonorType" runat="server" Text='<%# Bind("DonorType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Street Addr">
                <ItemTemplate>
                    <asp:Label ID="lblStreetAddr" runat="server" Text='<%# Bind("StreetAddr") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Zipcode">
                <ItemTemplate>
                    <asp:Label ID="lblZipcode" runat="server" Text='<%# Bind("Zipcode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <br/>

    <table class="buttonGroup cols-2">
        <tr>
            <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="cancel"  Width="100%" OnClick="btnCancel_Click" /></td>
            <td><asp:LinkButton ID="btnFind" runat="server" Text="Find" CssClass="submit" OnClick="btnFind_Click" /></td>
        </tr>
    </table>

</asp:Content>
