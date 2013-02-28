using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    private short passedDonorTypeID;
    private FoodSourceType dnrTypeResult;


    //************************ Page Load **************************************//
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            loadFoodSourceID(); //loads and converts the foodSourceID

            if (!IsPostBack) //prevent the code from being overridden
                loadFoodSourceType();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //******************************** loadFoodSourceID ********************************//
    private void loadFoodSourceID()
    {
        short num;

        try
        {
            if ((Request.QueryString["id"] != null) && (short.TryParse(Request.QueryString["id"], out num))) //checks that it is a number
            {
                passedDonorTypeID = short.Parse(Request.QueryString["id"]);
            }
            else
            {
                Response.Redirect("default.aspx"); //redirect to the default donor type page
            }

        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

    }


    //******************************** loadFoodSourceType method ********************************//
    private void loadFoodSourceType()
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                dnrTypeResult = (from d in db.FoodSourceTypes
                                 where d.FoodSourceTypeID == passedDonorTypeID
                                 select d).FirstOrDefault();
            }

            if (dnrTypeResult != null)
            {
                lblDonorTypeName.Text = dnrTypeResult.FoodSourceType1.ToString();
                txtDonorType.Text = dnrTypeResult.FoodSourceType1.ToString();
            }
            else
            {
                lblMessage.Text += "The Donor Type with ID " + passedDonorTypeID + " could not be found.<br/>";
            }

        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //******************************** updateFoodSourceTypeIfNotExist method ********************************//
    private void updateFoodSourceTypeIfNotExist()
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                
                dnrTypeResult = (from d in db.FoodSourceTypes
                                 where d.FoodSourceTypeID == passedDonorTypeID
                                 select d).FirstOrDefault();
     
                if (dnrTypeResult != null)
                {
                    
                    //must check that new donor type does not exist prior to updating the into database
                    FoodSourceType lookupResult = (from t in db.FoodSourceTypes
                                                    where t.FoodSourceType1.Equals(txtDonorType.Text)
                                                    select t).FirstOrDefault();

                    //if the updated donor type info doesn't already exist
                    if (lookupResult == null)
                    {
                        dnrTypeResult.FoodSourceType1 = txtDonorType.Text;
                        dnrTypeResult.FoodSourceTypeID = passedDonorTypeID;

                        //process update without the confirm page
                        dnrTypeResult.FoodSourceType1 = txtDonorType.Text;
                        db.SaveChanges(); // commit changes

                        LogChange.logChange("Donor Type " + dnrTypeResult.FoodSourceType1 + " was edited.", DateTime.Now, short.Parse(Session["userID"].ToString()));
                    }
                    else
                    {
                        lblMessage.Text += txtDonorType.Text + " Donor Type exists already!<br/>";
                    }
                    
                }
                else
                {
                    lblMessage.Text += "The Donor Type with ID " + passedDonorTypeID + " could not be found.<br/>";
                }
                 
            } //end of using connection

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
            if (txtDonorType.Text == null || txtDonorType.Text.Equals(""))
                lblMessage.Text += "The Donor Type cannot be empty.<br/>";

            if (txtDonorType.Text.Length > 50)
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


    //******************************** btnSubmit_Click method ********************************//
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            validateDonorTypeInput();

            if ( lblMessage.Text.Length == 0 || lblMessage.Text == null)
            {
                updateFoodSourceTypeIfNotExist();
            }

            if (lblMessage.Text.Length == 0 || lblMessage.Text == null)
            {
                Response.Redirect("default.aspx"); //to prevent resubmission
            }

        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //******************************** btnCancel_Click method ********************************//
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("default.aspx");
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


}