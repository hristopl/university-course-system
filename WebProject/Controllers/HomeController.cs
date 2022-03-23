using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.DataContext;
using WebProject.Entities;

namespace WebProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoginStudent()
        {
            if (Session["loggedUser"] != null)
                return RedirectToAction("Index", "Home");


            return View();
        }

        [HttpPost]
        public ActionResult LoginStudent(string username, string password)
        {
            if (Session["loggedUser"] != null)
                return RedirectToAction("Index", "Home");

            ViewData["username"] = username;
            ViewData["password"] = password;

            bool isValid = true;

            if (username == "")
            {
                isValid = false;
                ViewData["usernameError"] = "This field is required";
            }

            if (password == "")
            {
                isValid = false;
                ViewData["passwordError"] = "This field is required";
            }

            if (isValid == false)
                return View();

            MyDbContext context = new MyDbContext();

            var student = context.Students
                       .Where(s => s.StudentUsername == username)
                       .Where(s => s.StudentPassword == password)
                       .FirstOrDefault<Student>();

            Session["loggedUser"] = student;

            if (Session["loggedUser"] == null)
            {
                isValid = false;
                ViewData["authError"] = "Authentication failed!";
            }

            if (isValid == false)
                return View();


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult LoginTeacher()
        {
            if (Session["loggedUser"] != null)
                return RedirectToAction("Index", "Home");


            return View();
        }

        [HttpPost]
        public ActionResult LoginTeacher(string username, string password)
        {
            if (Session["loggedUser"] != null)
                return RedirectToAction("Index", "Home");

            ViewData["username"] = username;
            ViewData["password"] = password;

            bool isValid = true;

            if (username == "")
            {
                isValid = false;
                ViewData["usernameError"] = "This field is required";
            }

            if (password == "")
            {
                isValid = false;
                ViewData["passwordError"] = "This field is required";
            }

            if (isValid == false)
                return View();

            MyDbContext context = new MyDbContext();

            var teacher = context.Teachers
                       .Where(s => s.TeacherUsername == username)
                       .Where(s => s.TeacherPassword == password)
                       .FirstOrDefault<Teacher>();

            Session["loggedUser"] = teacher;

            if (Session["loggedUser"] == null)
            {
                isValid = false;
                ViewData["authError"] = "Authentication failed!";
            }

            if (isValid == false)
                return View();


            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            Session["loggedUser"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}