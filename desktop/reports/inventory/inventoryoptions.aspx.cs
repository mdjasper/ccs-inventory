using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_reports_incoming_InventoryOptions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["reportTemplateRow"] == null || Session["reportTemplate"] == null)
                    Response.Redirect(Config.DOMAIN() + "desktop/reports");

                InventoryReportTemplate template = (InventoryReportTemplate)Session["reportTemplate"];

                if (template.SortByChoice == InventoryReportTemplate.SortBy.Category)
                    rbCategory.Checked = true;
                else
                    rbLocation.Checked = true;

                if (template.ShowOnlyCategoryTotals)
                    rbTotal.Checked = true;
                else
                    rbFoodBin.Checked = true;

                cbGrandTotal.Checked = template.ShowGrandTotal;
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
            InventoryReportTemplate template = (InventoryReportTemplate)Session["reportTemplate"];

            if (rbCategory.Checked)
                template.SortByChoice = InventoryReportTemplate.SortBy.Category;
            else
                template.SortByChoice = InventoryReportTemplate.SortBy.Location;

            template.ShowOnlyCategoryTotals = rbTotal.Checked;
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