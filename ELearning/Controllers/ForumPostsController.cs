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

namespace ELearning.Controllers
{
    public class ForumPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ForumPosts
        public ActionResult Index()
        {
            return View(db.ForumPosts.ToList());
        }

        // GET: ForumPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumPost forumPost = db.ForumPosts.Find(id);
            if (forumPost == null)
            {
                return HttpNotFound();
            }
            var replies = db.ForumPostReplies.Where(r => r.ForumPost.ID == id);
            ViewBag.rep = replies;
           
            return View(forumPost);
        }

        // GET: ForumPosts/Create
        public ActionResult Create(int? id)
        {
            var forum = new SelectList(db.Forum.ToList(), "ID", "CourseTitle");
            ViewBag.Forum = forum;
            return View();
        }

        // POST: ForumPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ForumPost forumPost)
        {
            string appuser = System.Web.HttpContext.Current.User.Identity.GetUserId();

            string query = "addNewForumPost '" + forumPost.Question + "','" + forumPost.Discription + "','" + appuser + "','" + forumPost.Forum.ID + "'";
            bool res = new SystemDAL().executeNonQuerys(query);
            if (res == true)
            {
                return RedirectToAction("Index");
            }

            //db.ForumPosts.Add(forumPost);
            //db.SaveChanges();
            //return RedirectToAction("Index");


            return View(forumPost);
        }

        // GET: ForumPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumPost forumPost = db.ForumPosts.Find(id);
            if (forumPost == null)
            {
                return HttpNotFound();
            }
            return View(forumPost);
        }

        // POST: ForumPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Question,Discription")] ForumPost forumPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forumPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(forumPost);
        }

        // GET: ForumPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumPost forumPost = db.ForumPosts.Find(id);
            if (forumPost == null)
            {
                return HttpNotFound();
            }
            return View(forumPost);
        }

        // POST: ForumPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ForumPost forumPost = db.ForumPosts.Find(id);
            db.ForumPosts.Remove(forumPost);
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
