using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing;


public partial class container_print : System.Web.UI.Page
{
    public int id;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            id = int.Parse(Request.QueryString["id"]);

            using (CCSEntities db = new CCSEntities())
            {
                FoodIn foodInRecord = (from c in db.FoodIns
                                       where c.FoodInID == id
                                       select c).FirstOrDefault();

                lblAddress.Text = foodInRecord.FoodSource.Address.StreetAddress1 + " " + foodInRecord.FoodSource.Address.StreetAddress2;
                lblCity.Text = foodInRecord.FoodSource.Address.City.CityName;
                lblDateRecived.Text = foodInRecord.TimeStamp.ToString("d");
                lblDonor.Text = foodInRecord.FoodSource.Source;
                lblState.Text = foodInRecord.FoodSource.Address.State.StateShortName;
                lblZip.Text = foodInRecord.FoodSource.Address.Zipcode.ZipCode1;
                lblWeight.Text = foodInRecord.Weight.ToString();
                lblDescription.Text = foodInRecord.USDACategory == null ? foodInRecord.FoodCategory.CategoryType : foodInRecord.USDACategory.Description;
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