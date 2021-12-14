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
    public class RegistrationController : Controller
    {
        private UniversityEntities db = new UniversityEntities();

        // GET: Registration
        public ActionResult Index()
        {
            var registration = db.Registration.Include(r => r.Student).Include(r => r.TaskOnTheSubject);
            return View(registration.ToList());
        }

        // GET: Registration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registration.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Registration/Create
        public ActionResult Create()
        {
            ViewBag.Student_StudentNumber = new SelectList(db.Student, "StudentNumber", "Fulll_Name");
            ViewBag.TaskOnTheSubject_NumberOfSubject = new SelectList(db.TaskOnTheSubject, "NumberOfSubject", "Subject_Title");
            return View();
        }

        // POST: Registration/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Student_StudentNumber,TaskOnTheSubject_NumberOfSubject,DeliveryDate,DateOfIssue")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Registration.Add(registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Student_StudentNumber = new SelectList(db.Student, "StudentNumber", "Fulll_Name", registration.Student_StudentNumber);
            ViewBag.TaskOnTheSubject_NumberOfSubject = new SelectList(db.TaskOnTheSubject, "NumberOfSubject", "Subject_Title", registration.TaskOnTheSubject_NumberOfSubject);
            return View(registration);
        }

        // GET: Registration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registration.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.Student_StudentNumber = new SelectList(db.Student, "StudentNumber", "Fulll_Name", registration.Student_StudentNumber);
            ViewBag.TaskOnTheSubject_NumberOfSubject = new SelectList(db.TaskOnTheSubject, "NumberOfSubject", "Subject_Title", registration.TaskOnTheSubject_NumberOfSubject);
            return View(registration);
        }

        // POST: Registration/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Student_StudentNumber,TaskOnTheSubject_NumberOfSubject,DeliveryDate,DateOfIssue")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Student_StudentNumber = new SelectList(db.Student, "StudentNumber", "Fulll_Name", registration.Student_StudentNumber);
            ViewBag.TaskOnTheSubject_NumberOfSubject = new SelectList(db.TaskOnTheSubject, "NumberOfSubject", "Subject_Title", registration.TaskOnTheSubject_NumberOfSubject);
            return View(registration);
        }

        // GET: Registration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registration.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registration registration = db.Registration.Find(id);
            db.Registration.Remove(registration);
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
