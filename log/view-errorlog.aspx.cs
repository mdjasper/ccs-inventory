using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class viewErrorLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bindGridView();
    }

    // Paging the gridView
    protected void gvErrorLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvErrorLog.PageIndex = e.NewPageIndex;
        bindGridView();
    }

    private void bindGridView()
    {
        try
        {

            using (CCSEntities db = new CCSEntities())
            {
                var errorLogs = (from a in db.ErrorLogs
                              select a).OrderByDescending(x => x.TimeStamp);

                gvErrorLog.DataSource = errorLogs.ToList();
                gvErrorLog.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "An unexpected problem occurred: " + ex.Message;
        }
    }
}