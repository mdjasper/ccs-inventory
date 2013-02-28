using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_editfood : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userID"] == null)
            Response.Redirect("login.aspx");
        else
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["do"].Equals("i"))
                {
                    editFoodOutDiv.Visible = false;
                    lblTitle.Text = "Edit Food In Data";

                    try
                    {
                        using (CCSEntities db = new CCSEntities())
                        {

                            short shFoodInID = short.Parse(Session["editWhat"].ToString());

                            var editFoodInQuery = (from c in db.FoodIns
                                                   where c.FoodInID.Equals(shFoodInID)
                                                   select new { c.FoodInID, c.FoodCategoryID, c.Weight, c.FoodSourceID, c.TimeStamp });

                            if (editFoodInQuery.ToList().ElementAt(0).FoodInID.ToString() != null)
                                txtEditFoodInID.Text = editFoodInQuery.ToList().ElementAt(0).FoodInID.ToString();

                            if (editFoodInQuery.ToList().ElementAt(0).FoodCategoryID != null)
                                ddlFoodInCategoryType.SelectedValue = editFoodInQuery.ToList().ElementAt(0).FoodCategoryID.ToString();

                            if (editFoodInQuery.ToList().ElementAt(0).Weight.ToString() != null)
                                txtEditFoodInWeight.Text = editFoodInQuery.ToList().ElementAt(0).Weight.ToString();

                            if (editFoodInQuery.ToList().ElementAt(0).FoodSourceID.ToString() != null)
                                ddlEditFoodInSource.SelectedValue = editFoodInQuery.ToList().ElementAt(0).FoodSourceID.ToString();

                            if (editFoodInQuery.ToList().ElementAt(0).TimeStamp != null)
                                txtEditFoodInTime.Text = editFoodInQuery.ToList().ElementAt(0).TimeStamp.ToString();

                        }
                    }
                    catch (System.Threading.ThreadAbortException) { }
                    catch (Exception ex)
                    {
                        LogError.logError(ex);
                        Response.Redirect("../errorpages/error.aspx");
                    }
                }
                else if (Request.QueryString["do"].Equals("o"))
                {
                    editFoodInDiv.Visible = false;
                    lblTitle.Text = "Edit Food Out Data";

                    try
                    {
                        using (CCSEntities db = new CCSEntities())
                        {

                            short shDistID = short.Parse(Session["editWhat"].ToString());

                            var userInfoQuery = (from c in db.FoodOuts
                                                 where c.DistributionID.Equals(shDistID)
                                                 select new { c.DistributionID, c.FoodCategoryID, c.USDACategory.USDANumber, c.Weight, c.Count, c.TimeStamp });

                            txtFoodOutDistID.Text = userInfoQuery.ToList().ElementAt(0).DistributionID.ToString();

                            if(userInfoQuery.ToList().ElementAt(0).FoodCategoryID != null )
                                ddlFoodOutCategoryType.SelectedValue = userInfoQuery.ToList().ElementAt(0).FoodCategoryID.ToString();

                            if (userInfoQuery.ToList().ElementAt(0).USDANumber != null)
                                txtFoodOutUSDAID.Text = userInfoQuery.ToList().ElementAt(0).USDANumber.ToString();
                            txtFoodOutWeight.Text = userInfoQuery.ToList().ElementAt(0).Weight.ToString();
                            txtFoodOutCount.Text = userInfoQuery.ToList().ElementAt(0).Count.ToString();
                            txtFoodOutTime.Text = userInfoQuery.ToList().ElementAt(0).TimeStamp.ToString();
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


    // @author: Anthony Dietrich - Save Food In Edit Data
    // Saves Edit Data from the Food In version of the editfood page.
    protected void btnEditFoodInSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                short shFoodInID = short.Parse(Session["editWhat"].ToString());

                FoodIn editFoodIn = (from c in db.FoodIns
                                     where c.FoodInID.Equals(shFoodInID)
                                     select c).FirstOrDefault();

                editFoodIn.FoodCategoryID = short.Parse(ddlFoodInCategoryType.SelectedValue);
                editFoodIn.Weight = Convert.ToDecimal(txtEditFoodInWeight.Text);
                editFoodIn.FoodSourceID = short.Parse(ddlEditFoodInSource.SelectedValue);
                editFoodIn.TimeStamp = Convert.ToDateTime(txtEditFoodInTime.Text);

                db.SaveChanges();
                lblResponse.Visible = true;
                lblResponse.Text = "Changes to Food In ID: " + txtEditFoodInID.Text + ", successfully saved!";
            }
        }
        catch (System.Threading.ThreadAbortException) { }
        catch (Exception ex)
        {
            LogError.logError(ex);
            Response.Redirect("../errorpages/error.aspx");
        }

    }

    // @author: Anthony Dietrich - Save Food Out Edit Data
    // Saves Edit Data from the Food Out version of the editfood page.
    protected void btnEditFoodOutSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (CCSEntities db = new CCSEntities())
            {

                short shFoodOutID = short.Parse(Session["editWhat"].ToString());

                FoodOut editFoodOut = (from c in db.FoodOuts
                                       where c.DistributionID.Equals(shFoodOutID)
                                       select c).FirstOrDefault();

                editFoodOut.DistributionID = short.Parse(txtFoodOutDistID.Text);
                editFoodOut.FoodCategoryID = short.Parse(ddlFoodOutCategoryType.SelectedValue);
               // editFoodOut.USDACategory = 
                editFoodOut.Weight = Convert.ToDouble(txtFoodOutWeight.Text);
                editFoodOut.Count = short.Parse(txtFoodOutCount.Text);
                editFoodOut.TimeStamp = Convert.ToDateTime(txtFoodOutTime.Text);

                db.SaveChanges();
                lblResponse.Visible = true;
                lblResponse.Text = "Changes to Distribution ID: " + txtFoodOutDistID.Text + ", successfully saved!";
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