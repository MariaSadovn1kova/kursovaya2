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
        public ActionResult Index(int pg = 1)
        {
            List<Registration> registrations = db.Registration.ToList();
            const int pageSize = 50;
            if (pg < 1)
                pg = 1;

            int rescCount = registrations.Count();
            var pages = new Pages(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = registrations.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages;
            return View(data);
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
            ViewBag.Student_StudentNumber = new SelectList(db.Student, "StudentNumber", "StudentNumber");
            ViewBag.TaskOnTheSubject_NumberOfSubject = new SelectList(db.TaskOnTheSubject, "NumberOfSubject", "NumberOfSubject");
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
                try
                {
                    db.addRegistration(registration.ID, registration.Student_StudentNumber, registration.TaskOnTheSubject_NumberOfSubject, registration.DeliveryDate, registration.DateOfIssue);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ошибка! Запись не может быть добавлена");
                }
            }

            ViewBag.Student_StudentNumber = new SelectList(db.Student, "StudentNumber", "StudentNumber", registration.Student_StudentNumber);
            ViewBag.TaskOnTheSubject_NumberOfSubject = new SelectList(db.TaskOnTheSubject, "NumberOfSubject", "NumberOfSubject", registration.TaskOnTheSubject_NumberOfSubject);
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
            ViewBag.Student_StudentNumber = new SelectList(db.Student, "StudentNumber", "StudentNumber", registration.Student_StudentNumber);
            ViewBag.TaskOnTheSubject_NumberOfSubject = new SelectList(db.TaskOnTheSubject, "NumberOfSubject", "NumberOfSubject", registration.TaskOnTheSubject_NumberOfSubject);
            return View(registration);
        }

        // POST: Registration/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Student_StudentNumber,TaskOnTheSubject_NumberOfSubject,DeliveryDate,DateOfIssue")] Registration registration)
        {
            try
            {
                db.upRegistration(registration.ID, registration.Student_StudentNumber, registration.TaskOnTheSubject_NumberOfSubject, registration.DeliveryDate, registration.DateOfIssue);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ошибка! Запись не может быть отредактирована ");
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
