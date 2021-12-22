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
    public class StudentController : Controller
    {
        private UniversityEntities db = new UniversityEntities();

        // GET: Student
        public ActionResult Index(int pg = 1)
        {
            List<Student> students = db.Student.ToList();
            const int pageSize = 50;
            if (pg < 1)
                pg = 1;

            int rescCount = students.Count();
            var pages = new Pages(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = students.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages;

            return View(data);
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentNumber,Group_NumberOfGroup,Fulll_Name")] Student student)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    db.addStudent(student.StudentNumber, student.Group_NumberOfGroup, student.Fulll_Name);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ошибка! Студент не может быть добавлен");
                }
                return View(student);
            }

            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentNumber,Group_NumberOfGroup,Fulll_Name")] Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.upStudent(student.StudentNumber, student.Group_NumberOfGroup, student.Fulll_Name);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ошибка! Данные о студенте не могут быть отредактированы");
                }
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Student.Find(id);
            try
            {
                db.Student.Remove(student);
                db.SaveChanges();
                return RedirectToAction("Index");

            } catch(Exception ex)
            {
                ModelState.AddModelError("", "Ошибка! Объект используется в другой таблице");
            }
            return View(student);
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
