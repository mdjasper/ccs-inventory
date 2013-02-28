using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    private int id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) { 
            loadLocation(); 
        }
    }
    private void loadLocation()
    {
        lblMessage.Text = "";

        if (Request.QueryString["id"] != null)
            id = int.Parse(Request.QueryString["id"]);
        else
            Response.Redirect("default.aspx");

        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                var location = (from l in db.Locations
                      where l.LocationID == id
                      select l).FirstOrDefault();
            
                if (location != null)
                {
                    txtLocation.Text = location.RoomName;
                    txtLocationId.Text = location.LocationID.ToString();
                }
                else
                    lblMessage.Text = "The specified location was not found.";
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
        updateLocation();
    }
    private void updateLocation()
    {
        try
        {
            lblMessage.Text = "";
            id = int.Parse(Request.QueryString["id"]);
            using (CCSEntities db = new CCSEntities())
            {
                // ensure that the record is selected
                var location = (from l in db.Locations
                      where l.LocationID == id
                      select l).FirstOrDefault();
                if (location != null)
                {
                    if (txtLocation.Text == "") // check if name is empty
                        lblMessage.Text = "You must enter a location";

                    else if (txtLocation.Text.Length > 30)
                        lblMessage.Text = "The location name cannot be longer that 30 characters.";
                    else
                    {
                        location.RoomName = txtLocation.Text;         // update name

                        db.SaveChanges(); // commit

                        //Response.Redirect("default.aspx");
                    }
                }
                else
                    lblMessage.Text = "An unexpected error occurred.";
            }
        }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        removeLocation();
    }
    private void removeLocation()
    {
        try
        {
            lblMessage.Text = "";

            using (CCSEntities db = new CCSEntities())
            {
                // ensure that the record is selected
                var location = (from l in db.Locations
                      where l.LocationID == id
                      select l).FirstOrDefault();

                if (location != null)
                {
                    db.Locations.Remove(location);   // remove specified record
                    db.SaveChanges();               // commit changes

                    Response.Redirect("default.aspx");
                }
                else
                    lblMessage.Text = "An unexpected problem occurred: Please try again later.";
            }
        }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

    }

}