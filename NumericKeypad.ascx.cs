using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NumericKeypad : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    public float Value
    {
        get
        {
            float Num;
            if (txtValue.Text == "")
            {
                return 0f;
            }
            else if (!(float.TryParse(txtValue.Text, out Num))) //ensures that value is a number
            {
                return 0f;
            }
            else 
            {
                return float.Parse(txtValue.Text);
            }
        }
        set
        {
            txtValue.Text = this.Value.ToString();
        }
    }

}