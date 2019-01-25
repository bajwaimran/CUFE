
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
            //using(UnitOfWork uow = new UnitOfWork())
            //{
            UnitOfWork uow = new UnitOfWork();
                ViewBag.CompanyList = uow.Query<Company>().ToList();
                var model = uow.Query<XpoApplicationUser>();
                return PartialView("~/Views/Admin/_GridViewPartial.cshtml", model.ToList());

                //if (User.IsInRole("SuperAdmin"))
                //{
                //    var model = uow.Query<ApplicationUser>();
                //    return PartialView("~/Views/Admin/_GridViewPartial.cshtml", model.ToList());
                //}
                //else if (User.IsInRole("Admin"))
                //{
                //    int companyId = int.Parse(User.Identity.GetCompanyId());
                //    var model = db.Users.Where(u => u.CompanyId == companyId);
                //    return PartialView("~/Views/Admin/_GridViewPartial.cshtml", model.ToList());
                //}
                //else
                //{
                //    var userId = User.Identity.GetUserId();
                //    var model = db.Users.Find(userId);
                //    return PartialView("~/Views/Admin/_GridViewPartial.cshtml", model);
                //}
           //}
            
            
            
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
                    var user = new ApplicationUser { UserName = item.UserName, Email = item.Email, CompanyId = company.Oid };
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
        public ActionResult GridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] CUFE.Models.ApplicationUser item)
        {
            using(UnitOfWork uow = new UnitOfWork())
            {
                ViewBag.CompanyList = uow.Query<Company>().ToList();
                
                var model = uow.Query<XpoApplicationUser>();
                if (ModelState.IsValid)
                {
                    //var temp = UserManager.FindById(item.Id) as ApplicationUser;
                    var currentItem = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("Id==?", item.Id));
                    if (currentItem != null)
                    {
                        currentItem.EmailConfirmed = item.EmailConfirmed;
                        currentItem.CompanyId = item.CompanyId;
                        currentItem.UserName = item.UserName;
                        uow.CommitChanges();
                        return PartialView("~/Views/Admin/_GridViewPartial.cshtml", model.ToList());
                    }
                    ViewData["EditError"] = "Please, correct all errors.";
                    return PartialView("~/Views/Admin/_GridViewPartial.cshtml", model.ToList());

                }
                ViewData["EditError"] = "Invalid Model state.";
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