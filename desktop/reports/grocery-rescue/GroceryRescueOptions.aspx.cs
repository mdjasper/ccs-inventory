using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_reports_incoming_GroceryRescueOptions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["reportTemplateRow"] == null && Session["reportTemplate"] == null)
                    Response.Redirect(Config.DOMAIN() + "desktop/reports");

                GroceryRescueReportTemplate template = (GroceryRescueReportTemplate)Session["reportTemplate"];
                txtAgency.Text = template.Agency;
                txtComment.Text = template.AdditonalComments;
                txtContact.Text = template.Contact;
                txtEmail.Text = template.Email;
                txtMC.Text = template.MC;
                txtPhone.Text = template.Phone;
                txtRE.Text = template.RE;
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
            GroceryRescueReportTemplate template = (GroceryRescueReportTemplate)Session["reportTemplate"];
            template.Agency = txtAgency.Text;
            template.AdditonalComments = txtComment.Text;
            template.Contact = txtContact.Text;
            template.Email = txtEmail.Text;
            template.MC = txtMC.Text;
            template.Phone = txtPhone.Text;
            template.RE = txtRE.Text;
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