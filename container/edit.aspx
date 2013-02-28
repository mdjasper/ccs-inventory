<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function checkboxClick() {
            if ($('#<%= chkIsUSDA.ClientID %>').is(':checked')) {
                $('.usda').show('fast');
                $('.nonusda').hide();
            } else {
                $('.usda').hide();
                $('.nonusda').show('fast');
            }
        }
        $('#<%= chkIsUSDA.ClientID %>').click(function () {
            checkboxClick();
        });
        $(document).ready(function () {
            checkboxClick();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-container.png" />Edit Container:
    <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Modify Container</h1>
    <table class="form">
        <tr>
            <td>Weight</td>
            <td><asp:TextBox ID="txtWeight" runat="server" type="number" step="any"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Is USDA?</td>
            <td>
                <asp:CheckBox ID="chkIsUSDA" runat="server" CssClass="checkBox" onClick="checkboxClick()" />
            </td>
        </tr>
        <tr class="nonusda">
            <td>Food Category</td>
            <td>
                <asp:DropDownList ID="ddlType" runat="server"   DataTextField="CategoryType" DataValueField ="FoodCategoryID">
                    <asp:ListItem Value="1">Select Food Type</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="usda">
            <td>USDA Category</td>
            <td>
                <asp:DropDownList ID="ddlUSDAType" runat="server"  DataTextField="Description" DataValueField ="USDAID">
                    <asp:ListItem Value="1">Select Food Type</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="usda">
            <td>Number of Cases</td>
            <td>
                <asp:TextBox ID="txtNumberOfCases" runat="server" type="number"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Location</td>
            <td>
                <asp:DropDownList ID="ddlLocation" runat="server"  DataTextField="RoomName" DataValueField ="LocationID">
                    <asp:ListItem Value="1">Select Location</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td colspan="2">
                <asp:Label ID="lblWeightError" runat="server" ForeColor="Red" />
            </td>
        </tr>
    </table>
    <div id="savedMessage" class="notification" runat="server" visible="false">
        <asp:Label ID="message" runat="server"></asp:Label>
    </div>
    <table class="buttonGroup cols-3">
        <tr>
            <td><a href="javascript:history.back()" class="cancel">Cancel</a></td>
            <td><asp:LinkButton ID="btnRemove" runat="server" Text="Delete" CssClass="delete confirm" OnClick="btnRemove_Click" /></td>
            <td><asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="submit confirm" OnClick="btnSave_Click" /></td>
        </tr>
    </table>        


                

                

    
         

</asp:Content>

