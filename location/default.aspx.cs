using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        updateGridView();
    }
    private void updateGridView()
    {
        try
        {
            lblMessage.Text = "";

            using (CCSEntities db = new CCSEntities())
            {
                List<Location> lstLocations = db.Locations.OrderBy(x => x.RoomName).ToList();

                gvLocation.DataSource = lstLocations;
                gvLocation.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    protected void btnAddLocationSubmit_Click(object sender, EventArgs e)
    {
        addLocation();
        Response.Redirect("default.aspx");
    }

    private void addLocation()
    {
        try
        {
            lblMessage.Text = "";

            using (CCSEntities db = new CCSEntities())
            {
                Location newLocation = new Location(); // create a new food category with the specified name

                if (txtAddLocation.Text != "")
                {
                    newLocation.RoomName = txtAddLocation.Text;

                    db.Locations.Add(newLocation); // add the new food category record
                    db.SaveChanges();
                }
                else
                    lblMessage.Text = "You must specify a location";
            }
        }

        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    
}