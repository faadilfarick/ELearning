using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELearning.Models;
using ELearning.DAL;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;

namespace ELearning.Controllers
{
    
    public class SubCategoriesController : Controller
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

        // GET: SubCategories
        public ActionResult Index()
        {
            //var catList = db.Categories.ToList();
            //ViewBag.cat = catList;
            //var subCatList = db.SubCategories.ToList();
            //ViewBag.subCat = subCatList;
            return View(db.SubCategories.ToList());
        }

        // GET: SubCategories/Details/5
        public ActionResult Details(int? id)
        {
            var userID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            string role = getRoleForUserID(userID);
            ViewBag.role = role;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            List<Course> cou = db.Courses.Where(c => c.SubCategory.ID == id).ToList();//getting courses for the category
            ViewBag.cou = cou;
            return View(subCategory);
        }

        // GET: SubCategories/Create
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create()
        {
            ViewBag.catList =new SelectList( db.Categories.ToList(), "ID", "Name");
            return View();
        }

        // POST: SubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Create(SubCategory subCategory)
        {
            if (!ModelState.IsValid)
            {
                //db.SubCategories.Add(subCategory);
                //db.SaveChanges();
                string query = "addSubCategories '" + subCategory.Name + "','" + subCategory.Discription + "','" + subCategory.Category.ID + "'";
                bool res = new SystemDAL().executeNonQuerys(query);
                if (res == true)
                {
                    return RedirectToAction("Index");
                }                
                else
                {
                    ViewBag.catList = new SelectList(db.Categories.ToList(), "ID", "Name");
                    return View(subCategory);
                }
                    
            }
            ViewBag.catList = new SelectList(db.Categories.ToList(), "ID", "Name");
            return View(subCategory);
        }

        // GET: SubCategories/Edit/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.catList = new SelectList(db.Categories.ToList(), "ID", "Name");
            return View(subCategory);
        }

        // POST: SubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Edit( SubCategory subCategory)
        {
            if (!ModelState.IsValid)
            {
                //db.Entry(subCategory).State = EntityState.Modified;
                //db.SaveChanges();
                string query = "UpdateSubCategories '"+subCategory.ID+"','" + subCategory.Name + "','" + subCategory.Discription + "','" + subCategory.Category.ID + "'";
                bool res = new SystemDAL().executeNonQuerys(query);
                if(res==true)
                    return RedirectToAction("Index");
            }
            ViewBag.catList = new SelectList(db.Categories.ToList(), "ID", "Name");
            return View(subCategory);
        }

        // GET: SubCategories/Delete/5
        [Authorize(Roles = "ADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // POST: SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public ActionResult DeleteConfirmed(int id)
        {
            SubCategory subCategory = db.SubCategories.Find(id);
            db.SubCategories.Remove(subCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public List<Category> Passcategory()
        {
            var catList = db.Categories.ToList();
            
           return catList;
           

        }
        public  List<SubCategory> PassSubCategory()
        {
            var subCatList = db.SubCategories.ToList();
            return subCatList;
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
