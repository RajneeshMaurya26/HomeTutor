using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using demoproject.Models;

namespace demoproject.Controllers
{
    public class AdminController : Controller
    {
        DBManager dm = new DBManager();
        [NonAction]
        public bool ADMINSESSION()
        {
            bool status = false;
            try
            {
                int len = Session["Uid"].ToString().Length;
                if (len > 0)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch
            {
                status = false;
            }

            return status;
        }
        public ActionResult Dashboard()
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login","Home");
            return View();
        }
        [HttpGet]
        public ActionResult Enquiry()
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");
            return View();
        }
        [HttpGet]
        public ActionResult DelEnquiry(string n)
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");
            try
            {
                dm.MyCommandText = "Delete from Enquiry where Id='" + n + "'";
                bool status = dm.ExecuteInsertDeleteOrUpdate();
                if (status == true)
                {
                    TempData["msg"] = "Enquiry Deleted";
                }
                else
                {
                    TempData["msg"] = " Unable to Deleted Enquiry ";
                }
            }
            catch
            {
                TempData["msg"] = "Due To Some Technical Problem Unable to Deleted Enquiry ";
            }
            return RedirectToAction("Enquiry","Admin");
        }
        [HttpGet]
        public ActionResult Feedback()
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");
            return View();
        }

        [HttpGet]
        public ActionResult TutionRequest()
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");
            return View();
        }

        [HttpGet]
        public ActionResult ManageTutorProfile()
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");
            return View();
        }

        [HttpGet]
        public ActionResult DelTProfile(string p)
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");
            try
            {
                dm.MyCommandText = "Delete from Tutor_Registration where Email='" + p + "'";
                bool status = dm.ExecuteInsertDeleteOrUpdate();
                if (status == true)
                {
                    TempData["Msg"] = "Tutor Profile Deleted Successfully";
                }
                else
                {
                    TempData["Msg"] = "Unable to Delete Profile";
                }
            }
            catch
            {
                TempData["Msg"] = "Due To Some Technical Problem Unable to Delete Tutor Profile";
            }
            return RedirectToAction("ManageTutorProfile","Admin");
        }

        [HttpGet]
        public ActionResult ChangePass()
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult ChangePass(FormCollection ch)
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");
            if (ch[1].Equals(ch[2]))
            {
                try
                {
                    Cryptography cr = new Cryptography();
                    string pass = cr.EncryptMyData(ch[2]);
                    dm.MyCommandText = "Update Login set Pass='" + ch[2] + "' where UserId='" + Session[0].ToString() + "' and Utype ='Admin'";
                    bool status = dm.ExecuteInsertDeleteOrUpdate();
                    if (status == true)
                    {
                        TempData["Msg"] = "Password Change Successfully";
                    }
                    else
                    {
                        TempData["Msg"] = "Unable To Change Password";
                    }
                }
                catch
                {
                    TempData["Msg"] = "Please Confirm Your Password";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");

            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult FinalizeRequest(string n)
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");
            try
            {
                dm.MyCommandText = "Update TRequest set Finelize='true' where RequestId='" + n + "'";
                bool status = dm.ExecuteInsertDeleteOrUpdate();
                if (status == true)
                {
                    TempData["msg"] = "Request Finalized Successfully";
                }
                else
                {
                    TempData["msg"] = "Sorry! Unable Finalize The Request";
                }

                return RedirectToAction("TutionRequest", "Admin");
            }
            catch
            {
                TempData["msg"] = "Sorry! Due Some Problem Unable Finalize The Request";
            }
            return RedirectToAction("TutionRequest", "Admin");
        }

        [HttpGet]
        public ActionResult MNotification()
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult MNotification(string NMessage)
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");
            try
            {
                dm.MyCommandText = "Insert into Notification values('" + NMessage + "','" + DateTime.Now.ToString("dd/MM/yy hh:mm:ss tt") + "')";
                bool status = dm.ExecuteInsertDeleteOrUpdate();
                if (status == true)
                    ViewBag.Msg = "Notication Send Successfully";
                else
                    ViewBag.Msg = "Unable to Send Notification";
            }
            catch
            {
                ViewBag.Msg = "Unable to Send Notification";
            }
            return View();
        }
        [HttpGet]
        public ActionResult DelMNotification(string p)
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");
            try
            {
                dm.MyCommandText = "Delete from Notification where Id='"+p+"'";
                bool status = dm.ExecuteInsertDeleteOrUpdate();
                if (status == true)
                    ViewBag.Msg = "Notication Deleted Successfully";
                else
                    ViewBag.Msg = "Unable to Delete Notification";
            }
            catch
            {
                ViewBag.Msg = "Unable to Delete Notification";
            }
            return View();
        }
        [HttpGet]
        public ActionResult SendSMS()
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");


            return View();
        }

        [HttpPost]
        public ActionResult SendSMS(string Mob_Number, string Message)
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");
            try
            {
                MySMS sms = new MySMS();
                bool status = sms.SendMySMS(Mob_Number, Message);
                if (status == true)
                {
                    ViewBag.msg = "Message Send Successfully";
                }
                else
                {
                    ViewBag.msg = "Unable To Send Message";
                }
            }
            catch
            {
                ViewBag.msg = "Unable To Send Message";
            }

            return View();
        }
        [HttpGet]
        public ActionResult StudyMaterial()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudyMaterial(string Name,string Description,string file)
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");
            try
            {
                HttpPostedFileBase myfile = Request.Files["file"];
                if (myfile != null)
                {
                    string fpath = Server.MapPath("/Content/StudyMaterial");
                    if (!Directory.Exists(fpath))
                        Directory.CreateDirectory(fpath);
                    string fileName = Path.GetRandomFileName() + myfile.FileName;
                    myfile.SaveAs(fpath + "/" + fileName);
                    dm.MyCommandText = "Insert into StudyMaterial values('" + Name + "','" + fileName + "','" + fpath + "','" + Description + "','" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + "')";
                    bool status = dm.ExecuteInsertDeleteOrUpdate();
                    if (status == true)
                        ViewBag.msg = "Study Material Uploaded Successfully.";
                    else
                        ViewBag.msg = "Unable To  Uploaded .";
                }
                else
                    ViewBag.msg = "Please Select a file.";
            }
            catch
            {
                ViewBag.msg = "Unable To Uploaded .";
            }

            return View();
        }
        [HttpGet]
        public ActionResult DelStudyMaterial(string n)
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");
            try
            {
                dm.MyCommandText = "Delete from StudyMaterial where Id='" + n + "'";
                bool status = dm.ExecuteInsertDeleteOrUpdate();
                if (status == true)
                    ViewBag.Msg = "Deleted Successfully.";
                else
                    ViewBag.Msg = "Unable to Delete.";
            }
            catch
            {
                ViewBag.Msg = "Unable to Delete.";
            }
            return RedirectToAction("StudyMaterial","Admin");
        }
        [HttpGet]
        public ActionResult CurrentTution(string n)
        {
            if (ADMINSESSION() == false)
                return RedirectToAction("Login", "Home");
            return View();
        }

    }
}
