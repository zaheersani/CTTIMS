using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DBFirstSession.Models;

namespace DBFirstSession.Controllers
{
    public class InstructorController : Controller
    {
        private CTTIDBEntities db = new CTTIDBEntities();

        // TODO: Remove uid when inserting/editing record
        // GET: /Instructor/
        public ActionResult Index()
        {
            var instructors = db.Instructors.Include(i => i.Department).Include(i => i.User);
            return View(instructors.ToList());
        }

        // GET: /Instructor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // GET: /Instructor/Create
        public ActionResult Create()
        {
            ViewBag.DeptID = new SelectList(db.Departments, "ID", "Name");
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName");
            return View();
        }

        // POST: /Instructor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,uID,FirstName,LastName,FatherName,CNIC,Gender,Designation,DeptID,Email,DeptPosition,MobileNo,PhoneNo,PresentAddress,PermanentAddress,PresentCity,PermanentCity,ExperienceYear,ExperienceMonth,JoiningDate,ResignationDate,Photo,Status,CreatedOn,ModifiedOn")] Instructor instructor, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    instructor.Photo = new byte[image.ContentLength];
                    image.InputStream.Read(instructor.Photo, 0, image.ContentLength);
                }
                db.Instructors.Add(instructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeptID = new SelectList(db.Departments, "ID", "Name", instructor.DeptID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", instructor.uID);
            return View(instructor);
        }

        // GET: /Instructor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeptID = new SelectList(db.Departments, "ID", "Name", instructor.DeptID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", instructor.uID);
            return View(instructor);
        }

        // POST: /Instructor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,uID,FirstName,LastName,FatherName,CNIC,Gender,Designation,DeptID,Email,DeptPosition,MobileNo,PhoneNo,PresentAddress,PermanentAddress,PresentCity,PermanentCity,ExperienceYear,ExperienceMonth,JoiningDate,ResignationDate,Photo,Status,CreatedOn,ModifiedOn")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instructor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeptID = new SelectList(db.Departments, "ID", "Name", instructor.DeptID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", instructor.uID);
            return View(instructor);
        }

        // GET: /Instructor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: /Instructor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            db.Instructors.Remove(instructor);
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
