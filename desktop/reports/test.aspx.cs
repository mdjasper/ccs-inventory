using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_reports_test : System.Web.UI.Page
{
    List<string> items = new List<string>();
    List<string> items2 = new List<string>();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            items.Add("DASF");
            items.Add("DASF");
            items.Add("DASF");

            items.Add("DASF");
            lstAvailableItems.DataSource = items;
            lstAvailableItems.DataBind();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        foreach (var i in lstAvailableItems.GetSelectedIndices())
        {
            lstChosenItems.Items.Add(lstAvailableItems.Items[i]);
        }
    }
}