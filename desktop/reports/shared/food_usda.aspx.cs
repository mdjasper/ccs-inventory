using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_reports_incoming_Food_USDA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["reportTemplateRow"] == null || Session["reportTemplate"] == null)
                    Response.Redirect(Config.DOMAIN() + "desktop/reports");

                IFoodUSDACategeories template = (IFoodUSDACategeories)Session["reportTemplate"];

                using (CCSEntities db = new CCSEntities())
                {
                    lstRegularFood.SelectedType = template.CategoriesSelection;
                    lstUSDA.SelectedType = template.USDASelection;

                    lstRegularFood.AvailableList = (from f in db.FoodCategories
                                                    select new { ID = f.FoodCategoryID, Display =  f.CategoryType }).ToList();
                    lstUSDA.AvailableList = (from u in db.USDACategories
                                             select new { ID = u.USDAID, Display = "(" + u.USDANumber + ") " + u.Description }).ToList();

                    lstUSDA.SelectedIDs = template.USDACategories;
                    lstRegularFood.SelectedIDs = template.FoodCategories;
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
            IFoodUSDACategeories template = (IFoodUSDACategeories)Session["reportTemplate"];
            Template row = (Template)Session["reportTemplateRow"];
            template.USDACategories = lstUSDA.SelectedIDs;
            template.FoodCategories = lstRegularFood.SelectedIDs;
            template.CategoriesSelection =  lstRegularFood.SelectedType;
            template.USDASelection = lstUSDA.SelectedType;


            ReportTemplate.ReportType type = (ReportTemplate.ReportType)row.TemplateType;
            string urlRedirect = "";
            switch (type)
            {
                case ReportTemplate.ReportType.Incoming:
                    {
                        urlRedirect = Config.DOMAIN() + "desktop/reports/incoming/incomingoptions.aspx";
                        break;
                    }
                case ReportTemplate.ReportType.Outgoing:
                    {
                        urlRedirect = Config.DOMAIN() + "desktop/reports/outgoing/outgoingoptions.aspx";
                        break;
                    }
                case ReportTemplate.ReportType.InOut:
                    {
                        urlRedirect = Config.DOMAIN() + "desktop/reports/in-out/inoutoptions.aspx";
                        break;
                    }
                case ReportTemplate.ReportType.Inventory:
                    {
                        urlRedirect = Config.DOMAIN() + "desktop/reports/inventory/inventoryoptions.aspx";
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