<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="editfood.aspx.cs" Inherits="desktop_editfood" %>
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
    <div id="editFoodInDiv" runat ="server">  
         
            <ul class="vertical-list">
                <li>
                    <div class="left">
                    <asp:Label ID="lblEditFoodInID" runat="server" Text="Food In ID"></asp:Label>
                    </div>
                    <asp:TextBox ID="txtEditFoodInID" runat="server" Enabled="false" Text="" CssClass="txtEditFood"></asp:TextBox>
                </li>
                <li>
                    <div class="left">
                    <asp:Label ID="lblEditFoodInCategoryType" runat="server" Text="Category Type"></asp:Label>
                    </div>
                    <asp:DropDownList ID="ddlFoodInCategoryType" runat="server" DataSourceID="sqlDDLFoodInCategoryType" 
                        DataTextField="CategoryType" DataValueField="FoodCategoryID" CssClass="ddlEditFood"></asp:DropDownList>
                    <asp:SqlDataSource ID="sqlDDLFoodInCategoryType" runat="server" ConnectionString="<%$ ConnectionStrings:CCSTestDBConnectionString %>" 
                        SelectCommand="SELECT * FROM [FoodCategory]"></asp:SqlDataSource>
                  
                </li>
                <li>
                    <div class ="left">
                    <asp:Label ID="lblEditFoodInWeight" runat="server" Text="Weight" ></asp:Label>
                    </div>
                    <asp:TextBox ID="txtEditFoodInWeight" runat="server" Text="" CssClass="weight"></asp:TextBox>
                </li>
                <li>
                    <div class ="left">
                    <asp:Label ID="lblEditFoodInSource" runat="server" Text="Source"></asp:Label>
                    </div>
                    <asp:DropDownList ID="ddlEditFoodInSource" runat="server" DataSourceID="sqlDDLEditFoodInSource" 
                        DataTextField="Source" DataValueField="FoodSourceID" CssClass="ddlSource"></asp:DropDownList>
                    <asp:SqlDataSource ID="sqlDDLEditFoodInSource" runat="server" ConnectionString="<%$ ConnectionStrings:CCSTestDBConnectionString %>" 
                        SelectCommand="SELECT * FROM [FoodSource]"></asp:SqlDataSource>
                </li>
                <li>
                    <div class ="left">
                    <asp:Label ID="lblEditFoodInTime" runat="server" Text="Time Stamp"></asp:Label>
                    </div>
                    <asp:TextBox ID="txtEditFoodInTime" runat="server" Text="" CssClass="time"></asp:TextBox>
                </li>
            </ul>

        <table class="buttonGroup cols-2">
            <tr>
                <td>
                    <a href="managedata.aspx?do=FI" class="cancel">Cancel</a>
                </td>
                <td>
                    <asp:LinkButton ID="btnEditSave" runat="server" Text="Save" CssClass="submit" OnClick="btnEditFoodInSave_Click" 
                        OnClientClick="return confirm('Are you sure you would like to edit this data?');"/>
                </td>
            </tr>
        </table>

        </div>

        <div id="editFoodOutDiv" runat="server">

        <ul class="vertical-list">
            
        <li>
            <div class="left">
            <asp:Label ID="lblFoodOutDistID" runat="server" Text="Distribution ID"></asp:Label>
            </div>
        <asp:TextBox ID="txtFoodOutDistID" runat="server" Text="" CssClass="txtEditDist" Enabled="false"></asp:TextBox>
        </li>
        <li>
        <div class="left">
            <asp:Label ID="lblFoodOutCatID" runat="server" Text="Food Category"></asp:Label>
                </div>
        <asp:DropDownList ID="ddlFoodOutCategoryType" runat="server" DataSourceID="sqlDDLFoodInCategoryType" 
                        DataTextField="CategoryType" DataValueField="FoodCategoryID" CssClass="ddlFoodOutCat"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CCSTestDBConnectionString %>" 
                        SelectCommand="SELECT * FROM [FoodCategory]"></asp:SqlDataSource>
        </li>
        <li>
        <div class="left">
            <asp:Label ID="lblFoodOutUSDAID" runat="server" Text="USDA ID"></asp:Label>
                </div>
        <asp:TextBox ID="txtFoodOutUSDAID" runat="server" Text="" CssClass="txtEditUSDAID"></asp:TextBox>
        </li>

        <li>
            <div class="left">
            <asp:Label ID="lblFoodOutWeight" runat="server" Text="Weight"></asp:Label>
                 </div>
        <asp:TextBox ID="txtFoodOutWeight" runat="server" Text="" CssClass="txtEditWeight"></asp:TextBox>

        </li>
        <li>
            <div class="left">
            <asp:Label ID="lblFoodOutCount" runat="server" Text="Count"></asp:Label>
                 </div>
        <asp:TextBox ID="txtFoodOutCount" runat="server" Text="" CssClass="txtEditCount"></asp:TextBox>

        </li>
        <li>
        <div class="left">
            <asp:Label ID="lblFoodOutTime" runat="server" Text="Time Stamp"></asp:Label>
                 </div>
        <asp:TextBox ID="txtFoodOutTime" runat="server" Text="" CssClass="txtEditTime"></asp:TextBox>

        </li>
        </ul>    

        <table class="buttonGroup cols-2" runat="server">
            <tr>
                <td>
                    <a href="managedata.aspx?do=FO" class="cancel">Cancel</a>
                </td>
               <td>
                    <asp:LinkButton ID="btnEditFoodOut" runat="server" Text="Save" CssClass="submit" OnClick="btnEditFoodOutSave_Click" 
                        OnClientClick="return confirm('Are you sure you would like to edit this data?');"/>
                </td>
            </tr>
        </table>
            </div>

</asp:Content>
