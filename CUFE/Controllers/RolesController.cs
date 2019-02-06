using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CUFE.Models;
using DX.Data.Xpo.Identity;
using DevExpress.Data.Filtering;

namespace CUFE.Controllers
{
    public class RolesController : BaseXpoController
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public ActionResult GridViewPartial()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<XpoApplicationRole>();
                return PartialView("_GridViewPartial" , model.ToList());
            }
        }
        

        //public ActionResult Add([ModelBinder(typeof(XpoModelBinder))]XpoApplicationRole item)
        //{

        //    using (UnitOfWork uow = new UnitOfWork())
        //    {
        //        var users = uow.Query<XpoApplicationUser>();
        //        var roles = uow.Query<XpoApplicationRole>();
        //        var companies = uow.Query<Company>();

        //        var model = uow.Query<XpoApplicationRole>();
        //        if (ModelState.IsValid)
        //        {
        //            XpoApplicationRole role = new XpoApplicationRole(uow)
        //            {
        //                Name = item.Name
        //            };
        //            uow.CommitChanges();
        //        }
        //        else
        //            ViewData["EditError"] = "Please, correct all errors.";
        //        return PartialView("_GridViewPartial", model.ToList());
        //    }
        //}

        //public ActionResult Update([ModelBinder(typeof(XpoModelBinder))]XpoApplicationRole item)
        //{

        //    using (UnitOfWork uow = new UnitOfWork())
        //    {
        //        var model = uow.Query<XpoApplicationRole>();
        //        var role = uow.FindObject<XpoApplicationRole>(CriteriaOperator.Parse("Id==?", item.Id));
        //        role.Name = item.Name;
        //        uow.CommitChanges();

        //        return PartialView("_GridViewPartial", model.ToList());
        //    }

        //}
        //public ActionResult Delete(string Oid)
        //{
        //    using (var uow = XpoHelper.GetNewUnitOfWork())
        //    {
        //        var model = uow.Query<XpoApplicationRole>();
        //        var item = model.First(m => m.Id == Oid);
        //        uow.Delete(item);
        //        uow.CommitChanges();
        //        return PartialView("_GridViewPartial", model.ToList());
        //    }

        //}
    }
}