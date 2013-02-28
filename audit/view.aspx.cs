using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View : System.Web.UI.Page
{
    private short id;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindGridView();
    }

    private void bindGridView()
    {
        try
        {
            if (short.TryParse(Request.QueryString["id"], out id))
            {
                List<Adjustment> lstAdjustment;
                using (CCSEntities db = new CCSEntities())
                {
                    lstAdjustment = db.Adjustments.Where(x => x.AuditID == id).ToList();
                }
                gvAdjustments.DataSource = lstAdjustment;
                gvAdjustments.DataBind();
            }
            else
                Response.Redirect("default.aspx", false);
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // Paging the gridView
    protected void gvAdjustments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAdjustments.PageIndex = e.NewPageIndex;
            bindGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }
}