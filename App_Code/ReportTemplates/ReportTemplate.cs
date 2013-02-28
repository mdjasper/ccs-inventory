using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReportTemplate
/// </summary>
public partial class ReportTemplate
{
    public enum ReportType { Incoming, Outgoing, Inventory, GroceryRescue, InOut};
    public enum SelectionType { ALL, NONE, SOME, PERISHABLE, NONFOOD, REGULAR };

    public static string[] ReportTypeNames = { "Incoming", "Outgoing", "Inventory", "Grocery Rescue", "In\\Out" };
}