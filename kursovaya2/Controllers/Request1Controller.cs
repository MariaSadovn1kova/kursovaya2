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
            return View();
        }

        public ActionResult Result ()
        {
            SqlParameter parameter = new SqlParameter("@GroupNumber", 1000021);
            var registrations = db.Database.SqlQuery<Request1_Result>("Request1 @GroupNumber", parameter);
            return View(registrations);
        }
    }
}