using CUFE.Models;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CUFE.Extensions;
using DevExpress.Data.Filtering;
using Microsoft.AspNet.Identity;

namespace CUFE.Controllers
{
    public class TrucksController : BaseXpoController
    {
        public static List<TruckType> truckTypeList;
        public TrucksController()
        {
            UnitOfWork uow = new UnitOfWork();

            truckTypeList = uow.Query<TruckType>().ToList();

        }
        public ActionResult GridviewPartial()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                int companyId = int.Parse(User.Identity.GetCompanyId());
                var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
                var model = company.Trucks;
                //var model = uow.Query<Truck>();
                ViewBag.TruckTypesList = truckTypeList;
                return PartialView("_GridViewPartial", model.ToList());
            }
        }
        public ActionResult GridViewAddNew([ModelBinder(typeof(XpoModelBinder))]Truck item)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                int companyId = int.Parse(User.Identity.GetCompanyId());
                var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid == ?", companyId));
                var model = company.Trucks;
                ViewBag.TruckTypesList = truckTypeList;
                if (ModelState.IsValid)
                {
                    var truck = new Truck(uow)
                    {
                        RegNo = item.RegNo,
                        TruckType = item.TruckType,
                        Capacity = item.Capacity,
                        IsFrigo = item.IsFrigo,
                        IsAdr = item.IsAdr,
                        IsGps = item.IsGps,
                        IsisoThermal = item.IsisoThermal,
                        IsLift = item.IsLift
                    };
                    company.Trucks.Add(truck);
                    uow.CommitChanges();
                }
                else
                {
                    ViewData["EditError"] = "Please, correct all errors.";
                }
                return PartialView("_GridViewPartial", model.ToList());
            }
        }
        public ActionResult GridViewUpdate([ModelBinder(typeof(XpoModelBinder))]Truck item)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                int companyId = int.Parse(User.Identity.GetCompanyId());
                var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid == ?", companyId));
                var model = company.Trucks;
                ViewBag.TruckTypesList = truckTypeList;
                if (ModelState.IsValid)
                {
                    if (item.IsChanged)
                        item.Save();
                }
                else
                {
                    ViewData["EditError"] = "Please, correct all errors.";
                }
                return PartialView("_GridViewPartial", model.ToList());
            }
        }
        public ActionResult GridViewDelete(System.Int32 Oid)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                int companyId = int.Parse(User.Identity.GetCompanyId());
                var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid == ?", companyId));
                var model = company.Trucks;
                ViewBag.TruckTypesList = truckTypeList;
                var item = model.First(m => m.Oid == Oid);
                uow.Delete(item);
                uow.CommitChanges();
                return PartialView("_GridViewPartial", model.ToList());
            }
        }
    }
}