using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_reports_incoming_OutgoingReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["reportTemplateRow"] == null || Session["reportTemplate"] == null)
                    Response.Redirect(Config.DOMAIN() + "desktop/reports");

                OutgoingReportTemplate template = (OutgoingReportTemplate)Session["reportTemplate"];

                cbGrandTotal.Checked = template.ShowGrandTotal;

                switch (template.DisplayType)
                {
                    case OutgoingReportTemplate.Display.All:
                        rbAllTrans.Checked = true;
                        break;
                    case OutgoingReportTemplate.Display.ShowAllTranactionsByDist:
                        rbDistributionTotal.Checked = true;
                        break;
                    case OutgoingReportTemplate.Display.ShowTotalsDist:
                        rbDistributionTypes.Checked = true;
                        break;
                    case OutgoingReportTemplate.Display.ShowTotalsFoodSourceTypes:
                        rbTotalFoodSource.Checked = true;
                        break;
                }
                txtTitle.Text = template.Title;

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
            if (Session["reportTemplateRow"] == null || Session["reportTemplate"] == null)
                Response.Redirect(Config.DOMAIN() + "desktop/reports");

            OutgoingReportTemplate template = (OutgoingReportTemplate)Session["reportTemplate"];

            if (rbAllTrans.Checked)
                template.DisplayType = OutgoingReportTemplate.Display.All;
            else if (rbDistributionTypes.Checked)
                template.DisplayType = OutgoingReportTemplate.Display.ShowAllTranactionsByDist;
            else if (rbDistributionTotal.Checked)
                template.DisplayType = OutgoingReportTemplate.Display.ShowTotalsDist;
            else
                template.DisplayType = OutgoingReportTemplate.Display.ShowTotalsFoodSourceTypes;

            template.ShowGrandTotal = cbGrandTotal.Checked;
            template.Title = txtTitle.Text;

            Session["reportTemplate"] = template;
            Response.Redirect(Config.DOMAIN() + "desktop/reports/shared/save.aspx");
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }
}