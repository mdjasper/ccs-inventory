using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InventoryTemplate
/// </summary>
public class InventoryReportTemplate: ReportTemplate, IFoodUSDACategeories
{
    public string Title { get; set; }

    public enum SortBy { Location, Category }

    public bool ShowOnlyCategoryTotals { get; set; }
    public bool ShowGrandTotal { get; set; }
    public SortBy SortByChoice { get; set; }

    public SelectionType CategoriesSelection { get; set; }
    public SelectionType USDASelection { get; set; }
    public SelectionType LocationsSelection { get; set; }


    public List<string> FoodCategories { get; set; }
    public List<string> USDACategories { get; set; }
    public List<string> Locations { get; set; }

    public InventoryReportTemplate()
    {
        FoodCategories = new List<string>();
        USDACategories = new List<string>();
        Locations = new List<string>();
        ShowGrandTotal = true;
        ShowOnlyCategoryTotals = true;
        Title = "Inventory";
    }
  
}