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
    public class NotificationsController : BaseXpoController
    {
        UnitOfWork _unitOfWork = new UnitOfWork();
        // GET: Notifications
        public ActionResult GridViewPartial()
        {
            var model = _unitOfWork.Query<CUFE.Models.Notification>();
            return PartialView("_GridViewPartial", model.ToList());
        }
        public ActionResult Add([ModelBinder(typeof(XpoModelBinder))]Notification item)
        {
            var model = _unitOfWork.Query<CUFE.Models.Notification>();
            if (ModelState.IsValid)
            {
                new Notification(_unitOfWork)
                {
                    NotificationText = item.NotificationText,
                    Timestamp = DateTime.Now
                };
                _unitOfWork.CommitChanges();
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
            }

            return PartialView("_GridViewPartial", model.ToList());
        }

        public ActionResult Update([ModelBinder(typeof(XpoModelBinder))]Notification item)
        {
            var model = _unitOfWork.Query<CUFE.Models.Notification>();
            if (ModelState.IsValid)
            {
                if (item.IsChanged)
                {
                    item.Save();
                }                    
                
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
            }
            return PartialView("_GridViewPartial", model.ToList());
        }
        public ActionResult Delete(int Oid)
        {
            
            var item = _unitOfWork.FindObject<Notification>(CriteriaOperator.Parse("Oid==?", Oid));
            if(item != null)
            {
                _unitOfWork.Delete(item);
                _unitOfWork.CommitChanges();
            }
            var model = _unitOfWork.Query<CUFE.Models.Notification>();
            return PartialView("_GridViewPartial", model.ToList());
        }
    }
}