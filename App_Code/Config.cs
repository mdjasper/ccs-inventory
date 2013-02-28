using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

/// <summary>
/// Global Configuration settings for Application
/// </summary>
public static class Config
{
    public static string DOMAIN()
    {
        /* Source Attribution:
         * http://devio.wordpress.com/2009/10/19/get-absolut-url-of-asp-net-application/
         */

        string path = string.Empty;

        //Getting the current context of HTTP request
        var context = HttpContext.Current;

        //Checking the current context content
        if (context != null)
        {
            //Formatting the fully qualified website url/name
            path = string.Format("{0}://{1}{2}{3}",
                                    context.Request.Url.Scheme,
                                    context.Request.Url.Host,
                                    context.Request.Url.Port == 80
                                        ? string.Empty
                                        : ":" + context.Request.Url.Port,
                                    context.Request.ApplicationPath);
        }

        if (!path.EndsWith("/"))
            path += "/";

        return path;
    }

    public static bool MOBILE()
    {

        //Getting the current context of HTTP request
        var context = HttpContext.Current;

        bool isMobileDevice = false;

        string[] MobileDevices = new string[] {
            "iPhone",
            "iPad",
            "iPod",
            "BlackBerry",
            "Nokia",
            "Android",
            "WindowsPhone",
            "Mobile"
        };

        foreach (string MobileDeviceName in MobileDevices)
        {
            if ((context.Request.UserAgent.IndexOf(MobileDeviceName, StringComparison.OrdinalIgnoreCase)) > 0)
            {
                isMobileDevice = true;
                break;
            }
        }//end:foreach loop

        return isMobileDevice;
    }
    public static string REVISION()
    {
        /*
         * Logs on the svn repo, retrieves page, scrapes revision number
         */

        string requestUri = "http://3750.mikedoesweb.com";

        WebRequest request = WebRequest.Create(requestUri);

        string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("mike:rookie1"));
        request.Headers.Add("Authorization", "Basic " + credentials);

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        // Open the stream using a StreamReader for easy access.
        StreamReader reader = new StreamReader(dataStream);
        // Read the content.
        return reader.ReadToEnd().Split(' ')[3].Replace(":", ""); //Find the revision number in the html
 
    }
}