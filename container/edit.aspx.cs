using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    public int id;
    private Container container;
    private List<Location> lstLocations;
    private List<FoodCategory> lstFoodCategories;
    private List<USDACategory> lstUSDACategories;
    public bool isUSDA;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("default.aspx");
            }

            id = int.Parse(Request.QueryString["id"]);



            using (CCSEntities db = new CCSEntities())
            {
                container = (from c in db.Containers
                             where c.BinNumber == (Int16)id
                             select c).FirstOrDefault();

                if (!Page.IsPostBack)
                {

                    lstLocations = db.Locations.OrderBy(x => x.RoomName).ToList();
                    ddlLocation.DataSource = lstLocations;
                    ddlLocation.DataBind();

                    lstFoodCategories = db.FoodCategories.OrderBy(x => x.CategoryType).ToList();
                    ddlType.DataSource = lstFoodCategories;
                    ddlType.DataBind();

                    lstUSDACategories = db.USDACategories.OrderBy(x => x.Description).ToList();
                    ddlUSDAType.DataSource = lstUSDACategories;
                    ddlUSDAType.DataBind();

                    isUSDA = (bool)container.isUSDA;
                    chkIsUSDA.Checked = isUSDA;

                    txtNumberOfCases.Text = container.Cases.ToString();

                    if (container != null)
                    {

                        lblID.Text = container.BinNumber.ToString();
                        txtWeight.Text = container.Weight.ToString();
                        if (container.FoodCategory != null)
                            ddlType.Items.FindByValue(container.FoodCategoryID.ToString()).Selected = true;

                        if (container.Location != null)
                            ddlLocation.Items.FindByValue(container.LocationID.ToString()).Selected = true;

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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            double num;
            if (!(double.TryParse(txtWeight.Text, out num))) //checks that it is a number
            {
                lblWeightError.Text = "Invalid Weight Number!";
            }
            else if ((double.Parse(txtWeight.Text)) < 0)  //checks that number is not less than 0
            {
                lblWeightError.Text = "Invalid Weight Number!";
            }
            else
            {
                using (CCSEntities db = new CCSEntities())
                {
                    Container cont = (from c in db.Containers
                                      where c.ContainerID == container.ContainerID
                                      select c).FirstOrDefault();

                    cont.Weight = decimal.Parse(txtWeight.Text);
                    cont.LocationID = short.Parse(ddlLocation.SelectedValue);
                    cont.FoodCategoryID = short.Parse(ddlType.SelectedValue);
                    cont.USDAID = short.Parse(ddlUSDAType.SelectedValue);
                    cont.isUSDA = chkIsUSDA.Checked;
                    cont.Cases = short.Parse(txtNumberOfCases.Text==""? "0" : txtNumberOfCases.Text);

                    db.SaveChanges();
                    LogChange.logChange("Container " + cont.BinNumber + " Edited.", DateTime.Now, short.Parse(Session["userID"].ToString()));
                }
            }
            message.Text = "Changes Saved!";
            savedMessage.Visible = true;
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                Container c = (from co in db.Containers
                               where co.BinNumber == (short)id
                               select co).First();

                String binNumber = c.BinNumber.ToString(); // saved for logging purposes
                db.Containers.Remove(c);
                db.SaveChanges();

                LogChange.logChange("Container " + binNumber + " Removed.", DateTime.Now, short.Parse(Session["userID"].ToString()));
            }
            Response.Redirect("default.aspx");
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }
        
    }
}