<%@ WebHandler Language="C#" Class="CategoryService" %>

using System;
using System.Web;
using System.Linq;

public class CategoryService : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/javascript";
        using (CCSEntities db = new CCSEntities())
        {
            var fetchCategory = (from d in db.FoodCategories.OrderBy(x => x.CategoryType)
                              select d).ToList();
            String categoryNames = "[";
            foreach (FoodCategory d in fetchCategory)
            {
                categoryNames += "'" + d.CategoryType + "', ";
            }
            categoryNames = categoryNames.Remove(categoryNames.Length - 2); //removes the last comma
            categoryNames += "]";
            context.Response.Write("categoryNames = " + categoryNames);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}