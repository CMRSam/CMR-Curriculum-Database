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
    public class contentController : Controller
    {
        private curriculumEntities db = new curriculumEntities();

        // GET: contents
        public ActionResult Index(string searchString)
        {
            var content = from m in db.content select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                content = content.Where(m => m.Module_Name___CURRENT.Contains(searchString) ||
                                             m.Objectives.Contains(searchString) ||
                                             m.Industry_Tagging___FOR_WEBSITE.Contains(searchString) ||
                                             m.Introduction.Contains(searchString) ||
                                             m.Audio_Talent_used.Contains(searchString) ||
                                             m.Resources_Type.Contains(searchString));
            }
            return View(content.ToList());
        }

        // GET: contents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            content content = db.content.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // GET: contents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: contents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContentID,Parent_Course_Name__if_applicable_,Module_Name___CURRENT,Module_Name___PREVIOUS,Introduction,Objectives,Audio_Talent_used,ACTIVE_ON_WEBSITE,Bonnie_s_Notes,Allowed_for_ASM_,MAIE_Modules,Main_Category_1,Subcategory,Main_Category_2,Subcategory_of_2,Main_Category_3,Sub_of_3,Search_Tagging,Industry_Tagging___FOR_WEBSITE,Resources_Type,Delivered_In___Storyline__Rise__PDF__MP3_,Resource_Duration,Level__Foundational__Intermediate__Advanced_,Role___Rep,Role___DM,Role___Account_Manager,Training_Planner,Last_Revision_Date,Related_Content__Podcasts__Coaching_Guides__etc__,Passing_Score_changed_to_80_,Writer,SME,ACPE_Module")] content content)
        {
            if (ModelState.IsValid)
            {
                db.content.Add(content);
                db.SaveChanges();
                return RedirectToAction("../parent_course_map/Create");
            }

            return View(content);
        }

        // GET: contents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            content content = db.content.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // POST: contents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContentID,Parent_Course_Name__if_applicable_,Module_Name___CURRENT,Module_Name___PREVIOUS,Introduction,Objectives,Audio_Talent_used,ACTIVE_ON_WEBSITE,Bonnie_s_Notes,Allowed_for_ASM_,MAIE_Modules,Main_Category_1,Subcategory,Main_Category_2,Subcategory_of_2,Main_Category_3,Sub_of_3,Search_Tagging,Industry_Tagging___FOR_WEBSITE,Resources_Type,Delivered_In___Storyline__Rise__PDF__MP3_,Resource_Duration,Level__Foundational__Intermediate__Advanced_,Role___Rep,Role___DM,Role___Account_Manager,Training_Planner,Last_Revision_Date,Related_Content__Podcasts__Coaching_Guides__etc__,Passing_Score_changed_to_80_,Writer,SME,ACPE_Module")] content content)
        {
            if (ModelState.IsValid)
            {
                db.Entry(content).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../content/Index");
            }
            return View(content);
        }

        // GET: contents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            content content = db.content.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // POST: contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            content content = db.content.Find(id);
            
            db.content.Remove(content);

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
