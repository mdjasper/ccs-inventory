<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Default2" %>
<%@ Register Src="~/NumericKeypad.ascx" TagPrefix="uc1" TagName="NumericKeypad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../js/jquery-1.8.2.min.js"></script>
    <script src="../js/jquery-ui-1.10.0.custom.min.js" type="text/javascript"></script>
    <script src="../donor/DonorService.ashx" type="text/javascript"></script>
    <script src="CategoryService.ashx" type="text/javascript"></script>
    <script src="USDAItemService.ashx" type="text/javascript"></script>
    <link href="../css/ui-lightness/jquery-ui-1.10.0.custom.min.css" rel="stylesheet" />
    <style>
      tr.spaceBelow > td
        {
            padding-bottom:10px;
        }

    </style>
    <script>
        $(document).ready(function () {

            if ($('#<%= ckbxIsUSDA.ClientID %>').is(":checked")) {
                //console.log("toggleField() true");
                $('#<%= nonUSDA.ClientID %>').hide("slow");
                $('#<%= isUSDA1.ClientID %>').show("fast");
                $('#<%= isUSDA2.ClientID %>').show("fast");

            }
            else {
                //console.log("toggleField() false");
                $('#<%= nonUSDA.ClientID %>').show("fast");
                $('#<%= isUSDA1.ClientID %>').hide("slow");
                $('#<%= isUSDA2.ClientID %>').hide("slow");

            }

            $('#<%= txtDonorName.ClientID %>').autocomplete({ source: donorNames });
            $('#<%= txtCategoryType.ClientID %>').autocomplete({ source: categoryNames });
            $('#<%= txtUSDAItemNo.ClientID %>').autocomplete({ source: usdaItmNumbers });

        });

        function toggleField() {
            if ($('#<%= ckbxIsUSDA.ClientID %>').is(":checked")) {
                //console.log("toggleField() true");
                $('#<%= nonUSDA.ClientID %>').hide("slow");
                $('#<%= isUSDA1.ClientID %>').show("fast");
                $('#<%= isUSDA2.ClientID %>').show("fast");
            }
            else {
                //console.log("toggleField() false");
                $('#<%= nonUSDA.ClientID %>').show("fast");
                $('#<%= isUSDA1.ClientID %>').hide("slow");
                $('#<%= isUSDA2.ClientID %>').hide("slow");
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-category.png" />Edit Incoming Food
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    
    <h1>Edit Incoming Food</h1>

    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" style ="font-size:12pt; font-weight:bold;"></asp:Label>
    
    <br/>
    
    <table class="buttonGroup cols-3">
         <tr class="spaceBelow">
           <td>Donation from USDA?</td>
            <td><asp:CheckBox ID="ckbxIsUSDA" runat="server"  CssClass="checkBox"  onClick="toggleField()"/></td>
             <td> </td>
        </tr>
        
        <tr id="nonUSDA" runat="server" class="spaceBelow">
            <td>Donor:</td>
            <td><asp:TextBox runat="server" ID="txtDonorName" style="margin-right:2em;"></asp:TextBox></td>
            <td>
                <asp:Button ID="btnAddDonor" runat="server" Text="Create Donor" CssClass="submit" Width="97%" style="height:1.5em;
                    margin-left:3%; font-size:x-large;" OnClick="btnAddDonor_Click"/>
            </td>
        </tr>
       
        <tr id="isUSDA1" runat="server" class="spaceBelow">
            <td>USDA Item Number:</td>
            <td><asp:TextBox runat="server" ID="txtUSDAItemNo" style="margin-right:2em;" ></asp:TextBox></td>
            <td><asp:Button ID="btnAddUSDA" runat="server" Text="Create USDA No" CssClass="submit" Width="97%" style="height:1.5em;
                    margin-left:3%; font-size:x-large;" OnClick="btnAddUSDA_Click"/>
            </td>
        </tr>
        
         <tr id="isUSDA2" runat="server" class="spaceBelow">
            <td>Number of Cases:</td>
            <td><asp:TextBox runat="server" ID="txtUSDACases" style="margin-right:2em;" type="number"></asp:TextBox></td>
            <td> </td>
        </tr>
        
        <tr class="spaceBelow">
            <td>Category Type:</td>
            <td><asp:TextBox runat="server" ID="txtCategoryType" style="margin-right:2em;"></asp:TextBox></td>
            <td><asp:Button ID="btnAddCategory" runat="server" Text="Create Category" CssClass="submit" Width="97%" style="height:1.5em;
                    margin-left:3%; font-size:x-large;" OnClick="btnAddCategory_Click"/>
            </td>
        </tr>
        
        <tr class="spaceBelow">
            <td>Weight:</td>
            <td><asp:TextBox runat="server" ID="txtWeight" style="margin-right:2em;" type="number" step="any"></asp:TextBox></td>
            <td> </td>
        </tr>

        <tr class="spaceBelow">
            <td>Print In-Kind Receipt?</td>
            <td><asp:CheckBox ID="ckbxPrint" CssClass="checkBox" runat="server" /></td>
            <td> </td>
        </tr>
    </table>

    <br/><br/><br/>
    
    <table class="buttonGroup cols-2">
        <tr>
            <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="cancel"  Width="100%" OnClick="btnCancel_Click" /></td>
            <td><asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="submit confirm" OnClick="btnSave_Click" /></td>
        </tr>
    </table>
</asp:Content>