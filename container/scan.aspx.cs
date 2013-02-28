using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtValue.Focus();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Container container=null;
        try
        {
            short binNumber = short.Parse(txtValue.Text);
            

            using (CCSEntities db = new CCSEntities())
            {
                container = (from c in db.Containers
                             where c.BinNumber == binNumber
                             select c).FirstOrDefault();
            }

            if (container != null)
            {
                Response.Redirect("menu.aspx?id=" + container.BinNumber);
            }
            else
            {
                lblError.Text = "Invalid Bin Number!";
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }
}