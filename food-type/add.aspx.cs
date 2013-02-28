using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddFoodCategory : System.Web.UI.Page
{
    private String pageTarget;
    private String[] passedfoodInInfo;

    protected void Page_Load(object sender, EventArgs e)
    {
        loadPassedFoodInInfo();
    }

    //******************************** loadPassedFoodInInfo ********************************//
    private void loadPassedFoodInInfo()
    {
        try
        {
            if (Session["PassedFoodInInfo"] != null)
            {
                passedfoodInInfo = Session["PassedFoodInInfo"] as String[];
            }

            if (Session["PassedFoodInURL"] != null)
            {
                pageTarget = (String)Session["PassedFoodInURL"];
            }

        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    protected void btnAddFoodTypeSubmit_Click(object sender, EventArgs e)
    {
        addFoodCategory();
    }

    private void addFoodCategory()
    {
        try
        {
            lblMessage.Text = "";

            using (CCSEntities db = new CCSEntities())
            {
                FoodCategory fc = new FoodCategory(); // create a new food category with the specified name

                if (txtAddFoodType.Text == "")
                    lblMessage.Text = "You must specify a Food Category name to add.";
                else if (txtAddFoodType.Text.Length > 30)
                    lblMessage.Text = "The Food Category name cannot exceed 30 characters.";
                else if (isFoodCategoryPresent(txtAddFoodType.Text))
                    lblMessage.Text = "A Food Category with that name already exists.";
                else
                {
                    fc.CategoryType = txtAddFoodType.Text;

                    if (cbPerishable.Checked)
                        fc.Perishable = true;
                    else
                        fc.Perishable = false;

                    if (cbNonFood.Checked)
                        fc.NonFood = true;
                    else
                        fc.NonFood = false;

                    db.FoodCategories.Add(fc); // add the new food category record
                    db.SaveChanges();

                    LogChange.logChange("Added food category called " + fc.CategoryType + ".", DateTime.Now, short.Parse(Session["UserID"].ToString()));

                    if (pageTarget != null)
                    {
                        if (passedfoodInInfo != null)
                        {
                            Session["PassedFoodInInfo"] = passedfoodInInfo;
                        }

                        Response.Redirect(pageTarget);
                    }
                    else
                    {
                        Response.Redirect("default.aspx");
                    }
                    
                }
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

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
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

        return result;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (pageTarget != null)
            {
                if (passedfoodInInfo != null)
                {
                    Session["PassedFoodInInfo"] = passedfoodInInfo;
                }

                Response.Redirect(pageTarget);
            }
            else
            {
                Response.Redirect("default.aspx");
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

    }
}