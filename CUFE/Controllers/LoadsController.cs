using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using Microsoft.AspNet.Identity;
using CUFE.Extensions;
using CUFE.Models;
using System.Threading.Tasks;
using CUFE.Models.ViewModels;

namespace CUFE.Controllers
{
    public class LoadsController : BaseXpoController
    {
        UnitOfWork uow = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GridViewPartial()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                
                //int companyId = int.Parse(User.Identity.GetCompanyId());
                //var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
                //var model = company.Trucks;
                var model = uow.Query<Load>();                
                return PartialView("_GridViewPartial", model.ToList());
            }
           
        }

        public ActionResult MyGridViewPartial()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                int companyId = int.Parse(User.Identity.GetCompanyId());
                var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
                var model = company.Loads;
                return PartialView("_MyGridViewPartial", model.ToList());
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
        public async Task<ActionResult> Add(LoadAddViewModel model)
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
                    var load = new Load(uow)
                    {

                        EndDate = model.EndDate,
                        EndLocationCoordinates = model.EndLocationCoordinates,
                        EndLocationCountry = model.EndLocationCountry,
                        EndLocationName = model.EndLocationName,
                        EndLocationZip = model.EndLocationZip,
                        LoadType = model.Loadtype,
                        LoadVolume = model.LoadVolume,
                        OfferredPrice = model.OfferredPrice,
                        StartDate = model.StartDate,
                        StartLocationCoordinates = model.StartLocationCoordinates,
                        StartLocationCountry = model.StartLocationCountry,
                        StartLocationName = model.StartLocationName,
                        StartLocationZip = model.StartLocationZip,
                        TruckTypeNeeded = model.TruckTypeNeeded,
                        StartLat = model.StartLat,
                        StartLon = model.StartLon,
                        EndLat = model.EndLat,
                        EndLon = model.EndLon,
                        Height = model.Height,
                        Width = model.Width,
                        Notes = model.Notes,
                        ConactPersonName = model.CotactPerson,
                        ContactPersonPhone = model.Phone,
                        ContactPersonEmail = model.Email,                        
                        UserId = userId,
                        CreatedOn = DateTime.Now
                    };
                    company.Loads.Add(load);
                    uow.CommitChanges();

                    
                    
                    try
                    {
                        
                        //await SendEmail(load.LoadId);
                        return RedirectToAction("Loads","Client");
                    }
                    catch (Exception e)
                    {
                        ViewBag.errorMessage = e.Message;
                        return View("Error");
                    }
                }
            }

        }


        public ActionResult GridViewPartialAddNew([ModelBinder(typeof(XpoModelBinder))]Load item)
        {
            if (ModelState.IsValid)
            {
                string user = User.Identity.GetUserId();
                int companyId = int.Parse(User.Identity.GetCompanyId());
                var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
                new Load(uow)
                {
                    StartDate = item.StartDate,
                    StartCity = item.StartCity,
                    StartLat = item.StartLat,
                    StartLocationZip = item.StartLocationZip,
                    StartLocationCountry = item.StartLocationCountry,
                    StartLocationName = item.StartLocationName,
                    StartLon = item.StartLon,
                    StartLocationCoordinates = item.StartLocationCoordinates,
                    EndCity = item.EndCity,
                    EndDate = item.EndDate,
                    EndLat = item.EndLat,
                    EndLocationCoordinates = item.EndLocationCoordinates,
                    EndLocationCountry = item.EndLocationCountry,
                    EndLocationName = item.EndLocationName,
                    EndLocationZip = item.EndLocationZip,
                    EndLon = item.EndLon,
                    OfferredPrice = item.OfferredPrice,
                    TruckTypeNeeded = item.TruckTypeNeeded,
                    ConactPersonName = item.ConactPersonName,
                    ContactPersonEmail = item.ContactPersonEmail,
                    ContactPersonPhone = item.ContactPersonPhone,
                    CreatedOn = DateTime.Now,
                    Company = company,
                    UserId = user,
                    Height = item.Height,
                    Width = item.Width,
                    LoadType = item.LoadType,
                    LoadTypeId = item.LoadTypeId,
                    LoadVolume = item.LoadVolume,
                    Notes = item.Notes,
                    CompanyId = company.Oid
                };
                uow.CommitChanges();

                var model = company.Loads;
                return PartialView("_MyGridViewPartial", model.ToList());
            }
            return null;
        }

        public ActionResult GridViewPartialUpdate([ModelBinder(typeof(XpoModelBinder))]Load item)
        {
            int companyId = int.Parse(User.Identity.GetCompanyId());
            var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
            var model = company.Loads;
            if (ModelState.IsValid)
            {
                if (item.IsChanged)
                    item.Save();

                
                return PartialView("_MyGridViewPartial", model.ToList());

            }
            else
            {

            }
            return null;
        }
        public ActionResult GridViewPartialDelete(int Oid)
        {
            int companyId = int.Parse(User.Identity.GetCompanyId());
            var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
            var model = company.Loads;
            var item = uow.FindObject<Load>(CriteriaOperator.Parse("Oid==?", Oid));
            if(item != null)
            {
                uow.Delete(item);
                
            }
            return PartialView("_MyGridViewPartial", model.ToList());
        }

    }
}