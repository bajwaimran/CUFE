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
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ClientController : Controller
    {
        private UnitOfWork uow = new UnitOfWork();
        public ActionResult Index()
        {
            return RedirectToAction("Dashboard") ;
        }
        public ActionResult TrucksManagement()
        {
            return View();
        }
        public ActionResult Loads()
        {
            return View();
        }
        public ActionResult AddLoad()
        {
            return View("~/Views/Loads/Add");
        }
        public ActionResult Freights()
        {
            return View();
        }
        public ActionResult AddFreight()
        {
            return View("~/Views/Freights/Add");
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
        public ActionResult Chat()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            return View();
        }
        [Authorize]
        public ActionResult Groups()
        {
            ViewBag.userinfo = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("UserName==?", User.Identity.Name));
            var groups = uow.Query<GroupChat>();
            if(groups != null)
            {
                return View(groups.ToList());
            }
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult AdvancedSearch()
        {
            return View();
        }

        

    }
}