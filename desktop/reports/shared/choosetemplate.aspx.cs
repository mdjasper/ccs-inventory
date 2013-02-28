using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_reports_shared_choosetemplate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["reportType"] == null)
                    Response.Redirect(Config.DOMAIN() + "desktop/reports");

                RefreshTemplateGrid();
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private void RefreshTemplateGrid()
    {
        try
        {
            int type = int.Parse(Request.QueryString["reportType"]);           
            using (CCSEntities db = new CCSEntities())
            {
                List<Template> templates = db.Templates.Where(t => t.TemplateType == type).ToList();
                if (templates.Count > 0)
                {
                    grdTemplates.DataSource = templates;
                    grdTemplates.DataBind();
                    pnlTemplate.Visible = true;

                }
                else
                {
                    pnlTemplate.Visible = false;
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

    protected void grdTemplates_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            index = Convert.ToInt32(grdTemplates.DataKeys[index].Value);

            if (e.CommandName == "runTemp")
            {
                RunTemplate(index, false);
            }
            else if (e.CommandName == "editTemp")
            {
                RunTemplate(index);
            }
            else if (e.CommandName == "deleteTemp")
            {
                DeleteTemplate(index);
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

 

    void grdTemplates_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            ReportTemplate.ReportType type = (ReportTemplate.ReportType)int.Parse(Request.QueryString["reportType"]);
            ReportTemplate template = null;
            Template templateRow = new Template();
            templateRow.TemplateName = "New Template";

            switch (type)
            {
                case ReportTemplate.ReportType.GroceryRescue:
                    {
                        templateRow.TemplateType = (int)ReportTemplate.ReportType.GroceryRescue;
                        template = new GroceryRescueReportTemplate();
                        break;
                    }
                case ReportTemplate.ReportType.InOut:
                    {
                        templateRow.TemplateType = (int)ReportTemplate.ReportType.InOut;
                        template = new InOutReportTemplate();
                        break;
                    }
                case ReportTemplate.ReportType.Inventory:
                    {
                        templateRow.TemplateType = (int)ReportTemplate.ReportType.Inventory;
                        template = new  InventoryReportTemplate();
                        break;
                    }
                case ReportTemplate.ReportType.Incoming:
                    {
                        templateRow.TemplateType = (int)ReportTemplate.ReportType.Incoming;
                        template = new IncomingReportTemplate();
                        break;
                    }
                case ReportTemplate.ReportType.Outgoing:
                    {
                        templateRow.TemplateType = (int)ReportTemplate.ReportType.Outgoing;
                        template = new OutgoingReportTemplate();
                        break;
                    }
            }

            Session["reportTemplateRow"] = templateRow;
            Session["reportTemplate"] = template;
            NextPage(type);
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private void NextPage(ReportTemplate.ReportType type, bool edit = true)
    {
        try
        {
            string urlRedirect = null;

            if (edit)
            {
                switch (type)
                {
                    case ReportTemplate.ReportType.GroceryRescue:
                        {
                            urlRedirect = Config.DOMAIN() + "desktop/reports/grocery-rescue/groceryrescueoptions.aspx";
                            break;
                        }
                    case ReportTemplate.ReportType.InOut:
                        {
                            urlRedirect = Config.DOMAIN() + "desktop/reports/shared/food_usda.aspx";
                            break;
                        }
                    case ReportTemplate.ReportType.Inventory:
                        {
                            urlRedirect = Config.DOMAIN() + "desktop/reports/inventory/locations.aspx";
                            break;
                        }
                    case ReportTemplate.ReportType.Outgoing:
                    case ReportTemplate.ReportType.Incoming:
                        {
                            urlRedirect = Config.DOMAIN() + "desktop/reports/shared/sourcetype.aspx";
                            break;
                        }
                }
            }
            else
            {
                if (type == ReportTemplate.ReportType.Inventory)
                    urlRedirect = Config.DOMAIN() + "desktop/reports/inventory/displayinventoryreport.aspx";
                else
                    urlRedirect = Config.DOMAIN() +  "desktop/reports/shared/selectdates.aspx";
            }

            Response.Redirect(urlRedirect);
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private void DeleteTemplate(int id)
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                db.Templates.Remove(db.Templates.Single(t => t.TemplateID == id));
                db.SaveChanges();
            }

            RefreshTemplateGrid();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private void RunTemplate(int id, bool edit = true)
    {
        try
        {
            Template templateRow = null;
            ReportTemplate template = null;

            using (CCSEntities db = new CCSEntities())
            {
                templateRow = db.Templates.Single(t => t.TemplateID == id);
            }

            ReportTemplate.ReportType type = (ReportTemplate.ReportType)int.Parse(Request.QueryString["reportType"]);
            template = DeserializeTemplate(templateRow, type);

            Session["reportTemplateRow"] = templateRow;
            Session["reportTemplate"] = template;
            NextPage(type, edit);
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private static ReportTemplate DeserializeTemplate(Template templateRow, ReportTemplate.ReportType type)
    {
        ReportTemplate template = null;

        switch (type)
        {
            case ReportTemplate.ReportType.GroceryRescue:
                {
                    template = XmlSerialize.Desrialize<GroceryRescueReportTemplate>(templateRow.Data);
                    break;
                }
            case ReportTemplate.ReportType.InOut:
                {
                    template = XmlSerialize.Desrialize<InOutReportTemplate>(templateRow.Data);
                    break;
                }
            case ReportTemplate.ReportType.Inventory:
                {
                    template = XmlSerialize.Desrialize<InventoryReportTemplate>(templateRow.Data);
                    break;
                }
            case ReportTemplate.ReportType.Incoming:
                {
                    template = XmlSerialize.Desrialize<IncomingReportTemplate>(templateRow.Data);
                    break;
                }
            case ReportTemplate.ReportType.Outgoing:
                {
                    template = XmlSerialize.Desrialize<OutgoingReportTemplate>(templateRow.Data);
                    break;
                }
        }
        return template;
    }
}