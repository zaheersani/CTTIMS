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
    public class AttendanceController : Controller
    {
        private CTTIDBEntities db = new CTTIDBEntities();

        private IEnumerable<Enrollment> GetEnrollments()
        {
            // Select a course for adding marks
            var r = db.InstructorCourses.Where(x => x.InstructorID == 9
                                       && x.BatchID == 1 && x.SectionID == 1 && x.CourseID == 3);

            InstructorCours instCourse = null;
            if (r.Count() > 0)
            {
                instCourse = r.ToList()[0];
                //instCourse.ClassRoom
            }
            // proceed only if a course is assigned to instructor
            if (instCourse != null)
            {
                // Get enrollments for filtered batch, section and course
                var enr = db.Enrollments.Where(x => x.InstructorCoursesID == instCourse.ID);

                //if (enr.Count() > 0)
                //{
                //    assessments = db.Assessments.Where(x => enr.Any(eid => eid.ID == x.EnrollmentID));
                //    return assessments.ToList();
                //}
                return enr.ToList();
            }
            return null;
        }

        // GET: /Attendance/
        public ActionResult Index()
        {
            var attendences = db.Attendences.Include(a => a.Enrollment).Include(a => a.Instructor).Include(a => a.User);
            return View(attendences.ToList());
        }

        // GET: /Attendance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // GET: /Attendance/Create
        public ActionResult Create()
        {
            IEnumerable<Enrollment> enrollments = this.GetEnrollments();
            if (enrollments != null)
            {
                ViewBag.Enrollments = enrollments.ToList();
                //return View(enrollments.ToList());
                List<Attendence> aList = new List<Attendence>();
                foreach (var item in enrollments)
                {
                    aList.Add(new Attendence()
                    {
                        EnrollmentID = item.ID
                    });
                }
                return View(aList);

            }
            return View();
        }

        // POST: /Attendance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IEnumerable<Attendence> attendenceList)
        {
            if (ModelState.IsValid)
            {
                db.Attendences.AddRange(attendenceList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: /Attendance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnrollmentID = new SelectList(db.Enrollments, "ID", "Status", attendence.EnrollmentID);
            ViewBag.uID = new SelectList(db.Instructors, "ID", "FirstName", attendence.uID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", attendence.uID);
            return View(attendence);
        }

        // POST: /Attendance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,EnrollmentID,uID,ClassRoom,Status,EntryDate,FromTime,ToTime,ModifiedOn,CreatedOn,TopicsCovered")] Attendence attendence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnrollmentID = new SelectList(db.Enrollments, "ID", "Status", attendence.EnrollmentID);
            ViewBag.uID = new SelectList(db.Instructors, "ID", "FirstName", attendence.uID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", attendence.uID);
            return View(attendence);
        }

        // GET: /Attendance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // POST: /Attendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendence attendence = db.Attendences.Find(id);
            db.Attendences.Remove(attendence);
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
