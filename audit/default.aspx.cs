using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AuditManager : System.Web.UI.Page
{
    private String sortingColumn;
    private Boolean sortAscending;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            sortingColumn = "Date"; // default sort
            sortAscending = false;
            ViewState["sortingColumn"] = sortingColumn;
            ViewState["sortAscending"] = sortAscending;
        }

        if(ViewState["sortingColumn"] != null)
        {
            sortingColumn = (String) ViewState["sortingColumn"];
            sortAscending =  (Boolean) ViewState["sortAscending"] ;
        }

        bindGridView();
    }

    // Paging the gridView
    protected void gvAudits_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAudits.PageIndex = e.NewPageIndex;
        bindGridView();
    }

    private void bindGridView()
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                var audits = (from a in db.Audits.Include("User")
                              select new { a.AuditID, a.Date, UserName = a.User.FirstName + " " + a.User.LastName }).ToList();

                // sort list according to user choice
                if (sortingColumn.Equals("Date")) // if user wants to sort by date
                {
                    if (sortAscending)
                        audits.Sort((x, y) => DateTime.Compare(x.Date, y.Date)); // ascending dates
                    else
                        audits.Sort((x, y) => DateTime.Compare(y.Date, x.Date)); // descending dates
                }

                if (sortingColumn.Equals("UserName")) // if user wants to sort by username
                {
                    if (sortAscending)
                        audits.Sort((x, y) => String.Compare(x.UserName, y.UserName)); // ascending usernames
                    else
                        audits.Sort((x, y) => String.Compare(y.UserName, x.UserName)); // descending usernames
                }
                // end sort list according to user choice

                gvAudits.DataSource = audits;
                gvAudits.DataBind();
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx", false);
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

            bindGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }
}