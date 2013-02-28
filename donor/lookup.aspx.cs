using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";

        if(lblMessage.Text.Length == 0)
        {
            if (txtDonorName.Text.Length != 0)
            {
                updateGridView();
            }
            else 
            {
                lblMessage.Text = "Unable to search for a donor with no values provided.";
            }
        }
    }

    private void updateGridView()
    {
        try
        {
            if (lblMessage.Text.Length ==0)
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

                }
            }
        }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Default.aspx");
    }

    // Paging the gridView
    protected void grdDonors_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDonors.PageIndex = e.NewPageIndex;
        updateGridView();
    }
}