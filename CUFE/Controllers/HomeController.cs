using CUFE.Models;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CUFE.Controllers
{
    public class HomeController : BaseXpoController
    {
        public ActionResult Index()
        {
            //using (UnitOfWork uow = new UnitOfWork())
            //{
            //    var company1 = new Company(uow)
            //    {
            //        CompanyName ="Ted Makers",
            //        Address = "LA",
            //        Phone = "00-125-897",
            //        Vat = "LA2018TM"
            //    };

            //    uow.CommitChanges();
            //}
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Blogs()
        {
            return View();
        }
    }
}