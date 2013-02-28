using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class container_menu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            int id = int.Parse(Request.QueryString["id"]);

            using (CCSEntities db = new CCSEntities())
            {
                Container c = (from co in db.Containers
                               where co.BinNumber == id
                               select co).First();
                if (c != null)
                {
                    btnEdit.NavigateUrl = "edit.aspx?id=" + id;
                    btnPrint.NavigateUrl = "print-label.aspx?id=" + id;
                    btnMoveOut.NavigateUrl = "out.aspx?id=" + id;

                    lblBinNumber.Text = id.ToString();
                }
                else
                {
                    //bad id
                    lblError.Text = "Error: The requested bin number does not exist.";
                    lblError.Visible = true;
                }
            }

            }
        else
        {
            //no id
            lblError.Text = "Error: No bin number was requested";
            lblError.Visible = true;
            
        }
    }
}