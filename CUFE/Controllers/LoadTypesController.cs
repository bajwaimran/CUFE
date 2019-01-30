using CUFE.Models;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CUFE.Controllers
{
    public class LoadTypesController : BaseXpoController
    {
        public ActionResult GridviewPartial()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<LoadType>();
                return PartialView("_GridViewPartial", model.ToList());
            }
        }
        public ActionResult GridViewAddNew([ModelBinder(typeof(XpoModelBinder))]LoadType item)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<LoadType>();
                if (ModelState.IsValid)
                {
                    var truckType = new LoadType(uow)
                    {
                        Type = item.Type,
                        IsActive = item.IsActive
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
        public ActionResult GridViewUpdate([ModelBinder(typeof(XpoModelBinder))]LoadType item)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<LoadType>();
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
                var model = uow.Query<LoadType>();
                var item = model.First(m => m.Oid == Oid);
                uow.Delete(item);
                uow.CommitChanges();
                return PartialView("_GridViewPartial", model.ToList());
            }
        }
    }
}