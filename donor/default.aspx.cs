using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class Default2 : System.Web.UI.Page
{
    private String sortingColumn;
    private Boolean sortAscending;

    //************************ Page Load **************************************//
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                sortingColumn = "Donor"; // default sort
                sortAscending = true;

                ViewState["sortingColumn"] = sortingColumn;
                ViewState["sortAscending"] = sortAscending;
                updateGridView();   //makes call to update the gridview that is displayed
            }
            else
            {
                sortingColumn = (String)ViewState["sortingColumn"];
                sortAscending = (Boolean)ViewState["sortAscending"];
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //****************************** btnFind_Click listener ***********************************//
    protected void btnFind_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";

            if (lblMessage.Text.Length == 0)
            {
                if (txtDonorName.Text.Length != 0)
                {
                    searchGridView();
                }
                else
                {
                    lblMessage.Text = "Unable to search for a donor with no values provided.";
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


    //****************************** btnReset_Click listener ***********************************//
    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";

            if (lblMessage.Text.Length == 0)
            {
                if (txtDonorName.Text.Length != 0)
                {
                    txtDonorName.Text = "";
                    updateGridView();
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


    //******************************** updateGridView method ********************************//
    private void updateGridView()
    {
        try
        {
            lblMessage.Text = "";   //empty out the error message field

            //using statement calls the dispose method on the object 
            // and causes the object to go out of scope at the end of the statement
            using (CCSEntities db = new CCSEntities())
            {
                var listFoodSources = (from d in db.FoodSources.OrderBy(x => x.Source)
                                 select new
                                 {  d.FoodSourceID,
                                     d.Source,
                                     d.FoodSourceType.FoodSourceType1,
                                     d.Address.StreetAddress1,
                                     d.Address.City.CityName,
                                     d.Address.State.StateFullName,
                                     d.Address.Zipcode.ZipCode1
                                 }).ToList();

                // sort list according to user choice
                if (sortingColumn.Equals("Donor")) // if user wants to sort by donor
                {
                    if (sortAscending)
                        listFoodSources.Sort((x, y) => String.Compare(x.Source, y.Source)); // ascending donor
                    else
                        listFoodSources.Sort((x, y) => String.Compare(y.Source, x.Source)); // descending donor
                }

                if (sortingColumn.Equals("DonorType")) // if user wants to sort by DonorType
                {
                    if (sortAscending)
                        listFoodSources.Sort((x, y) => String.Compare(x.FoodSourceType1, y.FoodSourceType1)); // ascending DonorType
                    else
                        listFoodSources.Sort((x, y) => String.Compare(y.FoodSourceType1, x.FoodSourceType1)); // descending DonorType
                }

                if (sortingColumn.Equals("Address")) // if user wants to sort by Address
                {
                    if (sortAscending)
                        listFoodSources.Sort((x, y) => String.Compare(x.StreetAddress1, y.StreetAddress1)); // ascending Address
                    else
                        listFoodSources.Sort((x, y) => String.Compare(y.StreetAddress1, x.StreetAddress1)); // descending Address
                }

                if (sortingColumn.Equals("Zipcode")) // if user wants to sort by Zipcode
                {
                    if (sortAscending)
                        listFoodSources.Sort((x, y) => String.Compare(x.ZipCode1, y.ZipCode1)); // ascending Zipcode
                    else
                        listFoodSources.Sort((x, y) => String.Compare(y.ZipCode1, x.ZipCode1)); // descending Zipcode
                }
                // end sort list according to user choice

                DataTable dtDonorResults = new DataTable();     //creates a new data table object


                dtDonorResults.Columns.Add("Id");       //adds an id column to the data table object
                dtDonorResults.Columns.Add("Donor");    //adds an id column to the data table object
                dtDonorResults.Columns.Add("DonorType");    //adds a donor type column to the data table object
                dtDonorResults.Columns.Add("StreetAddr");
                dtDonorResults.Columns.Add("Zipcode");


                for (int i = 0; i < listFoodSources.Count; i++)     //loops through the list of FoodSource results 
                {
                    //adds a new row or result to the data table object.  
                    //The first parameter is the value for the id field of the current food source
                    //The second parameter is the value for the Donor/Source field of the current food source
                    //The third parameter is the value for the foodSourceType/Donation Type field of the current food source
                    dtDonorResults.Rows.Add(listFoodSources.ElementAt(i).FoodSourceID,
                        listFoodSources.ElementAt(i).Source, listFoodSources.ElementAt(i).FoodSourceType1, 
                        listFoodSources.ElementAt(i).StreetAddress1, listFoodSources.ElementAt(i).ZipCode1);

                } //end of for loop


                //adds the datatable results to the gridview on the default.aspx page if row count more than 0
                if (dtDonorResults.Rows.Count > 0)
                {
                    grdDonors.DataSource = dtDonorResults;  //assigns the dataTable object results to the gridview
                    grdDonors.DataBind();       //binds the datasource to the gridview
                }

            }//end of the using statement
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

    } //end of updateGridView method


    //****************************** btnAddDonor_Click listener ***********************************//
    protected void btnAddDonor_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("add.aspx");  //redirects to the Add Donor webpage
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //****************************** paging the gridview ***********************************//
    protected void grdDonors_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDonors.PageIndex = e.NewPageIndex;
            updateGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //****************************** sorting the gridview column ***********************************//
    protected void TaskGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (sortingColumn != null && sortingColumn.Equals(e.SortExpression))    // if the list is already sorted by this column,                                                                  
            {                                                                       // toggle ascending/descending
                sortAscending = !sortAscending;
            }
            else                                                                    // else, set new column and set sorting to ascending
            {
                sortingColumn = e.SortExpression;
                sortAscending = true;
            }

            ViewState["sortingColumn"] = sortingColumn;
            ViewState["sortAscending"] = sortAscending;

            updateGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //****************************** searchGridView() method ***********************************//
    private void searchGridView()
    {
        try
        {
            if (lblMessage.Text.Length == 0)
            {
                using (CCSEntities db = new CCSEntities())
                {
                    var listFoodSources = (from d in db.FoodSources.OrderBy(x => x.Source)
                                           where d.Source.ToLower().Contains(txtDonorName.Text.ToLower())
                                           select new
                                           {
                                               d.FoodSourceID,
                                               d.Source,
                                               d.FoodSourceType.FoodSourceType1,
                                               d.Address.StreetAddress1,
                                               d.Address.City.CityName,
                                               d.Address.State.StateFullName,
                                               d.Address.Zipcode.ZipCode1
                                           }).ToList();

                    // sort list according to user choice
                    if (sortingColumn.Equals("Donor")) // if user wants to sort by donor
                    {
                        if (sortAscending)
                            listFoodSources.Sort((x, y) => String.Compare(x.Source, y.Source)); // ascending donor
                        else
                            listFoodSources.Sort((x, y) => String.Compare(y.Source, x.Source)); // descending donor
                    }

                    if (sortingColumn.Equals("DonorType")) // if user wants to sort by DonorType
                    {
                        if (sortAscending)
                            listFoodSources.Sort((x, y) => String.Compare(x.FoodSourceType1, y.FoodSourceType1)); // ascending DonorType
                        else
                            listFoodSources.Sort((x, y) => String.Compare(y.FoodSourceType1, x.FoodSourceType1)); // descending DonorType
                    }

                    if (sortingColumn.Equals("Address")) // if user wants to sort by Address
                    {
                        if (sortAscending)
                            listFoodSources.Sort((x, y) => String.Compare(x.StreetAddress1, y.StreetAddress1)); // ascending Address
                        else
                            listFoodSources.Sort((x, y) => String.Compare(y.StreetAddress1, x.StreetAddress1)); // descending Address
                    }

                    if (sortingColumn.Equals("Zipcode")) // if user wants to sort by Zipcode
                    {
                        if (sortAscending)
                            listFoodSources.Sort((x, y) => String.Compare(x.ZipCode1, y.ZipCode1)); // ascending Zipcode
                        else
                            listFoodSources.Sort((x, y) => String.Compare(y.ZipCode1, x.ZipCode1)); // descending Zipcode
                    }
                    // end sort list according to user choice

                    DataTable dtDonorResults = new DataTable();     //creates a new data table object


                    dtDonorResults.Columns.Add("Id");       //adds an id column to the data table object
                    dtDonorResults.Columns.Add("Donor");    //adds an id column to the data table object
                    dtDonorResults.Columns.Add("DonorType");    //adds a donor type column to the data table object
                    dtDonorResults.Columns.Add("StreetAddr");
                    dtDonorResults.Columns.Add("Zipcode");


                    for (int i = 0; i < listFoodSources.Count; i++)     //loops through the list of FoodSource results 
                    {
                        //adds a new row or result to the data table object.  
                        //The first parameter is the value for the id field of the current food source
                        //The second parameter is the value for the Donor/Source field of the current food source
                        //The third parameter is the value for the foodSourceType/Donation Type field of the current food source
                        dtDonorResults.Rows.Add(listFoodSources.ElementAt(i).FoodSourceID,
                            listFoodSources.ElementAt(i).Source, listFoodSources.ElementAt(i).FoodSourceType1,
                            listFoodSources.ElementAt(i).StreetAddress1, listFoodSources.ElementAt(i).ZipCode1);

                    } //end of for loop


                    //adds the datatable results to the gridview on the default.aspx page if row count more than 0
                    grdDonors.DataSource = dtDonorResults;  //assigns the dataTable object results to the gridview
                    grdDonors.DataBind();       //binds the datasource to the gridview

                    if (dtDonorResults.Rows.Count <= 0)
                    {
                        lblMessage.Text = "Unable to find a donor name that contains " + txtDonorName.Text + ".";
                    }

                }//end of using statement
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