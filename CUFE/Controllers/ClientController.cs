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
        public async Task<ActionResult> FreightSearchresult()
        {
            CriteriaOperator criteria = null;
            SortingCollection sortProps = new SortingCollection();
            sortProps.Add(new SortProperty("CreatedOn", DevExpress.Xpo.DB.SortingDirection.Descending));
            string OriginCountry = !string.IsNullOrEmpty(Request.Params["OriginCountry"]) ? Request.Params["OriginCountry"] : "Germany";
            string DestCountry = !string.IsNullOrEmpty(Request.Params["DestCountry"]) ? Request.Params["DestCountry"] : "Germany";

            if (!string.IsNullOrEmpty(Request.Params["SearchByradius"]))
            {
                FreightsController freightsController = new FreightsController();
                int srad = !string.IsNullOrEmpty(Request.Params["OriginRadius"]) ? int.Parse(Request.Params["OriginRadius"]) : 10;
                int erad = !string.IsNullOrEmpty(Request.Params["DestRadius"]) ? int.Parse(Request.Params["DestRadius"]) : 10;

                double slat = double.Parse(Request["startLat"]);
                double slon = double.Parse(Request["startLon"]);
                double elat = double.Parse(Request["endLat"]);
                double elon = double.Parse(Request["endLon"]);

                double sminlat = freightsController.MinLat(slat, srad);
                double smaxlat = freightsController.MaxLat(slat, srad);
                double sminlon = freightsController.MinLon(slon, srad, slat);
                double smaxlon = freightsController.MaxLon(slon, srad, slat);
                //Getting min and max geo coordinates for destination
                double eminlat = freightsController.MinLat(elat, erad);
                double emaxlat = freightsController.MaxLat(elat, erad);
                double eminlon = freightsController.MinLon(elon, erad, elat);
                double emaxlon = freightsController.MaxLon(elon, erad, elat);
               
                criteria = CriteriaOperator.Parse("StartLat >= ? and StartLat <= ? and StartLon >= ? and StartLon <= ? and EndLat >= ? and EndLat <= ? and EndLon >= ? and EndLon >= ?", 
                    sminlat, smaxlat, sminlon, smaxlon, eminlat, emaxlat,eminlon, emaxlon);
            }else if(!string.IsNullOrEmpty(Request.Params["OriginZip"]) && !string.IsNullOrEmpty(Request.Params["DestZip"]))
            {
                string OriginZip = Request.Params["OriginZip"];
                string DestZip = Request.Params["DestZip"];
                criteria = CriteriaOperator.Parse("StartLocationZip == ? and EndLocationZip== ? and StartLocationCountry== ? and EndLocationCountry == ?", OriginZip, DestZip, OriginCountry, DestCountry);
                
            }else if(!string.IsNullOrEmpty(Request.Params["OriginCity"]) && !string.IsNullOrEmpty(Request.Params["DestCity"]))
            {
                string OriginCity = Request.Params["OriginCity"];
                string DestCity = Request.Params["DestCity"];
                criteria = CriteriaOperator.Parse("StartLocationCountry== ? and EndLocationCountry == ? and StartCity==? and EndCity==?",OriginCountry, DestCountry,OriginCity, DestCity);
            } else{
                criteria = CriteriaOperator.Parse("StartLocationCountry== ? and EndLocationCountry == ? ", OriginCountry, DestCountry);
            }
            var model = uow.GetObjects(uow.GetClassInfo(typeof(Freight)), criteria, sortProps, 50, false, false);
            return PartialView("~/Views/Freights/_GridViewPartial.cshtml", model);

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
        public ActionResult Results()
        {
            string OriginCountry = !string.IsNullOrEmpty(Request.Params["originCountry"]) ? Request.Params["originCountry"] : null;
            string DestCountry = !string.IsNullOrEmpty(Request.Params["destCountry"]) ? Request.Params["destCountry"] : null;
            string OriginZip = !string.IsNullOrEmpty(Request.Params["originZip"]) ? Request.Params["originZip"] : null;
            string DestZip = !string.IsNullOrEmpty(Request.Params["destZip"]) ? Request.Params["destZip"] : null;
            string OriginCity = !string.IsNullOrEmpty(Request.Params["originCity"]) ? Request.Params["originCity"] : null;
            string DestCity = !string.IsNullOrEmpty(Request.Params["destCity"]) ? Request.Params["destCity"] : null;
            string sd = !string.IsNullOrEmpty(Request.Params["startDate"]) ? Request.Params["startDate"] : null;
            string ed = !string.IsNullOrEmpty(Request.Params["endDate"]) ? Request.Params["endDate"] : null;
            string sr = !string.IsNullOrEmpty(Request.Params["originRad"]) ? Request.Params["originRad"] : null;
            string er = !string.IsNullOrEmpty(Request.Params["destRad"]) ? Request.Params["destRad"] : null;
            string RadiusSearch = !string.IsNullOrEmpty(Request.Params["radiusSearch"]) ? Request.Params["radiusSearch"] : null;
            if (!string.IsNullOrEmpty(Request.Params["search"]))
            {
                string search = Request.Params["search"];
                if(search == "Freights")
                {
                    ViewBag.ResultType = "Freights";
                    var model = uow.Query<Freight>();
                    return PartialView(model.ToList());
                }
                else
                {
                    ViewBag.ResultType = "Loads";
                    var model = uow.Query<Load>();
                    return PartialView(model.ToList());
                }
                
            }
            
            ViewBag.Message = "Nothing";
            return PartialView();
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