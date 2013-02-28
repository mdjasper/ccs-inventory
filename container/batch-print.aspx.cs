using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class container_batch_print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        using (CCSEntities db = new CCSEntities())
        {
            List<Container> lstContainers = (from c in db.Containers select c).ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Type");
            dt.Columns.Add("Date");
            dt.Columns.Add("img");
                
            for (int i = 0; i < lstContainers.Count; i++)
            {
                dt.Rows.Add(
                    lstContainers.ElementAt(i).BinNumber,
                    (bool)lstContainers.ElementAt(i).isUSDA ?
                        lstContainers.ElementAt(i).USDACategory.Description : lstContainers.ElementAt(i).FoodCategory.CategoryType,
                    DateTime.Today.ToString("d"),
                    "barcode.ashx?data=" + lstContainers.ElementAt(i).BinNumber
                );
            }

            rptLabels.DataSource = dt;
            rptLabels.DataBind();
        }
    }
}