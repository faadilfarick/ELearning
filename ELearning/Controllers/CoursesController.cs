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
using PagedList;
using System.IO;
using System.Configuration;
using System.Web.Script.Serialization;

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
        public ActionResult Index(int? page)
        {
            var userID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            string role = getRoleForUserID(userID);
            ViewBag.role = role;

            //diplaying cources according to the users 
            if (role == "STUDENT" || role == "")//if role=student or not logged in all cources are visible
            {
                List<Course> cou = db.Courses.ToList();
                var products = cou.OrderBy(v => v.ID); //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

                var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
                var onePageOfProducts = products.ToPagedList(pageNumber, 6); // will only contain 25 products max because of the pageSize

                ViewBag.OnePageOfProducts = onePageOfProducts;
                return View();
            }
            else//displaying cources based on owner for edit purposes
            {
                List<Course> co = db.Courses.Where(c => c.ApplicationUser.Id == userID).ToList();
                var products = co.OrderBy(v => v.ID); //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

                var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
                var onePageOfProducts = products.ToPagedList(pageNumber, 6); // will only contain 25 products max because of the pageSize

                ViewBag.OnePageOfProducts = onePageOfProducts;
                return View();
            }

        }

        [HttpPost]
        public ActionResult SubCatByCat(int categoryId)
        {
            // Filter the states by country. For example:
            var subs = (from s in db.SubCategories
                        where s.ID == s.Category.ID
                        select new
                        {
                            id = s.ID,
                            state = s.Name
                        }).ToArray();

            return Json(subs);
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id, int? page)
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


            var products = videoListForCourse.OrderBy(v => v.Name); //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = products.ToPagedList(pageNumber, 8); // will only contain 25 products max because of the pageSize

            ViewBag.OnePageOfProducts = onePageOfProducts;

            return View(course);
        }


        public ActionResult GetSubCat(int id /* drop down value */)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<SubCategory> model = db.SubCategories.Where(c => c.Category.ID == id).ToList(); // This is for example put your code to fetch record.   
            ViewBag.Subcategories = model;
           
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Courses/Create
        [Authorize(Roles = "ADMIN,INSTRUCTOR")]
        public ActionResult Create()
        {
           
            ViewBag.categoriess = db.Categories.ToList();
            ViewBag.categories = new SelectList(db.Categories.ToList(), "ID", "Name");
            return View();

        }

        //ViewBag.CourseInfo = new SelectList(db.Courses.Where(c => c.ApplicationUser.Id == userID).ToList(), "ID", "Name");

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN,INSTRUCTOR")]
        public ActionResult Create(Course course, HttpPostedFileBase upload)
        {
            var fileName = "";
            var path = "";
            if (upload == null)
            {

                ViewBag.vidError = "Image Requrired...";
                return View(course);
            }
            else if (upload.ContentLength > 0)
            {
                string extension = Path.GetExtension(upload.FileName);
                fileName = course.ID + course.Name + extension;
                path = Path.Combine(Server.MapPath("~/Content/Images"));
                upload.SaveAs(path + "\\" + fileName);
            }
            //if (ModelState.IsValid)
            //{
                
                var userID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                string query = "AddCourse '" + course.Name + "','" + course.Discription + "','" + userID + "','" + course.Price + "','" + fileName + "','" + course.MainCategory.ID + "','" + course.SubCategory.ID + "'";
                bool res = new SystemDAL().executeNonQuerys(query);
                return RedirectToAction("Index");
            //}

            //return View(course);
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
            ViewBag.categoriess = db.Categories.ToList();
            ViewBag.categories = new SelectList(db.Categories.ToList(), "ID", "Name");
            var couese=db.Courses.Find(id);
            ViewBag.subcategories = new SelectList(db.SubCategories.Where(c => c.Category.ID == couese.MainCategory.ID).ToList(), "ID", "Name");
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN,INSTRUCTOR")]
        public ActionResult Edit( Course course, HttpPostedFileBase upload)
        {

            var CurrentLoggedUser = System.Web.HttpContext.Current.User.Identity.GetUserId();
            Course c = db.Courses.Find(course.ID);
            var owner = c.ApplicationUser.Id;
            var fileName = c.Image;
            var path = "";
            //if (ModelState.IsValid)
            //{
                if (CurrentLoggedUser == owner)
                {
                    if (upload != null)
                    {
                        string extension = Path.GetExtension(upload.FileName);
                        fileName = course.ID + course.Name + extension;
                        path = Path.Combine(Server.MapPath("~/Content/Images"));
                        upload.SaveAs(path + "\\" + fileName);
                    }
                    ////detach this entity from the DbSet
                    //db.Entry(c).State = EntityState.Detached;

                    ////set the state from the entity that you just received to modified 
                    //db.Entry(course).State = EntityState.Modified;
                    //db.SaveChanges();
                    string query = "UpdateCourse '" + course.ID + "','" + course.Name + "','" + course.Discription + "','" + course.Price + "','" + fileName + "','" + course.MainCategory.ID + "','" + course.SubCategory.ID + "'"; 
                    bool res = new SystemDAL().executeNonQuerys(query);
                    if (res == true)
                        return RedirectToAction("Index");
                    else
                    {
                        ViewBag.categoriess = db.Categories.ToList();
                        ViewBag.categories = new SelectList(db.Categories.ToList(), "ID", "Name");
                        ViewBag.subcategories = new SelectList(db.SubCategories.ToList(), "ID", "Name");
                        return View(course);
                    }
                }
                else
                {
                    ViewBag.info = "You are Not Authorized to do changes to this content";
                    return View("~/Views/Shared/NotAuthorized.cshtml");
                }

            //}
            //return View(course);
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

        // SignalIR Chat
        public ActionResult Chat()
        {
            return View();
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
