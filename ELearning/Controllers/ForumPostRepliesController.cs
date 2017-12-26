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

namespace ELearning.Controllers
{
    public class ForumPostRepliesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ForumPostReplies
        public ActionResult Index()
        {
            return View(db.ForumPostReplies.ToList());
        }

        // GET: ForumPostReplies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumPostReply forumPostReply = db.ForumPostReplies.Find(id);
            if (forumPostReply == null)
            {
                return HttpNotFound();
            }
            return View(forumPostReply);
        }

        // GET: ForumPostReplies/Create
        public ActionResult Create(int? id)
        {
            var Ques = new SelectList(db.ForumPosts.Where(p=>p.ID==id), "ID", "Question");
            ViewBag.que = Ques;
            return View();
        }

        // POST: ForumPostReplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ForumPostReply forumPostReply)
        {
            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            string query = "addAnswers '" + forumPostReply.Answer + "','" + user + "','" + forumPostReply.ForumPost.ID + "'";
            bool res = new SystemDAL().executeNonQuerys(query);
            if (res == true)
            {
                return RedirectToAction("Index");
            }
            //db.ForumPostReplies.Add(forumPostReply);
            //db.SaveChanges();

            return View(forumPostReply);
        }

        // GET: ForumPostReplies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumPostReply forumPostReply = db.ForumPostReplies.Find(id);
            if (forumPostReply == null)
            {
                return HttpNotFound();
            }
            return View(forumPostReply);
        }

        // POST: ForumPostReplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Answer")] ForumPostReply forumPostReply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forumPostReply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(forumPostReply);
        }

        // GET: ForumPostReplies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumPostReply forumPostReply = db.ForumPostReplies.Find(id);
            if (forumPostReply == null)
            {
                return HttpNotFound();
            }
            return View(forumPostReply);
        }

        // POST: ForumPostReplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ForumPostReply forumPostReply = db.ForumPostReplies.Find(id);
            db.ForumPostReplies.Remove(forumPostReply);
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
