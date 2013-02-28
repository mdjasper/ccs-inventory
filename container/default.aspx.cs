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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            sortingColumn = "Category"; // default sort
            sortAscending = true;
            ViewState["sortingColumn"] = sortingColumn;
            ViewState["sortAscending"] = sortAscending;
            updateGridView();
        }

        if (ViewState["sortingColumn"] != null)
        {
            sortingColumn = (String)ViewState["sortingColumn"];
            sortAscending = (Boolean)ViewState["sortAscending"];
        }
    }

    private void updateGridView()
    {
        try
        {
            lblMessage.Text = "";

            using (CCSEntities db = new CCSEntities())
            {
                //lstContainers = db.Containers.ToList();
                var lstContainers = (from c in db.Containers
                                 select new {
                                    BinNumber = c.BinNumber,
                                    Location = c.Location.RoomName,
                                    isUSDA = c.isUSDA,
                                    Category = !(bool)c.isUSDA ? 
                                        c.FoodCategory.CategoryType : c.USDACategory.Description,
                                    Weight = c.Weight
                                 }).ToList();

                // sort list according to user choice
                if (sortingColumn != null)
                {
                    switch (sortingColumn)
                    {
                        case "Location":
                            if (sortAscending)
                                lstContainers.Sort((x, y) => String.Compare(x.Location, y.Location)); // ascending Location
                            else
                                lstContainers.Sort((x, y) => String.Compare(y.Location, x.Location)); // descending Location
                            break;
                        case "Category":
                            if (sortAscending)
                                lstContainers.Sort((x, y) => String.Compare(x.Category, y.Category)); // ascending CategoryType
                            else
                                lstContainers.Sort((x, y) => String.Compare(y.Category, x.Category)); // descending CategoryType
                            break;
                        case "Weight":
                            if (sortAscending)
                                lstContainers.Sort((x,y) => x.Weight.CompareTo(y.Weight)); // ascending CategoryType
                            else
                                lstContainers.Sort((x,y) => y.Weight.CompareTo(x.Weight)); // descending CategoryType
                            break;
                    }
                }
                grdContainers.DataSource = lstContainers;
                grdContainers.DataBind();
            }

            
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }
    protected void grdContainers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdContainers.PageIndex = e.NewPageIndex;
            updateGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }
    protected void grdContainers_Sorting(object sender, GridViewSortEventArgs e)
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
                sortAscending = true; // set false because default is ascending
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