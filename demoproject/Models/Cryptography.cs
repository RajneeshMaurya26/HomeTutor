using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demoproject.Models
{
    public class Cryptography
    {
        public string EncryptMyData(string PlainText)
        {
            int ASCII;
            char ch;
            string CyperText = "";
            foreach (char myChar in PlainText)
            {
                ASCII = myChar;//To get ASCII value of character
                if (ASCII >= 65 && ASCII <= 90)
                    ASCII = ASCII + 2;
                else if (ASCII >= 97 && ASCII <= 122)
                    ASCII = 122 - ASCII + 97;
                else if (ASCII >= 48 && ASCII <= 57)
                    ASCII = 57 - ASCII + 48;
                ch = (char)ASCII;//To get new Character from new Ascii value
                CyperText = CyperText + ch;
            }
            return CyperText;
        }
    }
}