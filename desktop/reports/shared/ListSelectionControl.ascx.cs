using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class desktop_reports_shared_ListSelectionControl : System.Web.UI.UserControl
{
    public string Title { get; set; }

    static int _CurrentControlNumber = 0;

    public static int CurrentControlNumber
    {
        set{ _CurrentControlNumber = value;}
        get { return _CurrentControlNumber; }
    }

    public int ControlNumber { get; set; }

    public  ReportTemplate.SelectionType SelectedType
    {
        get
        {
            if (chkAll.Checked)
                return ReportTemplate.SelectionType.ALL;
            else if (chkNone.Checked)
                return ReportTemplate.SelectionType.NONE;
            else if (chkPerishable.Checked)
                return ReportTemplate.SelectionType.PERISHABLE;
            else if (chkRegular.Checked)
                return ReportTemplate.SelectionType.REGULAR;
            else if (chkNonFood.Checked)
                return ReportTemplate.SelectionType.NONFOOD;
            else
                return ReportTemplate.SelectionType.SOME;
        }
        set
        {
            if (value == ReportTemplate.SelectionType.ALL)
                chkAll.Checked = true;
            else if (value == ReportTemplate.SelectionType.NONE)
                chkNone.Checked = true;
            else if (value == ReportTemplate.SelectionType.PERISHABLE)
                chkPerishable.Checked = true;
            else if (value == ReportTemplate.SelectionType.REGULAR)
                chkRegular.Checked = true;
            else if (value == ReportTemplate.SelectionType.NONFOOD)
                chkNonFood.Checked = true;
            else
                tableSelectItems.Visible = true;

        }
    }


    private bool _AllowNone = true;
    public bool AllowNone
    { 
        get { return _AllowNone; }
        set
        {
            _AllowNone = value;
            chkNone.Visible = _AllowNone;
        }
    }


    private bool _FoodCategories = false;
    public bool FoodCategories
    {
        get { return _AllowNone; }
        set
        {   
            _FoodCategories = value;
            pnlFoodTypes.Visible = _FoodCategories;
        }
    }

    public object AvailableList
    {
        get { return lstAvailableItems.DataSource; }
        set
        {
            lstAvailableItems.DataSource = value;
            lstAvailableItems.DataBind();
        }
    }

    public string DataTextField
    {
        get { return lstAvailableItems.DataTextField; }
        set
        {
            lstAvailableItems.DataTextField = value;
            lstChosenItems.DataTextField = value;
        }
    }

    public string DataValueField
    {
        get { return lstAvailableItems.DataValueField; }
        set
        {
            lstAvailableItems.DataValueField = value;
            lstChosenItems.DataValueField = value;
        }
    }

    public List<string> SelectedIDs
    {
        get
        {
            List<string> items = new List<string>();
            foreach (ListItem i in lstChosenItems.Items)
            {
                items.Add(i.Value);
            }
            return items;
        }
        set
        {
            List<ListItem> itemsToTransfer = new List<ListItem>();

            foreach (string i in value)
            {
                foreach (ListItem item in lstAvailableItems.Items)
                {
                    if (item.Value == i)
                    {
                        itemsToTransfer.Add(item);
                    }
                }
            }

            foreach (ListItem item in itemsToTransfer)
                lstChosenItems.Items.Add(item);

            
            foreach (ListItem item in itemsToTransfer)
                lstAvailableItems.Items.Remove(item);

        }
    }

    
    public bool SelectAll
    {
        get { return chkAll.Checked; }
        set { chkAll.Checked = value; }
    }

    public bool SelectNone
    {
        get { return chkNone.Checked; }
        set { chkNone.Checked = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ControlNumber = CurrentControlNumber++;
    }



    protected void btnAdd_Click(object sender, EventArgs e)
    {
        List<ListItem> itemsToRemove = new List<ListItem>();
        foreach (var i in lstAvailableItems.GetSelectedIndices())
        { 
            ListItem item = lstAvailableItems.Items[i];
            itemsToRemove.Add(item);
            lstChosenItems.Items.Add(item);
        }

        foreach (var i in itemsToRemove)
            lstAvailableItems.Items.Remove(i);
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        List<ListItem> itemsToRemove = new List<ListItem>();
        foreach (var i in lstChosenItems.GetSelectedIndices())
        {
            ListItem item = lstChosenItems.Items[i];
            itemsToRemove.Add(item);
            lstAvailableItems.Items.Add(item);
        }

        foreach (var i in itemsToRemove)
            lstChosenItems.Items.Remove(i);
    }

    protected void CheckChanged(object sender, EventArgs e)
    {
        bool wasChecked = (sender as CheckBox).Checked;

        chkAll.Checked = chkNone.Checked = chkNonFood.Checked = chkPerishable.Checked = chkRegular.Checked = false;

        if(wasChecked)
        {
            (sender as CheckBox).Checked = true;
            tableSelectItems.Visible = false;
        }
        else
        {
            tableSelectItems.Visible = true;
        }
    }
}