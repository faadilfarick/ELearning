using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;
using ELearning.DAL;
using System.Data.SqlClient;
using System.IO;

namespace ELearning.Models
{
    public class VideosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string userID = System.Web.HttpContext.Current.User.Identity.GetUserId();

        // GET: Videos
        public ActionResult Index()
        {
            var data = db.Videos.Where(v => v.Course.ApplicationUser.Id == userID).ToList();
            return View(data);

        }

        // GET: Videos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Videos videos = db.Videos.Find(id);
            if (videos == null)
            {
                return HttpNotFound();
            }
            return View(videos);
        }

        // GET: Videos/Create       

        public ActionResult Create()
        {
            //List<SelectListItem> list = new List<SelectListItem>();

            //foreach (var course in db.Courses.ToList())
            //    list.Add(new SelectListItem() { Value = course.ID.ToString(), Text = course.Name });
            //var userID = System.Web.HttpContext.Current.User.Identity.GetUserId();

            ViewBag.CourseInfo = new SelectList(db.Courses.Where(c => c.ApplicationUser.Id == userID).ToList(), "ID", "Name");
            return View();
        }


        // POST: Videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Videos videos, HttpPostedFileBase upload)//[Bind(Include = "ID,Name,Discription,Course.ID")]
        {
            if (!ModelState.IsValid)
            {

                var path = "";
                if (upload == null)
                {
                    ViewBag.CourseInfo = new SelectList(db.Courses.Where(c => c.ApplicationUser.Id == userID).ToList(), "ID", "Name");
                    ViewBag.vidError = "Video Requrired...";
                    return View(videos);
                }
                else if (upload.ContentLength > 0)
                {
                    var fileName = videos.Course.ID + " " + videos.Name; 
                    path = Path.Combine(Server.MapPath("~/Content/Videos"));
                    upload.SaveAs(path+"\\"+fileName);
                }
                
                
                string query = "AddVideos '" + videos.Name + "','" + videos.Discription + "','" + videos.Course.ID + "','"+path+"'";
                bool res = new SystemDAL().executeNonQuerys(query);
                return RedirectToAction("Index");
            }
            ViewBag.CourseInfo = new SelectList(db.Courses.Where(c => c.ApplicationUser.Id == userID).ToList(), "ID", "Name");
            return View(videos);
        }

        // GET: Videos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Videos videos = db.Videos.Find(id);
            if (videos == null)
            {
                return HttpNotFound();
            }

            ViewBag.CourseInfo = new SelectList(db.Courses.Where(c => c.ApplicationUser.Id == userID).ToList(), "ID", "Name");

            return View(videos);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Videos videos)
        {
            if (!ModelState.IsValid)
            {
                //UpdateVideos
                string query = "UpdateVideos '" + videos.ID + "','" + videos.Name + "'," +
                    "'" + videos.Discription + "','" + videos.Course.ID + "'";
                bool res = new SystemDAL().executeNonQuerys(query);

                return RedirectToAction("Index");
            }
            ViewBag.CourseInfo = new SelectList(db.Courses.Where(c => c.ApplicationUser.Id == userID).ToList(), "ID", "Name");

            return View(videos);
        }

        // GET: Videos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Videos videos = db.Videos.Find(id);
            if (videos == null)
            {
                return HttpNotFound();
            }
            return View(videos);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Videos videos = db.Videos.Find(id);
            db.Videos.Remove(videos);
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
