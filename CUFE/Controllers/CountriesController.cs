using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CUFE.Models;
using DevExpress.Web.Mvc;
using DevExpress.Xpo;
using DX.Data.Xpo.Identity;
//using CUFE.Helpers;

namespace CUFE.Controllers
{
    public class CountriesController : BaseXpoController
    {
        // GET: Country
        public ActionResult GridViewPartialCountry()
        {
            using(UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<Country>();
                return PartialView(model.ToList());
            }
            
        }
        public ActionResult GridViewPartialAddNew([ModelBinder(typeof(XpoModelBinder))]Country item)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<Country>();
                if (ModelState.IsValid)
                {

                    //item.Session.CommitTransaction();
                    var country = new Country(uow)
                    {
                        CountryName = item.CountryName,
                        CountryCode = item.CountryCode
                    };

                    uow.CommitChanges();
                }
                else
                    ViewData["EditError"] = "Please, correct all errors.";
                return PartialView("~/Views/Countries/GridViewPartialCountry.cshtml", model.ToList());
            }
        }

        public ActionResult GridViewPartialUpdate([ModelBinder(typeof(XpoModelBinder))]Country item)
        {
            if (item.IsChanged)
            {
                item.Save();
                //item.Session.CommitTransaction();
            }
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<Country>();
                return PartialView("~/Views/Countries/GridViewPartialCountry.cshtml", model.ToList());
            }
            
        }
        public ActionResult GridViewPartialDelete(System.Int32 Oid)
        {
            using( var uow = XpoHelper.GetNewUnitOfWork())
            {
                var model = uow.Query<Country>();
                var item = model.First(m => m.Oid == Oid);
                uow.Delete(item);
                uow.CommitChanges();
                return PartialView("~/Views/Countries/GridViewPartialCountry.cshtml", model.ToList());
            }
            
        }

    }
}