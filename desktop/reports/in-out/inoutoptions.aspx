<%@ Page Title="" Language="C#" MasterPageFile="~/desktop/MasterPage.master" AutoEventWireup="true" CodeFile="inoutoptions.aspx.cs" Inherits="desktop_reports_incoming_In_OutOptions" %>

<%@ Register Src="~/NumericKeypad.ascx" TagPrefix="uc1" TagName="NumericKeypad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    &nbsp;  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <h1>In\Out Report Options</h1>
    <br />

        <div id="center">
            <br />
            <asp:Panel  runat="server" BackColor="#FFFF99" BorderColor="Black" BorderStyle="None">
                <asp:Label  runat="server" Text="Title" Font-Bold="True" Font-Size="20pt"></asp:Label>
                <br />
                <asp:TextBox ID="txtTitle" runat="server" Width="200" />
           </asp:Panel>
          <asp:Panel ID="Panel1" runat="server" BackColor="#FFFF99" BorderColor="Black" BorderStyle="None">
        <asp:Label ID="Label1" runat="server" Text="Show" Font-Bold="True" Font-Size="20pt"></asp:Label>
        <br />
              <asp:CheckBox ID="cbGrandTotal" runat="server" Text="Grand Total" />
              <br />
              <asp:CheckBox ID="cbShowCount" runat="server" Text="Show Count" />
              <br />
              &nbsp;<asp:CheckBox ID="cbShowWeight" runat="server" Text="Show Weight" />
              <br />
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:CheckBox ID="cbShowCurrentBalance" runat="server" Text="Show Current Balance" />
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


