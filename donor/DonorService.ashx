<%@ WebHandler Language="C#" Class="DonorService" %>

using System;
using System.Web;
using System.Linq;

public class DonorService : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/javascript";
        using (CCSEntities db = new CCSEntities())
        {
            var fetchDonor = (from d in db.FoodSources.OrderBy(x => x.Source)
                              select d).ToList();
            String donorNames = "[";
            foreach (FoodSource d in fetchDonor)
            {
                donorNames += "'" + d.Source + "', ";
            }
            donorNames = donorNames.Remove(donorNames.Length - 2); //removes the last comma
            donorNames += "]";
            context.Response.Write("donorNames = " + donorNames);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}