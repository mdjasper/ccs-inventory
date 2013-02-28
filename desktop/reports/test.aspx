<%@ Page Title="" Language="C#" MasterPageFile="~/desktop/MasterPage.master" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="desktop_reports_test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">


    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <table>
        <tr>
            <td><p>Avaliable Options</p></td>
            <td></td>
            <td><p>Chosen Options</p></td>
        </tr>
        <tr>
            <td rowspan="4"><asp:ListBox Height="250" Width="200" ID="lstAvailableItems" runat="server" SelectionMode="Multiple" ></asp:ListBox></td>
                    <td><asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" /></td>
            <td rowspan="4"><asp:ListBox Height="250" Width="200" ID="lstChosenItems" runat="server" SelectionMode="Multiple" ></asp:ListBox></td>
        </tr>

        <tr>
            <td><asp:Button runat="server" ID="btnRemove" Text="Remove" /></td>
        </tr>
    </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

