<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="users.aspx.cs" Inherits="desktop_users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" runat="Server">
    <img src="../images/icon-person.png" />Manage Users
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" runat="Server">
    <div>   

         <asp:GridView ID="grdUsers" runat="server"
            AutoGenerateColumns="false" DataKeyNames="UserID"
            AllowPaging="true" PageSize="5"
            OnRowCommand="grdUsers_RowCommand"
            OnRowDeleting="grdUsers_RowDeleting"
            OnPageIndexChanging="grdUsers_PageIndexChanging"
            HeaderStyle-HorizontalAlign="Left"
            CssClass="dataTable"
            CellSpacing="8" >
        <Columns>

             <asp:TemplateField HeaderText="User ID">
                <ItemTemplate>
                    <asp:Label ID="lblUserID" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="User Name">
                <ItemTemplate>
                    <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="First Name">
                <ItemTemplate>
                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Last Name">
                <ItemTemplate>
                    <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Admin?">
                <ItemTemplate>
                    <asp:Label ID="lblAdmin" runat="server" Text='<%# Bind("Admin") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:ButtonField ButtonType="button" CommandName="editUser" runat="server" Text="Edit"  ControlStyle-CssClass="submit"></asp:ButtonField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnDelete" Text="Delete"
                            CommandName="delete" OnClientClick="return confirm('Are you sure you would like to remove this user?');"
                            CommandArgument='<%# Container.DataItemIndex %>'  ControlStyle-CssClass="delete"/>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        </asp:GridView>
          <ul class="vertical-list">
        <li><asp:LinkButton ID="btnAddUser" runat="server" Text="Add User" CssClass="submit" OnClick="btnAddUser_Click" /></li>
        </ul>
        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>

    </div>
</asp:Content>
