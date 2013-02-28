<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        #<%= optionalStoreIdField.ClientID %> {
            display: none;
        }
    </style>

    <script>
        $(document).ready(function () {
            if ($("#<%= ddlDonorType.ClientID %>>option:selected").text() == "Grocery Rescue")
            {
                $('#<%= optionalStoreIdField.ClientID %>').show();
            }
        });
        
        function toggleField(id) {
            if ($("#<%= ddlDonorType.ClientID %>>option:selected").text() == "Grocery Rescue")
            {
                console.log("toggleField('" + id + "') called");
                $(<%= optionalStoreIdField.ClientID %>).show("slow");
            } else {
                $(<%= optionalStoreIdField.ClientID %>).hide("slow");
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-person.png" />Edit Donor
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    
    <h1>Edit Donor: <asp:Label runat="server" ID="lblDonorName"></asp:Label></h1>

    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" style ="font-size:12pt; font-weight:bold;"></asp:Label>
    
    <h2>Information</h2>
    
    <table class="form">
        <tr>
            <td>Name:</td>
            <td><asp:TextBox runat="server" ID="txtDonorName"></asp:TextBox></td>
        </tr>
        <tr id="optionalStoreIdField" runat="server">
            <td>Store ID:</td>
            <td><asp:TextBox runat="server" ID="txtStoreID" ></asp:TextBox></td>
        </tr> 
        <tr>
            <td>Donor Type:</td>
            <td>
                <asp:DropDownList ID="ddlDonorType" 
                    runat="server"   
                    DataTextField="FoodSourceType1" 
                    DataValueField ="FoodSourceTypeID" 
                    AppendDataBoundItems="True" onChange="toggleField('#<%= optionalStoreIdField.ClientID %>')">
                        <asp:ListItem Value="0"> &#60; Select Donor Type &#62;</asp:ListItem>
                    </asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td></td>
            <td>
                <asp:Button ID="btnAddDonorType" runat="server" Text="Add Donor Type" CssClass="button submit right" 
                    style="font-size: 14pt;" OnClick="btnAddDonorType_Click" />

            </td>
        </tr>  
        
    </table>

    <h2>Address</h2>
    
    <table class="form">
        <tr>
            <td>Street Address 1:</td>
            <td><asp:TextBox runat="server" ID="txtStreet1"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Street Address 2:</td>
            <td><asp:TextBox runat="server" ID="txtStreet2"></asp:TextBox></td>
        </tr>
        <tr>
            <td>City:</td>
            <td><asp:TextBox runat="server" ID="txtCity"></asp:TextBox></td>
        </tr>
        <tr>
            <td>State:</td>
            <td><asp:DropDownList ID="ddlState" 
                    runat="server"   
                    DataTextField="StateFullName" 
                    DataValueField ="StateID" 
                    AppendDataBoundItems="True">
                        <asp:ListItem Value="0"> &#60; Select a State &#62;</asp:ListItem>
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Zip:</td>
            <td><asp:TextBox runat="server" ID="txtZip"></asp:TextBox></td>
        </tr>
    </table>

    <table class="buttonGroup cols-2">
        <tr>
            <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="cancel"  Width="100%" OnClick="btnCancel_Click" /></td>
            <td><asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="submit confirm" OnClick="btnSave_Click" /></td>
        </tr>
    </table>

</asp:Content>