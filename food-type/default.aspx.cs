using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FoodCateogryManager : System.Web.UI.Page
{
    private List<FoodCategory> lstFoodCategories;
    private String sortingColumn;
    private Boolean sortAscending;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            sortingColumn = "CategoryType"; // default sort
            sortAscending = true;
            ViewState["sortingColumn"] = sortingColumn;
            ViewState["sortAscending"] = sortAscending;
            bindGridView();
        }

        if (ViewState["sortingColumn"] != null)
        {
            sortingColumn = (String)ViewState["sortingColumn"];
            sortAscending = (Boolean)ViewState["sortAscending"];
        }
    }

    private void bindGridView()
    {
        try
        {
            lblMessage.Text = "";

            using (CCSEntities db = new CCSEntities())
            {
                lstFoodCategories = db.FoodCategories.ToList();

                // sort list according to user choice
                if (sortingColumn != null && sortingColumn.Equals("CategoryType")) // if user wants to sort by CategoryType
                {
                    if (sortAscending)
                        lstFoodCategories.Sort((x, y) => String.Compare(x.CategoryType, y.CategoryType)); // ascending CategoryType
                    else
                        lstFoodCategories.Sort((x, y) => String.Compare(y.CategoryType, x.CategoryType)); // descending CategoryType
                }
                // end sort list according to user choice
            }

            gvFoodType.DataSource = lstFoodCategories;
            gvFoodType.DataBind();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // Paging the gridView
    protected void gvFoodType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvFoodType.PageIndex = e.NewPageIndex;
            bindGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // sorting
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
                sortAscending = false; // set false because default is ascending
            }

            ViewState["sortingColumn"] = sortingColumn;
            ViewState["sortAscending"] = sortAscending;

            bindGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    protected void btnAddFoodCat_Click(object sender, EventArgs e)
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
}