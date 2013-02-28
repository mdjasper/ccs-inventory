using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class viewChangeLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //LogChange.logChange("This is a test", DateTime.Now, 4);
        bindGridView();
    }

    // Paging the gridView
    protected void gvChangeLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvChangeLog.PageIndex = e.NewPageIndex;
        bindGridView();
    }

    private void bindGridView()
    {
        try
        {

            using (CCSEntities db = new CCSEntities())
            {
                var changeLogs = (from a in db.Logs
                                  select new { a.Description, a.Date, UserName = a.User.FirstName + " " + a.User.LastName }).OrderByDescending(x => x.Date);

                gvChangeLog.DataSource = changeLogs.ToList();
                gvChangeLog.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "An unexpected problem occurred. Please try again later.";

            LogError.logError(ex);
        }
    }
}