using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_edituser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userID"] == null)
            Response.Redirect("login.aspx");
        else
        {

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["do"].Equals("e"))
                {
                    addUserPanel.Visible = false;
                    addButtons.Visible = false;
                    lblTitle.Text = "Edit User";

                    try
                    {
                        using (CCSEntities db = new CCSEntities())
                        {

                            short uID = short.Parse(Session["editWho"].ToString());

                            var userInfoQuery = (from c in db.Users
                                                 where c.UserID.Equals(uID)
                                                 select new { c.UserID, c.UserName, c.Password, c.FirstName, c.LastName });

                            bool isAdmin = (from c in db.Users
                                            where c.UserID.Equals(uID)
                                            select c.Admin).FirstOrDefault();

                            if (isAdmin.Equals(true))
                                ddlEditAdmin.SelectedValue.Equals("Yes");
                            else
                                ddlEditAdmin.SelectedValue.Equals("No");

                            txtEditUserID.Text = userInfoQuery.ToList().ElementAt(0).UserID.ToString();
                            txtEditUserName.Text = userInfoQuery.ToList().ElementAt(0).UserName.ToString();
                            txtEditPass.Text = userInfoQuery.ToList().ElementAt(0).Password.ToString();
                            txtEditFirstName.Text = userInfoQuery.ToList().ElementAt(0).FirstName.ToString();
                            txtEditLastName.Text = userInfoQuery.ToList().ElementAt(0).LastName.ToString();
                        }
                    }
                    catch (System.Threading.ThreadAbortException) { }
                    catch (Exception ex)
                    {
                        LogError.logError(ex);
                        Response.Redirect("../errorpages/error.aspx");
                    }
                }
                else if (Request.QueryString["do"].Equals("a"))
                {
                    editUserDiv.Visible = false;
                    lblTitle.Text = "Add User";
                }
            }
        }
    }

    // @author: Anthony Dietrich - Save Add User Data
    // Saves User Data from the Add version of the Edit page
    protected void btnAddSave_Click(object sender, EventArgs e)
    {
        // Ensure all required fields are filled out
        if (txtFirstName.Text.Equals("") || txtLastName.Text.Equals("") || txtUserName.Text.Equals("") || txtPass.Text.Equals(""))
        {
            lblResponse.Visible = true;
            lblResponse.ForeColor = System.Drawing.Color.Red;
            lblResponse.Text = "Please fill out all fields!";
        }else
        {
            try
            {
                using (CCSEntities db = new CCSEntities())
                {
                    User user = new User();
                    user.FirstName = txtFirstName.Text;
                    user.LastName = txtLastName.Text;
                    user.UserName = txtUserName.Text;
                    user.Password = txtPass.Text;
                    if (ddlAddAdmin.SelectedItem.ToString().Equals("Yes"))
                        user.Admin = true;
                    if (ddlAddAdmin.SelectedItem.ToString().Equals("No"))
                        user.Admin = false;

                    // Check for duplicate userName
                    if (db.Users.Where(u => u.UserName.ToLower() == txtUserName.Text.ToLower()).Count() > 0)
                    {
                        lblResponse.Visible = true;
                        lblResponse.ForeColor = System.Drawing.Color.Red;
                        lblResponse.Text = "Cannot add duplicate username: " + txtUserName.Text + "!";
                    }
                    else
                    {
                        db.Users.Add(user);
                        db.SaveChanges();

                        // Check to make sure the new user got saved
                        if (db.Users.Where(u => u.UserName.ToLower() == txtUserName.Text.ToLower()).Count() > 0)
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = "Success!  Username: " + txtUserName.Text + " added to the system!";
                        }
                        else
                        {
                            lblResponse.Visible = true;
                            lblResponse.Text = "Error saving to the database!";
                        }

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

    // @author: Anthony Dietrich - Save Edit User Data
    // Saves Edit Data from the Edit version of the Edit page
    protected void btnEditSave_Click(object sender, EventArgs e)
    {
        // Ensure all required fields are filled out
        if (txtEditFirstName.Text.Equals("") || txtEditLastName.Text.Equals("") ||
            txtEditUserName.Text.Equals("") || txtEditPass.Text.Equals(""))
        {
            lblResponse.Visible = true;
            lblResponse.ForeColor = System.Drawing.Color.Red;
            lblResponse.Text = "Please fill out all fields!";
        }
        else
        {
            try
            {
                using (CCSEntities db = new CCSEntities())
                {

                    short uID = short.Parse(Session["editWho"].ToString());

                    User editUserInfo = (from c in db.Users
                                         where c.UserID.Equals(uID)
                                         select c).FirstOrDefault();

                    string tempName = editUserInfo.UserName.ToString();

                    editUserInfo.UserName = txtEditUserName.Text;
                    editUserInfo.Password = txtEditPass.Text;
                    editUserInfo.FirstName = txtEditFirstName.Text;
                    editUserInfo.LastName = txtEditLastName.Text;
                    if (ddlEditAdmin.SelectedItem.ToString().Equals("Yes"))
                        editUserInfo.Admin = true;
                    if (ddlEditAdmin.SelectedItem.ToString().Equals("No"))
                        editUserInfo.Admin = false;

                    //Check to see if userName has been changed
                    if (!tempName.Equals(txtEditUserName.Text))
                    {
                        // Check to see if the new userName is a duplicate
                        if (db.Users.Where(u => u.UserName.ToLower() == txtEditUserName.Text.ToLower()).Count() > 0)
                        {
                            lblResponse.Visible = true;
                            lblResponse.ForeColor = System.Drawing.Color.Red;
                            lblResponse.Text = "Cannot save duplicate username: " + txtEditUserName.Text + "!";
                        }
                        else
                        {
                            db.SaveChanges();
                            lblResponse.Visible = true;
                            lblResponse.Text = "Changes to username: " + txtEditUserName.Text + ", successfully saved!";
                        }
                    }
                    else // userName has not been changed, just save changes
                    {
                        db.SaveChanges();
                        lblResponse.Visible = true;
                        lblResponse.Text = "Changes to username: " + txtEditUserName.Text + ", successfully saved!";
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
}