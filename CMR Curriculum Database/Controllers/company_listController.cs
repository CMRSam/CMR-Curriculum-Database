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
using System.IO;

namespace CMR_Curriculum_Database.Controllers
{
    public class company_listController : Controller
    {
        private curriculumEntities db = new curriculumEntities();
        private string companyName;

        public List<CheckBoxListItem> companies { get; set; }

        // GET: company_list
        public ActionResult Index(string searchString)
        {
            char delim = ' ';
            
            var company = from m in db.company_list select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                setCompanyName(searchString);
                string[] searchStringArray = searchString.Split(delim);
                foreach (var s in searchStringArray)
                {
                    company = company.Where(m => m.Company_Name.Contains(s) ||
                                                 m.Program.Contains(s) ||
                                                 m.Delivery_Method.Contains(s) ||
                                                 m.Notes.Contains(s));
                }
            }
            Console.WriteLine(companyName);
            return View(company.ToList().OrderBy(c => c.Company_Name));
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

        public void setCompanyName(string cn)
        {
            companyName = cn;
        }

        public string getCompanyName()
        {
            return companyName;
        }

        public void ExportCompanyProgramsExcel()
        {
            string searchString = getCompanyName();
            var sb = new StringBuilder();
            var data = from c in db.content
                       join pcm in db.parent_course_map on c.ContentID equals pcm.Module_ID
                       join pc in db.parent_courses on pcm.Parent_Course_ID equals pc.Parent_Course_ID
                       join catmap in db.category_map on c.ContentID equals catmap.ContentID
                       join categor in db.categories on catmap.CategoryID equals categor.CategoryID
                       join compmap in db.company_maps on c.ContentID equals compmap.ContentID
                       join comp in db.company_list on compmap.CompanyID equals comp.CompanyID
                       where comp.Company_Name == searchString
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
    }
}
