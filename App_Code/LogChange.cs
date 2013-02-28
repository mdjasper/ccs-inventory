using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Log
/// </summary>
public class LogChange
{
    public LogChange()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void logChange(String description, DateTime timestamp, short userID)
    {
        try
        {
            using(CCSEntities db = new CCSEntities())
            {
                Log log = new Log();

                log.Description = description;
                log.Date = timestamp;
                log.UserID = userID;

                db.Logs.Add(log);
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            LogError.logError(ex);
        }
    }
}