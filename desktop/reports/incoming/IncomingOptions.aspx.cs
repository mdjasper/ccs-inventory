using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_reports_incoming_Incoming_Options : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["reportTemplateRow"] == null || Session["reportTemplate"] == null)
                    Response.Redirect(Config.DOMAIN() + "desktop/reports");

                IncomingReportTemplate template = (IncomingReportTemplate)Session["reportTemplate"];

                switch (template.DisplayType)
                {
                    case IncomingReportTemplate.Display.All:
                        rbAllTrans.Checked = true;
                        break;
                    case IncomingReportTemplate.Display.OnlySourceTotals:
                        rbTotalSources.Checked = true;
                        break;
                    case IncomingReportTemplate.Display.OnlyTypeTotals:
                        rbTotalTypes.Checked = true;
                        break;
                }

                cbGrandTotal.Checked = template.ShowGrandTotal;
                cbShowDonorAddress.Checked = template.ShowDonorAddress;
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
            IncomingReportTemplate template = (IncomingReportTemplate)Session["reportTemplate"];

            if (rbAllTrans.Checked)
                template.DisplayType = IncomingReportTemplate.Display.All;
            else if (rbTotalSources.Checked)
                template.DisplayType = IncomingReportTemplate.Display.OnlySourceTotals;
            else
                template.DisplayType = IncomingReportTemplate.Display.OnlyTypeTotals;


            template.ShowGrandTotal = cbGrandTotal.Checked;
            template.ShowDonorAddress = cbShowDonorAddress.Checked;
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