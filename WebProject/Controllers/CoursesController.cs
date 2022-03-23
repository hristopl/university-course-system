using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProject.DataContext;
using WebProject.Entities;

namespace WebProject.Controllers
{
    public class CoursesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        public ActionResult Index()
        {
            if (Session["loggedUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["loggedUser"] is Teacher)
            {
                Teacher loggedTeacher = (Teacher)Session["loggedUser"];
                int loggedTeacherId = loggedTeacher.TeacherId;
                var teacherCourses = db.Courses.Where(c => c.TeacherId == loggedTeacherId).Include(c => c.Teacher);

                return View(teacherCourses.ToList());
            }
            else if (Session["loggedUser"] is Student)
            {
                Student student = db.Students.Find(((Student)Session["loggedUser"]).StudentId);
                List<Course> allCourses = db.Courses.Include(c => c.Teacher).ToList();
                List<Course> signedCourses = student.Courses.ToList();
                return View(allCourses.Except(signedCourses));
            }
            return View();
        }

        public ActionResult StudentCourses()
        {
            if (Session["loggedUser"] == null || !(Session["loggedUser"] is Student)) {
                return RedirectToAction("Index", "Home");
            }

            Student student = db.Students.Find(((Student)Session["loggedUser"]).StudentId);

            return View("StudentCourses", student.Courses.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        public ActionResult Create()
        {
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherFirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseName,TeacherId")] Course course)
        {
            if(Session["loggedUser"] == null || !(Session["loggedUser"] is Teacher))
            {
                return RedirectToAction("Index", "Home");
            }

            course.TeacherId = ((Teacher)Session["loggedUser"]).TeacherId;

            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if(((Teacher)Session["loggedUser"]).TeacherId != course.TeacherId)
            {
                return RedirectToAction("Index", "Home");
            }
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherFirstName", course.TeacherId);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,CourseName,TeacherId")] Course course)
        {
            course.TeacherId = ((Teacher)Session["loggedUser"]).TeacherId;
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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

        public ActionResult Sign(int id)
        {
            if(Session["loggedUser"] == null || !(Session["loggedUser"] is Student))
            {
                return RedirectToAction("Index", "Home");
            }

            Course course = db.Courses.Find(id);
            Student loggedStudent = db.Students.Find(((Student)Session["loggedUser"]).StudentId);
            loggedStudent.Courses.Add(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UnSign(int id)
        {
            if (Session["loggedUser"] == null || !(Session["loggedUser"] is Student))
            {
                return RedirectToAction("Index", "Home");
            }

            Course course = db.Courses.Find(id);
            Student loggedStudent = db.Students.Find(((Student)Session["loggedUser"]).StudentId);
            loggedStudent.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("StudentCourses");
        }
    }
}
