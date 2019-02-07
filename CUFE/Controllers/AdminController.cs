using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DX.Data.Xpo.Identity;
using Microsoft.AspNet.Identity;
using CUFE.Models;

namespace CUFE.Controllers
{
    public class AdminController : BaseXpoController
    {
        // GET: Admin
        //[Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult UserManagement()
        {
            return View();
        }
        public ActionResult CompanyManagement()
        {
            return View();
        }

        public ActionResult CountryManagement()
        {
            return View();
        }

        public ActionResult TruckTypeManagement()
        {
            return View();
        }
        public ActionResult LoadTypeManagement()
        {
            return View();
        }

        public ActionResult ChatGroups()
        {
            return View();
        }

        public ActionResult GroupMembers()
        {
            return View();
        }
        public ActionResult Blogs()
        {
            return View();
        }
        public ActionResult Notifications()
        {
            return View();
        }

    }
}