using kursovaya2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web; 
using System.Web.Mvc;
namespace kursovaya2.Controllers
{
    public class Request2Controller : Controller
    {
        private UniversityEntities db = new UniversityEntities();
        // GET: Request2
        public ActionResult Index(int pg = 1)
        {
            SqlParameter parameter = new SqlParameter("@SubjectTitle", "Англ Анализ");
            SqlParameter parameter2 = new SqlParameter("@CountOfTasks", 2);
            List<Request2_Result> requests = db.Database.SqlQuery<Request2_Result>("Request2 @SubjectTitle, @CountOfTasks", parameter, parameter2).ToList();
            const int pageSize = 50;
            if (pg < 1)
                pg = 1;

            int rescCount = requests.Count();
            var pages = new Pages(rescCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = requests.Skip(recSkip).Take(pages.PageSize).ToList();
            this.ViewBag.Pager = pages;

            return View(data);
        }
    }
}