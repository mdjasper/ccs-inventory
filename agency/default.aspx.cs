using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class agency : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            using (CCSEntities db = new CCSEntities())
            {
                grdAgency.DataSource = (from a in db.Agencies
                                        select new
                                        {
                                            ID = a.AgencyID,
                                            Name = a.AgencyName,
                                            City = a.Address.City.CityName
                                        }).ToList();
                grdAgency.DataBind();
            }
        }
    }
    protected void grdAgency_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdAgency_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void btnAddAgency_Click(object sender, EventArgs e)
    {
        Response.Redirect("add.aspx");
    }
}