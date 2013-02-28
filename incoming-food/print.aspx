<%@ Page Language="C#" AutoEventWireup="true" CodeFile="print.aspx.cs" Inherits="container_print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>

        #printarea {
            border:2px solid #000;
            width:600px;
            margin:30px auto;
            padding:5px;
        }

        p
        {
            margin:0px;
        }

        .label
        {

             font-weight:bold;
             text-align:left;
        }

        .info
        {
            border-bottom:1px solid black;
            margin-left:5px;
            text-align:left;
        }

        .center
        {
            text-align:center;
        }

        table
        {
           border-collapse:collapse;
           margin:10px 5px;
        }

        td
        {
            border: 1px solid black;
            height:14px;
        }


        .submit{
	        -moz-box-shadow:inset 0px 1px 0px 0px #c1ed9c;
	        -webkit-box-shadow:inset 0px 1px 0px 0px #c1ed9c;
	        box-shadow:inset 0px 1px 0px 0px #c1ed9c;
	        background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #9dce2c), color-stop(1, #8cb82b) );
	        background:-moz-linear-gradient( center top, #9dce2c 5%, #8cb82b 100% );
	        filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#9dce2c', endColorstr='#8cb82b');
	        background-color:#9dce2c;
	        border:1px solid #83c41a;
	        display:block;
            margin:0px auto;
	        color:#ffffff;
	        font-family:arial;
	        font-size:36px;
	        font-weight:bold;
	        padding:6px 24px;
	        text-decoration:none;
	        text-shadow:1px 1px 0px #689324;
            text-align:center;
            width:400px;
        }
        .submit:hover {
	        background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #8cb82b), color-stop(1, #9dce2c) );
	        background:-moz-linear-gradient( center top, #8cb82b 5%, #9dce2c 100% );
	        filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#8cb82b', endColorstr='#9dce2c');
	        background-color:#8cb82b;
        }
        .submit:active {
	        position:relative;
	        top:1px;
        }

        
        @media print
        {
    	    .hide{ display: none; }
        }
    </style>
</head>
<body onload="window.print()">
    <form id="form1" runat="server">
    <a href="<%= Config.DOMAIN() %>incoming-food/" class="submit hide"> Back to Incoming Food</a>
    <div id="printarea" style="font-family:Calibri">
         <div class="center"><img  src="../images/ccs-logo.jpg" /> </div>
        <p class="center"><strong>745 East South ♦ Salt Lake City, UT. 84102</strong></p>
        <p class="center"><strong>(801)977-9119 ♦ Fax (801) 977-8227</strong></p>
        <br />
        <div id="donor-info">
            <span class="label">Donor</span><asp:Label runat="server" CssClass="info" ID="lblDonor"  Width="550px"/>
            <br />
            <br />
            <span class="label">Address</span><asp:Label runat="server" CssClass="info" ID="lblAddress" Width="540px" />
            <br />
            <br />
            <span class="label">City</span><asp:Label runat="server" CssClass="info" ID="lblCity" Width="300px" />
            <span class="label">State</span><asp:Label runat="server" CssClass="info" ID="lblState"  Width="80px" />
            <span class="label">Zip</span><asp:Label runat="server" CssClass="info" ID="lblZip"  Width="100px" />
            <br />
            <br />

            <span class="label">Email</span><asp:Label runat="server" CssClass="info" ID="lblEmail"   Width="250px"/>
            <span class="label">Phone</span><asp:Label runat="server" CssClass="info" ID="lblPhone"   Width="250px"/>
        </div>

        <table> 
            <tr>
                <th style="width:150px">Quantity\Weight</th>
                <th style="width:300px">Description</th>
                <th style="width:150px">Value</th>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lblWeight" /></td>
                <td><asp:Label runat="server" ID="lblDescription" /></td>
                <td><asp:Label runat="server" ID="Label4" /></td>
            </tr>
            <tr><td></td><td></td><td></td></tr>
            <tr><td></td><td></td><td></td></tr>
            <tr><td></td><td></td><td></td></tr>
            <tr><td></td><td></td><td></td></tr>
            <tr><td></td><td></td><td></td></tr>
        </table>

        <div id="volunteer-hours">
            <span class="label">Number of Volunteers</span><asp:Label runat="server" CssClass="info" ID="lblNumofVolunteers" Width="75px" />
            <span class="label">Total Volunteer Hours</span><asp:Label runat="server" CssClass="info" ID="lblTotalVolunteerHours" Width="75px" />
            <br />
             <br />
            <span class="label">Estimated Value (<em>Determined by Doner</em>)</span><asp:Label runat="server" CssClass="info" ID="lblEstimatedValue" Width="75px"/>
        </div>
        <br />


       <span class="label">Taxable</span><asp:Label runat="server" CssClass="info" ID="Label1"  Width="75px"/>
        <span>(Donor <strong>paid</strong> taxes on donation)</span>
        <br />
        <br />
        <span class="label">Non-Taxable</span><asp:Label runat="server" CssClass="info" ID="Label2" Width="75px" />
        <span>(Donor did <strong>not pay</strong> taxes on donation)</span>

        <br />
        <br />
        <br />

        <span class="label">Received By</span><asp:Label runat="server" CssClass="info" ID="lblRecievedBy" Width="300px" />
        <span class="label">Date</span><asp:Label runat="server" CssClass="info" ID="lblDateRecived" Width="150px" />

        <br />

        <p class="center">
            Thank you for your donation! You are helping Catholic Communty Services of Utah, a 501c3 charity, help hundreds of
            thousands of needy people annually. In-kind donations are deductible, to the extent allowed by law, for income tax purposes.
        </p>

        <br />
        <p class="center"> <strong>Catholic Community Services of Utah</strong></p>
        <p class="center"><strong> Refuge Resettlement ♦ Treatment Service ♦ Basic Needs Services</strong></p>
        <p class="center"><strong>Northern Utah</strong></p>
    </div>
    </form>
</body>
</html>
