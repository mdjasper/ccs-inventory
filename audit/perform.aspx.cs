using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Perform : System.Web.UI.Page
{
    private DataTable dtUnverified;
    private DataTable dtChanges;

    private List<Container> lstUnverifiedContainers;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";

            txtContainerNumber.Focus();
            if (Session["unverified"] == null)
            {
                loadUnverified();
                loadChanges();
            }
            else
            {
                lstUnverifiedContainers = (List<Container>) Session["unverified"];
                dtChanges = (DataTable)Session["changes"];
                loadUnverified(lstUnverifiedContainers);
                loadChanges(dtChanges);
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private void loadUnverified()
    {
        try
        {
            dtUnverified = new DataTable();
            using (CCSEntities db = new CCSEntities())
            {
                lstUnverifiedContainers = db.Containers.Include("FoodCategory").Include("Location").Include("USDACategory").OrderBy(x => x.BinNumber).ToList();
            }

            loadUnverified(lstUnverifiedContainers);
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private void loadUnverified(List<Container> lstUnverifiedContainers)
    {
        try
        {
            DataTable dtUnverified = new DataTable();

            dtUnverified.Columns.Add("ContainerID");
            dtUnverified.Columns.Add("ContainerNumber");

            foreach (Container c in lstUnverifiedContainers)
                dtUnverified.Rows.Add(c.ContainerID, c.BinNumber);

            gvUnverified.DataSource = lstUnverifiedContainers;
            gvUnverified.DataBind();
            Session["unverified"] = lstUnverifiedContainers;
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private void loadChanges()
    {
        try
        {
            dtChanges = new DataTable();

            dtChanges.Columns.Add("Container Number");
            dtChanges.Columns.Add("Weight");
            dtChanges.Columns.Add("Food Category");
            dtChanges.Columns.Add("Location");
            dtChanges.Columns.Add("USDA");
            dtChanges.Columns.Add("USDA Number");
            dtChanges.Columns.Add("Units");

            loadChanges(dtChanges);
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private void loadChanges(DataTable dtChanges)
    {
        try
        {
            gvChanges.DataSource = dtChanges;
            gvChanges.DataBind();
            Session["changes"] = dtChanges;
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private void pushSession()
    {
        try
        {
            Session["unverified"] = lstUnverifiedContainers;
            Session["changes"] = dtChanges;
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private void pullSession()
    {
        try
        {
            if (Session["unverified"] == null || Session["changes"] == null)
                lblMessage.Text = "An unexpected problem occurred. Please try again later.";
            else
            {
                lstUnverifiedContainers = (List<Container>)Session["unverified"];
                dtChanges = (DataTable)Session["changes"];
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    protected void btnLookupContainer_Click(object sender, EventArgs e)
    {
        try
        {
            int containerID;
            if (txtContainerNumber.Text == "" || !int.TryParse(txtContainerNumber.Text, out containerID))
                lblMessage.Text = "You must enter a valid Container number";
            else
            {
                String containerNumber;

                using (CCSEntities db = new CCSEntities())
                {
                    containerNumber = (from c in db.Containers            // lookup container with containerNumber
                          where c.BinNumber == containerID
                          select c.ContainerID).FirstOrDefault().ToString();
                }

                if (containerNumber.Equals("0")) // default value
                    lblMessage.Text = "You must enter a valid container number.";
                else
                    Response.Redirect("verify.aspx?id=" + containerNumber, false);
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }
    protected void btnFinish_Click(object sender, EventArgs e)
    {
        try
        {
            pushChangeLog();

            Session["unverified"] = null;
            Session["changes"] = null;

            Response.Redirect("default.aspx", false);
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    private void pushChangeLog()
    {
        try
        {
            // connect
            // make audit record, retain pk
            // for each row in datatable
            //  add record to Adjustment
            //  add record to AuditAdjustment
            dtChanges = (DataTable)Session["changes"];

            using (CCSEntities db = new CCSEntities())
            {
                Audit audit = new Audit();
                audit.Date = DateTime.Now;
                audit.UserID = short.Parse(Session["userID"].ToString());

                db.Audits.Add(audit);

                Adjustment adjustment;

                for (int i = 0; i < dtChanges.Rows.Count; i++)
                {
                    adjustment = new Adjustment();

                    if (!dtChanges.Rows[i][1].ToString().Equals(""))
                        adjustment.Weight = Decimal.Parse(dtChanges.Rows[i][1].ToString());

                    adjustment.FoodCategory = dtChanges.Rows[i][2].ToString();
                    adjustment.Location = dtChanges.Rows[i][3].ToString();

                    if (!dtChanges.Rows[i][4].ToString().Equals(""))
                        adjustment.isUSDA = Boolean.Parse(dtChanges.Rows[i][4].ToString());

                    adjustment.USDANumber = dtChanges.Rows[i][5].ToString();

                    if (!dtChanges.Rows[i][6].ToString().Equals(""))
                        adjustment.Cases = short.Parse(dtChanges.Rows[i][6].ToString());

                    adjustment.AuditID = audit.AuditID;

                    db.Adjustments.Add(adjustment); // add record
                    db.SaveChanges();               // update db table
                }
            }

            LogChange.logChange("Performed an audit.", DateTime.Now, short.Parse(Session["userID"].ToString()));
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Session["changes"] = null;
            Session["unverified"] = null;
            Response.Redirect("default.aspx", false);
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // Paging the gridView
    protected void gvUnverified_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvUnverified.PageIndex = e.NewPageIndex;
            if (lstUnverifiedContainers == null)
                pullSession();
            loadUnverified(lstUnverifiedContainers);
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // Paging the gridView
    protected void gvChanges_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvChanges.PageIndex = e.NewPageIndex;
            if (dtChanges == null)
                pullSession();
            loadChanges(dtChanges);
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }
}