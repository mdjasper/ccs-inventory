using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity.Validation;

public partial class container_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool autoGenerate = chkAutoGen.Checked;
        int binNumber = 0;
        using (CCSEntities db = new CCSEntities())
        {
            if (autoGenerate)
            {
                Random rnd = new Random();

                while (binNumber == 0)
                {
                    int BinNumberCandidate = rnd.Next(1000, 9999);

                    int count = (from c in db.Containers
                                 where c.BinNumber == BinNumberCandidate
                                 select c).Count();

                    if (count == 0)
                    {
                        binNumber = BinNumberCandidate;
                    }
                }

            }
            else
            {
                if (txtBinNumber.Text != "")
                {
                    binNumber = int.Parse(txtBinNumber.Text);

                }
                else
                {
                    lblError.Text = "Whoops! an error occured";
                    lblError.Visible = true;
                }
            }

            if (binNumber != 0)
            {
                Container c = new Container();
                c.BinNumber = (short)binNumber;
                
                Location l = (from lo in db.Locations
                              where lo.RoomName == "(NONE)" select lo).First();
                c.Location = l;

                FoodCategory f = (from fc in db.FoodCategories
                                  where fc.CategoryType == "(NONE)" 
                                  select fc).First();
                c.FoodCategory = f;

                USDACategory u = (from uc in db.USDACategories select uc).First();
                c.USDACategory = u;

                c.isUSDA = false;
                c.Cases = 0;

                //c.LocationID = null;

                db.Containers.Add(c);
                try
                {
                    db.SaveChanges();
                    LogChange.logChange("Container " + binNumber + " Added.", DateTime.Now, short.Parse(Session["userID"].ToString()));
                    Response.Redirect("edit.aspx?id=" + binNumber);
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Response.Write("Property: " + validationError.PropertyName+" Error: " + validationError.ErrorMessage);
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
}