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
using WaWiXPO.Helper.Master;
using System.Collections.Generic;

namespace CUFE.Controllers
{
    public class FreightsController : Controller
    {
        private UnitOfWork _abonaUnitOfWork;
        UnitOfWork uow = new UnitOfWork();

        const string
            SelectedItemIDKey = "Oid",
            SearchTextKey = "SearchText";
        protected int SelectedItemID
        {
            get
            {
                var id = Request.Params[SelectedItemIDKey];
                if (string.IsNullOrEmpty(id))
                    return DefaultSelectedItemID;
                return Convert.ToInt32(id);
            }
        }
        protected virtual void EnsureViewBagInfo()
        {
            ViewBag.Title = string.Format("{0} - DevAV Demo | ASP.NET Controls by DevExpress", ControllerName);
            ViewBag.PageLogoImageUrl = string.Format("/Content/Images/LogoMenuIcons/{0}.png", ControllerName);
            ViewBag.ToolbarMenuXPath = string.Format("Pages/{0}/Item", ControllerName);
            ViewBag.ShowSearchBox = ShowSearchBox;
            //ViewBag.IsGridView = DemoUtils.IsEmployeeGridViewMode; //Should be virtual or abstract. 
            ViewBag.ControllerName = ControllerName;
            ViewBag.SearchText = SearchText;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            EnsureViewBagInfo();
            base.OnActionExecuting(filterContext);
        }
        public  string ControllerName { get { return "Freights"; } }
        public  bool ShowSearchBox { get { return true; } }
        protected string SearchText { get { return Request != null ? Request.Params[SearchTextKey] : string.Empty; } }
        protected  int DefaultSelectedItemID { get { return (int)uow.Query<Freight>().First().Oid; } }

        protected Freight SelectedEmployee { get { return uow.FindObject<Freight>(CriteriaOperator.Parse("Oid==?", SelectedItemID)); } }
        protected List<Freight> Freights { get { return uow.Query<Freight>().ToList(); } }





        public FreightsController()
        {
            string connectionString = "XpoProvider = MSSqlServer;data source=WAWIDEV;user id=WaWiXPO;password=wawixpo;initial catalog=WaWiXPO;Persist Security Info=true;";
            MasterXpoHelper.InitiateDataLayer(connectionString);
            _abonaUnitOfWork = WaWiXPO.Helper.Master.MasterXpoHelper.GetNewUnitOfWork();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            int freightID = !string.IsNullOrEmpty(Request.Params["FreightID"]) ? int.Parse(Request.Params["FreightID"]) : uow.Query<Freight>().First().Oid;
            var model = uow.FindObject<Freight>(CriteriaOperator.Parse("Oid==?", freightID));
            return PartialView("Details", model);
        }
        
        public ActionResult GridViewPartial()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<Freight>();
                return PartialView("_GridViewPartial", model.ToList());
            }

        }

        public ActionResult MyGridViewPartial()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                int companyId = int.Parse(User.Identity.GetCompanyId());
                var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
                var model = company.Freights;
                return PartialView("_GridViewPartial", model.ToList());
            }
        }
        public ActionResult Add()
        {

            using (UnitOfWork uow = new UnitOfWork())
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FreightAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    var userId = User.Identity.GetUserId();
                    var companyId = int.Parse(User.Identity.GetCompanyId());
                    var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
                    var freight = new Freight(uow)
                    {

                        EndDate = model.EndDate,
                        EndLocationCoordinates = model.EndLocationCoordinates,
                        EndLocationCountry = model.EndLocationCountry,
                        EndLocationName = model.EndLocationName,
                        EndLocationZip = model.EndLocationZip,
                        OfferredPrice = model.OfferredPrice,
                        StartDate = model.StartDate,
                        StartLocationCoordinates = model.StartLocationCoordinates,
                        StartLocationCountry = model.StartLocationCountry,
                        StartLocationName = model.StartLocationName,
                        StartLocationZip = model.StartLocationZip,
                        StartLat = model.StartLat,
                        StartLon = model.StartLon,
                        EndLat = model.EndLat,
                        EndLon = model.EndLon,
                        Notes = model.Notes,
                        ConactPersonName = model.ContactPersonName,
                        ContactPersonPhone = model.ContactPersonPhone,
                        ContactPersonEmail = model.ContactPersonEmail,
                        UserId = userId,
                        TruckId = model.TruckId,
                        CreatedOn = DateTime.Now
                    };
                    company.Freights.Add(freight);
                    uow.CommitChanges();



                    try
                    {

                        //await SendEmail(load.LoadId);
                        return RedirectToAction("Freights", "Client");
                    }
                    catch (Exception e)
                    {
                        ViewBag.errorMessage = e.Message;
                        return View("Error");
                    }
                }
            }
        }

        public ActionResult AbonaFreights()
        {
            
            var model = _abonaUnitOfWork.Query<FreightExchangeDemo.FreightExchangeOrders>();
            return PartialView("AbonaFreights", model.ToList());
        }

        public ActionResult Content()
        {
            
            int Oid;
            if(Request.Params["Oid"] == null)
            {
                Oid = uow.Query<Freight>().First().Oid;
            }
            else
            {
                Oid =  int.Parse(Request.Params["Oid"]);
            }
            
            //int Oid = (!string.IsNullOrEmpty(Request.Params["Oid"])) ? int.Parse(Request.Params["Oid"]) : uow.Query<Freight>().First().Oid;
            var model = uow.FindObject<Freight>(CriteriaOperator.Parse("Oid==?", Oid));
            return PartialView("Content", model);
        }

        public ActionResult GridView_Detail()
        {
            return PartialView(SelectedEmployee);
        }
        
    }
}