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
        /*DASHBOARD RELATED METHODS*/
        [Authorize(Roles = "Admin, User")]
        public ActionResult Index()
        {
            return RedirectToAction("Dashboard") ;
        }
        [Authorize(Roles = "Admin, User")]
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

        /*VEHICLE RELATED METHODS*/
        [Authorize(Roles = "Admin")]
        public ActionResult TrucksManagement()
        {
            return View();
        }

        /*LOADS RELATED METHODS*/
        [Authorize(Roles = "Admin, User")]
        public ActionResult Loads()
        {
            return View();
        }
        public ActionResult LoadDetails()
        {
            int freightID = !string.IsNullOrEmpty(Request.Params["FreightID"]) ? int.Parse(Request.Params["FreightID"]) : uow.Query<Load>().First().Oid;
            var model = uow.FindObject<Load>(CriteriaOperator.Parse("Oid==?", freightID));
            return PartialView("LoadDetails", model);
        }
        [Authorize(Roles = "Admin, User")]
        public ActionResult AddLoad()
        {
            return View("~/Views/Loads/Add.cshtml");
        }
        [Authorize]
        public ActionResult Load(int id)
        {
            var model = uow.FindObject<Load>(CriteriaOperator.Parse("Oid==?", id));
            return View(model);
        }
        /*FREIGHTS RELATED METHODS*/
        [Authorize(Roles = "Admin, User")]
        public ActionResult Freights()
        {
            return View();
        }
        public ActionResult FreightDetails()
        {
            int freightID = !string.IsNullOrEmpty(Request.Params["FreightID"]) ? int.Parse(Request.Params["FreightID"]) : uow.Query<Freight>().First().Oid;
            var model = uow.FindObject<Freight>(CriteriaOperator.Parse("Oid==?", freightID));
            return PartialView("FreightDetails", model);
        }
        [Authorize(Roles = "Admin, User")]
        public ActionResult AddFreight()
        {
            return View("~/Views/Freights/Add.cshtml");
        }
        [Authorize]
        public ActionResult Freight(int id)
        {
            var model = uow.FindObject<Freight>(CriteriaOperator.Parse("Oid==?", id));
            return View(model);
        }
           
        /*USER MANAGEMENT RELATED METHODS*/
        [Authorize(Roles = "Admin")]
        public ActionResult Users()
        {
            return View();
        }




        /*SEARCH RELATED METHODS*/
        [Authorize]
        [Authorize(Roles = "SuperAdmin, Admin, User, Driver")]
        public ActionResult Search()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin, Admin, User, Driver")]
        public ActionResult AdvancedSearch()
        {
            return View();
        }

        /*CHAT RELATED METHODS*/
        [Authorize(Roles = "SuperAdmin, Admin, User, Driver")]
        public ActionResult Chat()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            return View();
        }
        [Authorize(Roles = "SuperAdmin, Admin, User, Driver")]
        public ActionResult Groups()
        {
            ViewBag.userinfo = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("UserName==?", User.Identity.Name));
            ViewBag.Messages = uow.Query<GroupChatMessage>().ToList();
            var groups = uow.Query<GroupChat>();
            if (groups != null)
            {
                return View(groups.ToList());
            }

            return View();
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult ChatGroups()
        {
            return View();
        }

        public ActionResult Lagers()
        {
            return View();
        }
        
        public ActionResult Abona()
        {
            return View();
        }

    }
}