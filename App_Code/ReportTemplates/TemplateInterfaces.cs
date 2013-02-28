using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public interface IFoodSourceTypes
{
    List<string> FoodSourceTypes { get; set; }

    ReportTemplate.SelectionType FoodSourceTypesSelection { get; set; }

}

public interface IFoodUSDACategeories
{
     List<string> FoodCategories { get; set; }
     List<string> USDACategories { get; set; }
     ReportTemplate.SelectionType CategoriesSelection { get; set; }
     ReportTemplate.SelectionType USDASelection { get; set; }
}

