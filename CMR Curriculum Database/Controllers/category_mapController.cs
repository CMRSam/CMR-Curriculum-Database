﻿using System;
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
    public class category_mapController : Controller
    {
        private curriculumEntities db = new curriculumEntities();

        // GET: category_map
        public ActionResult Index()
        {
            var category_map = db.category_map.Include(c => c.category).Include(c => c.content);
            return View(category_map.ToList());
        }

        // GET: category_map/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category_map category_map = db.category_map.Find(id);
            if (category_map == null)
            {
                return HttpNotFound();
            }
            return View(category_map);
        }

        // GET: category_map/Create
        public ActionResult Create(int? id)
        {
            
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            content c = db.content.Find(id);
            category cat = db.categories.Find(id);

            if (c == null)
            {
                ViewBag.ContentID = new SelectList(db.content, "ContentID", "Module_Name___CURRENT");
            }
            else
            {
                ViewBag.ContentID = new SelectList(db.content, "ContentID", "Module_Name___CURRENT", c.ContentID);
            }
            if (cat == null)
            {
                ViewBag.CategoryID = new SelectList(db.categories.OrderBy(item => item.Category1), "CategoryID", "Category1");
            }
            else
            {
                ViewBag.CategoryID = new SelectList(db.categories.OrderBy(item => item.Category1), "CategoryID", "Category1", cat.CategoryID);
            }
            return View();
        }

        // POST: category_map/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ContentID,CategoryID")] category_map category_map, int? id)
        {
            if (ModelState.IsValid)
            {
                db.category_map.Add(category_map);
                db.SaveChanges();
                return RedirectToAction("../content/Details/"+id);
            }
            
            ViewBag.Category_ID = new MultiSelectList(db.categories, "CategoryID", "Category1");
            ViewBag.ContentID = new MultiSelectList(db.content, "ContentID", "Module_Name___CURRENT");
            return View(category_map);
        }

        // GET: category_map/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category_map category_map = db.category_map.Find(id);
            if (category_map == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_ID = new SelectList(db.categories, "CategoryID", "Category1", category_map.CategoryID);
            ViewBag.ContentID = new SelectList(db.content, "ContentID", "Module_Name___CURRENT", category_map.ContentID);
            return View(category_map);
        }

        // POST: category_map/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ContentID,Category_ID")] category_map category_map)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category_map).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_ID = new SelectList(db.categories, "CategoryID", "Category1", category_map.CategoryID);
            ViewBag.ContentID = new SelectList(db.content, "ContentID", "Module_Name___CURRENT", category_map.ContentID);
            return View(category_map);
        }

        // GET: category_map/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category_map category_map = db.category_map.Find(id);
            if (category_map == null)
            {
                return HttpNotFound();
            }
            return View(category_map);
        }

        // POST: category_map/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? contentID)
        {
            category_map category_map = db.category_map.Find(id);
            db.category_map.Remove(category_map);
            db.SaveChanges();
            return RedirectToAction("../content/Details/"+contentID);
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
