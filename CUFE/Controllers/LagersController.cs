using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using CUFE.Models;

namespace CUFE.Controllers
{
    public class LagersController : BaseXpoController
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        // GET: Lagers
        public ActionResult GridViewPartial()
        {
            var model = _unitOfWork.Query<CUFE.Models.Lager>();
            return PartialView(model.ToList());
        }

        public ActionResult Add([ModelBinder(typeof(XpoModelBinder))]CUFE.Models.Lager item)
        {
            var model = _unitOfWork.Query<CUFE.Models.Lager>();
            if (ModelState.IsValid)
            {
                new Lager(_unitOfWork)
                {
                    Address = item.Address,
                    Capacity = item.Capacity,
                    Width = item.Width,
                    Height = item.Height,
                    Length = item.Length,
                    Active = item.Active
                };
                _unitOfWork.CommitChanges();
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
            }
            return PartialView("GridViewPartial", model.ToList());

        }
        public ActionResult Update([ModelBinder(typeof(XpoModelBinder))]CUFE.Models.Lager item)
        {
            var model = _unitOfWork.Query<CUFE.Models.Lager>();
           
            if (item.IsChanged)
                item.Save();
            return PartialView("GridViewPartial", model.ToList());
           
        }

        public ActionResult Delete(int Oid)
        {
            var model = _unitOfWork.Query<CUFE.Models.Lager>();
            var item = _unitOfWork.FindObject<CUFE.Models.Lager>(CriteriaOperator.Parse("Oid ==?", Oid));
            if(item != null)
            {
                _unitOfWork.Delete(item);
                _unitOfWork.CommitChanges();
            }
            return PartialView("GridViewPartial", model.ToList());
        }
    }
}