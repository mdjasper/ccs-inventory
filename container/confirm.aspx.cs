using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected Container container;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            container = (Container)Session["Container"];

            if (container == null)
            {
                Response.Redirect("default.aspx");
            }

            lblID.Text = container.BinNumber.ToString();
            //lblID.Text = container.ContainerID.ToString();
            lblWeight.Text = container.Weight.ToString();

            using (CCSEntities db = new CCSEntities())
            {
                lblLocation.Text = (from l in db.Locations
                                    where l.LocationID == container.LocationID
                                    select l).First().RoomName;

                lblType.Text = (from t in db.FoodCategories
                                where t.FoodCategoryID == container.FoodCategoryID
                                select t).First().CategoryType;

            }
        }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                Container cont = (from c in db.Containers
                                  where c.ContainerID == container.ContainerID
                                  select c).FirstOrDefault();

                cont.Weight = container.Weight;
                cont.LocationID = container.LocationID;
                cont.FoodCategoryID = container.FoodCategoryID;

                db.SaveChanges();
            }
            Response.Redirect("default.aspx");
        }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

    }

}