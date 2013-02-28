using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    private short passedFoodInID;


    //***************************** Page_Load **************************************//
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            loadFoodInID(); //loads and converts the foodSourceID

            
            if (!Page.IsPostBack)
            {
                loadFoodIn();
                loadPassedSessionInfo();
            }

            if (Config.MOBILE()) //if on mobile device
            {
                ckbxPrint.Checked = false;
                ckbxPrint.Enabled = false;
            }

        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

    }


    //******************************** loadPassedSessionInfo ********************************//
    private void loadPassedSessionInfo()
    {
        try
        {
            if (Session["PassedFoodInInfo"] != null)
            {
                String[] passedfoodInInfo = Session["PassedFoodInInfo"] as String[];
                txtDonorName.Text = passedfoodInInfo[0];
                txtCategoryType.Text = passedfoodInInfo[1];
                txtWeight.Text = passedfoodInInfo[2];
                txtUSDACases.Text = passedfoodInInfo[3];
                txtUSDAItemNo.Text = passedfoodInInfo[4];
                ckbxIsUSDA.Checked = passedfoodInInfo[5].Equals("true") ? true : false;
                ckbxPrint.Checked = passedfoodInInfo[6].Equals("true") ? true : false;
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //******************************** loadFoodInID ********************************//
    private void loadFoodInID()
    {
        short num;

        try
        {
            if ((Request.QueryString["id"] != null) && (short.TryParse(Request.QueryString["id"], out num))) //checks that it is a number
            {
                passedFoodInID = short.Parse(Request.QueryString["id"]);
            }
            else
            {
                Response.Redirect("default.aspx"); //redirect to the default foodIn page
            }

        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //******************************** loadFoodIn method ********************************//
    private void loadFoodIn()
    {
        //ckbxIsUSDA
        //txtDonorName
        //txtUSDAItemNo (null)
        //txtUSDACases (null)
        //txtCategoryType(null)
        //txtWeight

        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                var foodResult = (from d in db.FoodIns
                                 where d.FoodInID == passedFoodInID
                                 select new
                                 {
                                    d.TimeStamp,
                                    d.Weight,
                                    d.Count,
                                    d.FoodSource.Source,
                                    d.FoodCategory.CategoryType,
                                    d.USDACategory.USDANumber
                                 }).FirstOrDefault();

                if (foodResult != null)
                {
                    txtDonorName.Text = foodResult.Source.ToString();
                    txtWeight.Text = foodResult.Weight.ToString();

                    if (txtDonorName.Text.ToString().ToLower() == "usda")
                    {
                        ckbxIsUSDA.Checked = true;
                    }
                    else 
                    {
                        ckbxIsUSDA.Checked = false;
                    }


                    if (foodResult.USDANumber != null)
                    {
                        txtUSDAItemNo.Text = foodResult.USDANumber.ToString();
                    }
                    else
                    {
                        txtUSDAItemNo.Text = "";
                    }


                    if (foodResult.CategoryType != null)
                    {
                        txtCategoryType.Text = foodResult.CategoryType.ToString();
                    }
                    else
                    {
                        txtCategoryType.Text = "";
                    }

                    
                    if (foodResult.Count != null)
                    {
                        txtUSDACases.Text = foodResult.Count.ToString();
                    }
                    else
                    {
                        txtUSDACases.Text = "";
                    }

                }
                else
                {
                    lblMessage.Text += "The Food Donation with ID " + passedFoodInID + " could not be found.<br/>";
                }
            }//end of using connection
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //***************************** btnSave_Click method **************************************//
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = ""; //empty out the error message field

            checkForNullFields(); //makes sure the required database fields are not null or empty
            formValidation();   //validate the form information

            if (lblMessage.Text.Length == 0 || lblMessage.Text == null)
            {
                if (ckbxIsUSDA.Checked == true)
                {
                    updateIncomingUSDADonation();   //validate the form information
                }

                if (ckbxIsUSDA.Checked == false)
                {
                    updateIncomingNonUSDADonation();

                    if (lblMessage.Text.Length == 0 || lblMessage.Text == null)
                    {
                        if (ckbxPrint.Checked == true && Config.MOBILE() == false)
                        {
                            getInKindPrintPage();
                        }
                    }
                }
            }

            if (lblMessage.Text.ToString().Length == 0)
            {
                Session["PassedFoodInInfo"] = null;
                Session["PassedFoodInURL"] = null;
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


    //***************************** checkForNullFields **************************************//
    private void checkForNullFields()
    {
        //checks that 2 required input fields are not empty
        //txtDonorName IE the FoodSource
        //txtWeight IE the weight 
        try
        {
            if (ckbxIsUSDA.Checked == true)
            {
                using (CCSEntities db = new CCSEntities())
                {
                    var dnrRslt = (from d in db.FoodSources
                                   where d.Source.Equals("usda", StringComparison.OrdinalIgnoreCase)
                                   && d.FoodSourceType.FoodSourceType1.Equals("usda", StringComparison.OrdinalIgnoreCase)
                                   select d).FirstOrDefault();

                    if (dnrRslt != null)
                    {
                        txtDonorName.Text = dnrRslt.Source;

                        if (txtUSDACases.Text == null || txtUSDACases.Text.Equals(""))
                        {
                            lblMessage.Text += "Number of Cases must be provided for a USDA type donation.<br/>";
                        }
                    }
                    else
                        lblMessage.Text += "A problem occured when linking USDA to the USDA donor.<br/>";
                }
            }

            if (txtDonorName.Text == null || txtDonorName.Text.Equals(""))
                lblMessage.Text += "The Donor cannot be empty.<br/>";

            if (txtWeight.Text.ToString() == null || txtWeight.Text.ToString().Equals(""))
                lblMessage.Text += "The Weight cannot be empty.<br/>";
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //***************************** formValidation method **************************************//
    private void formValidation()
    {
        try
        {
            double num;
            //checks that the specified zipcode field is a number before inserting it into the database
            if (!(double.TryParse(txtWeight.Text.ToString(), out num))) //checks that the weight is a number
            {
                lblMessage.Text += "The Weight must be a number.<br/>";
            }

            if (double.TryParse(txtWeight.Text.ToString(), out num)) //weight is a number
            {
                double weight = double.Parse(txtWeight.Text.ToString());
                // Math.Round(weight, 2);
                if (weight > 9999999.99)
                {
                    lblMessage.Text += "The Weight amount exceeds the database numerical limit to be saved.<br/>";
                }

                if (weight == 0)
                {
                    lblMessage.Text += "The Weight for the donation cannot be zero.<br/>";
                }

                if (weight < 0)
                {
                    lblMessage.Text += "The Weight for the donation cannot be a negative number.<br/>";
                }
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    } //end of validation method


    //***************************** updateIncomingNonUSDADonation method **************************************//
    private void updateIncomingNonUSDADonation()
    {
        try
        {
            FoodSource fs = lookupDonor();
            FoodCategory fc = null;
            if (txtCategoryType.Text.Length != 0)
                fc = lookupCategory();

            if (fs == null)
            {
                lblMessage.Text += "The donation cannot be added yet. Please create the new Donor:  " + txtDonorName.Text + "  first.<br/>";
            }

            if (fs != null && lblMessage.Text.Length == 0)
            {
                using (CCSEntities db = new CCSEntities())
                {
                    FoodIn updateDonation = (from d in db.FoodIns
                                             where d.FoodInID == passedFoodInID
                                             select d).FirstOrDefault();

                    if (updateDonation != null)
                    {
                        //insert the following nonUSDA fields
                        //TimeStamp (not null)
                        //Weight (not null)
                        //FoodSourceID(not null)
                        //CategoryID (nullable)

                        //adds a timestamp to the donation record
                        DateTime ds = DateTime.Now;
                        updateDonation.TimeStamp = ds;

                        //adds the weight
                        decimal donationWeight = decimal.Parse(txtWeight.Text.ToString());
                        donationWeight = Math.Round(donationWeight, 2);
                        updateDonation.Weight = donationWeight;

                        //adds the FoodSourceID of the txtDonorName
                        updateDonation.FoodSourceID = fs.FoodSourceID;

                        if (fc == null && txtCategoryType.Text.Length != 0)
                        {
                            lblMessage.Text += "The donation cannot be added yet. Please create the new Category Type:  " + txtCategoryType.Text + "  first.<br/>";
                        }

                        if (fc != null)
                        {
                            //adds the FoodCategoryID of the txtCategoryType
                            updateDonation.FoodCategoryID = fc.FoodCategoryID;
                        }
                        else 
                        {
                            updateDonation.FoodCategoryID = null;
                        }

                        updateDonation.Count = null; //cases
                        updateDonation.USDAID = null;

                        //check if not able to print the donation b/c not an inkind donation
                        //and check that the print checkbox is selected.
                        if (ckbxPrint.Checked == true)
                        {
                            if (checkCanPrint() == false)
                            {
                                lblMessage.Text += "Only In-Kind donor types can have In-Kind receipts printed.<br/>"
                                    + "Print In-Kind Receipt checkbox has been changed to not request printing.<br/>";
                                ckbxPrint.Checked = false; //set it to false. 
                            }

                            if (Config.MOBILE()) //if on mobile device
                            {
                                lblMessage.Text += "Printing is only allowed on the desktop.<br/>"
                                    + "Print In-Kind Receipt checkbox has been changed to not request printing.<br/>";
                                ckbxPrint.Checked = false; //set it to false. 
                            }

                        }

                        if (lblMessage.Text.Length == 0)
                        {
                            db.SaveChanges();
                            LogChange.logChange("Incoming food from " + fs.Source + " was edited.", DateTime.Now, short.Parse(Session["userID"].ToString()));
                        }
                    }
                }//end of using 
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }//end of updateIncomingDonation method


    //***************************** updateIncomingUSDADonation method **************************************//
    private void updateIncomingUSDADonation()
    {
        try
        {
            FoodSource fs = lookupDonor();
            FoodCategory fc = null;
            USDACategory us = null;

            if (txtCategoryType.Text.Length != 0)
                fc = lookupCategory();

            if (txtUSDAItemNo.Text.Length != 0)
                us = lookupUSDA();

            if (fs == null)
            {
                lblMessage.Text += "Problem occured when linking the donation with the USDA donor: " + txtDonorName.Text + ".<br/>";
            }

            if (fs != null && lblMessage.Text.Length == 0)
            {
                using (CCSEntities db = new CCSEntities())
                {
                    FoodIn updateDonation = (from d in db.FoodIns
                                             where d.FoodInID == passedFoodInID
                                             select d).FirstOrDefault();

                    if (updateDonation != null)
                    {
                        //insert the following USDA fields
                        //TimeStamp (not null)
                        //Weight (not null)
                        //FoodSourceID(not null)
                        //CategoryID (nullable)
                        //USDAID (nullable)
                        //Units (nullable)


                        //adds a timestamp to the donation record
                        DateTime ds = DateTime.Now;
                        updateDonation.TimeStamp = ds;

                        //adds the weight
                        decimal donationWeight = decimal.Parse(txtWeight.Text.ToString());
                        donationWeight = Math.Round(donationWeight, 2);
                        updateDonation.Weight = donationWeight;

                        //adds the FoodSourceID of the txtDonorName
                        updateDonation.FoodSourceID = fs.FoodSourceID;

                        if (fc == null && txtCategoryType.Text.Length != 0)
                        {
                            lblMessage.Text += "The donation cannot be added yet. Please create the new Category Type:  " + txtCategoryType.Text + "  first.<br/>";
                        }

                        if (fc != null)
                        {
                            //adds the FoodCategoryID of the txtCategoryType
                            updateDonation.FoodCategoryID = fc.FoodCategoryID;
                        }
                        else
                        {
                            updateDonation.FoodCategoryID = null;
                        }


                        if (us == null && txtUSDAItemNo.Text.Length != 0)
                        {
                            lblMessage.Text += "The donation cannot be added yet. Please create the new USDA Item Number:  " + txtUSDAItemNo.Text + "  first.<br/>";
                        }

                        if (us != null)
                        {
                            //adds the USDAID of the txtUSDAItemNo
                            updateDonation.USDAID = us.USDAID;
                        }
                        else
                        {
                            updateDonation.USDAID = null;
                        }

                        if (txtUSDACases.Text.Length != 0)
                        {
                            short cases;
                            //check if not a number 
                            if (!(short.TryParse(txtUSDACases.Text, out cases)))
                            {
                                lblMessage.Text += "The Number of Cases is not a number.<br/>";
                            }

                            if (short.TryParse(txtUSDACases.Text, out cases))
                            {
                                short numCases = short.Parse(txtUSDACases.Text);

                                if (numCases == 0)
                                {
                                    lblMessage.Text += "The Number of Cases cannot be zero.<br/>";
                                }

                                if (numCases < 0)
                                {
                                    lblMessage.Text += "The Number of Cases cannot be a negative number.<br/>";
                                }

                                if (numCases > 0)
                                {
                                    //adds the number of cases IE Units to the donation record
                                    updateDonation.Count = numCases;
                                }
                            }
                        }


                        //check if not able to print the donation b/c not an inkind donation
                        //and check that the print checkbox is selected.
                        if (ckbxPrint.Checked == true)
                        {
                            lblMessage.Text += "Only In-Kind donor types can have In-Kind receipts printed.<br/>"
                                + "Print In-Kind Receipt checkbox has been changed to not request printing.<br/>";
                            ckbxPrint.Checked = false; //set it to false. 

                            if (Config.MOBILE()) //if on mobile device
                            {
                                lblMessage.Text += "Printing is only allowed on the desktop.<br/>"
                                    + "Print In-Kind Receipt checkbox has been changed to not request printing.<br/>";
                                ckbxPrint.Checked = false; //set it to false. 
                            }
                        }


                        if (lblMessage.Text.Length == 0)
                        {
                            db.SaveChanges();
                            LogChange.logChange("Incoming USDA food from " + fs.Source + " was edited.", DateTime.Now, short.Parse(Session["userID"].ToString()));
                        }
                    }
                }//end of using
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }//end of updateIncomingDonation method


    //***************************** lookupDonor method **************************************//
    private FoodSource lookupDonor()
    {
        FoodSource fs = null;
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                var dnrRslt = (from d in db.FoodSources
                               where d.Source.Equals(txtDonorName.Text, StringComparison.OrdinalIgnoreCase)
                               select d).FirstOrDefault();

                if (dnrRslt != null)
                    fs = dnrRslt;
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
        return fs;
    }


    //***************************** lookupCategory method **************************************//
    private FoodCategory lookupCategory()
    {
        FoodCategory fc = null;
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                var categoryRslt = (from d in db.FoodCategories
                                    where d.CategoryType.Equals(txtCategoryType.Text, StringComparison.OrdinalIgnoreCase)
                                    select d).FirstOrDefault();
                if (categoryRslt != null)
                    fc = categoryRslt;
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
        return fc;
    }


    //***************************** lookupUSDA method **************************************//
    private USDACategory lookupUSDA()
    {
        USDACategory us = null;
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                var usCategoryRslt = (from d in db.USDACategories
                                      where d.USDANumber.Equals(txtUSDAItemNo.Text, StringComparison.OrdinalIgnoreCase)
                                      select d).FirstOrDefault();
                if (usCategoryRslt != null)
                    us = usCategoryRslt;
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
        return us;
    }


    //***************************** btnCancel_Click method **************************************//
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Session["PassedFoodInInfo"] = null;
            Session["PassedFoodInURL"] = null;
            Response.Redirect("default.aspx");
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //***************************** btnAddDonor_Click method **************************************//
    protected void btnAddDonor_Click(object sender, EventArgs e)
    {
        try
        {
            String[] foodIn = new String[7];
            foodIn[0] = txtDonorName.Text.ToString().Length == 0 ? "" : txtDonorName.Text;
            foodIn[1] = txtCategoryType.Text.ToString().Length == 0 ? "" : txtCategoryType.Text;
            foodIn[2] = txtWeight.Text.ToString().Length == 0 ? "" : txtWeight.Text;
            foodIn[3] = txtUSDACases.Text.ToString().Length == 0 ? "" : txtUSDACases.Text;
            foodIn[4] = txtUSDAItemNo.Text.ToString().Length == 0 ? "" : txtUSDAItemNo.Text;
            foodIn[5] = ckbxIsUSDA.Checked == true ? "true" : "false";
            foodIn[6] = ckbxPrint.Checked == true ? "true" : "false";

            Session["PassedFoodInInfo"] = foodIn;
            Session["PassedFoodInURL"] = "../incoming-food/edit.aspx?id=" + passedFoodInID;
            Response.Redirect("../donor/add.aspx");
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //***************************** btnAddUSDA_Click method **************************************//
    protected void btnAddUSDA_Click(object sender, EventArgs e)
    {
        try
        {
            String[] foodIn = new String[7];
            foodIn[0] = txtDonorName.Text.ToString().Length == 0 ? "" : txtDonorName.Text;
            foodIn[1] = txtCategoryType.Text.ToString().Length == 0 ? "" : txtCategoryType.Text;
            foodIn[2] = txtWeight.Text.ToString().Length == 0 ? "" : txtWeight.Text;
            foodIn[3] = txtUSDACases.Text.ToString().Length == 0 ? "" : txtUSDACases.Text;
            foodIn[4] = txtUSDAItemNo.Text.ToString().Length == 0 ? "" : txtUSDAItemNo.Text;
            foodIn[5] = ckbxIsUSDA.Checked == true ? "true" : "false";
            foodIn[6] = ckbxPrint.Checked == true ? "true" : "false";

            Session["PassedFoodInInfo"] = foodIn;
            Session["PassedFoodInURL"] = "../incoming-food/edit.aspx?id=" + passedFoodInID;

            Response.Redirect("../usda-type/default.aspx");
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //***************************** btnAddCategory_Click method **************************************//
    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        try
        {
            String[] foodIn = new String[7];
            foodIn[0] = txtDonorName.Text.ToString().Length == 0 ? "" : txtDonorName.Text;
            foodIn[1] = txtCategoryType.Text.ToString().Length == 0 ? "" : txtCategoryType.Text;
            foodIn[2] = txtWeight.Text.ToString().Length == 0 ? "" : txtWeight.Text;
            foodIn[3] = txtUSDACases.Text.ToString().Length == 0 ? "" : txtUSDACases.Text;
            foodIn[4] = txtUSDAItemNo.Text.ToString().Length == 0 ? "" : txtUSDAItemNo.Text;
            foodIn[5] = ckbxIsUSDA.Checked == true ? "true" : "false";
            foodIn[6] = ckbxPrint.Checked == true ? "true" : "false";

            Session["PassedFoodInInfo"] = foodIn;
            Session["PassedFoodInURL"] = "../incoming-food/edit.aspx?id=" + passedFoodInID;
            Response.Redirect("../food-type/default.aspx");
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //***************************** checkCanPrint method **************************************//
    private bool checkCanPrint()
    {

        bool canPrint = false;
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                var dnrTypeRslt = (from d in db.FoodSources
                                   where d.Source.Equals(txtDonorName.Text, StringComparison.OrdinalIgnoreCase)
                                   select d.FoodSourceType.FoodSourceType1).FirstOrDefault();

                if (dnrTypeRslt != null)
                {
                    if (dnrTypeRslt.ToString().ToLower().StartsWith("in-kind") == false)
                    {
                        canPrint = false;
                    }

                    if (dnrTypeRslt.ToString().ToLower().StartsWith("in-kind") == true)
                    {
                        canPrint = true;
                    }
                }

            }

        } //end of try
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

        return canPrint;

    }//end of checkCanPrint method


    //***************************** getInKindPrintPage method **************************************//
    private void getInKindPrintPage()
    {
        try
        {
            bool foundRecord = false;
            short recordId = -1;

            using (CCSEntities db = new CCSEntities())
            {
                //check the the donation that was just added  to the database on the submit button was a in-kind donation
                var dnrRslt = (from d in db.FoodSources
                               where d.Source.Equals(txtDonorName.Text, StringComparison.OrdinalIgnoreCase)
                               && d.FoodSourceType.FoodSourceType1.ToLower().StartsWith("in-kind")
                               select d).FirstOrDefault();

                if (dnrRslt != null)
                {
                    //gets the weight
                    decimal donationWeight = decimal.Parse(txtWeight.Text.ToString());
                    donationWeight = Math.Round(donationWeight, 2);

                    //finds and orders by the most recent datetime first to get the record's id
                    var foodRslt = (from d in db.FoodIns.OrderByDescending(x => x.TimeStamp)
                                    where d.FoodSource.Source.Equals(txtDonorName.Text, StringComparison.OrdinalIgnoreCase)
                                    && d.Weight == donationWeight
                                    select d).FirstOrDefault();

                    if (foodRslt != null)
                    {
                        recordId = foodRslt.FoodInID;
                        foundRecord = true; //did find the recently added food-in record and its id
                    }

                }//end of using

                if (foundRecord == true)
                {
                    //redirect to the print out of inkind donation page
                    //only if the donation type is an inkind.
                    if (recordId != -1)
                    {
                        Session["PassedFoodInInfo"] = null;
                        Session["PassedFoodInURL"] = null;
                        Response.Redirect("print.aspx?id=" + recordId);
                    }
                }

            }

        } //end of try
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    } //end of getInKindPrintPage() method


}