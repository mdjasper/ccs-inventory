using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Default2 : System.Web.UI.Page
{
    private String sortingColumn;
    private Boolean sortAscending;

    //******************************** Page_Load ********************************//
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                sortingColumn = "TimeStamp"; // default sort
                sortAscending = false;
                ViewState["sortingColumn"] = sortingColumn;
                ViewState["sortAscending"] = sortAscending;
            }

            if (ViewState["sortingColumn"] != null)
            {
                sortingColumn = (String)ViewState["sortingColumn"];
                sortAscending = (Boolean)ViewState["sortAscending"];
            }

            updateGridView();   //makes call to update the gridview that is displayed
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
                var listFoodIn = (from d in db.FoodIns
                                       select new
                                       {
                                           d.FoodInID,
                                           d.FoodCategory.CategoryType,
                                           d.Weight,
                                           d.FoodSource.Source,
                                           d.TimeStamp,
                                           d.FoodSource.FoodSourceType.FoodSourceType1
                                       }).ToList();

                // sort list according to user choice
                switch (sortingColumn)
                {
                    case "Donor":
                        if (sortAscending)
                            listFoodIn.Sort((x, y) => String.Compare(x.Source, y.Source)); // ascending Donor
                        else
                            listFoodIn.Sort((x, y) => String.Compare(y.Source, x.Source)); // descending Donor
                        break;
                    case "Category":
                        if (sortAscending)
                            listFoodIn.Sort((x, y) => String.Compare(x.CategoryType, y.CategoryType)); // ascending Category
                        else
                            listFoodIn.Sort((x, y) => String.Compare(y.CategoryType, x.CategoryType)); // descending Category
                        break;
                    case "Weight":
                        if (sortAscending)
                            listFoodIn.Sort((x, y) => x.Weight.CompareTo(y.Weight)); // ascending Weight
                        else
                            listFoodIn.Sort((x, y) => y.Weight.CompareTo(x.Weight)); // descending Weight
                        break;
                    case "TimeStamp":
                        if (sortAscending)
                            listFoodIn.Sort((x, y) => DateTime.Compare(x.TimeStamp, y.TimeStamp)); // ascending TimeStamp
                        else
                            listFoodIn.Sort((x, y) => DateTime.Compare(y.TimeStamp, x.TimeStamp)); // descending TimeStamp
                        break;
                }

                DataTable dtFoodIn = new DataTable();     //creates a new data table object

                dtFoodIn.Columns.Add("Id");
                dtFoodIn.Columns.Add("Category"); 
                dtFoodIn.Columns.Add("Weight");    
                dtFoodIn.Columns.Add("Donor");
                dtFoodIn.Columns.Add("TimeStamp");
                dtFoodIn.Columns.Add("DonorType");


                for (int i = 0; i < listFoodIn.Count; i++)     //loops through the list of FoodSource results 
                {
                    dtFoodIn.Rows.Add(listFoodIn.ElementAt(i).FoodInID, listFoodIn.ElementAt(i).CategoryType,
                        listFoodIn.ElementAt(i).Weight, listFoodIn.ElementAt(i).Source,
                        listFoodIn.ElementAt(i).TimeStamp, listFoodIn.ElementAt(i).FoodSourceType1);

                } //end of for loop


                //adds the datatable results to the gridview on the default.aspx page if row count more than 0
                if (dtFoodIn.Rows.Count > 0)
                {
                    grdFoodIn.DataSource = dtFoodIn;  //assigns the dataTable object results to the gridview
                    grdFoodIn.DataBind();       //binds the datasource to the gridview
                    grdFoodIn.Columns[5].Visible = false;
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


    //******************************** btnAddIncomingFood method ********************************//
    protected void btnAddIncomingFood_Click(object sender, EventArgs e)
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


    //******************************** paging the gridview ********************************//
    protected void grdFoodIn_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdFoodIn.PageIndex = e.NewPageIndex;
            updateGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    //******************************** modify the print buttons the gridview ********************************//
    protected void grdFoodIn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find CheckBox control in GridView
                String donorType = (String)DataBinder.Eval(e.Row.DataItem, "DonorType");
                HyperLink h1 = (HyperLink)e.Row.FindControl("printInKind");

                if (h1.ID.Equals("printInKind")) //checks that only setting the print in-kind button
                {

                    if(Config.MOBILE()) //if on mobile device
                    {
                        h1.Enabled = false;
                        h1.CssClass = "button";
                        h1.Text = "Print Disabled";
                    }
                    else //if the h1.text equals print
                    {

                        if (donorType.ToLower().StartsWith("in-kind"))
                        {
                            h1.Enabled = true;
                            h1.CssClass = "printBtn";
                        }

                        if (!donorType.ToLower().StartsWith("in-kind"))
                        {
                            h1.Enabled = false;
                            h1.CssClass = "button";
                        }
                    }
                }
            } //end of outer if
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


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