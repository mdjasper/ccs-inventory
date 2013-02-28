using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userID"] == null)
            Response.Redirect("login.aspx");
        else
        {
            if (!IsPostBack)
            {
                try
                {
                    lblError.Text = "";
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

    }

    // @author: Anthony Dietrich - row functions
    // Selects the row the user pressed the button on and performs
    // the corresponding (CommandName) action/code to that button.
    protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "editUser")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdUsers.Rows[index];

                Label temp = row.FindControl("lblUserID") as Label;
                if (temp != null) { Session["editWho"] = temp.Text; }

                Response.Redirect("edituser.aspx?do=e");
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
    protected void grdUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            GridViewRow row = grdUsers.Rows[index];
            String deleteUserID = "";
            Label temp = row.FindControl("lblUserID") as Label;
            if (temp != null) { deleteUserID = temp.Text; }

            if (!deleteUserID.Equals("1"))
            {
                short deleteWho = short.Parse(deleteUserID.ToString());
                deleteUser(deleteWho);
                bindGridView();
            }
            else
            {
                lblError.Text = "Default UserID cannot be deleted!";
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
        
    }

    // @author: Anthony Dietrich - deleteUser()
    // Deletes a user given the short UserID.
    protected void deleteUser(short deleteUser)
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                db.Users.Remove(db.Users.Single(x => x.UserID == deleteUser));
                db.SaveChanges();
                Response.Redirect("users.aspx");
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // Paging the gridView
    protected void grdUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdUsers.PageIndex = e.NewPageIndex;
            bindGridView();
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    // @author: Anthony Dietrich - bindGridView()
    // Queries the database and returns all users.
    // Binds that data to the html gridview.
    private void bindGridView()
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {

                var allUsersQuery = (from c in db.Users
                                     select new { c.UserID, c.UserName, c.FirstName, c.LastName, c.Admin });

                grdUsers.DataSource = allUsersQuery.ToList();
                grdUsers.DataBind();
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    //Add User redirect
    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("edituser.aspx?do=a");
    }
}