using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FoodCategoryEditor : System.Web.UI.Page
{
    private int id;
    private FoodCategory fc;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
            loadFoodCategory();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        updateFoodCategory();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (!isFoodCategoryUsed())
            removeFoodCategory();
        else
            lblMessage.Text = "You can't delete this Food Category because it is being used.";
    }

    private void updateFoodCategory()
    {
        try
        {
            lblMessage.Text = "";
            id = Int16.Parse(lblID.Text);

            using (CCSEntities db = new CCSEntities())
            {
                // ensure that the record is selected
                fc = (from category in db.FoodCategories
                      where category.FoodCategoryID == id
                      select category).FirstOrDefault();

                if (fc != null)
                {
                    if (txtName.Text == "") // check if name is empty
                        lblMessage.Text = "You must enter a Food Category Name";

                    else if (txtName.Text.Length > 30)
                        lblMessage.Text = "The Food Category Name cannot be longer that 30 characters.";
                    else if (isFoodCategoryPresent(txtName.Text))
                        lblMessage.Text = "A Food Category with that name already exists";
                    else
                    {
                        fc.CategoryType = txtName.Text;         // update name
                        fc.Perishable = cbPerishable.Checked;   // update perishable status
                        fc.NonFood = cbNonFood.Checked;         // update non food status

                        db.SaveChanges(); // commit

                        LogChange.logChange("Edited food category called " + fc.CategoryType + ".", DateTime.Now, short.Parse(Session["UserID"].ToString())); 
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

    private void loadFoodCategory()
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
                fc = (from category in db.FoodCategories
                      where category.FoodCategoryID == id
                      select category).FirstOrDefault();
            }

            if (fc != null)
            {
                lblID.Text = id.ToString();

                txtName.Text = fc.CategoryType;
                cbPerishable.Checked = fc.Perishable;
                cbNonFood.Checked = fc.NonFood;
            }
            else
                lblMessage.Text = "The Food Category with ID " + id + " could not be found.";

        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private void removeFoodCategory()
    {
        try
        {
            lblMessage.Text = "";
            id = Int16.Parse(lblID.Text);

            using (CCSEntities db = new CCSEntities())
            {
                // ensure that the record is selected
                fc = (from category in db.FoodCategories
                      where category.FoodCategoryID == id
                      select category).FirstOrDefault();

                if (fc != null)
                {
                    String categoryName = fc.CategoryType; // saved for logging purposes
                    db.FoodCategories.Remove(fc);   // remove specified record
                    db.SaveChanges();               // commit changes

                    LogChange.logChange("Removed food category called " + categoryName + ".", DateTime.Now, short.Parse(Session["UserID"].ToString()));
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

    // check if the category is being used in other entities
    private Boolean isFoodCategoryUsed()
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
                     where container.FoodCategoryID == id
                     select container).FirstOrDefault();

                fi = (from foodin in db.FoodIns
                      where foodin.FoodCategoryID == id
                      select foodin).FirstOrDefault();

                fo = (from foodout in db.FoodOuts
                      where foodout.FoodCategoryID == id
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

    // check if a category with the same name already exists
    private Boolean isFoodCategoryPresent(String fcName)
    {
        Boolean result = true;
        FoodCategory fc;
        try
        {
            lblMessage.Text = "";

            using (CCSEntities db = new CCSEntities())
            {
                fc = (from category in db.FoodCategories
                      where category.CategoryType.Equals(fcName)
                      select category).FirstOrDefault();
            }

            if (fc == null)
                result = false;
            else if (fc.FoodCategoryID == int.Parse(lblID.Text))
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