using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CUFE.Models.ChatModels;
using CUFE.Models;
using DevExpress.Data.Filtering;

namespace CUFE.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                //ViewBag.AllUsers = db.Users.Where(u => u.UserName != User.Identity.Name).ToList();
                ViewBag.AllUsers = uow.Query<XpoApplicationUser>().Where( u => u.UserName != User.Identity.Name).ToList();
                return View();
            }
            
        }

        public ActionResult GroupChat()
        {
            UnitOfWork uow = new UnitOfWork();

            ViewBag.MyGroups = uow.Query<GroupChat>().ToList();
                //ViewBag.AllUsers = db.Users.Where(u => u.UserName != User.Identity.Name).ToList();
                ViewBag.AllUsers = uow.Query<XpoApplicationUser>().Where(u => u.UserName != User.Identity.Name).ToList();
                return View();
            
        }
        public ActionResult GridViewPartial()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<GroupChat>();
                return PartialView("GridViewPartial", model.ToList());
            }
            
        }


    }
}