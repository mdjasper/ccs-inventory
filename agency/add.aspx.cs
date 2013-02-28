using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class agency_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            using(CCSEntities db = new CCSEntities()){
                ddlState.DataSource = db.States.ToList();
                ddlState.DataBind();
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = ""; //empty out the error message field

            checkForNullFields(); //makes sure the required database fields are not null or empty
            formValidation();   //validate the form information

            if (lblMessage.Text.Length == 0 || lblMessage.Text == null)
            {
                insertCityIfNotExist();

                insertZipIfNotExist();

                insertAddrIfNotExist();

                insertDonorNmIfNotExist();
            }


        } //end of try
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    private void insertDonorNmIfNotExist()
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                Agency ag = new Agency();
                short donorRecordID = findAgencyRecord(); //checks if agency the exists already


                //add the new agency to the database, since it doesn't exist already
                if (donorRecordID == -1)
                {
                    //FoodSource Table fields: Source, StoreID(Nullable), FoodSourceTypeID, and AddressID
                    ag.AgencyName = txtAgencyName.Text;

                    short addrRecordID = findAddrRecord();

                    //add the donor to the database if the able to lookup the address after the address was just inserted in 
                    if (addrRecordID != -1 && lblMessage.Text.Length == 0)
                    {
                        ag.AddressID = addrRecordID; //adds the AddressID to the FoodSource
                        db.Agencies.Add(ag); //add the foodSource to the database
                        db.SaveChanges();

                        LogChange.logChange("Agency " + ag.AgencyName + " was added.", DateTime.Now, short.Parse(Session["userID"].ToString()));
                    }
                    else
                    {
                        lblMessage.Text += "The agency was unable to be added.<br/>";
                    }

                }
                else
                {
                    lblMessage.Text += txtAgencyName.Text + " agency with that address exists already!<br/>";
                }

            } //end of using statement
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //***************************** lookupAgencyRecord **************************************//
    private short findAgencyRecord()
    {
        short recordID = -1;
        try
        {

            short selectedAddrID = findAddrRecord();
            using (CCSEntities db = new CCSEntities())
            {
                var agencyResult = (from a in db.Agencies
                                            where a.AgencyName.Equals(txtAgencyName.Text, StringComparison.OrdinalIgnoreCase)
                                            && a.AddressID == selectedAddrID
                                            select a).FirstOrDefault();

                if (agencyResult != null)
                {
                    recordID = agencyResult.AgencyID;
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
    //***************************** formValidation method **************************************//
    private void formValidation()
    {
        try
        {

            int num;

            //check that the values don't exceed database limits
            if (txtAgencyName.Text.Length > 100) //checks that the street addr1 is not longer than 50 characters
            {
                lblMessage.Text += "The donor's name cannot exceed 30 characters.<br/>";
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
            if (txtAgencyName.Text.Length > 0 && txtAgencyName.Text.Trim().Length == 0)
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

    //***************************** checkForNullFields **************************************//
    private void checkForNullFields()
    {
        try
        {
            if (txtAgencyName.Text == null || txtAgencyName.Text.Equals(""))
                lblMessage.Text += "The Donor Name cannot be empty.<br/>";

            if (txtStreet1.Text == null || txtStreet1.Text.Equals(""))
                lblMessage.Text += "The Street Address 1 cannot be empty.<br/>";

            if (txtCity.Text == null || txtCity.Text.Equals(""))
                lblMessage.Text += "The City field cannot be empty.<br/>";

            if (ddlState.SelectedValue.ToString().Equals("0") || ddlState.SelectedItem.ToString().Equals("< Select a State >"))
                lblMessage.Text += "A State must be selected.<br/>";

            if (txtZip.Text == null || txtZip.Text.Equals(""))
                lblMessage.Text += "The Zip field cannot be empty.<br/>";
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

}