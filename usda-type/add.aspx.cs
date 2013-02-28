using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddUSDACategory : System.Web.UI.Page
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

    protected void btnAddUSDATypeSubmit_Click(object sender, EventArgs e)
    {
        addUSDAType();
    }

    private void addUSDAType()
    {
        try
        {
            lblMessage.Text = "";

            using (CCSEntities db = new CCSEntities())
            {
                USDACategory uc = new USDACategory(); // create a new food category with the specified name

                if (txtUSDADescription.Text == "")              // cannot enter a blank usda name
                    lblMessage.Text = "You must enter a USDA Category Name";
                else if (txtUSDADescription.Text.Length > 30)   // usda name is too long for database 
                    lblMessage.Text = "The USDA Category Name cannot be longer than 30 characters in length.";
                else if (txtUSDANumber.Text == "")          // cannot enter a blank usda number
                    lblMessage.Text = "You must enter a USDA Category Number";
                else if (txtUSDANumber.Text.Length > 20)    // usda number is too long for database 
                    lblMessage.Text = "The USDA Category Number cannot be longer than 20 characters in length.";
                else if(isUSDACategoryPresent(txtUSDADescription.Text))
                    lblMessage.Text = "A USDA Cateogry with that name already exists.";
                else if(isUSDANumberPresent(txtUSDANumber.Text))
                    lblMessage.Text = "A USDA Cateogry with that number already exists.";
                else
                {
                    uc.Description = txtUSDADescription.Text;
                    uc.USDANumber = txtUSDANumber.Text;

                    db.USDACategories.Add(uc); // add the new usda category record
                    db.SaveChanges();

                    LogChange.logChange("Added USDA category called " + uc.Description + ".", DateTime.Now, short.Parse(Session["UserID"].ToString()));

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

    private Boolean isUSDANumberPresent(String ucNumber)
    {
        Boolean result = true;
        USDACategory uc;
        try
        {
            lblMessage.Text = "";

            using (CCSEntities db = new CCSEntities())
            {
                uc = (from category in db.USDACategories
                      where category.USDANumber.Equals(ucNumber)
                      select category).FirstOrDefault();
            }

            if (uc == null)
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

    private Boolean isUSDACategoryPresent(String ucName)
    {
        Boolean result = true;
        USDACategory uc;
        try
        {
            lblMessage.Text = "";

            using (CCSEntities db = new CCSEntities())
            {
                uc = (from category in db.USDACategories
                      where category.Description.Equals(ucName)
                      select category).FirstOrDefault();
            }

            if (uc == null)
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