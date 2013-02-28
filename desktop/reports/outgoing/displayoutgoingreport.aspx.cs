using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class desktop_reports_outgoing_displayoutgoingreport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                LogChange.logChange("Ran Outgoing Report", DateTime.Now, short.Parse(Session["userID"].ToString()));

                if (Session["startDate"] == null || Session["endDate"] == null || Session["reportTemplate"] == null)
                    Response.Redirect(Config.DOMAIN() + "desktop/reports");

                DateTime startDate = (DateTime)Session["startDate"];
                DateTime endDate = (DateTime)Session["endDate"];
                OutgoingReportTemplate template = (OutgoingReportTemplate)Session["reportTemplate"];

                ReportDataSource source = new ReportDataSource("DataSet", (DataTable)(LoadData(startDate, endDate, template).Outgoing));
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.LocalReport.SetParameters(new ReportParameter("startDate", startDate.ToString("d")));
                reportViewer.LocalReport.SetParameters(new ReportParameter("endDate", endDate.ToString("d")));
                reportViewer.LocalReport.SetParameters(new ReportParameter("reportTitle", template.Title));
                reportViewer.LocalReport.SetParameters(new ReportParameter("showGrandTotal", (!template.ShowGrandTotal).ToString()));
                reportViewer.LocalReport.SetParameters(new ReportParameter("display", ((int)template.DisplayType).ToString()));


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

    private ReportsDataSet LoadData(DateTime startdate, DateTime enddate, OutgoingReportTemplate template)
    {
        ReportsDataSet ds = new ReportsDataSet();
        using (CCSEntities db = new CCSEntities())
        {
            List<FoodOut> data = db.FoodOuts.Where(f => f.TimeStamp >= startdate && f.TimeStamp <= enddate).ToList();

            if (template.FoodSourceTypesSelection == ReportTemplate.SelectionType.SOME)
            {
                List<short> selectedTypes = new List<short>();
                foreach (var i in template.FoodSourceTypes)
                    selectedTypes.Add(short.Parse(i));

                data = (from c in data
                        where selectedTypes.Contains((short)c.FoodSourceTypeID)
                        select c).ToList();
            }

            if (template.DistributionSelection == ReportTemplate.SelectionType.SOME)
            {
                List<short> selectedDistribution = new List<short>();
                foreach (var i in template.DistributionTypes)
                    selectedDistribution.Add(short.Parse(i));

                data = (from c in data
                        where selectedDistribution.Contains((short)c.DistributionTypeID)
                        select c).ToList();
            }

            if (template.AgenciesSelection == ReportTemplate.SelectionType.SOME)
            {
                List<short> selectedAgencies = new List<short>();
                foreach (var i in template.Agencies)
                    selectedAgencies.Add(short.Parse(i));

                data = (from c in data
                        where selectedAgencies.Contains((short)c.AgencyID)
                        select c).ToList();
            }

            List<FoodOut> foodInRegularData = data.Where(f => f.FoodCategory != null).ToList();
            List<FoodOut> foodInUSDAData = data.Where(f => f.USDACategory != null).ToList();


            if (template.CategoriesSelection == ReportTemplate.SelectionType.REGULAR)
            {
                foodInRegularData = (from c in foodInRegularData
                                     where c.FoodCategory.Perishable == false && c.FoodCategory.NonFood == false
                                     select c).ToList();
            }
            else if (template.CategoriesSelection == ReportTemplate.SelectionType.REGULAR)
            {
                foodInRegularData = (from c in foodInRegularData
                                     where c.FoodCategory.Perishable == false && c.FoodCategory.NonFood == false
                                     select c).ToList();
            }
            else if (template.CategoriesSelection == ReportTemplate.SelectionType.PERISHABLE)
            {
                foodInRegularData = (from c in foodInRegularData
                                     where c.FoodCategory.Perishable == false && c.FoodCategory.NonFood == false
                                     select c).ToList();
            }
            else if (template.CategoriesSelection == ReportTemplate.SelectionType.NONFOOD)
            {
                foodInRegularData = (from c in foodInRegularData
                                     where c.FoodCategory.Perishable == false && c.FoodCategory.NonFood == false
                                     select c).ToList();
            }
            else if (template.CategoriesSelection == ReportTemplate.SelectionType.SOME)
            {
                List<short> selectedCategories = new List<short>();
                foreach (var i in template.FoodCategories)
                    selectedCategories.Add(short.Parse(i));

                foodInRegularData = (from c in foodInRegularData
                                     where selectedCategories.Contains((short)c.FoodCategoryID)
                                     select c).ToList();
            }


            if (template.USDASelection == ReportTemplate.SelectionType.SOME)
            {
                List<short> selectedUSDA = new List<short>();
                foreach (var i in template.USDACategories)
                    selectedUSDA.Add(short.Parse(i));

                foodInUSDAData = (from u in foodInUSDAData
                                  where selectedUSDA.Contains((short)u.USDAID)
                                  select u).ToList();
            }

            foodInRegularData.InsertRange(0, foodInUSDAData);

            data = foodInRegularData;

            foreach (var i in data)
            {
                ds.Outgoing.AddOutgoingRow(i.FoodCategory.CategoryType, i.TimeStamp, (double)(i.Count ?? 0), i.Weight, i.Agency == null ? "No-Agency" : i.Agency.AgencyName, i.DistributionType.DistributionType1, i.FoodSourceType.FoodSourceType1);
            }
        }
        return ds;
    }
}