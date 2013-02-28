<%@ WebHandler Language="C#" Class="FoodService" %>

using System;
using System.Web;
using System.Linq;

public class FoodService : IHttpHandler 
{
    
    public void ProcessRequest (HttpContext context) 
    {
        try
        {
            context.Response.ContentType = "text/javascript";
            using (CCSEntities db = new CCSEntities())
            {
                var fetchFood = (from c in db.FoodCategories.OrderBy(x => x.FoodCategoryID)
                                 select c).ToList();
                String foodNames = "[";
                foreach (FoodCategory c in fetchFood)
                {
                    foodNames += "'" + c.CategoryType + "', ";
                }
                foodNames = foodNames.Remove(foodNames.Length - 2); //removes the last comma
                foodNames += "]";
                context.Response.Write("foodNames = " + foodNames);
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
        }
    }
 
    public bool IsReusable 
    {
        get 
        {
            return false;
        }
    }

}