<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="edituser.aspx.cs" Inherits="desktop_edituser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" runat="Server">
    <img src="../images/icon-person.png" />
    <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" runat="Server">
     <div class="notify">
     <asp:Label ID="lblResponse" runat="server" Text=""  CssClass="notification" Visible="False"></asp:Label>
        </div>
    <div id="editUserDiv" runat ="server">  
         
            <ul class="vertical-list">
                 <li>
                    <asp:Label ID="lblEditUserID" runat="server" Text="User ID"></asp:Label>
                    <asp:TextBox ID="txtEditUserID" runat="server" Enabled="false" Text="" CssClass="txtID"></asp:TextBox>
                </li>
                
                <li>
                    <asp:Label ID="lblEditFirstName" runat="server" Text="First Name"></asp:Label>
                    <asp:TextBox ID="txtEditFirstName" runat="server" Text="" CssClass="txtFirst"></asp:TextBox></li>
                <li>
                    <asp:Label ID="lblEditLastName" runat="server" Text="Last Name"></asp:Label>
                    <asp:TextBox ID="txtEditLastName" runat="server" Text="" CssClass="txtLast"></asp:TextBox></li>
                <li>
                    <asp:Label ID="lblEditUserName" runat="server" Text="User Name"></asp:Label>
                    <asp:TextBox ID="txtEditUserName" runat="server" Text="" CssClass="txtUser"></asp:TextBox></li>
                <li>
                    <asp:Label ID="lblEditPass" runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="txtEditPass" runat="server" Text="" CssClass="txtPass"></asp:TextBox></li>
                <li>
                    <asp:Label ID="lblEditAdmin" runat="server" Text="Admin?"></asp:Label>
                    <asp:DropDownList ID="ddlEditAdmin" runat="server" CssClass="ddlAdmin">
                        <asp:ListItem>Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:DropDownList></li>
            </ul>

        <table class="buttonGroup cols-2">
            <tr>
                <td>
                    <a href="users.aspx" class="cancel">Cancel</a>
                </td>
                <td>
                    <asp:LinkButton ID="btnEditSave" runat="server" Text="Save" CssClass="submit" OnClick="btnEditSave_Click" 
                        OnClientClick="return confirm('Are you sure you would like to edit this user?');"/>
                </td>
            </tr>
        </table>
            </div>

        <div id="addUserPanel" runat="server">
        <ul class="vertical-list">
        <li> <asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label>
        <asp:TextBox ID="txtFirstName" runat="server" Text="" CssClass="txtFirst"></asp:TextBox></li>
        <li> <asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label>
        <asp:TextBox ID="txtLastName" runat="server" Text="" CssClass="txtLast"></asp:TextBox></li>
        <li> <asp:Label ID="lblUserName" runat="server" Text="User Name"></asp:Label>
        <asp:TextBox ID="txtUserName" runat="server" Text="" CssClass="txtUser"></asp:TextBox></li>
        <li> <asp:Label ID="lblPass" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="txtPass" runat="server" Text="" CssClass="txtPass"></asp:TextBox></li>
       <li> <asp:Label ID="lblAddAdmin" runat="server" Text="Admin?"></asp:Label>
            <asp:DropDownList ID="ddlAddAdmin" runat="server" CssClass="ddlAdmin">
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem Selected="True">No</asp:ListItem>
            </asp:DropDownList></li>
            </ul>
            

        <table class="buttonGroup cols-2" id="addButtons" runat="server">
            <tr>
                <td>
                    <a href="users.aspx" class="cancel">Cancel</a>
                </td>
                <td>
                    <asp:LinkButton ID="btnAddSave" runat="server" Text="Save" CssClass="submit" OnClick="btnAddSave_Click" 
               OnClientClick="return confirm('Are you sure you would like to add this user?');"/>
                </td>
            </tr>
        </table>
            </div>
</asp:Content>

