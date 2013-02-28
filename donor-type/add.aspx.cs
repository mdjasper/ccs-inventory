using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{

    private String pageTarget;
    private String[] donorInfo;

    protected void Page_Load(object sender, EventArgs e)
    {
            loadPassedSessionInfo();
    }


    //******************************** loadPassedSessionInfo ********************************//
    private void loadPassedSessionInfo()
    {
        try
        {
            if (Session["PassedDonorInfo"] != null)
            {
                donorInfo = Session["PassedDonorInfo"] as String[];
            }

            if (Session["PassedDonorURL"] != null)
            {
                pageTarget = (String)Session["PassedDonorURL"];
            }

        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //****************************** btnSave ***********************************//
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";   //empty out the error message field
            validateDonorTypeInput();

            //insert the value only if there are no errors
            if (lblMessage.Text.Length == 0 || lblMessage.Text == null)
            {
                insertNewDonorTypeIfNotExist();
            }

            if (lblMessage.Text.Length == 0 || lblMessage.Text == null)
            {
                if (pageTarget != null)
                {
                    if (donorInfo != null)
                    {
                        Session["PreviousPage"] = "donor-type add.aspx";
                        Session["PassedDonorInfo"] = donorInfo;
                    }

                    Response.Redirect(pageTarget);
                    
                }
                else
                {
                    Session["PreviousPage"] = null;
                    Session["PassedDonorInfo"] = null;
                    Response.Redirect("default.aspx"); //to prevent resubmission
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


    //****************************** validateDonorTypeInput ***********************************//
    private void validateDonorTypeInput()
    {
        try
        {
            //check if the New Donor Type is null
            if (txtNewDonorType.Text == null || txtNewDonorType.Text.Equals(""))
                lblMessage.Text += "The Donor Type cannot be empty.<br/>";

            if (txtNewDonorType.Text.Length > 50)
            {
                lblMessage.Text += "The Donor Type cannot exceed 50 characters.<br/>";
            }

        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //****************************** insertNewDonorTypeIfNotExist ***********************************//
    private void insertNewDonorTypeIfNotExist()
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                FoodSourceType donorType = new FoodSourceType();

                //must check that do not exist prior to adding into database
                FoodSourceType lookupResult = (from t in db.FoodSourceTypes
                                               where t.FoodSourceType1.Equals(txtNewDonorType.Text, StringComparison.OrdinalIgnoreCase)
                                               select t).FirstOrDefault();

                if (lookupResult != null)
                {
                    lblMessage.Text += txtNewDonorType.Text + " Donor Type exists already!<br/>";
                }
                else
                {
                    //insert the donor type in the text field to the database.
                    donorType.FoodSourceType1 = txtNewDonorType.Text.ToString();
                    db.FoodSourceTypes.Add(donorType);
                    db.SaveChanges();

                    txtNewDonorType.Text = ""; //reset the field to empty

                    LogChange.logChange("Donor Type " + donorType.FoodSourceType1 + " was added.", DateTime.Now, short.Parse(Session["userID"].ToString()));
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


    //****************************** btnCancel ***********************************//
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (pageTarget != null)
            {
                if (donorInfo != null)
                {
                    Session["PreviousPage"] = "donor-type add.aspx";
                    Session["PassedDonorInfo"] = donorInfo;
                }

                Response.Redirect(pageTarget);
            }
            else
            {
                Session["PreviousPage"] = null;
                Session["PassedDonorInfo"] = null;
                Response.Redirect("default.aspx"); //goes back to the donor type default page
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