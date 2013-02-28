<%@ Page Title="" Language="C#" MasterPageFile="~/desktop/MasterPage.master" AutoEventWireup="true" CodeFile="choosetemplate.aspx.cs" Inherits="desktop_reports_shared_choosetemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">
    <style>


        .submit
        {
            margin:20px auto;
            display:block;
        }

        .center-text 
        {
            text-align:center;
        }
    </style>
    <h1><% Response.Write(ReportTemplate.ReportTypeNames[int.Parse(Request.QueryString["reportType"])]); %></h1>
   <asp:Panel runat="server" ID="pnlTemplate">
       <h2 class="center-text">Select a Template:</h2>
        <asp:GridView ID="grdTemplates" runat="server"
                AutoGenerateColumns="false" DataKeyNames="TemplateID"
                AllowPaging="true" PageSize="10"
                OnRowCommand="grdTemplates_RowCommand"
                CssClass="mGrid templates" 
                HeaderStyle-HorizontalAlign="Center"
                CellSpacing="8" HorizontalAlign="Center">
                <Columns>

                     <asp:TemplateField HeaderText="Template Name">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("TemplateName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Last Updated By">
                        <ItemTemplate>
                            <asp:Label ID="lblLastUpdatedBy" runat="server" Text='<%# Bind("LastUpdatedBy") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Last Updated">
                        <ItemTemplate>
                            <asp:Label ID="lblLastUpdated"  runat="server" Text='<%# Bind("LastUpdated") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:ButtonField ButtonType="button" CommandName="runTemp" runat="server" Text="Run"></asp:ButtonField>

                    <asp:ButtonField ButtonType="button" CommandName="editTemp" runat="server" Text="Edit"></asp:ButtonField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="Button1" runat="server" Text="Delete"
                                    CommandName="deleteTemp" OnClientClick="return confirm('Are you sure you would like to remove this template?');"
                                    CommandArgument='<%# Container.DisplayIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>
            </asp:GridView>
         <h1>OR</h1>
       </asp:Panel>
     <asp:Button ID="btnNext" runat="server" CssClass="submit" Text="Create New Report" OnClick="btnNext_Click" />
</asp:Content>

