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
    public class company_mapController : Controller
    {
        private curriculumEntities db = new curriculumEntities();

        // GET: company_map
        public ActionResult Index()
        {
            var company_maps = db.company_maps.Include(c => c.company_list).Include(c => c.content);
            var content = from m in db.content select m;
            /* if (!String.IsNullOrEmpty(searchString))
             {
                 company_maps = company_maps.Where(m => m.Module_Name___CURRENT.Contains(searchString) ||
                                              m.Objectives.Contains(searchString) ||
                                              m.Main_Category_1.Contains(searchString) ||
                                              m.Industry_Tagging___FOR_WEBSITE.Contains(searchString) ||
                                              m.Introduction.Contains(searchString) ||
                                              m.Audio_Talent_used.Contains(searchString) ||
                                              m.Resources_Type.Contains(searchString));
        }*/
            return View(company_maps.ToList());
        }

        // GET: company_map/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            company_map company_map = db.company_maps.Find(id);
            if (company_map == null)
            {
                return HttpNotFound();
            }
            return View(company_map);
        }

        // GET: company_map/Create
        public ActionResult Create(int? id)
        {
            ViewBag.CompanyID = new SelectList((from s in db.company_list.ToList()
                                                select new
                                                {
                                                    CompanyID = s.CompanyID,
                                                    Full = s.Company_Name + " " + s.Program
                                                }),
            "CompanyID",
            "Full",
            null);
            /*
            ViewBag.CompanyID = new SelectList((from s in db.company_list.ToList()
                                                select new
                                                {
                                                    Full = s.Company_Name + " " + s.Program
                                                }),
                                                "Full",
                                                null);
                                                */
            ViewBag.ContentID = new SelectList(db.content.OrderBy(item => item.Module_Name___CURRENT), "ContentID", "Module_Name___CURRENT");
            return View();
        }

        // POST: company_map/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyMapID,CompanyID,ContentID")] company_map company_map)
        {
            if (ModelState.IsValid)
            {
                db.company_maps.Add(company_map);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.company_list, "CompanyID", "Company_Name", "Program", company_map.CompanyID);
            ViewBag.ContentID = new SelectList(db.content, "ContentID", "Module_Name___CURRENT", company_map.ContentID);
            return View();
        }

        // GET: company_map/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            company_map company_map = db.company_maps.Find(id);
            if (company_map == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.company_list, "CompanyID", "Company_Name", company_map.CompanyID);
            ViewBag.ContentID = new SelectList(db.content, "ContentID", "Parent_Course_Name__if_applicable_", company_map.ContentID);
            return View(company_map);
        }

        // POST: company_map/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyMapID,CompanyID,ContentID")] company_map company_map)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company_map).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.company_list, "CompanyID", "Company_Name", company_map.CompanyID);
            ViewBag.ContentID = new SelectList(db.content, "ContentID", "Parent_Course_Name__if_applicable_", company_map.ContentID);
            return View(company_map);
        }

        // GET: company_map/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            company_map company_map = db.company_maps.Find(id);
            if (company_map == null)
            {
                return HttpNotFound();
            }
            return View(company_map);
        }

        // POST: company_map/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            company_map company_map = db.company_maps.Find(id);
            db.company_maps.Remove(company_map);
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
