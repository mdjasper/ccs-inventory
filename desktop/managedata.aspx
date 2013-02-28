<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="managedata.aspx.cs" Inherits="desktop_managedata" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script src="../js/jquery-ui-1.10.0.custom.min.js" type="text/javascript"></script>
    <script src="FoodService.ashx" type="text/javascript"></script>
    <link href="../css/ui-lightness/jquery-ui-1.10.0.custom.min.css" rel="stylesheet" />
    
    <script>
        $(document).ready(function () {
            $('#<%= txtSearchFoodIn.ClientID %>').autocomplete({ source: foodNames });
            $('#<%= txtSearchFoodOut.ClientID %>').autocomplete({ source: foodNames });
        });
    </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" runat="Server">
    <img src="../images/icon-person.png" />Manage Data
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" runat="Server">
    
    <div id="tablechoice">
        <asp:Button ID="btnFoodIn" runat="server" Text="Food In" OnClick="btnFoodIn_Click"  ControlStyle-CssClass="btnTablechoice"/>
        <asp:Label ID="lblBtnFoodIn" runat="server" Text="Food In" Visible="false" ControlStyle-CssClass="lblTablechoice"></asp:Label>
        <asp:Button ID="btnFoodOut" runat="server" Text="Food Out" OnClick="btnFoodOut_Click"  ControlStyle-CssClass="btnTablechoice"/>
        <asp:Label ID="lblBtnFoodOut" runat="server" Text="Food Out" Visible="false" ControlStyle-CssClass="lblTablechoice"></asp:Label>
    </div>

   
    <div class="searchNotify">
        <asp:Label ID="lblResponse" runat="server" Text=""  CssClass="notification" Visible="False"></asp:Label>
    </div>
   
    <asp:Panel id="searchFoodInDiv" runat="server" Visible="false" >
        <div class="left">
            <asp:Label ID="lblFoodInSearch" runat="server" Text="Search Food by Category"></asp:Label>
        </div>
            <asp:TextBox ID="txtSearchFoodIn" runat="server" ControlStyle-CssClass="searchBox"></asp:TextBox>
            <asp:Button ID="btnSearchFoodIn" runat="server" Text="Search" ControlStyle-CssClass="btnSearch" OnClick="btnSearchFoodIn_Click"/>
    </asp:Panel>


    <asp:Panel id="searchFoodOutDiv" runat="server" Visible="false" >
        <div class="left">
            <asp:Label ID="lblSearchFoodOut" runat="server" Text="Search Food by Category"></asp:Label>
        </div>
            <asp:TextBox ID="txtSearchFoodOut" runat="server" ControlStyle-CssClass="searchBox"></asp:TextBox>
            <asp:Button ID="btnSearchFoodOut" runat="server" Text="Search" ControlStyle-CssClass="btnSearch" OnClick="btnSearchFoodOut_Click"/>
     </asp:Panel>



    <div id="foodInDiv" runat="server">
         <asp:GridView ID="grdFoodInData" runat="server"
            AutoGenerateColumns="false" DataKeyNames="FoodInID"
            AllowPaging="true" PageSize="5"
            OnRowCommand="grdFoodInData_RowCommand"
            OnRowDeleting="grdFoodInData_RowDeleting"
            OnPageIndexChanging="grdFoodInData_PageIndexChanging"
            HeaderStyle-HorizontalAlign="Left"
            CssClass="dataTable"
            CellSpacing="8" >
        <Columns>

             <asp:TemplateField HeaderText="Food In ID">
                <ItemTemplate>
                    <asp:Label ID="lblFoodInID" runat="server" Text='<%# Bind("FoodInID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Food Category">
                <ItemTemplate>
                    <asp:Label ID="lblFoodCatType" runat="server" Text='<%# Bind("CategoryType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Weight">
                <ItemTemplate>
                    <asp:Label ID="lblWeight" runat="server" Text='<%# Bind("Weight") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Source">
                <ItemTemplate>
                    <asp:Label ID="lblSource" runat="server" Text='<%# Bind("Source") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Time Stamp">
                <ItemTemplate>
                    <asp:Label ID="lblTimeStamp" runat="server" Text='<%# Bind("TimeStamp") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:ButtonField ButtonType="button" CommandName="editData" runat="server" Text="Edit"  ControlStyle-CssClass="submit"></asp:ButtonField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnFoodInDelete" Text="Delete"
                            CommandName="delete" OnClientClick="return confirm('Are you sure you would like to remove this data?');"
                            CommandArgument='<%# Container.DataItemIndex %>'  ControlStyle-CssClass="delete"/>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        </asp:GridView>
        <asp:Label ID="lblFoodInError" runat="server" Text=""></asp:Label>

    </div>

    <div id="foodOutDiv" runat="server">   
         <asp:GridView ID="grdFoodOutData" runat="server"
            AutoGenerateColumns="false" DataKeyNames="DistributionID"
            AllowPaging="true" PageSize="5"
            OnRowCommand="grdFoodOutData_RowCommand"
            OnRowDeleting="grdFoodOutData_RowDeleting"
            OnPageIndexChanging="grdFoodOutData_PageIndexChanging"
            HeaderStyle-HorizontalAlign="Left"
            CssClass="dataTable"
            CellSpacing="8" >
        <Columns>

            <asp:TemplateField HeaderText="Distribution ID">
                <ItemTemplate>
                    <asp:Label ID="lblDistributionID" runat="server" Text='<%# Bind("DistributionID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Food Category">
                <ItemTemplate>
                    <asp:Label ID="lblFoodCatType" runat="server" Text='<%# Bind("CategoryType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="USDA ID">
                <ItemTemplate>
                    <asp:Label ID="lblUSDAID" runat="server" Text='<%# Bind("USDANumber") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Weight">
                <ItemTemplate>
                    <asp:Label ID="lblWeight" runat="server" Text='<%# Bind("Weight") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Count">
                <ItemTemplate>
                    <asp:Label ID="lblCount" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Time Stamp">
                <ItemTemplate>
                    <asp:Label ID="lblTimestamp" runat="server" Text='<%# Bind("TimeStamp") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:ButtonField ButtonType="button" CommandName="editData" runat="server" Text="Edit"  ControlStyle-CssClass="submit"></asp:ButtonField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnFoodOutDelete" Text="Delete"
                            CommandName="delete" OnClientClick="return confirm('Are you sure you would like to remove this data?');"
                            CommandArgument='<%# Container.DataItemIndex %>'  ControlStyle-CssClass="delete"/>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        </asp:GridView>
        <asp:Label ID="lblFoodOutError" runat="server" Text=""></asp:Label>

    </div>
</asp:Content>

