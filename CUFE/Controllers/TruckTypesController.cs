using CUFE.Models;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CUFE.Controllers
{
    public class TruckTypesController : BaseXpoController
    {
        public ActionResult GridviewPartial()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<TruckType>();
                return PartialView("_GridViewPartial", model.ToList());
            }            
        }
        public ActionResult GridViewAddNew([ModelBinder(typeof(XpoModelBinder))]TruckType item)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<TruckType>();
                if (ModelState.IsValid)
                {
                    var truckType = new TruckType(uow)
                    {
                        Type = item.Type
                    };
                    uow.CommitChanges();
                }
                else
                {
                    ViewData["EditError"] = "Please, correct all errors.";
                }
                return PartialView("_GridViewPartial", model.ToList());
            }
        }
        public ActionResult GridViewUpdate([ModelBinder(typeof(XpoModelBinder))]TruckType item)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<TruckType>();
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
                var model = uow.Query<TruckType>();
                var item = model.First(m => m.Oid == Oid);
                uow.Delete(item);
                uow.CommitChanges();
                return PartialView("_GridViewPartial", model.ToList());
            }
        }
    }
}