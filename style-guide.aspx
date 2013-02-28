<%@ Page Title="" Language="C#" MasterPageFile="~/mobile.master" AutoEventWireup="true" CodeFile="style-guide.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_PageTitle" Runat="Server">
    Mobile Style Guide
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolder_Content" Runat="Server">

    <h1>Header 1</h1>
    <h2>Header 2</h2>

    <p>This brief style guide showcases very frequently used items, and their css classes. Keeping HTML markup
        simple and applying pre-existing classes will simplify and speed up development. This is a living document,
        please share in editing it!
    </p>
    
    <h2>DataTable/Gridview class="dataTable"</h2>

    <table class="dataTable" >
		<tbody>
            <tr>
			    <th scope="col">Location</th><th scope="col">Bin Number</th><th scope="col">USDA?</th><th scope="col">Number of Cases (USDA)</th>
		    </tr>
            <tr>
			    <td>South Room</td><td>5647</td><td><span class="aspNetDisabled"><input type="checkbox" checked="checked"></span></td><td>20</td>
		    </tr>
            <tr style="background-color:#FFFFCC;">
			    <td>South Room</td><td>4683</td><td><span class="aspNetDisabled"><input type="checkbox"></span></td><td>&nbsp;</td>
		    </tr>
            <tr>
			    <td>South Room</td><td>4683</td><td><span class="aspNetDisabled"><input type="checkbox"></span></td><td>&nbsp;</td>
		    </tr>
	    </tbody>
    </table>
    <h2>Horizontal button group</h2>
    <table class="buttonGroup cols-2">
        <tr>
            <td><a href="#" class="submit">Save</a></td>
            <td><a href="#" class="cancel">Cancel</td>
        </tr>
    </table>

    <table class="buttonGroup cols-3">
        <tr>
            <td><a href="#" class="submit">Save</td>
            <td><a href="#" class="cancel">Cancel</a></td>
            <td><a href="#" class="delete">Delete</td>
        </tr>
    </table>

    <h2>ul class="verticle-list"</h2>

    <ul class="vertical-list">
        <li><a href="#" class="button text-left"><img src="images/icon-container.png" />Scan Container</a></li>
        <li><a href="#" class="button text-left"><img src="images/icon-locate.png" />Locate Container</a></li>
        <li><a href="#" class="button text-left"><img src="images/gear.gif" />Advanced Settings</a></li>
        <li><a href="#" class="button text-left"><img src="images/icon-desktop.png" />Desktop Portal</a></li>
    </ul>

    <h2>table class="form"</h2>

    <table class="form">
       <tbody>
           <tr>
             <td>Text input</td>
             <td><input value="Text Input" type="text" /></td>
          </tr>
          <tr>
             <td>Number input</td>
             <td><input value="300" type="number" /></td>
          </tr>
          <tr>
             <td>Dropdown</td>
             <td>
                <select>
                   <option selected="selected" value="5">Bakery</option>
                   <option value="1">Beans</option>
                </select>
             </td>
          </tr>
       </tbody>
    </table>

</asp:Content>

