using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demoproject.Models;
using System.Data;

namespace demoproject.Controllers
{
    public class TutorController : Controller
    {
        //
        // GET: /Tutor/
        DBManager dm = new DBManager();
        [NonAction]
        public bool TUTORSESSION()
        {
            bool status = false;
            try
            {
                int len = Session["Uid"].ToString().Length;
                if (len > 0)
                {
                    dm.MyCommandText = "Select Name,Photo,Email from Tutor_Registration where Email='" + Session["Uid"] + "'";
                    DataTable dt = dm.ReadBulkRecord();
                    if (dt.Rows.Count > 0)
                    {
                        ViewBag.Name = dt.Rows[0][0].ToString();
                        ViewBag.Pic = dt.Rows[0][1].ToString();
                        TempData["Email"] = dt.Rows[0][2].ToString();
                    }
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
        [HttpGet]
        public ActionResult Dashboard()
        {

            if (TUTORSESSION() == false)
                return RedirectToAction("Login","Home");
            return View();
        }

        [HttpGet]
        public ActionResult My_Tutions()
        {
            if (TUTORSESSION() == false)
                return RedirectToAction("Login", "Home");
            return View();
        }

        [HttpGet]
        public ActionResult Tution_Request()
        {
            if (TUTORSESSION() == false)
                return RedirectToAction("Login", "Home");
            return View();
        }
        [HttpGet]
        public ActionResult AcceptRequest(string n)
        {
            if (TUTORSESSION() == false)
                return RedirectToAction("Login", "Home");
            try
            {
                dm.MyCommandText = "Update TRequest set Finelize='False' where RequestId='" + n + "'";
                bool status = dm.ExecuteInsertDeleteOrUpdate();
                if (status == true)
                {
                    TempData["msg"] = "Request Accepted..";
                }
                else
                {
                    TempData["msg"] = "Unable To Accept Request";
                }
                return RedirectToAction("Tution_Request", "Tutor");
            }
            catch
            {
                TempData["msg"] = "Unable To Accept Request";
            }
            return RedirectToAction("Tution_Request","Tutor");
        }

        [HttpGet]
        public ActionResult TProfile()
        {
            if (TUTORSESSION() == false)
                return RedirectToAction("Login", "Home");
            return View();
        }

        [HttpGet]
        public ActionResult Change_Password()
        {
            if (TUTORSESSION() == false)
                return RedirectToAction("Login", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult Change_Password(FormCollection ch)
        {
            if (TUTORSESSION() == false)
                return RedirectToAction("Login", "Home");
            try
            {
                if (ch[1].Equals(ch[2]))
                {
                    Cryptography cr = new Cryptography();
                    string pass = cr.EncryptMyData(ch[2]);
                    string oldpass = cr.EncryptMyData(ch[0]);
                    dm.MyCommandText = "Update Login set Pass='" + pass + "' where Pass ='" + oldpass + "' and Utype = 'Tutor'";
                    bool status = dm.ExecuteInsertDeleteOrUpdate();
                    if (status == true)
                    {
                        TempData["msg"] = "Your Password Changed Successfully";
                    }
                    else
                    {
                        TempData["msg"] = "Unable To Change Your Password !";
                    }
                }
                else
                {
                    TempData["msg"] = "Due to Some Problem Unable To Change Your Password !";
                }
            }
            catch
            {
                TempData["msg"] = "Due to Some Problem Unable To Change Your Password !";
            }

            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            if (TUTORSESSION() == false)
                return RedirectToAction("Login", "Home");

            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login","Home");
        }
     
    }
}
