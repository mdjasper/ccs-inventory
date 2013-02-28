using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    private string callback;
    
    protected void Page_Load(object sender, EventArgs e)
    {
         callback = Request.QueryString["callback"] != null ? Request.QueryString["callback"] : "default.aspx";
    }
    
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            UserData u = Auth.Authenticate(username, password);

            if (u.id > 0)
            {
                //Credentials are correct, login user
                Session["UserId"] = u.id;
                Session["UserName"] = u.username;
                Session["Admin"] = u.admin;
                Session["Name"] = u.name;
                Response.Redirect(callback, false);
            }
            else
            {
                //Credentails are false, obliterate the session
                Session.Clear();
                lblError.Text = "Your username or password are invalid.";
            }
        }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("errorpages/error.aspx");
        }
    }
}