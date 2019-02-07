
using CUFE.Models;
using CUFE.Models.ChatModels;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CUFE.Extensions;
using Microsoft.AspNet.Identity;

namespace CUFE.Controllers
{
    public class ChatsController : BaseXpoController
    {
        private UnitOfWork uow = new UnitOfWork();
        // GET: Chats
        public ActionResult Index()
        {
            return View();
        }

         
        public ActionResult GridViewPartial()
        {
            
                int companyId = int.Parse(User.Identity.GetCompanyId());
                var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
;                var model = company.Rooms;
                ViewBag.Users = uow.Query<XpoApplicationUser>().Where(u => u.CompanyId == companyId).ToList();
                return PartialView("_GridViewPartial", model.ToList());
           
            
        }
        public ActionResult Add([ModelBinder(typeof(XpoModelBinder))]Room item)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                int companyId = int.Parse(User.Identity.GetCompanyId());
                var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
                var model = company.Rooms;
                

                if (ModelState.IsValid)
                {

                    
                    var room = new Room(uow)
                    {
                        RoomName = item.RoomName
                    };
                    model.Add(room);
                    uow.CommitChanges();
                }
                else
                {
                    ViewData["EditError"] = "Please, correct all errors.";
                }
                return PartialView("_GridViewPartial", model.ToList());
            }
            
        }
        public ActionResult Update([ModelBinder(typeof(XpoModelBinder))]Room item)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                if(!ModelState.IsValid)
                    ViewData["EditError"] = "Please, correct all errors.";
                if (item.IsChanged)
                    item.Save();
                int companyId = int.Parse(User.Identity.GetCompanyId());
                var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
                var model = company.Rooms;
                return PartialView("_GridViewPartial", model.ToList());
            }
        }
        
        public ActionResult Delete(Int32 Oid)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                int companyId = int.Parse(User.Identity.GetCompanyId());
                var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
                var model = company.Rooms;
                var item = model.First(m => m.Oid == Oid);
                uow.Delete(item);
                uow.CommitChanges();
                return PartialView("_GridViewPartial", model.ToList());
            }
        }

        public ActionResult Members(int RoomId)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var room = uow.FindObject<Room>(CriteriaOperator.Parse("Oid==?", RoomId));
                var model = room.Members;
                return PartialView("_RoomMemberPartial", model.ToList());
            }
        }


        public void SaveMessage(string senderId, string receiverId, string message)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                try
                {
                    var user = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("Id==?", senderId));
                    var receiver = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("Id==?", receiverId));
                    if (receiver != null)
                    {
                        new SentMessage(uow)
                        {
                            Text = message,
                            ReceiverId = receiver.Id,
                            User = user.Id,
                            ApplicationUser = user,
                            Timestamp = DateTime.Now
                        };
                        uow.CommitChanges();
                    }
                }catch(Exception e)
                {
                    System.Console.WriteLine(e.Message);
                }
                


            }
        }

        public ActionResult GroupMembers()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<ChatUser>();
                return PartialView("_GroupMembersPartialView", model.ToList());
            }
        }

    }
}