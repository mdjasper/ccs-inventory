<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="out.aspx.cs" Inherits="container_out" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    <img src="../images/icon-container.png" /> Move Container Out
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Move Container <asp:Label ID="lblBinNumber1" runat="server" Text=""></asp:Label></h1>
    <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error"></asp:Label>
    <fieldset>
        <legend>Container Information</legend>
        <table>
            <tr>
                <td>Bin Number:</td>
                <td><asp:Label ID="lblBinNumber2" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>Weight:</td>
                <td><asp:Label ID="lblWeight" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>Is USDA:</td>
                <td><asp:CheckBox ID="chkUSDA" runat="server" Enabled="false" CssClass="checkBox" /></td>
            </tr>
            <tr>
                <td>Contains:</td>
                <td><asp:Label ID="lblCategory" runat="server" Text=""></asp:Label></td>
            </tr>
        </table>
    </fieldset>
    <h2>Move Reason:</h2>
    <asp:DropDownList ID="ddlDistributionType" runat="server" AppendDataBoundItems="true"
        DataTextField="DistributionType1" DataValueField="DistributionTypeID" 
        OnSelectedIndexChanged="ddlDistributionType_SelectedIndexChanged" AutoPostBack="true">
        <asp:ListItem Value="0">Select Reason</asp:ListItem>
    </asp:DropDownList>
    <div id="Agency" runat="server" visible="false">
        <h2>Agency:</h2>
        <asp:DropDownList ID="ddlAgency" runat="server" AppendDataBoundItems="true"
            DataTextField="AgencyName" DataValueField="AgencyID">
            <asp:ListItem Value="0">Select Agency</asp:ListItem>
        </asp:DropDownList>
    </div>
    <table class="buttonGroup cols-2">
        <tr>
            <td><a href="javascript:history.back()" class="cancel">Cancel</a></td>
            <td><asp:LinkButton ID="btnRemove" runat="server" Text="Move Out" CssClass="delete confirm" OnClick="btnRemove_Click" /></td>
        </tr>
    </table>
</asp:Content>

