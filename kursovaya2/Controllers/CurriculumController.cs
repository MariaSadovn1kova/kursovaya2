using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kursovaya2.Models;

namespace kursovaya2.Controllers
{
    public class CurriculumController : Controller
    {
        private UniversityEntities db = new UniversityEntities();

        // GET: Curriculum
        public ActionResult Index(int pg = 1)
        {
            List<Curriculum> curriculums = db.Curriculum.ToList();
            const int pageSize = 50;
            if (pg < 1)
                pg = 1;

            int rescCount = curriculums.Count();
            var pages = new Pages(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = curriculums.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages;
            return View(data);
        }

        // GET: Curriculum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curriculum curriculum = db.Curriculum.Find(id);
            if (curriculum == null)
            {
                return HttpNotFound();
            }
            return View(curriculum);
        }

        // GET: Curriculum/Create
        public ActionResult Create()
        {
            ViewBag.Group_NumberOfGroup = new SelectList(db.StudentGroup, "NumberOfGroup", "NumberOfGroup");
            ViewBag.Subject_Title = new SelectList(db.Subject, "Title", "Title");
            return View();
        }

        // POST: Curriculum/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Subject_Title,Group_NumberOfGroup")] Curriculum curriculum)
        {
            if (ModelState.IsValid)
            {
                db.Curriculum.Add(curriculum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Group_NumberOfGroup = new SelectList(db.StudentGroup, "NumberOfGroup", "NumberOfGroup", curriculum.Group_NumberOfGroup);
            ViewBag.Subject_Title = new SelectList(db.Subject, "Title", "Title", curriculum.Subject_Title);
            return View(curriculum);
        }

        // GET: Curriculum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curriculum curriculum = db.Curriculum.Find(id);
            if (curriculum == null)
            {
                return HttpNotFound();
            }
            ViewBag.Group_NumberOfGroup = new SelectList(db.StudentGroup, "NumberOfGroup", "NumberOfGroup", curriculum.Group_NumberOfGroup);
            ViewBag.Subject_Title = new SelectList(db.Subject, "Title", "Title", curriculum.Subject_Title);
            return View(curriculum);
        }

        // POST: Curriculum/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Subject_Title,Group_NumberOfGroup")] Curriculum curriculum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(curriculum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Group_NumberOfGroup = new SelectList(db.StudentGroup, "NumberOfGroup", "NumberOfGroup", curriculum.Group_NumberOfGroup);
            ViewBag.Subject_Title = new SelectList(db.Subject, "Title", "Title", curriculum.Subject_Title);
            return View(curriculum);
        }

        // GET: Curriculum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curriculum curriculum = db.Curriculum.Find(id);
            if (curriculum == null)
            {
                return HttpNotFound();
            }
            return View(curriculum);
        }

        // POST: Curriculum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Curriculum curriculum = db.Curriculum.Find(id);
            db.Curriculum.Remove(curriculum);
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
