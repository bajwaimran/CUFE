using System.Web;
using System.Web.Mvc;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using Microsoft.AspNet.Identity;
using CUFE.Extensions;
using CUFE.Models;
using System.Threading.Tasks;
using CUFE.Models.ViewModels;
using System.Linq;
using System;
using CUFE.Models.ChatModels;

namespace CUFE.Controllers
{
    public class ClientController : Controller
    {
        private UnitOfWork uow = new UnitOfWork();
        public ActionResult TrucksManagement()
        {
            return View();
        }
        public ActionResult Loads()
        {
            return View();
        }

        public ActionResult Freights()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            ViewBag.Blogs = uow.Query<Blog>().Take(4).ToList();
            int companyId = int.Parse(User.Identity.GetCompanyId());
            var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
            ViewBag.MyLoadsCount = company.Loads.Count;
            ViewBag.MyFreightsCount = company.Freights.Count;
            ViewBag.MyTrucksCount = company.Trucks.Count;
            ViewBag.MyOrdersCount = "0";
            return View();
        }
        [Authorize]
        public ActionResult Groups()
        {
            return View();
        }
        [Authorize]
        public ActionResult ManageGroup(int id)
        {
            UnitOfWork uow = new UnitOfWork();

            ViewBag.RoomId = id;
            int companyId = int.Parse(User.Identity.GetCompanyId());
            ViewBag.Users = uow.Query<XpoApplicationUser>().Where(u => u.CompanyId == companyId).ToList();
            //var newModel = (from r in uow.Query<Room>()
            //               join m in uow.Query<RoomMember>()
            //               on r.Oid equals m.Room
            //                select r);

            var rooms = uow.Query<Room>();
            
            return View();

        }


    }
}