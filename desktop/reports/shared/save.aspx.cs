using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_reports_incoming_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["reportTemplateRow"] == null || Session["reportTemplate"] == null)
                    Response.Redirect(Config.DOMAIN() + "desktop/reports");

                txtTitle.Text = ((Template)Session["reportTemplateRow"]).TemplateName;
            
                Template reportTemplate = (Template)Session["reportTemplateRow"];

                if (reportTemplate.TemplateID > 0)
                {
                    lblQuest.Text = "Would you like to save the changes you made to this template?";
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Template reportTemplate = (Template)Session["reportTemplateRow"];
            reportTemplate.TemplateName = txtTitle.Text;
            reportTemplate.LastUpdated = DateTime.Now;

            string data = "";

            switch (reportTemplate.TemplateType)
            {
                case ((int)ReportTemplate.ReportType.GroceryRescue):
                    data = ((GroceryRescueReportTemplate)Session["reportTemplate"]).Serialize<GroceryRescueReportTemplate>();
                    break;
                case ((int)ReportTemplate.ReportType.Incoming):
                    data = ((IncomingReportTemplate)Session["reportTemplate"]).Serialize<IncomingReportTemplate>();
                    break;
                case ((int)ReportTemplate.ReportType.Outgoing):
                    data = ((OutgoingReportTemplate)Session["reportTemplate"]).Serialize<OutgoingReportTemplate>();
                    break;
                case ((int)ReportTemplate.ReportType.InOut):
                    data = ((InOutReportTemplate)Session["reportTemplate"]).Serialize<InOutReportTemplate>();
                    break;
                case ((int)ReportTemplate.ReportType.Inventory):
                    data = ((InventoryReportTemplate)Session["reportTemplate"]).Serialize<InventoryReportTemplate>();
                    break;
            }

            int userID = int.Parse(Session["UserId"].ToString());

            using (CCSEntities db = new CCSEntities())
            {
                User user = db.Users.Single(u => u.UserID == userID);
                reportTemplate.LastUpdatedBy = user.FirstName + " " + user.LastName;
            }


            reportTemplate.Data = data;
            using (CCSEntities db = new CCSEntities())
            {
                db.Templates.Attach(reportTemplate);

                if (reportTemplate.TemplateID > 0)
                {
                    db.Templates.Attach(reportTemplate);
                    db.Entry(reportTemplate).State = System.Data.EntityState.Modified;
                }
                else
                {
                    db.Entry(reportTemplate).State = System.Data.EntityState.Added;
                    db.Templates.Add(reportTemplate);
                }

                db.SaveChanges();

            }
            LogChange.logChange(String.Format("Saved Report Template : {0}", reportTemplate.TemplateName), DateTime.Now, short.Parse(Session["userID"].ToString()));

            lblResponse.Text = "Template Successfully Saved";
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
            Template reportTemplate = (Template)Session["reportTemplateRow"];
            reportTemplate.TemplateName = txtTitle.Text;
            Session["reportTemplateRow"] = reportTemplate;

            string urlRedirect = null;

            if ((ReportTemplate.ReportType)reportTemplate.TemplateType == ReportTemplate.ReportType.Inventory)
                urlRedirect = Config.DOMAIN() + "desktop/reports/inventory/displayinventoryreport.aspx";
            else
                urlRedirect = Config.DOMAIN() + "desktop/reports/shared/selectdates.aspx";

            Response.Redirect(urlRedirect);
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }
}