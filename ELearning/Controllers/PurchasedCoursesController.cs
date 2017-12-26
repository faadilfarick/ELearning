using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELearning.Models;

namespace ELearning.Controllers
{
    public class PurchasedCoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PurchasedCourses
        public ActionResult Index()
        {
            return View(db.PurchasedCourses.ToList());
        }

        // GET: PurchasedCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasedCourse purchasedCourse = db.PurchasedCourses.Find(id);
            if (purchasedCourse == null)
            {
                return HttpNotFound();
            }
            return View(purchasedCourse);
        }

        // GET: PurchasedCourses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PurchasedCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID")] PurchasedCourse purchasedCourse)
        {
            if (ModelState.IsValid)
            {
                db.PurchasedCourses.Add(purchasedCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(purchasedCourse);
        }

        // GET: PurchasedCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasedCourse purchasedCourse = db.PurchasedCourses.Find(id);
            if (purchasedCourse == null)
            {
                return HttpNotFound();
            }
            return View(purchasedCourse);
        }

        // POST: PurchasedCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID")] PurchasedCourse purchasedCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchasedCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchasedCourse);
        }

        // GET: PurchasedCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasedCourse purchasedCourse = db.PurchasedCourses.Find(id);
            if (purchasedCourse == null)
            {
                return HttpNotFound();
            }
            return View(purchasedCourse);
        }

        // POST: PurchasedCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchasedCourse purchasedCourse = db.PurchasedCourses.Find(id);
            db.PurchasedCourses.Remove(purchasedCourse);
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
