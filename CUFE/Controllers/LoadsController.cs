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
    public class LoadsController : Controller
    {
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




    }
}