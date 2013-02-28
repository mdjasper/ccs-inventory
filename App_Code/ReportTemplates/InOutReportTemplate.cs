using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InOutReportTemplate
/// </summary>
public class InOutReportTemplate:ReportTemplate, IFoodUSDACategeories
{
    public string Title { get; set; }
    public bool ShowGrandTotal { get; set; }
    public bool ShowCount { get; set; }
    public bool ShowWeight { get; set; }
    public bool ShowCurrentBalance { get; set; }

    
    public List<string> FoodCategories { get; set; }
    public List<string> USDACategories { get; set; }

    public SelectionType CategoriesSelection { get; set; }
    public SelectionType USDASelection { get; set; }

    public InOutReportTemplate()
    {
        FoodCategories = new List<string>();
        USDACategories = new List<string>();
        CategoriesSelection = SelectionType.SOME;
        USDASelection = SelectionType.SOME;
        ShowCount = true;
        ShowCurrentBalance = true;
        ShowGrandTotal = true;
        ShowWeight = true;
        Title = "In\\Out";
    }
}