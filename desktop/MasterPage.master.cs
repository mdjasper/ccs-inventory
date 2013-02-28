using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_MasterPage : System.Web.UI.MasterPage
{
    public int UserId = 0;
    public string Name = "", Username = "";
    public bool Admin = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserId = int.Parse(Session["UserId"].ToString());
        Name = Session["Name"].ToString();
        Username = Session["Username"].ToString();
        Admin = bool.Parse(Session["Admin"].ToString());
    }

    protected void linkBtnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("~/login.aspx");
    }
}
