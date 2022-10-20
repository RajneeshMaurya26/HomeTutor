using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace demoproject.Models
{
    public class MySMS
    {
        public string UserID = "KSSCKT", Pass = "41f95f660fXX", SenderId = "KSSCKT";
        public bool SendMySMS(string MobileNo, string Message)
        {
            try
            {
                string myurl = "http://t.160smsalert.com/submitsms.jsp?user=" + UserID + "&key=" + Pass + "&mobile=" + MobileNo + "&message=" + Message + "&senderid=" + SenderId + "&accusage=1";
                WebClient wc = new WebClient();
                wc.DownloadString(myurl);//To send sms 
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}