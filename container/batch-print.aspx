<%@ Page Language="C#" AutoEventWireup="true" CodeFile="batch-print.aspx.cs" Inherits="container_batch_print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        h1 {
            font-size:55px;
            margin:.5em 0 0 0;
        }
        h2 {
            margin:0 0 1em 0;
            font-size:24px;
        }
        #printarea {
            border:2px solid #000;
            width:600px;
            margin:30px auto;
            page-break-after:always;
        }
    </style>
</head>
<body onload="window.print()">
    <form id="form1" runat="server">
    <div>
    <asp:Repeater ID="rptLabels" runat="server">
        <ItemTemplate>
            <div id="printarea" style="text-align:center; font-family:Calibri">
                <h1><asp:Label ID="lblBinNumber" runat="server" Text='<%# Eval("ID") %>'></asp:Label></h1>
                <h2><asp:Label ID="lblCategorgy" runat="server" Text='<%# Eval("Type") %>'></asp:Label></h2>
                <img src='<%# Eval("img") %>' runat="server" id="image" />
                <h2><asp:Label ID="lblDate" runat="server" Text='<%# Eval("date") %>'></asp:Label></h2>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    </div>
    </form>
</body>
</html>
