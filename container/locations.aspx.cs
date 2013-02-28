using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int category = Int32.Parse(Session["categoryId"].ToString());

            using (CCSEntities db = new CCSEntities())
            {
                var containers = (from c in db.Containers
                                  where c.FoodCategory.FoodCategoryID == category
                                  select new
                                  {
                                      c.Location.RoomName,
                                      c.BinNumber,
                                      c.isUSDA,
                                      c.Cases
                                  });

                grdContainers.DataSource = containers.ToList();
                grdContainers.DataBind();

                FoodCategory categoryValue = (from c in db.FoodCategories
                                              where c.FoodCategoryID == (short)category
                                              select c).FirstOrDefault();

                ltrCategory.Text = categoryValue.CategoryType;
            }
        }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }
}