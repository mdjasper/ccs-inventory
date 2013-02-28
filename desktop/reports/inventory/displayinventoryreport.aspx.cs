using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class desktop_reports_inventory_displayinventoryreport : System.Web.UI.Page
{

    string ReportByCategory = @"desktop\reports\inventory\InventoryByCategory.rdlc";
    string ReportByLocation = @"desktop\reports\inventory\InventoryByLocation.rdlc";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                LogChange.logChange("Ran Inventory Report", DateTime.Now, short.Parse(Session["userID"].ToString()));

                if (Session["reportTemplate"] == null)
                    Response.Redirect(Config.DOMAIN() + "desktop/reports");


                InventoryReportTemplate template = (InventoryReportTemplate)Session["reportTemplate"];

                if (template.SortByChoice == InventoryReportTemplate.SortBy.Category)
                    reportViewer.LocalReport.ReportPath = ReportByCategory;
                else
                    reportViewer.LocalReport.ReportPath = ReportByLocation;


                ReportDataSource source = new ReportDataSource("Inventory", (DataTable)(LoadData(template).Inventory));
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.LocalReport.SetParameters(new ReportParameter("reportTitle", template.Title));
                reportViewer.LocalReport.SetParameters(new ReportParameter("showBins", (template.ShowOnlyCategoryTotals).ToString()));
                reportViewer.LocalReport.SetParameters(new ReportParameter("showGrandTotal", (!template.ShowGrandTotal).ToString()));


                reportViewer.DataBind();
                reportViewer.LocalReport.Refresh();

            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private ReportsDataSet LoadData(InventoryReportTemplate template)
    {
        ReportsDataSet ds = new ReportsDataSet();
        using (CCSEntities db = new CCSEntities())
        {
            List<Container> containers = null;


            if (template.CategoriesSelection == ReportTemplate.SelectionType.ALL)
            {
                containers = db.Containers.ToList();
            }
            if (template.CategoriesSelection == ReportTemplate.SelectionType.REGULAR)
            {
                containers = (from c in db.Containers
                              where c.FoodCategory.Perishable == false && c.FoodCategory.NonFood == false
                              select c).ToList();
            }
            else if (template.CategoriesSelection == ReportTemplate.SelectionType.REGULAR)
            {
                containers = (from c in db.Containers
                              where c.FoodCategory.Perishable == false && c.FoodCategory.NonFood == false
                              select c).ToList();
            }
            else if (template.CategoriesSelection == ReportTemplate.SelectionType.PERISHABLE)
            {
                containers = (from c in db.Containers
                              where c.FoodCategory.Perishable == false && c.FoodCategory.NonFood == false
                              select c).ToList();
            }
            else if (template.CategoriesSelection == ReportTemplate.SelectionType.NONFOOD)
            {
                containers = (from c in db.Containers
                              where c.FoodCategory.Perishable == false && c.FoodCategory.NonFood == false
                              select c).ToList();
            }
            else if (template.CategoriesSelection == ReportTemplate.SelectionType.SOME)
            {
                List<short> selectedCategories = new List<short>();
                foreach (var i in template.FoodCategories)
                    selectedCategories.Add(short.Parse(i));

                containers = (from c in db.Containers
                              where  selectedCategories.Contains((short)c.FoodCategoryID)
                              select c).ToList();
            }


            if (template.LocationsSelection == ReportTemplate.SelectionType.SOME)
            {
                List<short> selectedLocations = new List<short>();

                foreach (var i in template.Locations)
                    selectedLocations.Add(short.Parse(i));

                containers = (from c in containers
                              where selectedLocations.Contains(c.Location.LocationID)
                              select c).ToList();


            }

            foreach (var c in containers)
            {
                ds.Inventory.AddInventoryRow(c.Location.RoomName, (bool)c.isUSDA ? c.USDACategory.Description : c.FoodCategory.CategoryType, c.BinNumber.ToString(), c.Cases == null ? 0 :(double)c.Cases, (double)c.Weight);
            }
        }
        return ds;
    }
}