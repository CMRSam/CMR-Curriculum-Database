using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMR_Curriculum_Database.Models;

namespace CMR_Curriculum_Database.Controllers
{
    public class parent_courseController : Controller
    {
        private curriculumEntities db = new curriculumEntities();

        // GET: parent_course
        public ActionResult Index(string searchString)
        {
            var pc = from m in db.parent_courses select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                pc = pc.Where(m => m.Parent_Course_Name.Contains(searchString)).OrderBy(p => p.Parent_Course_Name);
            }
            return View(pc.ToList().OrderBy(p => p.Parent_Course_Name));
        }

        // GET: parent_course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            parent_course parent_course = db.parent_courses.Find(id);
            if (parent_course == null)
            {
                return HttpNotFound();
            }
            return View(parent_course);
        }

        // GET: parent_course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: parent_course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Parent_Course_ID,Parent_Course_Name")] parent_course parent_course)
        {
            if (ModelState.IsValid)
            {
                db.parent_courses.Add(parent_course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parent_course);
        }

        // GET: parent_course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            parent_course parent_course = db.parent_courses.Find(id);
            if (parent_course == null)
            {
                return HttpNotFound();
            }
            return View(parent_course);
        }

        // POST: parent_course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Parent_Course_ID,Parent_Course_Name")] parent_course parent_course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parent_course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parent_course);
        }

        // GET: parent_course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            parent_course parent_course = db.parent_courses.Find(id);
            if (parent_course == null)
            {
                return HttpNotFound();
            }
            return View(parent_course);
        }

        // POST: parent_course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            parent_course parent_course = db.parent_courses.Find(id);
            db.parent_courses.Remove(parent_course);
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
