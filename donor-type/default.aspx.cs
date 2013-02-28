using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    private List<FoodSourceType> listFoodSourceTypes; //AKA List of Donor Types
    private String sortingColumn;
    private Boolean sortAscending;

    //************************ Page Load **************************************//
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                sortingColumn = "DonorType"; // default sort
                sortAscending = true;
                ViewState["sortingColumn"] = sortingColumn;
                ViewState["sortAscending"] = sortAscending;
                updateGridView();   //makes call to update the gridview that is displayed
            }

            if (ViewState["sortingColumn"] != null)
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


    //******************************** updateGridView method ********************************//
    private void updateGridView()
    {
        try
        {
            //using statement calls the dispose method on the object 
            // and causes the object to go out of scope at the end of the statement
            using (CCSEntities db = new CCSEntities())
            {
                //access the foodSourceType table, order the results
                //by the Source field and add results to a list
                listFoodSourceTypes = db.FoodSourceTypes.ToList();

                // sort list according to user choice
                if (sortingColumn.Equals("DonorType")) // if user wants to sort by donor type
                {
                    if (sortAscending)
                        listFoodSourceTypes.Sort((x, y) => String.Compare(x.FoodSourceType1, y.FoodSourceType1)); // ascending donor type
                    else
                        listFoodSourceTypes.Sort((x, y) => String.Compare(y.FoodSourceType1, x.FoodSourceType1)); // descending donor type
                }

                DataTable dtDonorTypeResults = new DataTable();     //creates a new data table object

                dtDonorTypeResults.Columns.Add("FoodSourceTypeID");       //adds an id column to the data table object
                dtDonorTypeResults.Columns.Add("DonorType");    //adds a donor type column to the data table object

                for (int i = 0; i < listFoodSourceTypes.Count; i++)     //loops through the list of FoodSource results 
                {
                    dtDonorTypeResults.Rows.Add(listFoodSourceTypes.ElementAt(i).FoodSourceTypeID,
                        listFoodSourceTypes.ElementAt(i).FoodSourceType1);
                }

                if (dtDonorTypeResults.Rows.Count > 0)
                {
                    grdDonorTypes.DataSource = dtDonorTypeResults;  //assigns the dataTable object results to the gridview
                    grdDonorTypes.DataBind();       //binds the datasource to the gridview
                }      //binds the datasource to the gridview

            }//end of the using statement
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    } //end of updateGridView method


    //******************************** Paging the gridView ********************************//
    protected void grdDonorTypes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDonorTypes.PageIndex = e.NewPageIndex;
            updateGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //******************************** btnAddDonorType ********************************//
    protected void btnAddDonorType_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("add.aspx");
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // sorting the gridview
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
}