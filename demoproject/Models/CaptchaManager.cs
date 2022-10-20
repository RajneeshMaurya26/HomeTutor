using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace demoproject.Models
{
    public class CaptchaManager
    {
        private string GetRandomCode()
        {
            string code = "";
            Random r = new Random();
            char ch = (char)r.Next(65, 90);
            code = code + ch;
            ch = (char)r.Next(97, 115);
            code = code + ch;
            ch = (char)r.Next(48, 57);
            code = code + ch;
            ch = (char)r.Next(100, 120);
            code = code + ch;
            ch = (char)r.Next(70, 80);
            code = code + ch;
            ch = (char)r.Next(48, 122);
            code = code + ch;
            int n = r.Next(1, 10);
            if (n % 2 == 0)
            {
                ch = (char)r.Next(65, 90);
                code = code + ch;
            }
            return code;
        }
        public string[] GetCaptchaCodeAndImagePath()
        {
            Bitmap bmp = new Bitmap(135, 35); // chart paper..
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Silver); // color of chart paper....
            Pen MaroonPen = new Pen(Color.Maroon); // Drawing pen color to draw border..
            SolidBrush sb = new SolidBrush(Color.Red); // To Write text on chart paper..
            Font f = new Font("Verdana", 15, FontStyle.Strikeout); // Style of text ,size of text and font family of text...
            g.DrawRectangle(MaroonPen, 0, 0, 133, 33); // To give the shap on chart paper...
            string code = GetRandomCode(); // calling code generater function...
            g.DrawString(code, f, sb, 10, 5); // printing all the code with font style,margin-top and left...
            g.Flush(); // To save the change on bitmap and refresh the buffer memory...
            string fpath = HttpContext.Current.Server.MapPath("/Content/CaptchaImg");
            if (!Directory.Exists(fpath))
                Directory.CreateDirectory(fpath);
            DirectoryInfo di = new DirectoryInfo(fpath);
            FileInfo[] fi = di.GetFiles();
            foreach (FileInfo fr in fi)
            {
                fr.Delete();
            }
            string fName = Path.GetRandomFileName() + ".jpg";
            bmp.Save(fpath + "/" + fName, ImageFormat.Jpeg);
            string[] arr = new string[2];
            arr[0] = code;
            arr[1] = fName;
            return arr;
        }

    }
}