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
    public class StudentGroupController : Controller
    {
        private UniversityEntities db = new UniversityEntities();

        // GET: StudentGroup
        public ActionResult Index(int pg = 1)
        {
            List<StudentGroup> studentgroups = db.StudentGroup.ToList();
            const int pageSize = 50;
            if (pg < 1)
                pg = 1;

            int rescCount = studentgroups.Count();
            var pages = new Pages(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = studentgroups.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages;

            return View(data);
        }

        // GET: StudentGroup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGroup studentGroup = db.StudentGroup.Find(id);
            if (studentGroup == null)
            {
                return HttpNotFound();
            }
            return View(studentGroup);
        }

        // GET: StudentGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentGroup/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NumberOfGroup,Speciality,YearOf")] StudentGroup studentGroup)
        {
            if (ModelState.IsValid)
            {
                db.StudentGroup.Add(studentGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentGroup);
        }

        // GET: StudentGroup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGroup studentGroup = db.StudentGroup.Find(id);
            if (studentGroup == null)
            {
                return HttpNotFound();
            }
            return View(studentGroup);
        }

        // POST: StudentGroup/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NumberOfGroup,Speciality,YearOf")] StudentGroup studentGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentGroup);
        }

        // GET: StudentGroup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGroup studentGroup = db.StudentGroup.Find(id);
            if (studentGroup == null)
            {
                return HttpNotFound();
            }
            return View(studentGroup);
        }

        // POST: StudentGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentGroup studentGroup = db.StudentGroup.Find(id);
            try
            {
                db.StudentGroup.Remove(studentGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ошибка! Объект используется в другой таблице");
            }
            return View(studentGroup);
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
