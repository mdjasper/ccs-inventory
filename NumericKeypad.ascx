<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NumericKeypad.ascx.cs" Inherits="NumericKeypad" %>
<style>
    #numeric_buttons input[type="button"] {
        width:100%;
        padding:20px;
        margin:0px;
        font-size:24px;
    }
    #numeric_buttons {
        width:100%;
    } 
    .numeric_value{
        width:100%;
    }
    #txtValue
    {
        font-size:24px;
    }
</style>
<div>
    <table id="numeric_buttons">
        <tr>
            <td colspan="3">
                <asp:TextBox ID="txtValue" runat="server" CssClass="numeric_value" type="number" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><input type="button" class="button" value="7" /></td>
            <td><input type="button" class="button" value="8" /></td>
            <td><input type="button" class="button" value="9" /></td>
        </tr>
        <tr>
            <td><input type="button" class="button" value="4" /></td>
            <td><input type="button" class="button" value="5" /></td>
            <td><input type="button" class="button" value="6" /></td>
        </tr>
        <tr>
            <td><input type="button" class="button" value="1" /></td>
            <td><input type="button" class="button" value="2" /></td>
            <td><input type="button" class="button" value="3" /></td>
        </tr>
        <tr>
            <td><input type="button" class="button" value="0" /></td>
            <td><input type="button" class="button" value="." /></td>
            <td><input type="button" class="button" value="&larr;" /></td>
        </tr>
    </table>
 </div>
<script>
    var numericValue = $(".numeric_value");

    numericValue.focus().change(numericChangeEvent).keyup(numericChangeEvent);
    // assign the function to change and keyup events

    $("#numeric_buttons .button").click(function ()
    {
        var toAdd = $(this).val();
        var originalValue = numericValue.val();
        var len = originalValue.length;

        if (toAdd === '?')
        {
            numericValue.val(originalValue.slice(0, len - 1));
        } else
        {
            if (toAdd === '.')
            {
                if (originalValue.indexOf('.') === -1)
                {
                    numericValue.val(originalValue + toAdd);
                }
            } else
            {
                numericValue.val(originalValue + toAdd);

                numericValue.change();
            }
        }
    });

    // called when a change() is called or a keyup event is fired
    function numericChangeEvent()
    {
        if (numericValue.val().length >= 4) // if the length is 4 or greater, submit the form
            $(".submit").click();
    }

</script>