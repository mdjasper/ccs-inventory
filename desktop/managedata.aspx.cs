using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class desktop_managedata : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userID"] == null)
            Response.Redirect("login.aspx");

        if (!IsPostBack)
        {
            if (Request.QueryString["do"] != null)
            {
                if (Request.QueryString["do"].Equals("FI"))
                {
                    try
                    {
                        searchFoodInDiv.DefaultButton = "btnSearchFoodIn";
                        lblFoodInError.Text = "";
                        bindFoodInGridView();
                        foodInDiv.Visible = true;
                        foodOutDiv.Visible = false;
                        btnFoodOut.Visible = true;
                        lblBtnFoodOut.Visible = false;
                        lblBtnFoodIn.Visible = true;
                        btnFoodIn.Visible = false;
                        searchFoodInDiv.Visible = true;
                        searchFoodOutDiv.Visible = false;
                        lblResponse.Visible = false;
                        lblResponse.Text = "";
                    }
                    catch (System.Threading.ThreadAbortException) { }
                    catch (Exception ex)
                    {
                        LogError.logError(ex);
                        Response.Redirect("../errorpages/error.aspx");
                    }
                }
                if (Request.QueryString["do"].Equals("FO"))
                {
                    try
                    {
                        searchFoodOutDiv.DefaultButton = "btnSearchFoodOut";
                        lblFoodOutError.Text = "";
                        bindFoodOutGridView();
                        foodInDiv.Visible = false;
                        foodOutDiv.Visible = true;
                        btnFoodOut.Visible = false;
                        lblBtnFoodOut.Visible = true;
                        lblBtnFoodIn.Visible = false;
                        btnFoodIn.Visible = true;
                        searchFoodInDiv.Visible = false;
                        searchFoodOutDiv.Visible = true;
                        lblResponse.Visible = false;
                        lblResponse.Text = "";
                    }
                    catch (System.Threading.ThreadAbortException) { }
                    catch (Exception ex)
                    {
                        LogError.logError(ex);
                        Response.Redirect("../errorpages/error.aspx");
                    }
                }
            }
            else
            {
                // By default, Food In is loaded.
                try
                {
                    searchFoodInDiv.DefaultButton = "btnSearchFoodIn";
                    lblFoodInError.Text = "";
                    bindFoodInGridView();
                    foodInDiv.Visible = true;
                    foodOutDiv.Visible = false;
                    btnFoodOut.Visible = true;
                    lblBtnFoodOut.Visible = false;
                    lblBtnFoodIn.Visible = true;
                    btnFoodIn.Visible = false;
                    searchFoodInDiv.Visible = true;
                    searchFoodOutDiv.Visible = false;
                    lblResponse.Visible = false;
                    lblResponse.Text = "";
                }
                catch (System.Threading.ThreadAbortException) { }
                catch (Exception ex)
                {
                    LogError.logError(ex);
                    Response.Redirect("../errorpages/error.aspx");
                }
            }
        }
    }

    // @author: Anthony Dietrich - row functions
    // Selects the row the user pressed the button on and performs
    // the corresponding (CommandName) action/code to that button.
    protected void grdFoodInData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "editData")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdFoodInData.Rows[index];

                Label temp = row.FindControl("lblFoodInID") as Label;
                if (temp != null) { Session["editWhat"] = temp.Text; }

                Response.Redirect("editfood.aspx?do=i");
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

    }

    // @author: Anthony Dietrich - row functions
    // Selects the row the user pressed the button on and performs
    // the corresponding (CommandName) action/code to that button.
    protected void grdFoodOutData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "editData")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdFoodOutData.Rows[index];

                Label temp = row.FindControl("lblDistributionID") as Label;
                if (temp != null) { Session["editWhat"] = temp.Text; }

                Response.Redirect("editfood.aspx?do=o");
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

    }

    // @author: Anthony Dietrich - delete row/user function
    // Selects the row the user pressed the button on, deletes the row,
    // and sends the corresponding userID/index data to the deleteUser method.
    protected void grdFoodInData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            GridViewRow row = grdFoodInData.Rows[index];
            String sDelFoodIn = "";
            Label temp = row.FindControl("lblFoodInID") as Label;
            if (temp != null) { sDelFoodIn = temp.Text; }

            short deleteWhat = short.Parse(sDelFoodIn.ToString());
            deleteFoodIn(deleteWhat);
            bindFoodInGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich - delete row/user function
    // Selects the row the user pressed the button on, deletes the row,
    // and sends the corresponding userID/index data to the deleteUser method.
    protected void grdFoodOutData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            GridViewRow row = grdFoodOutData.Rows[index];
            String sDelFoodOut = "";
            Label temp = row.FindControl("lblDistributionID") as Label;
            if (temp != null) { sDelFoodOut = temp.Text; }

            short deleteWhat = short.Parse(sDelFoodOut.ToString());
            deleteFoodOut(deleteWhat);
            bindFoodOutGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }


    // @author: Anthony Dietrich - deleteFoodIn()
    // Deletes the FoodIn table data given the FoodInID.
    protected void deleteFoodIn(short delFoodIn)
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                db.FoodIns.Remove(db.FoodIns.Single(x => x.FoodInID == delFoodIn));
                db.SaveChanges();
                Response.Redirect("managedata.aspx");
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich - deleteFoodOut()
    // Deletes the FoodOut table data given the DistributionID.
    protected void deleteFoodOut(short delFoodOut)
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                db.FoodOuts.Remove(db.FoodOuts.Single(x => x.DistributionID == delFoodOut));
                db.SaveChanges();
                Response.Redirect("managedata.aspx");
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // Paging the FoodIn gridView
    protected void grdFoodInData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdFoodInData.PageIndex = e.NewPageIndex;
            bindFoodInGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    //Paging the FoodOut gridView
    protected void grdFoodOutData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdFoodOutData.PageIndex = e.NewPageIndex;
            bindFoodOutGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich - bindGridView()
    // Queries the database and returns specified fields from FoodIns.
    // Binds that data to the html gridview.
    private void bindFoodInGridView()
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                var allFoodInQuery = (from c in db.FoodIns
                                      select new { c.FoodInID, c.FoodCategory.CategoryType, c.Weight, c.FoodSource.Source, c.TimeStamp });

                grdFoodInData.DataSource = allFoodInQuery.ToList();
                grdFoodInData.DataBind();

            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich - bindGridView()
    // Queries the database and returns specified fields from FoodOuts.
    // Binds that data to the html gridview.
    private void bindFoodOutGridView()
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {

                var allFoodOutQuery = (from c in db.FoodOuts
                                       select new { c.DistributionID, c.FoodCategory.CategoryType, c.USDACategory.USDANumber, c.Weight, c.Count, c.TimeStamp });

                grdFoodOutData.DataSource = allFoodOutQuery.ToList();
                grdFoodOutData.DataBind();

            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich
    // Simple button click toggle to swap Food In/Food Out views
    protected void btnFoodIn_Click(object sender, EventArgs e)
    {
        try
        {
            searchFoodInDiv.DefaultButton = "btnSearchFoodIn";
            lblFoodInError.Text = "";
            bindFoodInGridView();
            foodInDiv.Visible = true;
            foodOutDiv.Visible = false;
            btnFoodOut.Visible = true;
            lblBtnFoodOut.Visible = false;
            lblBtnFoodIn.Visible = true;
            btnFoodIn.Visible = false;
            searchFoodInDiv.Visible = true;
            searchFoodOutDiv.Visible = false;
            lblResponse.Visible = false;
            lblResponse.Text = "";
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich
    // Simple button click toggle to swap Food In/Food Out views
    protected void btnFoodOut_Click(object sender, EventArgs e)
    {
        try
        {
            searchFoodOutDiv.DefaultButton = "btnSearchFoodOut";
            lblFoodOutError.Text = "";
            bindFoodOutGridView();
            foodInDiv.Visible = false;
            foodOutDiv.Visible = true;
            btnFoodOut.Visible = false;
            lblBtnFoodOut.Visible = true;
            lblBtnFoodIn.Visible = false;
            btnFoodIn.Visible = true;
            searchFoodInDiv.Visible = false;
            searchFoodOutDiv.Visible = true;
            lblResponse.Visible = false;
            lblResponse.Text = "";
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich
    // Search Food In button click method.
    protected void btnSearchFoodIn_Click(object sender, EventArgs e)
    {
        if (txtSearchFoodIn.Text.Equals(""))
        {
            lblResponse.Visible = true;
            lblResponse.Text = "Please enter a search term.";
        }
        else
        {
            lblResponse.Visible = true;
            lblResponse.Text = "Searching for " + txtSearchFoodIn.Text + "...";
            searchFoodInDiv.DefaultButton = "btnSearchFoodIn";
            updateFoodInGrid();
        }
    }

    // @author: Anthony Dietrich
    // Update gridView with search term
    private void updateFoodInGrid()
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                var listFood = (from c in db.FoodIns.OrderBy(x => x.FoodCategoryID)
                                where c.FoodCategory.CategoryType.Contains(txtSearchFoodIn.Text.ToLower())
                                select new {c.FoodInID, c.FoodCategory.CategoryType, c.Weight, c.FoodSource.Source, c.TimeStamp}).ToList();

                DataTable dtFoodResults = new DataTable();
                dtFoodResults.Columns.Add("FoodInID");       
                dtFoodResults.Columns.Add("CategoryType");   
                dtFoodResults.Columns.Add("Weight");           
                dtFoodResults.Columns.Add("Source");
                dtFoodResults.Columns.Add("TimeStamp");

                for (int i = 0; i < listFood.Count; i++) 
                {
                        
                    dtFoodResults.Rows.Add(listFood.ElementAt(i).FoodInID,
                        listFood.ElementAt(i).CategoryType, listFood.ElementAt(i).Weight, 
                        listFood.ElementAt(i).Source, listFood.ElementAt(i).TimeStamp);

                }

                grdFoodInData.DataSource = dtFoodResults;
                grdFoodInData.DataBind();  

                if (dtFoodResults.Rows.Count <= 0)
                {
                    lblResponse.Visible = true;
                    lblResponse.Text = "Unable to find a result that contains " + txtSearchFoodIn.Text + ".";
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


    // @author: Anthony Dietrich
    // Search Food Out button click method.
    protected void btnSearchFoodOut_Click(object sender, EventArgs e)
    {
        if (txtSearchFoodOut.Text.Equals(""))
        {
            lblResponse.Visible = true;
            lblResponse.Text = "Please enter a search term.";
        }
        else
        {
            lblResponse.Visible = true;
            lblResponse.Text = "Searching for " + txtSearchFoodOut.Text + "...";
            searchFoodOutDiv.DefaultButton = "btnSearchFoodOut";
            updateFoodOutGrid();
        }
    }

    // @author: Anthony Dietrich
    // Update gridView with search term
    private void updateFoodOutGrid()
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                var listFood = (from c in db.FoodOuts.OrderBy(x => x.FoodCategoryID)
                                where c.FoodCategory.CategoryType.Contains(txtSearchFoodOut.Text.ToLower())
                                select new { c.DistributionID, c.FoodCategory.CategoryType, c.USDAID, c.Weight, c.Count, c.TimeStamp }).ToList();

                DataTable dtFoodResults = new DataTable();
                dtFoodResults.Columns.Add("DistributionID");
                dtFoodResults.Columns.Add("CategoryType");
                dtFoodResults.Columns.Add("USDAID");
                dtFoodResults.Columns.Add("Weight");
                dtFoodResults.Columns.Add("Count");
                dtFoodResults.Columns.Add("TimeStamp");

                for (int i = 0; i < listFood.Count; i++)
                {

                    dtFoodResults.Rows.Add(listFood.ElementAt(i).DistributionID, listFood.ElementAt(i).CategoryType,
                        listFood.ElementAt(i).USDAID, listFood.ElementAt(i).Weight,
                        listFood.ElementAt(i).Count, listFood.ElementAt(i).TimeStamp);

                }

                grdFoodOutData.DataSource = dtFoodResults;
                grdFoodOutData.DataBind();

                if (dtFoodResults.Rows.Count <= 0)
                {
                    lblResponse.Visible = true;
                    lblResponse.Text = "Unable to find a result that contains " + txtSearchFoodOut.Text + ".";
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

}