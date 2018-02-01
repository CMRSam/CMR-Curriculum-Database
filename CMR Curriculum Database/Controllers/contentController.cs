using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMR_Curriculum_Database.Models;
using System.Text;
using System.Reflection;
using System.IO;

namespace CMR_Curriculum_Database.Controllers
{
    public class contentController : Controller
    {
        private curriculumEntities db = new curriculumEntities();

        // GET: contents
        public ActionResult Index(string searchString)
        {
            char delim = ' ';
            var content = from m in db.content
                          join pcm in db.parent_course_map on m.ContentID equals pcm.Module_ID
                          join pc in db.parent_courses on pcm.Parent_Course_ID equals pc.Parent_Course_ID
                          orderby pc.Parent_Course_Name
                          select m;
            var parentCourse = from p in db.parent_courses select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                string[] searchStringArray = searchString.Split(delim);
                foreach (var s in searchStringArray)
                {
                    content = content.Where(m => m.Module_Name___CURRENT.Contains(s) ||
                                             m.Objectives.Contains(s) ||
                                             m.Industry_Tagging___FOR_WEBSITE.Contains(s) ||
                                             m.Introduction.Contains(s) ||
                                             m.Audio_Talent_used.Contains(s) ||
                                             m.Resources_Type.Contains(s));
                }
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
            List<String> types = new List<String>();
            string[] typeArray = { "eModule", "Whitepaper", "Microminutes", "Job aid"};
            foreach(var type in typeArray)
            {
                types.Add(type);
            }
            ViewBag.ListOfTypes = types;
            return View();
        }

        // POST: contents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContentID,Parent_Course_Name__if_applicable_,Module_Name___CURRENT,Module_Name___PREVIOUS,Introduction,Objectives,Audio_Talent_used,ACTIVE_ON_WEBSITE,Notes,Allowed_for_ASM_,MAIE_Modules,Main_Category_1,Subcategory,Main_Category_2,Subcategory_of_2,Main_Category_3,Sub_of_3,Search_Tagging,Industry_Tagging___FOR_WEBSITE,Resources_Type,Delivered_In_,Resource_Duration,Level__Foundational__Intermediate__Advanced_,Role___Rep,Role___DM,Role___Account_Manager,Training_Planner,In_Revision,Last_Revision_Date,Related_Content,Passing_Score_changed_to_80_,Writer,SME,ACPE_Module,Chicago_Approved")] content content, int? id)
        {
            if (ModelState.IsValid)
            {
                db.content.Add(content);
                db.SaveChanges();
                return RedirectToAction("../content/Index");
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
        public ActionResult Edit([Bind(Include = "ContentID,Parent_Course_Name__if_applicable_,Module_Name___CURRENT,Module_Name___PREVIOUS,Introduction,Objectives,Audio_Talent_used,ACTIVE_ON_WEBSITE,Notes,Allowed_for_ASM_,MAIE_Modules,Main_Category_1,Subcategory,Main_Category_2,Subcategory_of_2,Main_Category_3,Sub_of_3,Search_Tagging,Industry_Tagging___FOR_WEBSITE,Resources_Type,Delivered_In_,Resource_Duration,Level,Role___Rep,Role___DM,Role___Account_Manager,Training_Planner,In_Revision,Last_Revision_Date,Related_Content,Passing_Score_changed_to_80_,Writer,SME,ACPE_Module,Chicago_Approved")] content content, int? id)
        {
            if (ModelState.IsValid)
            {
                db.Entry(content).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../content/Details/"+id);
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
            return RedirectToAction("../content/Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void ExportExcel()
        {
            var sb = new StringBuilder();
            var data = from c in db.content
                       join pcm in db.parent_course_map on c.ContentID equals pcm.Module_ID
                       join pc in db.parent_courses on pcm.Parent_Course_ID equals pc.Parent_Course_ID
                       join catmap in db.category_map on c.ContentID equals catmap.ContentID
                       join categor in db.categories on catmap.CategoryID equals categor.CategoryID
                       select new
                       {
                           Parent_Course = pc.Parent_Course_Name,
                           Content_Title = c.Module_Name___CURRENT,
                           Previous_Content_Title = c.Module_Name___PREVIOUS,
                           Introduction = c.Introduction,
                           Objectives = c.Objectives,
                           Category = categor.Category1,
                           In_Revision = c.In_Revision,
                           Audio_Talent_Used = c.Audio_Talent_used,
                           Active_on_Website = c.ACTIVE_ON_WEBSITE,
                           Notes = c.Notes,
                           Allowed_for_ASM = c.Allowed_for_ASM_,
                           MAIE_Modules = c.MAIE_Modules,
                           Search_Tagging = c.Search_Tagging,
                           Industry_Tagging = c.Industry_Tagging___FOR_WEBSITE,
                           Resource_Type = c.Resources_Type,
                           Delivered_In = c.Delivered_In_,
                           Resource_Duration = c.Resource_Duration,
                           Level = c.Level,
                           Last_Revision_Date = c.Last_Revision_Date,
                           Related_Content = c.Related_Content,
                           Passing_Score_Changed_to_80 = c.Passing_Score_changed_to_80_,
                           Writer = c.Writer,
                           SME = c.SME,
                           Chicago_Approved = c.Chicago_Approved,
                           ACPE_Module = c.ACPE_Module
                       };
            var list = data.ToList();
            var grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = list;
            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Catalog.xls");
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            //byte[] bytes = Encoding.ASCII.GetBytes(sw.ToString());
            //return File(bytes, "application/text", "Catalog.xls");
        }

        public ActionResult Archive(int? id)
        {
            content c = db.content.Find(id);
            archived_content ac = new archived_content
            {
                Module_Name___CURRENT = c.Module_Name___CURRENT,
                Module_Name___PREVIOUS = c.Module_Name___PREVIOUS,
                Introduction = c.Introduction,
                Objectives = c.Objectives,
                In_Revision = c.In_Revision,
                Audio_Talent_used = c.Audio_Talent_used,
                ACTIVE_ON_WEBSITE = c.ACTIVE_ON_WEBSITE,
                Notes = c.Notes,
                Allowed_for_ASM_ = c.Allowed_for_ASM_,
                MAIE_Modules = c.MAIE_Modules,
                Search_Tagging = c.Search_Tagging,
                Industry_Tagging___FOR_WEBSITE = c.Industry_Tagging___FOR_WEBSITE,
                Resources_Type = c.Resources_Type,
                Delivered_In_ = c.Delivered_In_,
                Resource_Duration = c.Resource_Duration,
                Level = c.Level,
                Last_Revision_Date = c.Last_Revision_Date,
                Related_Content = c.Related_Content,
                Passing_Score_changed_to_80_ = c.Passing_Score_changed_to_80_,
                Writer = c.Writer,
                SME = c.SME,
                Chicago_Approved = c.Chicago_Approved,
                ACPE_Module = c.ACPE_Module
            };

            db.content.Remove(c);
            db.archived_content.Add(ac);
            db.SaveChanges();
            
            return RedirectToAction("../archived_content/Index");
        }

        

    }
}
