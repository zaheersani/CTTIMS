using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CTTIMS_Final.Models;

namespace CTTIMS_Final.Controllers
{
    public class UserController : Controller
    {
        private CTTIDBEntities db = new CTTIDBEntities();

        // TODO: Just for testing, Remove it for final release
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: /User/
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                User obj = db.Users.Where(x => x.UserName == user.UserName && x.UPassword == user.UPassword).SingleOrDefault();
                if (obj != null)
                {
                    Session[SessionRole.Admin.ToString()] = obj.UID;
                    return RedirectToAction("Index", "Batch");
                }
                else
                {
                    ViewBag.Message = "Invalid Username or Password";
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult Edit()
        {
            if (Session[SessionRole.Admin.ToString()] != null)
            {
                int uid = int.Parse(Session[SessionRole.Admin.ToString()].ToString());
                User obj = db.Users.Where(x => x.UID == uid).SingleOrDefault();
                return View(obj);
            }
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UID,UserName,UPassword,RePassword,RePassword2")] User user)
        {
            if (ModelState.IsValid)
            {
                // Update password if Current Password matches with the database
                User obj = db.Users.Where(x => x.UID == user.UID &&
                    x.UPassword == user.UPassword).SingleOrDefault();
                if (obj != null)
                {
                    // Do not update database if Current and New Password are same
                    if (user.UPassword != user.RePassword)
                    {
                        obj.UPassword = user.RePassword;
                        db.SaveChanges();
                    }
                    ViewBag.Success = "Password Updated Successfully!";   
                    return View(user);
                }
                ViewBag.Error = "Current Password is Incorrect!";
            }

            return View(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
