using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{

    private int id;
    Agency a;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                id = int.Parse(Request.QueryString["id"]);
                using (CCSEntities db = new CCSEntities())
                {

                    ddlState.DataSource = db.States.ToList();
                    ddlState.DataBind();

                    a = (from ag in db.Agencies where ag.AgencyID == id select ag).First();
                    
                    if(a != null){
                        txtAgencyName.Text = a.AgencyName;
                        txtStreet1.Text = a.Address.StreetAddress1;
                        txtStreet2.Text = a.Address.StreetAddress2;
                        txtCity.Text = a.Address.City.CityName;
                        txtZip.Text = a.Address.Zipcode.ZipCode1;
                        ddlState.SelectedIndex = a.Address.StateID;
                    }
                }
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        id = int.Parse(Request.QueryString["id"]);
        using (CCSEntities db = new CCSEntities())
        {
            a = (from ag in db.Agencies where ag.AgencyID == id select ag).First();
            //Update simple fields
            if(txtAgencyName.Text != "")
                a.AgencyName = txtAgencyName.Text;
            if (txtStreet1.Text != "")
                a.Address.StreetAddress1 = txtStreet1.Text;
            if (txtStreet2.Text != "")
                a.Address.StreetAddress2 = txtStreet2.Text;
            a.Address.StateID = (short)ddlState.SelectedIndex;

            string zip = txtZip.Text;
            string city = txtCity.Text;

        
            //does new zip already exist?
            if ((from z in db.Zipcodes where z.ZipCode1 == zip select z).Count() > 0)
            {
                //zip already exists, use existing zip
                a.Address.ZipID = (from z in db.Zipcodes where z.ZipCode1 == zip select z.ZipID).First();
            }
            else
            {
                //create new zip
                Zipcode z = new Zipcode();
                z.ZipCode1 = zip;
                db.Zipcodes.Add(z);
                a.Address.Zipcode = z;
            }

            //Does new city already exist?
            if ((from c in db.Cities where c.CityName == city select c).Count() > 0)
            {
                //zip already exists, use existing zip
                a.Address.City.CityID = (from c in db.Cities where c.CityName == city select c.CityID).First();
            }
            else
            {
                //create new zip
                City c = new City();
                c.CityName = city;
                db.Cities.Add(c);
                a.Address.City = c;
            }
            db.SaveChanges();
            saved.Visible = true;

        }

    }
}