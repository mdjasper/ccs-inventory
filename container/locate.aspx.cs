using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                using (CCSEntities db = new CCSEntities())
                {
                    ddlType.DataSource = db.FoodCategories.OrderBy(x => x.CategoryType).ToList();
                    ddlType.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Session["categoryId"] = ddlType.SelectedValue;
        Response.Redirect("locations.aspx");
    }
}