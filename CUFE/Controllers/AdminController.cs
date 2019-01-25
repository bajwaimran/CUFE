using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CUFE.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        //[Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult UserManagement()
        {
            return View();
        }
        
    }
}