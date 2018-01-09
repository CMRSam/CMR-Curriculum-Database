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
    public class parent_course_mapController : Controller
    {
        private curriculumEntities db = new curriculumEntities();

        // GET: parent_course_map
        public ActionResult Index()
        {
            var parent_course_map = db.parent_course_map.Include(p => p.parent_course).Include(p => p.content);
            return View(parent_course_map.ToList());
        }

        // GET: parent_course_map/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            parent_course_map parent_course_map = db.parent_course_map.Find(id);
            if (parent_course_map == null)
            {
                return HttpNotFound();
            }
            return View(parent_course_map);
        }

        // GET: parent_course_map/Create
        public ActionResult Create(int? id)
        {
            content c = db.content.Find(id);

            ViewBag.Module_ID = new SelectList(db.content.OrderBy(item => item.Module_Name___CURRENT), "ContentID", "Module_Name___CURRENT", c.ContentID);
            ViewBag.Parent_Course_ID = new SelectList(db.parent_courses.OrderBy(item => item.Parent_Course_Name), "Parent_Course_ID", "Parent_Course_Name");
            return View();
        }

        // POST: parent_course_map/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Parent_Course_ID,Module_ID")] parent_course_map parent_course_map)
        {
            if (ModelState.IsValid)
            {
                db.parent_course_map.Add(parent_course_map);
                db.SaveChanges();
                return RedirectToAction("../category_map/Create");
            }

            ViewBag.Parent_Course_ID = new SelectList(db.parent_courses, "Parent_Course_ID", "Parent_Course_Name", parent_course_map.Parent_Course_ID);
            ViewBag.Module_ID = new SelectList(db.content, "ContentID", "Module_Name___CURRENT", parent_course_map.Module_ID);
            return View(parent_course_map);
        }

        // GET: parent_course_map/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            parent_course_map parent_course_map = db.parent_course_map.Find(id);
            if (parent_course_map == null)
            {
                return HttpNotFound();
            }

            ViewBag.Parent_Course_ID = new SelectList(db.parent_courses, "Parent_Course_ID", "Parent_Course_Name", parent_course_map.Parent_Course_ID);
            ViewBag.Module_ID = new SelectList(db.content, "ContentID", "Module_Name___CURRENT", parent_course_map.Module_ID);
            return View(parent_course_map);
        }

        // POST: parent_course_map/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Parent_Course_ID,Module_ID")] parent_course_map parent_course_map)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parent_course_map).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Parent_Course_ID = new SelectList(db.parent_courses, "Parent_Course_ID", "Parent_Course_Name", parent_course_map.Parent_Course_ID);
            ViewBag.Module_ID = new SelectList(db.content, "ContentID", "Module_Name___CURRENT", parent_course_map.Module_ID);
            return View(parent_course_map);
        }

        // GET: parent_course_map/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            parent_course_map parent_course_map = db.parent_course_map.Find(id);
            if (parent_course_map == null)
            {
                return HttpNotFound();
            }
            return View(parent_course_map);
        }

        // POST: parent_course_map/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            parent_course_map parent_course_map = db.parent_course_map.Find(id);
            db.parent_course_map.Remove(parent_course_map);
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
