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
    public class FreightsController : BaseXpoController
    {
        private UnitOfWork _abonaUnitOfWork;
        UnitOfWork uow = new UnitOfWork();
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
                return PartialView("_MyGridViewPartial", model.ToList());
            }
        }
        public ActionResult GridViewPartialAddNew([ModelBinder(typeof(XpoModelBinder))]Freight item)
        {
            if (ModelState.IsValid)
            {
                string user = User.Identity.GetUserId();
                int companyId = int.Parse(User.Identity.GetCompanyId());
                var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
                new Freight(uow)
                {
                   StartDate = item.StartDate,
                   StartLocationCountry = item.StartLocationCountry,
                   StartCity = item.StartCity,
                   StartLocationName = item.StartLocationName,
                   StartLat = item.StartLat,
                   StartLon = item.StartLon,
                   StartLocationZip = item.StartLocationZip,
                   StartLocationCoordinates = item.StartLocationCoordinates,
                   EndDate = item.EndDate,
                   EndLocationCountry = item.EndLocationCountry,
                   EndCity = item.EndCity,
                   EndLocationZip = item.EndLocationZip,
                   EndLocationName = item.EndLocationName,
                   EndLat = item.EndLat,
                   EndLon = item.EndLon,
                   EndLocationCoordinates = item.EndLocationCoordinates,
                   OfferredPrice = item.OfferredPrice,
                   TruckId = item.TruckId,
                   Company = company,
                   ConactPersonName = item.ConactPersonName,
                   ContactPersonEmail = item.ContactPersonEmail,
                   ContactPersonPhone = item.ContactPersonPhone,
                   Notes = item.Notes,
                   CreatedOn = DateTime.Now,
                   CompanyId = company.Oid,
                   UserId = user
                };
                uow.CommitChanges();
                
                var model = company.Freights;
                return PartialView("_MyGridViewPartial", model.ToList());
            }
            return null;
        }

        public ActionResult GridViewPartialUpdate([ModelBinder(typeof(XpoModelBinder))]Freight item)
        {
            int companyId = int.Parse(User.Identity.GetCompanyId());
            var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
            
            if (ModelState.IsValid)
            {
                if (item.IsChanged)
                    item.Save();
            }
            else
            {

            }
            var model = company.Freights;
            return PartialView("_MyGridViewPartial", model.ToList());
        }
        public ActionResult GridViewPartialDelete(int Oid)
        {
            int companyId = int.Parse(User.Identity.GetCompanyId());
            var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
            var item = uow.FindObject<Freight>(CriteriaOperator.Parse("Oid==?", Oid));
            if (item != null)
            {
                uow.Delete(item);
            }
            var model = company.Freights;
            return PartialView("_MyGridViewPartial", model.ToList());            
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
                        CreatedOn = DateTime.Now,
                        
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
    }
}