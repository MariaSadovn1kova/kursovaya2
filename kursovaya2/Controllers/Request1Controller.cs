using kursovaya2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kursovaya2.Controllers
{
    public class Request1Controller : Controller
    {
        private UniversityEntities db = new UniversityEntities();
        public ActionResult Index()

        {
            ViewBag.NumberOfGroup = new SelectList(db.StudentGroup, "NumberOfGroup", "NumberOfGroup");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index (string NumberOfGroup)
        {
            return Redirect("Request1/Result?GroupNumber=" + NumberOfGroup);
        }

        public ActionResult Result (int GroupNumber)
        {
            SqlParameter parameter = new SqlParameter("@GroupNumber", GroupNumber);
            var registrations = db.Database.SqlQuery<Request1_Result>("Request1 @GroupNumber", parameter);
            return View(registrations);
        }
    }
}