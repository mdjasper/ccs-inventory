using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Default2 : System.Web.UI.Page
{
    private List<FoodSourceType> listFoodSourcesTypes;
    private List<State> listUSStates;
    private String[] passedDonorInfo;
    private short passedDonorID;


    //******************************** Page_Load ********************************//
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            loadFoodSourceID(); //loads and converts the foodSourceID

            using (CCSEntities db = new CCSEntities())
            {

                if (!Page.IsPostBack)
                {
                    //adds the Donor types to the drop down list
                    listFoodSourcesTypes = db.FoodSourceTypes.ToList(); //gets the foodSourceType results from database
                    ddlDonorType.DataSource = listFoodSourcesTypes; //assigns FoodSourceType list to drop down list
                    ddlDonorType.DataBind();    //binds the source to the drop down list

                    //adds the States to the drop down list
                    listUSStates = db.States.ToList();
                    ddlState.DataSource = listUSStates;
                    ddlState.DataBind();

                    if (Session["PreviousPage"] == null)
                    {
                        loadFoodSource();
                    }
                    else
                    {
                        loadPassedSessionInfo();
                    }
                }
            }//end of database connection

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
                passedDonorID = short.Parse(Request.QueryString["id"]);
            }
            else
            {
                Response.Redirect("default.aspx"); //redirect to the default donor page
            }

        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

    }


    //******************************** loadFoodSource method ********************************//
    private void loadFoodSource()
    {
        
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                var dnrResult = (from d in db.FoodSources
                             where d.FoodSourceID == passedDonorID
                             select new { d.Source, d.StoreID, d.FoodSourceTypeID, d.Address.StreetAddress1, 
                                 d.Address.StreetAddress2, d.Address.City.CityName, d.Address.StateID, 
                                 d.Address.Zipcode.ZipCode1 }).FirstOrDefault();
            
                if (dnrResult != null)
                {
                    lblDonorName.Text = dnrResult.Source.ToString();
                    txtDonorName.Text = dnrResult.Source.ToString();

                    if (dnrResult.StoreID != null)
                    {
                        txtStoreID.Text = dnrResult.StoreID.ToString();  
                    }
                    else
                    {
                        txtStoreID.Text = "";
                    }
                    
                    
                    ddlDonorType.Items.FindByValue(dnrResult.FoodSourceTypeID.ToString()).Selected = true;
                    
                    txtStreet1.Text = dnrResult.StreetAddress1.ToString();
                    
                    if (dnrResult.StreetAddress2 != null)
                    {
                        txtStreet2.Text = dnrResult.StreetAddress2.ToString(); 
                    }
                    else
                    {
                        txtStreet2.Text = "";
                    }
                    
                    txtCity.Text = dnrResult.CityName.ToString();

                    ddlState.Items.FindByValue(dnrResult.StateID.ToString()).Selected = true;

                    txtZip.Text = dnrResult.ZipCode1.ToString();

                }
                else
                {
                    lblMessage.Text += "The Donor Type with ID " + passedDonorID + " could not be found.<br/>";
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


    //******************************** loadPassedSessionInfo ********************************//
    private void loadPassedSessionInfo()
    {
        try
        {
            if (Session["PassedDonorInfo"] != null)
            {
                passedDonorInfo = Session["PassedDonorInfo"] as String[];

                txtDonorName.Text = passedDonorInfo[0];
                txtStoreID.Text = passedDonorInfo[1];
                ddlDonorType.Items.FindByValue(passedDonorInfo[2]).Selected = true;
                txtStreet1.Text = passedDonorInfo[3];
                txtStreet2.Text = passedDonorInfo[4];
                txtCity.Text = passedDonorInfo[5];
                ddlState.Items.FindByValue(passedDonorInfo[6]).Selected = true;
                txtZip.Text = passedDonorInfo[7];
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = ""; //empty out the error message field

            checkForNullFields(); //makes sure the required database fields are not null or empty


            if (lblMessage.Text.Length == 0 || lblMessage.Text == null)
            {
                formValidation();   //validate the form information
            }

            if (lblMessage.Text.Length == 0 || lblMessage.Text == null)
            {
                insertCityIfNotExist();
            }

            if (lblMessage.Text.Length == 0 || lblMessage.Text == null)
            {
                insertZipIfNotExist();
            }

            if (lblMessage.Text.Length == 0 || lblMessage.Text == null)
            {
                insertAddrIfNotExist();
            }

            if (lblMessage.Text.Length == 0 || lblMessage.Text == null)
            {
                updateSelectedDonorInfo();
            }

            if (lblMessage.Text.Length == 0 || lblMessage.Text == null)
            {
                Session["PreviousPage"] = null;
                Session["PassedDonorInfo"] = null;
                Session["PassedDonorURL"] = null;
                Response.Redirect("default.aspx"); //goes back to the donor default page
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
        if (txtDonorName.Text == null || txtDonorName.Text.Equals(""))
            lblMessage.Text += "The Donor Name cannot be empty.<br/>";

        if (ddlDonorType.SelectedValue.ToString().Equals("0") || ddlDonorType.SelectedItem.ToString().Equals("< Select Donor Type >"))
            lblMessage.Text += "A Donor Type must be selected.<br/>";

        if (txtStreet1.Text == null || txtStreet1.Text.Equals(""))
            lblMessage.Text += "The Street Address 1 cannot be empty.<br/>";

        if (txtCity.Text == null || txtCity.Text.Equals(""))
            lblMessage.Text += "The City field cannot be empty.<br/>";

        if (ddlState.SelectedValue.ToString().Equals("0") || ddlState.SelectedItem.ToString().Equals("< Select a State >"))
            lblMessage.Text += "A State must be selected.<br/>";

        if (txtZip.Text == null || txtZip.Text.Equals(""))
            lblMessage.Text += "The Zip field cannot be empty.<br/>";
    }


    //***************************** lookupZipcode **************************************//
    private Zipcode lookupZipcode()
    {
        Zipcode result = null;
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                //must check if exists
                result = (from t in db.Zipcodes
                          where t.ZipCode1.Equals(txtZip.Text, StringComparison.OrdinalIgnoreCase)
                          select t).FirstOrDefault();
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
        return result;
    }


    //***************************** lookupCity **************************************//
    private City lookupCity()
    {
        City result = null;
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                //must check if exists
                result = (from t in db.Cities
                          where t.CityName.Equals(txtCity.Text, StringComparison.OrdinalIgnoreCase)
                          select t).FirstOrDefault();
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
        return result;
    }


    //***************************** lookupAddrRecord **************************************//
    private short findAddrRecord()
    {
        short recordID = -1;

        try
        {

            short selectedStateID = short.Parse(ddlState.SelectedValue.ToString());

            //look for address record in the address table that has the same address in all address fields
            if (txtStreet2.Text.Length == 0 || txtStreet2.Text == null)
            {
                using (CCSEntities db = new CCSEntities())
                {
                    var addrWithoutStr2 = (from a in db.Addresses
                                           where a.Zipcode.ZipCode1.Equals(txtZip.Text, StringComparison.OrdinalIgnoreCase)
                                           && a.City.CityName.Equals(txtCity.Text, StringComparison.OrdinalIgnoreCase)
                                           && a.State.StateID == selectedStateID
                                           && a.StreetAddress1.Equals(txtStreet1.Text, StringComparison.OrdinalIgnoreCase)
                                           select new { a.AddressID, a.StreetAddress1, a.City.CityName, a.State.StateFullName, a.Zipcode.ZipCode1 }).FirstOrDefault();

                    if (addrWithoutStr2 != null)
                        recordID = addrWithoutStr2.AddressID;

                }

            }
            else
            {
                using (CCSEntities db = new CCSEntities())
                {
                    var addrWithStr2 = (from a in db.Addresses
                                        where a.Zipcode.ZipCode1.Equals(txtZip.Text, StringComparison.OrdinalIgnoreCase)
                                        && a.City.CityName.Equals(txtCity.Text, StringComparison.OrdinalIgnoreCase)
                                        && a.State.StateID == selectedStateID
                                        && a.StreetAddress1.Equals(txtStreet1.Text, StringComparison.OrdinalIgnoreCase)
                                        && a.StreetAddress2.Equals(txtStreet2.Text, StringComparison.OrdinalIgnoreCase)
                                        select new { a.AddressID, a.StreetAddress1, a.StreetAddress2, a.City.CityName, a.State.StateFullName, a.Zipcode.ZipCode1 }).FirstOrDefault();

                    if (addrWithStr2 != null)
                        recordID = addrWithStr2.AddressID;
                }
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

        return recordID;
    }


    //***************************** lookupDonorRecord **************************************//
    private short findDonorRecord()
    {
        short recordID = -1;
        try
        {
            short selectedDonorTypeID = short.Parse(ddlDonorType.SelectedValue.ToString());

            short selectedAddrID = findAddrRecord();

            if (txtStoreID.Text.Length == 0 || txtStoreID.Text == null)
            {
                //look for donor record in the FoodSource table that has the same fields as the input provided
                using (CCSEntities db = new CCSEntities())
                {
                    var dnrRsltNoStoreID = (from d in db.FoodSources
                                            where d.Source.Equals(txtDonorName.Text, StringComparison.OrdinalIgnoreCase)
                                            && d.AddressID == selectedAddrID
                                            && d.FoodSourceTypeID == selectedDonorTypeID
                                            select d).FirstOrDefault();

                    if (dnrRsltNoStoreID != null)
                        recordID = dnrRsltNoStoreID.FoodSourceID;
                }
            }
            else //lookup record that has a store id
            {
                //look for donor record in the FoodSource table that has the same fields as the input provided
                using (CCSEntities db = new CCSEntities())
                {
                    var dnrRsltWthStoreID = (from d in db.FoodSources
                                             where d.Source.Equals(txtDonorName.Text, StringComparison.OrdinalIgnoreCase)
                                             && d.StoreID.Equals(txtStoreID.Text, StringComparison.OrdinalIgnoreCase)
                                             && d.FoodSourceTypeID == selectedDonorTypeID
                                             && d.AddressID == selectedAddrID
                                             select d).FirstOrDefault();

                    if (dnrRsltWthStoreID != null)
                        recordID = dnrRsltWthStoreID.FoodSourceID;

                }
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

        return recordID;
    }


    //***************************** insertCityIfNotExist **************************************//
    private void insertCityIfNotExist()
    {
        try
        {
            City ct = new City();
            City result = lookupCity();

            //the city doesn't exist already. Add it to the database.
            if (result == null)
            {
                using (CCSEntities db = new CCSEntities())
                {
                    ct.CityName = txtCity.Text;
                    db.Cities.Add(ct);
                    db.SaveChanges();

                }   //end of db connection
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //***************************** insertZipIfNotExist **************************************//
    private void insertZipIfNotExist()
    {
        try
        {
            Zipcode zp = new Zipcode();
            Zipcode result = lookupZipcode();

            //the zipcode doesn't exist already. Add it to the database.
            if (result == null)
            {
                using (CCSEntities db = new CCSEntities())
                {
                    zp.ZipCode1 = txtZip.Text.ToString();
                    db.Zipcodes.Add(zp);
                    db.SaveChanges();

                } //end of db connection
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //***************************** insertAddrIfNotExist **************************************//
    private void insertAddrIfNotExist()
    {
        try
        {
            Address addr = new Address();
            short addrRecordID = findAddrRecord();

            //an Address record with street addr 1, street addr2, and zipcode do not already exist. Add it to the database.
            if (addrRecordID == -1)
            {
                using (CCSEntities db = new CCSEntities())
                {
                    addr.StreetAddress1 = txtStreet1.Text.ToString();

                    if (!txtStreet2.Text.ToString().Equals(""))
                    {
                        addr.StreetAddress2 = txtStreet2.Text.ToString();
                    }

                    City cityResult = lookupCity();
                    short statesResult = short.Parse(ddlState.SelectedValue.ToString());
                    Zipcode zipResult = lookupZipcode();

                    if (cityResult != null && statesResult != 0 && zipResult != null && lblMessage.Text.Length == 0)
                    {
                        addr.CityID = cityResult.CityID;
                        addr.StateID = statesResult;    //adds the stateID
                        addr.ZipID = zipResult.ZipID;
                        db.Addresses.Add(addr);
                        db.SaveChanges();

                    }//end of if
                } //end of using db connection

            } //end of outer else           

        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //***************************** updateSelectedDonorInfo method **************************************//
    private void updateSelectedDonorInfo()
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {

                FoodSource dnrResult = (from d in db.FoodSources
                                 where d.FoodSourceID == passedDonorID
                                 select d).FirstOrDefault();

                //ensures that the donor to be updated exists in the database, prior to updating its fields
                if (dnrResult != null)
                {
                    //must check that new donor does not exist prior to updating it into the database
                    short donorRecordID = findDonorRecord();


                    //add the updates the donor info in the database if didn't find a donor with the new changes
                    if (donorRecordID == -1) // -1 means that no donorRecordId was found
                    {
                        //process the update of the donor.
                        dnrResult.Source = txtDonorName.Text; //adds the Source

                        if (!txtStoreID.Text.Equals(""))
                        {
                            dnrResult.StoreID = txtStoreID.Text.ToString(); //adds the storeID in not empty
                        }

                        short donorType = short.Parse(ddlDonorType.SelectedValue.ToString());
                        dnrResult.FoodSourceTypeID = donorType;    //adds the FoodSourceTypeID

                        short addrRecordID = findAddrRecord();

                        //add the donor to the database if the able to lookup the address after the address was just inserted in 
                        if (addrRecordID != -1 && lblMessage.Text.Length == 0)
                        {
                            dnrResult.AddressID = addrRecordID; //adds the AddressID to the FoodSource
                            db.SaveChanges(); //uncomment to update the donor

                            LogChange.logChange("Donor " + dnrResult.Source + " was edited.", DateTime.Now, short.Parse(Session["userID"].ToString()));
                        }
                        else
                        {
                            lblMessage.Text += "The donor was unable to be added.<br/>";
                        }
                    }
                    else
                    {
                        lblMessage.Text += "A Donor already exists with that updated donor information!<br/>";

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


    //***************************** formValidation method **************************************//
    private void formValidation()
    {
        try
        {
            //txtDonorName
            //txtStoreID
            //txtStreet1 *
            //txtStreet2 *
            //txtCity *
            //txtState
            //txtZip  (limit to 5)

            int num;

            //check that the values don't exceed database limits
            if (txtDonorName.Text.Length > 30) //checks that the street addr1 is not longer than 50 characters
            {
                lblMessage.Text += "The donor's name cannot exceed 30 characters.<br/>";
            }

            if (txtStoreID.Text.Length > 10) //checks that the street addr1 is not longer than 50 characters
            {
                lblMessage.Text += "The Store ID cannot exceed 10 characters.<br/>";

            }

            if (txtStreet1.Text.Length > 50) //checks that the street addr1 is not longer than 50 characters
            {
                lblMessage.Text += "The street address 1 field cannot exceed 50 characters.<br/>";
            }

            if (txtStreet2.Text.Length > 50) //checks that the street addr2 is not longer than 50 characters
            {
                lblMessage.Text += "The street address 2 field cannot exceed 50 characters.<br/>";
            }

            if (txtCity.Text.Length > 30) //checks that the city is not longer than 30 characters
            {
                lblMessage.Text += "The city cannot exceed 30 characters.<br/>";
            }

            if (txtZip.Text.Length != 5 || txtZip.Text == null) //checks that the zipcode is only 5 digits
            {
                lblMessage.Text += "The zipcode must be a 5 digits.<br/>";
            }



            //check if user input just spaces in all fields
            if (txtDonorName.Text.Length > 0 && txtDonorName.Text.Trim().Length == 0)
            {
                lblMessage.Text += "Spaces are not valid input for the Donor's name.<br/>";
            }

            if (txtCity.Text.Length > 0 && txtCity.Text.Trim().Length == 0)
            {
                lblMessage.Text += "Spaces are not valid input for the city.<br/>";
            }

            if (txtStreet1.Text.Length > 0 && txtStreet1.Text.Trim().Length == 0)
            {
                lblMessage.Text += "Spaces are not valid input for the street address 1 field.<br/>";
            }

            if (txtZip.Text.Length > 0 && txtZip.Text.Trim().Length == 0)
            {
                lblMessage.Text += "Spaces are not valid input for the zipcode.<br/>";
            }


            //checks that the specified zipcode field is a number before inserting it into the database
            if (!(int.TryParse(txtZip.Text, out num))) //checks that the zipcode is a number
            {
                lblMessage.Text += "The zipcode must be a number.<br/>";
            }

        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

    } //end of validation method


    //******************************** btnCancel_Click method ********************************//
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Session["PreviousPage"] = null;
            Session["PassedDonorInfo"] = null;
            Session["PassedDonorURL"] = null;
            Response.Redirect("default.aspx"); //redirect to the default donor page
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //***************************** btnAddDonorType_Click method **************************************//
    protected void btnAddDonorType_Click(object sender, EventArgs e)
    {
        try
        {
            String[] fs = new String[8];
            fs[0] = txtDonorName.Text.ToString().Length == 0 ? "" : txtDonorName.Text;
            fs[1] = txtStoreID.Text.ToString().Length == 0 ? "" : txtStoreID.Text;
            fs[2] = ddlDonorType.SelectedValue.ToString();
            fs[3] = txtStreet1.Text.ToString().Length == 0 ? "" : txtStreet1.Text;
            fs[4] = txtStreet2.Text.ToString().Length == 0 ? "" : txtStreet2.Text;
            fs[5] = txtCity.Text.ToString().Length == 0 ? "" : txtCity.Text;
            fs[6] = ddlState.SelectedValue.ToString();
            fs[7] = txtZip.Text.ToString().Length == 0 ? "" : txtZip.Text;

            Session["PassedDonorInfo"] = fs;
            Session["PassedDonorURL"] = "../donor/edit.aspx?id=" + passedDonorID;
             
            Response.Redirect("../donor-type/add.aspx");
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


}