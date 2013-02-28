<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="404.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    Page not found
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <center>
        <h2>The requested page has not been found.</h2>
        <p>
            Go <a href="javascript:history.back()" class="cancel">back</a> to the previous page,
        </p>
        <p>
            or return to the <a href="../default.aspx" class="cancel">main menu</a>.
        </p>
    </center>
</asp:Content>

