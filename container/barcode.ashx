<%@ WebHandler Language="C#" Class="barcode" %>

using System;
using System.Web;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing;

public class barcode : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string data = context.Request.QueryString["data"];
        data = data.ToUpper();
        data = "*" + data + "*";

        context.Response.ContentType = "image/gif";
        context.Response.Clear();
        Bitmap barCode = GenerateBarCode(data);
        barCode.Save(context.Response.OutputStream, ImageFormat.Gif);
        context.Response.End();
        barCode.Dispose();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    private Bitmap GenerateBarCode(string data) //codeInfo is the info which you want to barcoded.
    {
        Bitmap barCode = new Bitmap(1, 1);

        FontFamily barcodeFont = new FontFamily("Free 3 of 9");
        
        Font threeOfNine = new Font(barcodeFont, 100,
            System.Drawing.FontStyle.Regular,
            System.Drawing.GraphicsUnit.Point);
        

        Graphics graphics = Graphics.FromImage(barCode);

        SizeF dataSize = graphics.MeasureString(data, threeOfNine);

        barCode = new Bitmap(barCode, dataSize.ToSize());

        graphics = Graphics.FromImage(barCode);

        graphics.Clear(Color.White);

        graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;

        graphics.DrawString(data, threeOfNine,
            new SolidBrush(Color.Black), 0, 0);

        graphics.Flush();

        threeOfNine.Dispose();
        graphics.Dispose();
        return barCode;
    }

}