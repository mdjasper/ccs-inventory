using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class USDACategoryManager : System.Web.UI.Page
{
    private List<USDACategory> lstUSDACategories;
    private String sortingColumn;
    private Boolean sortAscending;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            sortingColumn = "Description"; // default sort
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

    protected void gvUSDAType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUSDAType.PageIndex = e.NewPageIndex;
        bindGridView();
    }

    private void bindGridView()
    {
        try
        {
            lblMessage.Text = "";

            using (CCSEntities db = new CCSEntities())
            {
                lstUSDACategories = db.USDACategories.ToList();

                // sort list according to user choice
                if (sortingColumn != null)
                {
                    if (sortingColumn.Equals("Description")) // if user wants to sort by Description
                    {
                        if (sortAscending)
                            lstUSDACategories.Sort((x, y) => String.Compare(x.Description, y.Description)); // ascending Description
                        else
                            lstUSDACategories.Sort((x, y) => String.Compare(y.Description, x.Description)); // descending Description
                    }
                    if (sortingColumn.Equals("Number")) // if user wants to sort by USDANumber
                    {
                        if (sortAscending)
                            lstUSDACategories.Sort((x, y) => String.Compare(x.USDANumber, y.USDANumber)); // ascending USDANumber
                        else
                            lstUSDACategories.Sort((x, y) => String.Compare(y.USDANumber, x.USDANumber)); // descending USDANumber
                    }
                }
                // end sort list according to user choice

                gvUSDAType.DataSource = lstUSDACategories;
                gvUSDAType.DataBind();
            }
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
                sortAscending = true;
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

    protected void btnAddNewUSDACat_Click(object sender, EventArgs e)
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