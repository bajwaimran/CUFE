using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Xpo;
using CUFE.Models;
using DevExpress.Data.Filtering;

namespace CUFE.Controllers
{
    public class CompanyController : BaseXpoController
    {
        // GET: Companies
        public ActionResult Index()
        {
            return View();
        }



        [ValidateInput(false)]
        public ActionResult GridView1Partial()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<Company>();
                return PartialView("~/Views/Admin/_GridView1Partial.cshtml", model.ToList());
            }

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialAddNew([ModelBinder(typeof(XpoModelBinder))]Company item)
        {

            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<Company>();
                if (ModelState.IsValid)
                {
                    var company = new Company(uow)
                    {
                        CompanyName = item.CompanyName,
                        Address = item.Address,
                        Email = item.Email,
                        Phone = item.Phone,
                        Vat = item.Vat,
                        Fax = item.Fax,
                    };
                    uow.CommitChanges();
                }
                else
                    ViewData["EditError"] = "Please, correct all errors.";
                return PartialView("~/Views/Admin/_GridView1Partial.cshtml", model.ToList());
            }

        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialUpdate([ModelBinder(typeof(XpoModelBinder))]Company item)
        {
            if (item.IsChanged)
                item.Save();
            //item.Session.CommitTransaction();

            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<Company>();
                return PartialView("~/Views/Admin/_GridView1Partial.cshtml", model.ToList());
            }

        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialDelete(System.Int32 Oid)
        {

            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<Company>();
                var modelItem = model.First(m => m.Oid == Oid);
                uow.Delete(modelItem);
                uow.CommitChanges();
                return PartialView("~/Views/Admin/_GridView1Partial.cshtml", model.ToList());

            }

        }
    }
}