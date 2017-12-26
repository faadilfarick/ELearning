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
    public class RepliesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Replies
        public ActionResult Index()
        {
            return View(db.Replies.ToList());
        }

        // GET: Replies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Replies replies = db.Replies.Find(id);
            if (replies == null)
            {
                return HttpNotFound();
            }
            return View(replies);
        }

        // GET: Replies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Replies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PostID,Description")] Replies replies)
        {
            if (ModelState.IsValid)
            {
                db.Replies.Add(replies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(replies);
        }

        // GET: Replies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Replies replies = db.Replies.Find(id);
            if (replies == null)
            {
                return HttpNotFound();
            }
            return View(replies);
        }

        // POST: Replies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PostID,Description")] Replies replies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(replies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(replies);
        }

        // GET: Replies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Replies replies = db.Replies.Find(id);
            if (replies == null)
            {
                return HttpNotFound();
            }
            return View(replies);
        }

        // POST: Replies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Replies replies = db.Replies.Find(id);
            db.Replies.Remove(replies);
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
