<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="verify.aspx.cs" Inherits="Verify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    Verify Container #<asp:Label ID="lblNumber" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <table class="form">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Weight</td>
            <td><asp:TextBox ID="txtWeight" runat="server" type="number" step="any" CssClass="input"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Location</td>
            <td>
                <%-- Databind ddlLocation to location table in database --%>
                <asp:DropDownList ID="ddlLocation" runat="server"  DataTextField="RoomName" DataValueField ="LocationID" CssClass="input">
                    <asp:ListItem Value="1">Select Location</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Is USDA</td>
            <td>
                <asp:CheckBox ID="cbIsUSDA" CssClass="checkBox input" runat="server"/>
            </td>
        </tr>
        <tr id="trFoodCategory">
            <td>Food Category</td>
            <td>
                <%-- Databind ddlType to type table in database --%>
                    <asp:DropDownList ID="ddlFoodCategory" runat="server"   DataTextField="CategoryType" DataValueField ="FoodCategoryID" CssClass="input">
                        <asp:ListItem Value="1">Select Food Category</asp:ListItem>
                    </asp:DropDownList>
            </td>
        </tr>
        <tr id="trUSDACategory">
            <td>USDA Category</td>
            <td>
                <%-- Databind ddlLocation to USDA Category table in database --%>
                <asp:DropDownList ID="ddlUSDACategory" runat="server"  DataTextField="Description" DataValueField ="USDAID" CssClass="input">
                    <asp:ListItem Value="1">Select USDA Category</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
          <tr id="trUnits">
            <td>Units</td>
            <td><asp:TextBox ID="txtUnits" runat="server" type="number" step="any" CssClass="input"></asp:TextBox></td>
        </tr>
        </table>      
    <table class="buttonGroup cols-2">
        <tr>
            <td><a href="perform.aspx" class="cancel">Cancel</a></td>
            <td>
                <asp:LinkButton ID="btnVerify" runat="server" Text="Verify" CssClass="submit" OnClick="btnVerify_Click"/>
                <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="submit" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>

    <script>
        $(document).ready(function ()
        {

            $('#<%=btnSave.ClientID %>').hide();

            if (!$('#<%= cbIsUSDA.ClientID %>').is(':checked'))
            {
                $('#trUSDACategory').hide();
                $('#trUnits').hide();
            }
            else
            {
                $('#trFoodCategory').hide();
            }

            $('.input').change(function ()
            {
                $('#<%=btnVerify.ClientID %>').fadeOut(400,
                    function ()
                    {
                        $('#<%=btnSave.ClientID %>').fadeIn();
                    });
            });

            $('#<%= cbIsUSDA.ClientID %>').change(function ()
            {
                if ($('#<%= cbIsUSDA.ClientID %>').is(':checked'))
                {
                    $('#trUSDACategory').fadeIn();
                    $('#trUnits').fadeIn();
                    $('#trFoodCategory').fadeOut();
                }
                else
                {
                    $('#trUSDACategory').fadeOut();
                    $('#trUnits').fadeOut();
                    $('#trFoodCategory').fadeIn();
                }
            });
        });
    </script>
</asp:Content>

