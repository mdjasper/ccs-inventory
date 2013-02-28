using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class USDACategoryEditor : System.Web.UI.Page
{
    private int id;
    private USDACategory uc;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            loadUSDACategory();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        updateUSDACategory();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (!isUSDACategoryUsed())
        {
            removeUSDACategory();
        }
        else
            lblMessage.Text = "You cannot delete this USDA Category because it is in use";
    }

    private void updateUSDACategory()
    {
        try
        {
            lblMessage.Text = "";
            id = Int16.Parse(lblID.Text);

            using (CCSEntities db = new CCSEntities())
            {
                // ensure that the record is selected
                uc = (from category in db.USDACategories
                      where category.USDAID == id
                      select category).FirstOrDefault();
                if (uc != null)
                {
                    if (txtDescription.Text == "")              // cannot enter a blank usda name
                        lblMessage.Text = "You must enter a USDA Category Name";
                    else if (txtDescription.Text.Length > 30)   // usda name is too long for database 
                        lblMessage.Text = "The USDA Category Name cannot be longer than 30 characters in length.";
                    else if (txtUSDANumber.Text == "")          // cannot enter a blank usda number
                        lblMessage.Text = "You must enter a USDA Category Number";
                    else if (txtUSDANumber.Text.Length > 20)    // usda number is too long for database 
                        lblMessage.Text =  "The USDA Category Number cannot be longer than 20 characters in length.";
                    else                                        // passes validation
                    {
                        uc.Description = txtDescription.Text;   // update name

                        uc.USDANumber = txtUSDANumber.Text;     // update USDA number

                        db.SaveChanges();                       // commit

                        LogChange.logChange("Edited USDA category called " + uc.Description + ".", DateTime.Now, short.Parse(Session["UserID"].ToString()));
                        Response.Redirect("default.aspx");
                    }
                }
                else
                    lblMessage.Text = "An unexpected problem occurred: Please try again later.";
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private void loadUSDACategory()
    {
        try
        {
            lblMessage.Text = "";

            if (Request.QueryString["id"] != null)
                id = int.Parse(Request.QueryString["id"]);
            else
                Response.Redirect("default.aspx");

            using (CCSEntities db = new CCSEntities())
            {
                uc = (from category in db.USDACategories
                      where category.USDAID == id
                      select category).FirstOrDefault();
            }

            if (uc != null)
            {
                lblID.Text = id.ToString();

                txtDescription.Text = uc.Description;

                txtUSDANumber.Text = uc.USDANumber;
            }
            else
                lblMessage.Text = "The USDA Category with ID " + id + " could not be found.";

        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private void removeUSDACategory()
    {
        try
        {
            lblMessage.Text = "";
            id = Int16.Parse(lblID.Text);

            using (CCSEntities db = new CCSEntities())
            {
                // ensure that the record is selected
                uc = (from category in db.USDACategories
                      where category.USDAID == id
                      select category).FirstOrDefault();

                if (uc != null)
                {
                    String categoryName = uc.Description;   // saved for logging purposes
                    db.USDACategories.Remove(uc);   // remove specified record
                    db.SaveChanges();               // commit changes

                    LogChange.logChange("Removed USDA category called " + categoryName + ".", DateTime.Now, short.Parse(Session["UserID"].ToString()));
                    Response.Redirect("default.aspx");
                }
                else
                    lblMessage.Text = "An unexpected problem occurred: Please try again later.";
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private Boolean isUSDACategoryUsed()
    {
        Boolean result = true;

        Container c;
        FoodIn fi;
        FoodOut fo;

        try
        {
            id = Int16.Parse(lblID.Text);

            using (CCSEntities db = new CCSEntities())
            {
                c = (from container in db.Containers
                     where container.USDAID == id
                     select container).FirstOrDefault();

                fi = (from foodin in db.FoodIns
                      where foodin.USDAID == id
                      select foodin).FirstOrDefault();

                fo = (from foodout in db.FoodOuts
                      where foodout.USDAID == id
                      select foodout).FirstOrDefault();
            }

            if (c == null && fi == null && fo == null)
                result = false;
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

        return result;
    }
}