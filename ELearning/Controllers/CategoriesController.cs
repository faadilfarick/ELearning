using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELearning.Models;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using ELearning.DAL;

namespace ELearning.Controllers
{
    public class CategoriesController : Controller
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
        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            var userID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            string role = getRoleForUserID(userID);
            ViewBag.role = role;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
           List< SubCategory> subs = new List<SubCategory>();
            SubCategory subcat = new SubCategory();
            string query = "GetSubCategoriesForListItem '" + id + "'";
            SqlDataReader reader = new SystemDAL().executeQuerys(query);
            while (reader.Read())
            {
                subcat = new SubCategory();
                subcat.ID = Convert.ToInt32(reader[0]);
                subcat.Name = reader[1].ToString();
                subcat.Discription = reader[2].ToString();
                subs.Add(subcat);
            }
            ViewBag.sub = subs;
            List<Course> cou = db.Courses.Where(c=>c.MainCategory.ID==id).ToList();//getting courses for the category
            ViewBag.cou = cou;
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Discription")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Discription")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
