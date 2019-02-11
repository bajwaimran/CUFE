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
        UnitOfWork _unitOfWork = new UnitOfWork();
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
            var model = _unitOfWork.Query<Blog>();
            ViewBag.Blogs = model.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Message()
        {
            ViewBag.Title = "Registration Information Received!";
            ViewBag.Message = "Dear Blanco, Thank You for Registering with us. Your request has been received and You will receive a verification call between 2 business days. upon verfication you will be able to use your account.";
            ViewBag.errorMessage = "Das ist Error Message";
            return View("Message");
        }

        
    }
}