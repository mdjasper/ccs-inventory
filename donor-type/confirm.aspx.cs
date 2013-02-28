using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    private FoodSourceType donorTypeInfo; //to hold the session info
    private FoodSourceType dnrTypeResult;   //to hold the database results
    private String oldDnrTypeResult;


    //************************ Page Load **************************************//
    protected void Page_Load(object sender, EventArgs e)
    {
        loadPassedDonorTypeInfo();
    }


    //******************************** loadPassedDonorTypeInfo ********************************//
    private void loadPassedDonorTypeInfo()
    {
        try
        {
            donorTypeInfo = (FoodSourceType)Session["DonorTypeInfo"];
            oldDnrTypeResult = (String)Session["OldDonorTypeInfo"];

            if (donorTypeInfo == null)
            {
                Response.Redirect("default.aspx"); //go back to the home page
            }

            if (oldDnrTypeResult == null)
            {
                Response.Redirect("default.aspx"); //go back to the home page
            }

            if (donorTypeInfo.FoodSourceTypeID == 0)
            {
                lblMessage.Text += "Problem occurred when loading the Donor Type ID.<br/>";
                //Response.Redirect("default.aspx"); //go back to the home page
                //log error if neccessary
            }


            if (donorTypeInfo.FoodSourceType1 != null)
            {
                lblDonorType.Text = donorTypeInfo.FoodSourceType1.ToString();
            }
            else
            {
                lblDonorType.Text = "";
                lblMessage.Text += "Problem occured when loading the New Donor Type.<br/>";
                //Response.Redirect("default.aspx"); //go back to the home page
                //log error if neccessary
            }


            if (oldDnrTypeResult != null)
            {
                lblOldDonorType.Text = oldDnrTypeResult.ToString();
            }
            else
            {
                lblOldDonorType.Text = "";
                lblMessage.Text += "Problem occured when loading the Old Donor Type.<br/>";
                //Response.Redirect("default.aspx"); //go back to the home page
                //log error if neccessary
            }

        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    
    }


    //******************************** btnSubmit_Click ********************************//
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool changePage = false;
        try
        {
            if (lblMessage.Text.Length == 0)
            {
                using (CCSEntities db = new CCSEntities())
                {
                    dnrTypeResult = (from d in db.FoodSourceTypes
                                     where d.FoodSourceTypeID == donorTypeInfo.FoodSourceTypeID
                                     select d).FirstOrDefault();

                    if (dnrTypeResult != null)
                    {
                        //updates the info to what matches in the field
                        dnrTypeResult.FoodSourceType1 = lblDonorType.Text;
                        db.SaveChanges(); // commit changes

                        changePage = true;
                    }
                }//closes connection


                if (changePage == true)
                {
                    Response.Redirect("default.aspx");
                }

            }
            else 
            {
                lblMessage.Text += "Errors exist. Unable to process Donor Type modification.<br/>";
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

    }


    //******************************** btnCancel_Click ********************************//
    protected void btnCancel_Click(object sender, EventArgs e)
    {   try
        {
            if (donorTypeInfo.FoodSourceTypeID == 0)
            {
                //redirects to the default page
                Response.Redirect("default.aspx");
            }
            else
            {
                //redirects to the edit page and passes the donor type id back
                Response.Redirect("edit.aspx?id=" + donorTypeInfo.FoodSourceTypeID);
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