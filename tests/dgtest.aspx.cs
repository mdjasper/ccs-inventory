using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class tests_dgtest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (CCSEntities db = new CCSEntities())
        {
            
            var d = (from a in db.Addresses
                  where a.Zipcode.ZipCode1 == "84401"
                  select new { a.StreetAddress1, a.City.CityName, a.Zipcode.ZipCode1 }).ToList();
            grid.DataSource = d;
        }
        grid.DataBind();
    }

  
}