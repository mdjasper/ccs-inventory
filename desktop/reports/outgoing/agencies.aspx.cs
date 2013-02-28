using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_reports_incoming_Agencies : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["reportTemplateRow"] == null || Session["reportTemplate"] == null)
                    Response.Redirect(Config.DOMAIN() + "desktop/reports");

                OutgoingReportTemplate template = (OutgoingReportTemplate)Session["reportTemplate"];

                using (CCSEntities db = new CCSEntities())
                {
                    lstAgencies.SelectedType = template.AgenciesSelection;
                    lstAgencies.AvailableList = (from f in db.Agencies
                                                    select new { ID = f.AgencyID, Display = f.AgencyName }).ToList();

                    lstAgencies.SelectedIDs = template.Agencies;
                }

            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            OutgoingReportTemplate template = (OutgoingReportTemplate)Session["reportTemplate"];
            Template row = (Template)Session["reportTemplateRow"];
            template.Agencies = lstAgencies.SelectedIDs;
            template.AgenciesSelection = lstAgencies.SelectedType;

            Session["reportTemplate"] = template;
            Response.Redirect(Config.DOMAIN() + "desktop/reports/shared/food_usda.aspx");
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }
}