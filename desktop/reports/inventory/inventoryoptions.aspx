<%@ Page Title="" Language="C#" MasterPageFile="~/desktop/MasterPage.master" AutoEventWireup="true" CodeFile="inventoryoptions.aspx.cs" Inherits="desktop_reports_incoming_InventoryOptions" %>

<%@ Register Src="~/NumericKeypad.ascx" TagPrefix="uc1" TagName="NumericKeypad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    Inventory Options  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Inventory Report Options</h1>

        <div id="center">
            <br />

          <asp:Panel ID="Panel1" runat="server" BackColor="#FFFF99" BorderColor="Black" BorderStyle="None" Width="381px">
              <asp:Label ID="Label3"  runat="server" Text="Title" Font-Bold="True" Font-Size="20pt"></asp:Label>
                <br />
                <asp:TextBox ID="txtTitle" runat="server" Width="200" />
                <br />

        <asp:Label ID="Label1" runat="server" Text="Group By" Font-Bold="True" Font-Size="20pt"></asp:Label>
              <br />
        <br />
              <asp:RadioButton ID="rbCategory" runat="server" Text="Category" Font-Size="Small" GroupName="groupBy" />
              &nbsp;&nbsp;&nbsp;
              <asp:RadioButton ID="rbLocation" runat="server" Text="Location" Font-Size="Small" GroupName="groupBy" />
              <br />
        <br />
    </asp:Panel>
</div>

    <br />
<style>
   #center {text-align:center; margin: 0px auto; width: 26%;
        height: 166px;
    }
</style>
        <div id="show">

          <asp:Panel ID="Panel2" runat="server" BackColor="#FFFF99" BorderColor="Black" BorderStyle="None" Width="381px">
        <asp:Label ID="Label2" runat="server" Text="Show" Font-Bold="True" Font-Size="20pt"></asp:Label>
              <br />
        <br />
              <asp:RadioButton ID="rbFoodBin" runat="server" Text="Individual Food Bins" Font-Size="Small" GroupName="show" />
              &nbsp;&nbsp;&nbsp;
              <asp:RadioButton ID="rbTotal" runat="server" Text="Total of Category" Font-Size="Small" GroupName="show" />
              <br />
              <br />
              <asp:CheckBox ID="cbGrandTotal" runat="server" Font-Size="Small" Text="Grand Total"  />
              <br />
        <br />
    </asp:Panel>
</div>

    <br />

<style>
   #show {text-align:center; margin: 0px auto; width: 26%;
    }
</style>

    <br />
&nbsp;<br />
    <br />
    <asp:Label ID="lblError" runat="server" ForeColor="Red" />
    <br />
    <div id="Button">
    <asp:Button ID="btnBack" CssClass="cancel" runat="server" Text="Back" OnClientClick="JavaScript: window.history.go(-1); return false;" />
         &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:Button ID="btnNext" runat="server" CssClass="submit" Text="Next" OnClick="btnNext_Click" />
     </div>
    <style>
        #Button{ text-align:center; margin: 0px auto;}
    </style>
</asp:Content>







