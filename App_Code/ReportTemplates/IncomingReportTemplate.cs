using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FoodInReportTemplate
/// </summary>
public class IncomingReportTemplate: ReportTemplate, IFoodSourceTypes, IFoodUSDACategeories
{
    public enum Display { All, OnlySourceTotals, OnlyTypeTotals }

    public string Title { get; set; }

    public SelectionType FoodSourceTypesSelection { get; set; }
    public SelectionType FoodSourcesSelection { get; set; }
    public SelectionType CategoriesSelection { get; set; }
    public SelectionType USDASelection { get; set; }

    public List<string> FoodSourceTypes { get; set; }
    public List<string> FoodCategories { get; set; }
    public List<string> USDACategories { get; set; }
    public List<string> FoodSources { get; set; }


    public Display DisplayType { get; set; }
    public bool ShowGrandTotal { get; set; }
    public bool ShowDonorAddress { get; set; }

    public IncomingReportTemplate()
    {
        FoodCategories = new List<string>();
        FoodSourceTypes = new List<string>();
        USDACategories = new List<string>();
        FoodSources = new List<string>();
        ShowGrandTotal = true;
        ShowDonorAddress = true;
        Title = "Incoming";
    }
}