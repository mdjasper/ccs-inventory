using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class LogError
{
	public LogError()
	{
		
	}

    /// bulk of code written by: Alex Marcum - 11/20/2012
    public static void logError(Exception ex)
    {
        System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame(1, true);

        //System.IO.FileInfo temp = new System.IO.FileInfo(fileName);
        //fileName = temp.Name;

        //Class Name is the Whole path to the Filename
        string fileName = stackFrame.GetFileName();
        string functionName = stackFrame.GetMethod().Name.ToString();
        int line = stackFrame.GetFileLineNumber();

        try
        {
            using (CCSEntities db = new CCSEntities())
            {
                ErrorLog el = new ErrorLog();

                el.TimeStamp = DateTime.Now;
                el.FileName = fileName;
                el.FunctionName = functionName;
                el.LineNumber = line.ToString();
                el.ErrorText = ex.Message;

                db.ErrorLogs.Add(el);
                db.SaveChanges();
            }
        }
        catch (Exception /*ex*/)
        {
        }
    }
}