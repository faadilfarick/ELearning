using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELearning.Models;
using Microsoft.AspNet.Identity;
using ELearning.DAL;
using System.Data.SqlClient;

namespace ELearning.Controllers
{
  

    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private string getRoleForUserID(string id)
        {
            string role = "";
            string query = "getRoleForUserId '" + id + "'";
            SqlDataReader reader = new SystemDAL().executeQuerys(query);
            if (reader.Read())
                role = reader[0].ToString();
            return role;
        }
        // GET: Courses
        public ActionResult Index()
        {
            var userID = System.Web.HttpContext.Current.User.Identity.GetUserId();           
            string role= getRoleForUserID(userID);
            ViewBag.role = role;
            //diplaying cources according to the users 
            if(role== "STUDENT"||role=="")//if role=student or not logged in all cources are visible
            {
                List<Course> cou = db.Courses.ToList();
                return View(cou);
            }
            else//displaying cources based on owner for edit purposes
            {
                List<Course> co = db.Courses.Where(c => c.ApplicationUser.Id == userID).ToList();
                return View(co);
            }
           
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            string query = "select * from Videos where [Course_ID]='" + id + "'";
            SqlDataReader reader = new SystemDAL().executeQuerys(query);
            List<Videos> videoListForCourse = new List<Videos>();
            Videos vid = null;
            while (reader.Read())
            {
                vid = new Videos();
                vid.ID = Convert.ToInt32(reader[0]);
                vid.Name = reader[1].ToString();
                vid.Discription = reader[2].ToString();
            //    vid.Course.ID = Convert.ToInt32(reader[3]);
                vid.FilePath = reader[4].ToString();
                videoListForCourse.Add(vid);
            }
            ViewBag.videosList = videoListForCourse;
            var userID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            string role = getRoleForUserID(userID);
            ViewBag.role = role;
            //  var videoListForCourse=
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "ADMIN,INSTRUCTOR")]
        public ActionResult Create()
        {
            return View();

        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN,INSTRUCTOR")]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                var userID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                string query = "AddCourse '" + course.Name + "','" + course.Discription + "','" + userID + "'";
                bool res = new SystemDAL().executeNonQuerys(query);
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5  
        [Authorize(Roles = "ADMIN,INSTRUCTOR")]
        public ActionResult Edit(int? id)
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

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN,INSTRUCTOR")]
        public ActionResult Edit([Bind(Include = "ID,Name,Discription")] Course course)
        {

            var CurrentLoggedUser = System.Web.HttpContext.Current.User.Identity.GetUserId();
            Course c = db.Courses.Find(course.ID);
            var owner = c.ApplicationUser.Id;
            if (ModelState.IsValid)
            {
                if (CurrentLoggedUser == owner)
                {
                    //detach this entity from the DbSet
                    db.Entry(c).State = EntityState.Detached;

                    //set the state from the entity that you just received to modified 
                    db.Entry(course).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.info = "You are Not Authorized to do changes to this content";
                    return View("~/Views/Shared/NotAuthorized.cshtml");
                }

            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "ADMIN,INSTRUCTOR")]
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

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN,INSTRUCTOR")]
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
    }
}
