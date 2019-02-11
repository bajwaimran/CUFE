using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
namespace CUFE.Controllers
{
    public class MessagesController : BaseXpoController
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public ActionResult GridViewPartial()
        {
            var model = _unitOfWork.Query<CUFE.Models.Message>();
            return PartialView("_GridViewPartial", model.ToList());
        }
       

        [HttpPost]
        public ActionResult Send(CUFE.Models.Message item)
        {
            if (ModelState.IsValid)
            {
                new CUFE.Models.Message(_unitOfWork)
                {
                    Email = item.Email,
                    Name = item.Name,
                    Subject = item.Subject,
                    Text = item.Text,
                    Timestamp = DateTime.Now
                };

                _unitOfWork.CommitChanges();
                ViewBag.Title = "Message Sent";
                ViewBag.Message = "Thank You For Contacting Us. Your Message Has Been Received and We Will Get Back To You Within 2 Business Days, If Required!";
                return View("Message");
            }

            ViewBag.Title = "We Cannot Sent Your Message";
            ViewBag.errorMessage = "Please Try After Sometime.";
            return View("Message");
        }
    }
}