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
    public class TaskOnTheSubjectController : Controller
    {
        private UniversityEntities db = new UniversityEntities();

        // GET: TaskOnTheSubject
        public ActionResult Index(int pg = 1)
        {
            List<TaskOnTheSubject> taskonthesubjects = db.TaskOnTheSubject.ToList();
            const int pageSize = 50;
            if (pg < 1)
                pg = 1;

            int rescCount = taskonthesubjects.Count();
            var pages = new Pages(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = taskonthesubjects.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages;

            return View(data);
        }

        // GET: TaskOnTheSubject/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskOnTheSubject taskOnTheSubject = db.TaskOnTheSubject.Find(id);
            if (taskOnTheSubject == null)
            {
                return HttpNotFound();
            }
            return View(taskOnTheSubject);
        }

        // GET: TaskOnTheSubject/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskOnTheSubject/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NumberOfSubject,Subject_Title,Synopsis")] TaskOnTheSubject taskOnTheSubject)
        {
            if (ModelState.IsValid)
            {
                db.TaskOnTheSubject.Add(taskOnTheSubject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskOnTheSubject);
        }

        // GET: TaskOnTheSubject/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskOnTheSubject taskOnTheSubject = db.TaskOnTheSubject.Find(id);
            if (taskOnTheSubject == null)
            {
                return HttpNotFound();
            }
            return View(taskOnTheSubject);
        }

        // POST: TaskOnTheSubject/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NumberOfSubject,Subject_Title,Synopsis")] TaskOnTheSubject taskOnTheSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskOnTheSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskOnTheSubject);
        }

        // GET: TaskOnTheSubject/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskOnTheSubject taskOnTheSubject = db.TaskOnTheSubject.Find(id);
            if (taskOnTheSubject == null)
            {
                return HttpNotFound();
            }
            return View(taskOnTheSubject);
        }

        // POST: TaskOnTheSubject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskOnTheSubject taskOnTheSubject = db.TaskOnTheSubject.Find(id);
            db.TaskOnTheSubject.Remove(taskOnTheSubject);
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
