using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using demoproject.Models;

namespace demoproject.Controllers
{
    public class HomeController : Controller
    {
        DBManager dm = new DBManager();
        Cryptography cr = new Cryptography();
        [NonAction]
        void subject()
        {
            List<SelectListItem> Lst=new List<SelectListItem>(10);
            string[] arr=new string[]{"Maths","English","Science","Social Science","Hindi","Sanakrit","Computer","Art"};
            foreach (string s in arr)
            {
                SelectListItem ls = new SelectListItem();
                ls.Text = s;
                Lst.Add(ls);
            }
            ViewBag.Subject = Lst;
        }
        void qualification()
        {
            List<SelectListItem> Lst1 = new List<SelectListItem>(10);
            string[] arr = new string[] { "High School", "Intermediate", "BA", "BSC", "MA", "Diploma", "BCA","B.TECH","MCA","MBA" };
            foreach (string s1 in arr)
            {
                SelectListItem ls1 = new SelectListItem();
                ls1.Text = s1;
                Lst1.Add(ls1);
            }
            ViewBag.Qualification = Lst1;
        }

        void exp()
        {
            List<SelectListItem> Lst2 = new List<SelectListItem>(10);
            string[] arr = new string[] { "1 Year", "2 Year", "3 Year", "4 Year", "5 Year", "6 Year", "7 Year", "8 Year","9 Year","10 Year" };
            foreach (string s2 in arr)
            {
                SelectListItem ls2 = new SelectListItem();
                ls2.Text = s2;
                Lst2.Add(ls2);
            }
            ViewBag.Experience = Lst2;
        }
        void city()
        {
            List<SelectListItem> Lst3 = new List<SelectListItem>(10);
            string[] arr = new string[] { "Allahabad", "Kanpur", "Chitrakoot", "Kaushbambi", "Varansi", "Ballia", "Fatehpur", "Agra", "Etawa", "Mau" };
            foreach (string s3 in arr)
            {
                SelectListItem ls3 = new SelectListItem();
                ls3.Text = s3;
                Lst3.Add(ls3);
            }
            ViewBag.city = Lst3;
        }
        void course()
        {
            List<SelectListItem> Lst4 = new List<SelectListItem>(10);
            string[] arr = new string[] { "High School", "Intermediate", "BA", "BSC", "MA", "Diploma", "BCA", "B.TECH", "MCA", "MBA" };
            foreach (string s4 in arr)
            {
                SelectListItem ls4 = new SelectListItem();
                ls4.Text = s4;
                Lst4.Add(ls4);
            }
            ViewBag.course = Lst4;
        }
        public List<Notification> getNotification()
        {
            dm.MyCommandText = "Select *from Notification order by Id desc";
            DataTable dt = dm.ReadBulkRecord();
            List<Notification> LstN = new List<Notification>();
            foreach (DataRow dr in dt.Rows)
            {
                Notification n = new Notification();
                n.Id = dr[0].ToString();
                n.NMessage = dr[1].ToString();
                n.Date_Of_Notification = dr[2].ToString();
                LstN.Add(n);
            }
            return LstN;
        }
        [HttpGet]
        public ActionResult Index()
        {
            subject();
            city();
            List<Notification> LstN = getNotification();
            return View(LstN);
        }

        [HttpPost]
        public ActionResult Index(string ab)
        {
            subject();
            city();
            return View();
        }

        static string[] CodeAndImage = new string[2];
        [HttpGet]
        public ActionResult Registration( CaptchaManager cm)
        {
            CodeAndImage = cm.GetCaptchaCodeAndImagePath();
            ViewBag.captcha = CodeAndImage[1];
            subject();
            qualification();
            exp();
            city();
            return View();
        }
        public JsonResult RefreshCaptcha(CaptchaManager cm)
        {
            CodeAndImage = cm.GetCaptchaCodeAndImagePath();
            string NewImage = CodeAndImage[1];
            return Json(NewImage, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Registration(string ECaptcha,Registration r)
        {
            // for captcha mathing..
            try
            {
                if (ECaptcha.Trim().Equals(CodeAndImage[0]))
                {

                    // Database.......
                    HttpPostedFileBase myfile = Request.Files["Photo"];
                    if (myfile != null)
                    {
                        string fpath = Server.MapPath("/Content/Tutor_Photo");
                        if (!Directory.Exists(fpath))
                            Directory.CreateDirectory(fpath);
                        r.Photo = Path.GetRandomFileName() + myfile.FileName;
                        myfile.SaveAs(fpath + "/" + r.Photo);

                        dm.MyCommandText = "Insert Into Tutor_Registration values('" + r.Name + "','" + r.Gender + "','" +
                            r.DOB + "','" + r.Subject + "','" + r.City + "','" + r.Qualification + "','" + r.Experience + "','" +
                            r.Address + "','" + r.Number + "','" + r.Email + "','" + r.Photo + "','" +
                            DateTime.Now.ToString("dd/MM/yy") + "')";
                        bool b = dm.ExecuteInsertDeleteOrUpdate();
                        if (b == true)
                        {
                            string pass = cr.EncryptMyData(r.Password);
                            dm.MyCommandText = "Insert into Login values('" + r.Email + "','" + pass + "','Tutor','0','')";
                            b = dm.ExecuteInsertDeleteOrUpdate();
                            if (b == true)
                            {
                                TempData["msg"] = "You Registred Successfully";
                                return RedirectToAction("Login", "Home");
                            }
                            else
                                TempData["msg"] = "Sorry! Unable Register Your Account.";

                        }
                        else
                            TempData["msg"] = "Sorry! due to some Technical Problem we are Unable Register Your Account.";
                        subject();
                        qualification();
                        exp();
                        city();
                    }

                }
                else
                    TempData["msg"] = "Invalid Captcha";
            }
            catch
            {
                TempData["msg"] = "Invalid Captcha";
            }
            subject();
            qualification();
            exp();
            city();
            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(Contact c)
        {
            dm.MyCommandText = "Insert Into Contact_Us values('"+c.fName+"','"+c.Email+"','"+c.Number+"','"+c.Message+"','"+DateTime.Now.ToString()+"')";
            bool M = dm.ExecuteInsertDeleteOrUpdate();
            if (M == true)
                ViewBag.msg = "Message Successfully Send";
            else
                ViewBag.msg = "Unable to send Message";
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login lg)
        {
            try
            {
                string pass = cr.EncryptMyData(lg.Userpass);
                dm.MyCommandText = "Select UserId,Pass,Utype from Login where UserId='" + lg.Username + "' and Pass='" + pass + "' and Utype='" + lg.UType + "' ";
                DataTable dt = dm.ReadBulkRecord();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        
                        if (lg.UType.ToUpper() == "ADMIN")
                        {
                            Session["Uid"] = lg.Username;
                            return RedirectToAction("Dashboard", "Admin");
                        }
                        else
                        {
                            Session["Uid"] = lg.Username;
                            return RedirectToAction("Dashboard", "Tutor");
                        }
                    }
                    else
                    {
                        ViewBag.msg = "! Incorrect Username Or Password";
                    }
                }
                else
                {
                    ViewBag.msg = "! Incorrect Username Or Password";
                }
            }
            catch
            {
                ViewBag.msg = "! Incorrect Username Or Password";
            }
            return View();
        }

        public ActionResult Review()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Enquiry(Enquiry En)
        {
            dm.MyCommandText = "Insert into Enquiry values('"+En.Name+"','"+En.Email+"','"+En.Mob_Number+"','"+En.Topic+"','"+En.Query+"','"+DateTime.Now.ToString()+"')";
            bool status = dm.ExecuteInsertDeleteOrUpdate();
            if (status == true)
            {
                TempData["Msg"] = "Enquiry Send Successfully";
                return RedirectToAction("Index","Home");
            }
            else
            {
                TempData["Msg"] = "Due to Some Technical problem unable to Send You Enquiry";
            }
            return View();
        }
        [HttpGet]
        public ActionResult TutorRequest()
        {
            subject();
            course();
            return View();
        }
        [HttpPost]
        public ActionResult TutorRequest(TRequest tr)
        {
            dm.MyCommandText = "Insert into TRequest values('" +tr. RequestId + "','" + tr.Name + "','" +
                tr.Gender + "','" + tr.Course + "','" + tr.Subject + "','" + tr.Email + "','" + tr.Mob_Number + "','" +
                tr.Address+"','"+tr.Budget+"','"+tr.ExpectedDate+"','"+tr.Description+"','"+DateTime.Now.ToString()+"','')";
            bool Tr = dm.ExecuteInsertDeleteOrUpdate();
            if (Tr == true)
            {
                ViewBag.msg = "Your Request Send Successfully";
            }
            else
            {
                ViewBag.msg = "Unable To Send Your Request Due Some Technical Problem";
            }
            subject();
            course();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public ActionResult Feedback()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Feedback(string Name, string Review,string m)
        {
            TempData["TutorID"] = m;
            try
            {
                dm.MyCommandText = "Insert into Feedback values('" + Name + "','" + Review + "','"+m+"','" + DateTime.Now.ToString() + "')";
                bool status = dm.ExecuteInsertDeleteOrUpdate();
                if (status == true)
                {
                    TempData["msg"] = "Review Submitted Successfully";
                }
                else
                {
                    TempData["msg"] = "Unable to Submit Your Review..";
                }
            }
            catch
            {
                TempData["msg"] = "Unable to Submit Your Review..";
            }
            return View();
        }

        [HttpGet]
        public ActionResult ForgetPass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPass(string Email)
        {
            try
            {
                TempData["EMAIL"] = Email;
                Random r = new Random();
                int x = r.Next(1234, 9899);
                TempData["otp"] = x;
                dm.MyCommandText = "Select Number from Tutor_Registration where Email='" + Email + "'";
                DataTable dt = dm.ReadBulkRecord();
                string mob = dt.Rows[0][0].ToString();
                dm.MyCommandText = "Select Pass,Utype from Login where UserId='" + Email + "'";
                dt = dm.ReadBulkRecord();
                TempData["Pass"] = dt.Rows[0][0].ToString();
                TempData["Utype"] = dt.Rows[0][1].ToString();
                MySMS sms = new MySMS();
                bool status = sms.SendMySMS(mob.ToString(), x.ToString());
                return RedirectToAction("Confirm_OTP","Home");

            }
            catch
            {
                ViewBag.msg = "Unable To send OTP ";
                 
            }
          return View(); 
        }
        [HttpGet]
        public ActionResult Confirm_OTP()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Confirm_OTP(string cotp)
        {

                if (TempData["otp"].ToString().Equals(cotp))
                {
                    try
                    {
                        dm.MyCommandText = "Select UserId,Pass,Utype from Login where UserId='" + TempData["EMAIL"] + "' and Pass='" + TempData["Pass"] + "' and Utype='" + TempData["Utype"] + "' ";
                        DataTable dt = dm.ReadBulkRecord();
                        if (dt.Rows.Count > 0)
                        {

                            Session["Uid"] = TempData["EMAIL"];
                            if (TempData["Utype"].ToString().ToUpper() == "ADMIN")
                            {
                                TempData["adm"] = TempData["Pass"];
                                return RedirectToAction("ChangePass", "Admin");
                            }
                            else
                            {
                                TempData["tdm"] = TempData["Pass"];
                                return RedirectToAction("Change_Password", "Tutor");
                            }
                        }
                        else
                        {
                            ViewBag.msg = "! Incorrect Username Or Password";
                        }
                    }
                    catch
                    {
                        ViewBag.msg = "! Incorrect Username Or Password";
                    }
                }
               
            
            return View();
        }
        [HttpGet]
        public ActionResult StudyMaterial()
        {
            return View();
        }
    }
}
