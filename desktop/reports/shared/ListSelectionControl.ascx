<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ListSelectionControl.ascx.cs" Inherits="desktop_reports_shared_ListSelectionControl"  %>


<style>
    #tableSelectItems, #checkboxes
    {
        margin:0, auto;
        text-align:center;
    }

    button
    {
        width:75px;
        margin:50px 0px;
    }

    table { margin: auto; }

    p
    {
        font-size:14px;
        padding:0px;
        margin:0px;
        text-align:center;
        font-weight:bold;
    }
</style>



<h1><%= Title %></h1>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div id="checkboxes_<%= ControlNumber.ToString() %>">
        <asp:CheckBox runat="server" Text="All" ID="chkAll"  OnCheckedChanged="CheckChanged" AutoPostBack="true"/> <asp:CheckBox ID="chkNone" runat="server" Text="None" Visible="false" OnCheckedChanged="CheckChanged" AutoPostBack="true"/>
        <asp:Panel runat="server" ID="pnlFoodTypes" Visible ="false" OnCheckedChanged="CheckChanged" AutoPostBack="true">
           <asp:CheckBox ID="chkRegular" runat="server" Text="Regular" OnCheckedChanged="CheckChanged" AutoPostBack="true" />
           <asp:CheckBox ID="chkPerishable" runat="server" Text="Perishables" OnCheckedChanged="CheckChanged" AutoPostBack="true"/>
            <asp:CheckBox ID="chkNonFood" runat="server" Text="Non-Food" OnCheckedChanged="CheckChanged" AutoPostBack="true"/>
        </asp:Panel>
</div>
<div runat="server" id="tableSelectItems" visible="false">
  
    <table>
        <tr>
            <td><p>Avaliable Options</p></td>
            <td></td>
            <td><p>Chosen Options</p></td>
        </tr>
        <tr>
         <td rowspan="4"><asp:ListBox Height="250" Width="200" ID="lstAvailableItems" runat="server" SelectionMode="Multiple" DataValueField="ID" DataTextField="Display"  ></asp:ListBox></td>
                    <td><asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" /></td>
            <td rowspan="4"><asp:ListBox Height="250" Width="200" ID="lstChosenItems" runat="server" SelectionMode="Multiple" DataValueField="ID" DataTextField="Display" ></asp:ListBox></td>
        </tr>

        <tr>
            <td><asp:Button runat="server" ID="btnRemove" Text="Remove" OnClick="btnRemove_Click" /></td>
        </tr>
    </table>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>


<script>
    $(document).ready(function () {

        var chkAllID = '#<%= FindControl("chkAll").ClientID %>';
        var chkNoneID = '#<%= FindControl("chkNone").ClientID %>';
        var tblSelectedItems = '#tableSelectItems_<%= ControlNumber.ToString() %>';
        var checkboxArea = '#checkboxes_<%= ControlNumber.ToString() %>';

        $(checkboxArea + ' input[type=checkbox]').each(function () {
            if($(this).is(':checked'))
                $(tblSelectedItems).hide();
        });


        $(checkboxArea + ' input[type=checkbox]').each(function () {
            console.log("1");
            if ($(this).is(':checked'))
                $(tblSelectedItems).hide();
        });

        $(checkboxArea + ' input[type=checkbox]').click(function () {
            var checked = $(this).is(':checked');
            $(checkboxArea + ' input[type=checkbox]').each(function () {
                $(this).prop('checked', false);
            });

            if (checked) {

                $(tblSelectedItems).slideUp("fast");
                $(this).prop('checked', true);

            } else {
                $(tblSelectedItems).slideDown("fast");
            }
        });


    });
</script>

