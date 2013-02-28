<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="desktop_home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" runat="Server">
    <img src="../images/icon-desktop.png" />Desktop Home
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" runat="Server">
    <div>   
             
        <asp:GridView ID="grdUserInfo" runat="server" AutoGenerateColumns="false" CellPadding="5" PageIndex="10" Width="50%" AllowSorting="False" CssClass="dataTable">
        <AlternatingRowStyle BackColor="#FFFFCC" />
        <Columns>
            <asp:TemplateField HeaderText="Welcome!">         
            <ItemTemplate>
                <asp:Label ID="lblUserID" runat="server" Text="UserID: "></asp:Label>   
                <%# Eval("UserID") %><br />
                <asp:Label ID="lblUserName" runat="server" Text="User Name: "></asp:Label> 
                <%# Eval("UserName") %><br />
                <asp:Label ID="lblName" runat="server" Text="Name: "></asp:Label>         
                 <%# Eval("FirstName") %>&nbsp;<%# Eval("LastName") %> <br />
                <asp:Label ID="lblAdmin" runat="server" Text="Admin? "></asp:Label> 
                <%# Eval("Admin") %><br />
            </ItemTemplate>         
            </asp:TemplateField>   
        </Columns>
        </asp:GridView>
    </div>
</asp:Content>
