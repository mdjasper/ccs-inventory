using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_reports_incoming_SelectDate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["reportTemplate"] == null)
            Response.Redirect(Config.DOMAIN() + "desktop/reports");

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (calStart.SelectedDate != null)
        {
            Session["startDate"] = calStart.SelectedDate;
            Response.Redirect("displayInventoryReport?previousPage=" + HttpContext.Current.Request.Url.AbsoluteUri);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if(Request.QueryString["previousPage"] == null)
            Response.Redirect("default.aspx");

        string previousPage = Request.QueryString["previousPage"];
        Response.Redirect(previousPage);
    }
}