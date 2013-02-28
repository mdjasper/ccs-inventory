using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class desktop_reports_displayGroceryRescueReport : System.Web.UI.Page
{
    public DataTable source { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                LogChange.logChange("Ran Grocery Rescue Report", DateTime.Now, short.Parse(Session["userID"].ToString()));

                if (Session["startDate"] == null || Session["endDate"] == null || Session["reportTemplate"] == null)
                    Response.Redirect(Config.DOMAIN() + "desktop/reports");

                DateTime startDate = (DateTime)Session["startDate"];
                DateTime endDate = (DateTime)Session["endDate"];
                GroceryRescueReportTemplate template = (GroceryRescueReportTemplate)Session["reportTemplate"];

                ReportDataSource source = new ReportDataSource("dataSet", (DataTable)(LoadData(startDate, endDate).GroceryRescue));
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.LocalReport.SetParameters(new ReportParameter("startDate", startDate.ToString("d")));
                reportViewer.LocalReport.SetParameters(new ReportParameter("endDate", endDate.ToString("d")));
                reportViewer.LocalReport.SetParameters(new ReportParameter("Agency", template.Agency));
                reportViewer.LocalReport.SetParameters(new ReportParameter("Phone", template.Phone));
                reportViewer.LocalReport.SetParameters(new ReportParameter("Contact", template.Contact));
                reportViewer.LocalReport.SetParameters(new ReportParameter("Email", template.Email));
                reportViewer.LocalReport.SetParameters(new ReportParameter("mcNum", template.MC));
                reportViewer.LocalReport.SetParameters(new ReportParameter("reNum", template.RE));
                reportViewer.LocalReport.SetParameters(new ReportParameter("comments", template.AdditonalComments));



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

    private ReportsDataSet LoadData(DateTime startDate, DateTime endDate)
    {

        List<FoodSource> data;
        ReportsDataSet ds = new ReportsDataSet();
        using (CCSEntities db = new CCSEntities())
        {
            data = (from f in db.FoodSources
                    where f.FoodSourceType.FoodSourceType1 == "Grocery Rescue"
                    orderby f.Source
                    select f).ToList();

           
            foreach (FoodSource row in data)
            {
                string storeName = row.Source;
                Address add = row.Address;
                string storeAddress = string.Format("{0} {1} {2}", add.StreetAddress1, add.StreetAddress2, add.City.CityName);
                string storeID = row.StoreID;


                var foodIn = from f in row.FoodIns
                             where f.TimeStamp >= startDate && f.TimeStamp <= endDate
                             select f;
                string bakeryWeight = (from f in foodIn
                                       where f.FoodCategory.CategoryType == "Bakery"
                                       select f.Weight).Sum().ToString();
                string dairyWeight = (from f in foodIn
                                      where f.FoodCategory.CategoryType == "Dairy"
                                      select f.Weight).Sum().ToString();
                string meatWeight = (from f in row.FoodIns
                                     where f.FoodCategory.CategoryType == "Meat"
                                     select f.Weight).Sum().ToString();
                string produceWeight = (from f in foodIn
                                        where f.FoodCategory.CategoryType == "Produce"
                                        select f.Weight).Sum().ToString();
                string otherWeight = (from f in foodIn
                                      where f.FoodCategory.CategoryType != "Produce" &&
                                      f.FoodCategory.CategoryType != "Meat" &&
                                      f.FoodCategory.CategoryType != "Dairy" &&
                                      f.FoodCategory.CategoryType != "Bakery"
                                      select f.Weight).Sum().ToString();
                ds.GroceryRescue.AddGroceryRescueRow(storeAddress, storeName, storeID, bakeryWeight, dairyWeight, meatWeight, produceWeight, otherWeight);

            }
        }
        return ds;
    }
}