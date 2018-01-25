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
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class BatchController : Controller
    {
        private CTTIDBEntities db = new CTTIDBEntities();

        // Called everytime when Action is Executed
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Check Session
            if (Session[SessionRole.Admin.ToString()] != null)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                // Redirect to Login page if session is null
                filterContext.Result = new RedirectResult("~/User/Login");
            }
        }

        // GET: /Batch/
        public ActionResult Index()
        {
            if (Session[SessionRole.Admin.ToString()] != null)
            {
                var batches = db.Batches.Include(b => b.User);
                return View(batches.ToList());
            }
            return RedirectToAction("Login", "User");
        }

        // GET: /Batch/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        // GET: /Batch/Create
        public ActionResult Create()
        {
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName");
            return View();
        }

        // POST: /Batch/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,StartDt,EndDt,Status,CreatedOn,ModifiedOn,uID")] Batch batch)
        {
            if (ModelState.IsValid)
            {
                if (Session[SessionRole.Admin.ToString()] != null)
                {
                    batch.uID = int.Parse(Session[SessionRole.Admin.ToString()].ToString());
                    db.Batches.Add(batch);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", batch.uID);
            return View(batch);
        }

        // GET: /Batch/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", batch.uID);
            return View(batch);
        }

        // POST: /Batch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,StartDt,EndDt,Status,CreatedOn,ModifiedOn,uID")] Batch batch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(batch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", batch.uID);
            return View(batch);
        }

        // GET: /Batch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        // POST: /Batch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Batch batch = db.Batches.Find(id);
            db.Batches.Remove(batch);
            db.SaveChanges();
            return RedirectToAction("Index");
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
