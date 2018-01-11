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
    public class company_listController : Controller
    {
        private curriculumEntities db = new curriculumEntities();

        public List<CheckBoxListItem> companies { get; set; }

        // GET: company_list
        public ActionResult Index(string searchString)
        {
            var company = from m in db.company_list select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                company = company.Where(m => m.Company_Name.Contains(searchString) ||
                                             m.Program.Contains(searchString) ||
                                             m.Delivery_Method.Contains(searchString) ||
                                             m.Notes.Contains(searchString));
            }
            return View(company.ToList());
        }

        // GET: company_list/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            company_list company_list = db.company_list.Find(id);
            if (company_list == null)
            {
                return HttpNotFound();
            }
            return View(company_list);
        }

        // GET: company_list/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: company_list/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,Company_Name,Delivery_Method,Program,Notes")] company_list company_list)
        {
            if (ModelState.IsValid)
            {
                db.company_list.Add(company_list);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            companies = new List<CheckBoxListItem>();
            
            return View(company_list);
        }

        // GET: company_list/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            company_list company_list = db.company_list.Find(id);
            if (company_list == null)
            {
                return HttpNotFound();
            }
            return View(company_list);
        }

        // POST: company_list/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,Company_Name,Delivery_Method,Program,Notes")] company_list company_list, int? id)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company_list).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details/"+id);
            }
            return View(company_list);
        }

        // GET: company_list/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            company_list company_list = db.company_list.Find(id);
            if (company_list == null)
            {
                return HttpNotFound();
            }
            return View(company_list);
        }

        // POST: company_list/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            company_list company_list = db.company_list.Find(id);
            db.company_list.Remove(company_list);
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
