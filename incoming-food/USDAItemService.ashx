<%@ WebHandler Language="C#" Class="USDAItemService" %>

using System;
using System.Web;
using System.Linq;

public class USDAItemService : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/javascript";
        using (CCSEntities db = new CCSEntities())
        {
            var fetchUSDAItem = (from d in db.USDACategories.OrderBy(x => x.USDANumber)
                                 select d).ToList();
            String usdaItmNumbers = "[";
            foreach (USDACategory d in fetchUSDAItem)
            {
                usdaItmNumbers += "'" + d.USDANumber + "', ";
            }
            usdaItmNumbers = usdaItmNumbers.Remove(usdaItmNumbers.Length - 2); //removes the last comma
            usdaItmNumbers += "]";
            context.Response.Write("usdaItmNumbers = " + usdaItmNumbers);
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