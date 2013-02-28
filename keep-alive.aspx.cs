using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class keep_alive : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Add refresh header to refresh the page 60 seconds before session timeout
            Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) - 60));
        }
        catch (Exception)
        {
            throw;
        }
    }
}