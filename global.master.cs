using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class global : System.Web.UI.MasterPage
{
    public int UserId = 0;
    public string Name = "", Username = "";
    public bool Admin = false;

    protected void Page_Load(object sender, EventArgs e)
    {

        string target = HttpContext.Current.Request.Url.AbsolutePath;

        if (target.IndexOf("login.aspx") < 0 &&
            target.IndexOf("404.aspx") < 0 &&
            target.IndexOf("error.aspx") < 0
            )
        {
            //We're not on the login page
            if (Session["UserId"] == null)
            {
                //User has not logged in
                Response.Redirect("~/login.aspx?callback=" + target);
            }
            else
            {
                UserId = int.Parse(Session["UserId"].ToString());
                Name = Session["Name"].ToString();
                Username = Session["Username"].ToString();
                Admin = bool.Parse(Session["Admin"].ToString());
            }
        }
    }
}
