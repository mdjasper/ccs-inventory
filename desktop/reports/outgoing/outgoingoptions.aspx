<%@ Page Title="" Language="C#" MasterPageFile="~/desktop/MasterPage.master" AutoEventWireup="true" CodeFile="outgoingoptions.aspx.cs" Inherits="desktop_reports_incoming_OutgoingReport" %>

<%@ Register Src="~/NumericKeypad.ascx" TagPrefix="uc1" TagName="NumericKeypad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    Outgoing Options&nbsp;  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>Outgoing Options</h1>
    <br />

        <div id="center">
            <br />
            <asp:Panel ID="Panel2"  runat="server" BackColor="#FFFF99" BorderColor="Black" BorderStyle="None">
                <asp:Label ID="Label2"  runat="server" Text="Title" Font-Bold="True" Font-Size="20pt"></asp:Label>
                <br />
                <asp:TextBox ID="txtTitle" runat="server" Width="200" />
           </asp:Panel>
          <asp:Panel ID="Panel1" runat="server" BackColor="#FFFF99" BorderColor="Black" BorderStyle="None">
        <asp:Label ID="Label1" runat="server" Text="Show" Font-Bold="True" Font-Size="20pt"></asp:Label>
        <br />
              <asp:RadioButton ID="rbAllTrans" runat="server" Text="Show All Transactions from Agencies" GroupName="Show"  />
              <br />
              <br />
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbDistributionTypes" runat="server" GroupName="Show" Text="Show All Transactions from Distribution Types" />
              <br />
              <br />
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:RadioButton ID="rbDistributionTotal" runat="server" GroupName="Show" Text="Show Only Totals from Distribution Types" />
              <br />
              <br />
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:RadioButton ID="rbTotalFoodSource" runat="server" Text="Show Only Totals from Food Source Types" GroupName="Show" />
              <br />
              <br />
              <asp:CheckBox ID="cbGrandTotal" runat="server" Text="Grand Total" />
              <br />
        <br />
    </asp:Panel>
</div>

<style>
   #center {text-align:center; margin: 0px auto; width: 50%;}
</style>
    

    <br />
&nbsp;<br />
    <br />
    <asp:Label ID="lblError" runat="server" ForeColor="Red" />
    <br />
    <div id="Button">
    <asp:Button ID="btnBack" CssClass="cancel" runat="server" Text="Back" OnClientClick="JavaScript: window.history.go(-1); return false;"/>
         &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:Button ID="btnNext" runat="server" CssClass="submit" Text="Next" OnClick="btnNext_Click" />
     </div>
    <style>
        #Button{ text-align:center; margin: 0px auto;}
    </style>
</asp:Content>


