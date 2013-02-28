using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GenerateDatasetHelper
/// </summary>
public class GenerateDatasetHelper
{
        public static List<FoodCategory> GetFoodCategoriesBySelection(IFoodUSDACategeories template, CCSEntities db)
        {
            List<FoodCategory> foodCategoriesData = null;

            if (template.CategoriesSelection == ReportTemplate.SelectionType.ALL)
            {
                foodCategoriesData = db.FoodCategories.OrderBy(f => f.CategoryType).ToList();
            }
            else if (template.CategoriesSelection == ReportTemplate.SelectionType.REGULAR)
            {
                foodCategoriesData = (from f in db.FoodCategories
                                        where f.Perishable == false && f.NonFood == false
                                        orderby f.CategoryType
                                        select f).ToList();
            }
            else if (template.CategoriesSelection == ReportTemplate.SelectionType.PERISHABLE)
            {
                foodCategoriesData = (from f in db.FoodCategories
                                        where f.Perishable == true
                                        orderby f.CategoryType
                                        select f).ToList();
            }
            else if (template.CategoriesSelection == ReportTemplate.SelectionType.NONFOOD)
            {
                foodCategoriesData = (from f in db.FoodCategories
                                        where f.NonFood == true
                                        orderby f.CategoryType
                                        select f).ToList();
            }
            else if (template.CategoriesSelection == ReportTemplate.SelectionType.SOME)
            {
                List<short> selectedCategories = new List<short>();
                foreach (var i in template.FoodCategories)
                    selectedCategories.Add(short.Parse(i));

                foodCategoriesData = (from f in db.FoodCategories
                                        where selectedCategories.Contains(f.FoodCategoryID)
                                        orderby f.CategoryType
                                        select f).ToList();
            }

            return foodCategoriesData;
        }
}