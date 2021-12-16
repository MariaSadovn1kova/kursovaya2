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

        public ActionResult Result (int GroupNumber, int pg = 1)
        {
            SqlParameter parameter = new SqlParameter("@GroupNumber", GroupNumber);
            List<Request1_Result> requests = db.Database.SqlQuery<Request1_Result>("Request1 @GroupNumber", parameter).ToList();
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