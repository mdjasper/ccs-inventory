using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Outgoing
/// </summary>
public class OutgoingReportTemplate:ReportTemplate, IFoodSourceTypes, IFoodUSDACategeories
{
    public string Title { get; set; }

    public List<string> FoodSourceTypes { get; set; }
    public List<string> FoodCategories { get; set; }
    public List<string> USDACategories { get; set; }
    public List<string> DistributionTypes { get; set; }
    public List<string> Agencies { get; set; }

    public SelectionType AgenciesSelection { get; set; }
    public SelectionType DistributionSelection { get; set; }
    public SelectionType CategoriesSelection { get; set; }
    public SelectionType USDASelection { get; set; }
    public SelectionType FoodSourceTypesSelection { get; set; }

    public bool ShowGrandTotal { get; set; }

    public enum Display { All, ShowAllTranactionsByDist, ShowTotalsDist, ShowTotalsFoodSourceTypes }

    public Display DisplayType { get; set; }

    public OutgoingReportTemplate()
    {
        FoodSourceTypes = new List<string>();
        FoodCategories = new List<string>();
        USDACategories = new List<string>();
        DistributionTypes = new List<string>();
        Agencies = new List<string>();
        Title = "Outgoing";
        ShowGrandTotal = true;
    }

}