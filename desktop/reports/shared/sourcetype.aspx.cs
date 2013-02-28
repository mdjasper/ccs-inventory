using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_reports_incoming_SourceType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["reportTemplateRow"] == null || Session["reportTemplate"] == null)
                    Response.Redirect(Config.DOMAIN() + "desktop/reports");

                IFoodSourceTypes template = (IFoodSourceTypes)Session["reportTemplate"];

                using (CCSEntities db = new CCSEntities())
                {
                    lstFoodSourceTypes.SelectedType = template.FoodSourceTypesSelection;
                    lstFoodSourceTypes.AvailableList = (from f in db.FoodSourceTypes
                                                        select new { ID = f.FoodSourceTypeID, Display = f.FoodSourceType1 }).ToList();

                    lstFoodSourceTypes.SelectedIDs = template.FoodSourceTypes;
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
            IFoodSourceTypes template = (IFoodSourceTypes)Session["reportTemplate"];
            Template row = (Template)Session["reportTemplateRow"];
            template.FoodSourceTypes = lstFoodSourceTypes.SelectedIDs;
            template.FoodSourceTypesSelection = lstFoodSourceTypes.SelectedType;

            ReportTemplate.ReportType type = (ReportTemplate.ReportType)row.TemplateType;
            string urlRedirect = "";
            switch (type)
            {
                case ReportTemplate.ReportType.Incoming:
                    {
                        urlRedirect = Config.DOMAIN() + "desktop/reports/shared/foodsources.aspx";
                        break;
                    }
                case ReportTemplate.ReportType.Outgoing:
                    {
                        urlRedirect = Config.DOMAIN() + "desktop/reports/outgoing/distributiontypes.aspx";
                        break;
                    }
            }

            Session["reportTemplate"] = template;
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