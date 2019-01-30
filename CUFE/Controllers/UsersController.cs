
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CUFE.Extensions;
using CUFE.Models;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web.Mvc;

namespace CUFE.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public UsersController() {

        }
        public UsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {


            UnitOfWork uow = new UnitOfWork();
                ViewBag.CompanyList = uow.Query<Company>().ToList();
                var model = uow.Query<XpoApplicationUser>();
                return PartialView("~/Views/Admin/_GridViewPartial.cshtml", model.ToList());
            
            
            
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] CUFE.Models.ApplicationUser item)
        {
            using(UnitOfWork uow = new UnitOfWork())
            {
                ViewBag.CompanyList = uow.Query<Company>().ToList();
                var model = uow.Query<XpoApplicationUser>();
                if (ModelState.IsValid)
                {
                    var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", item.CompanyId));
                    var user = new ApplicationUser {
                        UserName = item.UserName,
                        Email = item.Email,
                        CompanyId = company.Oid,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Address1 = item.Address1,
                        Address2 = item.Address2,
                        City = item.City,
                        Province = item.Province,
                        Country = item.Country,
                        Birthdate = item.Birthdate
                    };
                    var result = UserManager.Create(user, item.PasswordHash);
                    if (result.Succeeded)
                    {
                        AddRole(user.Id, "Admin");
                        return PartialView("~/Views/Admin/_GridViewPartial.cshtml", model.ToList());
                    }
                    ViewData["EditError"] = "Unable to add";
                }
                else
                    ViewData["EditError"] = "Please, correct all errors.";                

                return PartialView("~/Views/Admin/_GridViewPartial.cshtml", model.ToList());

            }
            
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))]CUFE.Models.ApplicationUser item)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                if (ModelState.IsValid)
                {
                    //var user = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("Id==?", item.Id));
                    var user = (ApplicationUser)UserManager.FindById(item.Id);
                    //var u = UserManager.FindById(item.Id);
                    user.EmailConfirmed = item.EmailConfirmed;
                    user.CompanyId = item.CompanyId;
                    user.UserName = item.UserName;
                    user.Birthdate = item.Birthdate;
                    user.FirstName = item.FirstName;
                    user.LastName = item.LastName;
                    user.Address1 = item.Address1;
                    user.Address2 = item.Address2;
                    user.City = item.City;
                    user.Country = item.Country;
                    user.PhoneNumber = item.PhoneNumber;

                    var result = UserManager.Update(user);
                    if(!result.Succeeded)
                        ViewData["EditError"] = "Please, correct all errors." + result.Errors;

                }
                else
                {
                    ViewData["EditError"] = "Please, correct all errors.";
                }
            //using (UnitOfWork uow = new UnitOfWork())
            //{
                ViewBag.CompanyList = uow.Query<Company>().ToList();
                var model = uow.Query<XpoApplicationUser>();
                return PartialView("~/Views/Admin/_GridViewPartial.cshtml", model.ToList());
            }


        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]System.String Id)
        {
            using( UnitOfWork uow = new UnitOfWork())
            {
                ViewBag.CompanyList = uow.Query<Company>().ToList();
                var model = uow.Query<XpoApplicationUser>();
                //var user = uow.FindObject<ApplicationUser>(CriteriaOperator.Parse("Id==?", Id));
                var user = model.First(m => m.Id == Id);
                if(user != null)
                {
                    uow.Delete(user);
                    uow.CommitChanges();
                    return PartialView("~/Views/Admin/_GridViewPartial.cshtml", model.ToList());
                }

                ViewData["EditError"] = "Correct All errors";
                return PartialView("~/Views/Admin/_GridViewPartial.cshtml", model.ToList());                
            }
            
        }

        private  ActionResult AddRole(string userId, string role)
        {
            try
            {
                UserManager.AddToRole(userId, role);
                return Content("Done");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }
    }
}