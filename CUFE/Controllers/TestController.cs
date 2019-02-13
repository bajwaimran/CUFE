using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Xpo;
using CUFE.Models;
using DevExpress.Data.Filtering;

namespace CUFE.Controllers
{
    public class TestController : Controller
    {
        UnitOfWork _unitOfWork = new UnitOfWork();
        protected int DefaultSelectedItemID { get { return (int)_unitOfWork.Query<CUFE.Models.Freight>().First().Oid; } }
        const string
            SelectedItemIDKey = "SelectedItemID",
            SearchTextKey = "SearchText";


        
        protected int SelectedItemID { get {
                var id = Request.Params[SelectedItemIDKey];
                if (string.IsNullOrEmpty(id))
                    return DefaultSelectedItemID;
                return Convert.ToInt32(id);
            } }
        protected Freight SelectedFreight { get { return _unitOfWork.FindObject<Freight>(CriteriaOperator.Parse("Oid==?", SelectedItemID));  } }

        protected List<Freight> Freights { get { return _unitOfWork.Query<Freight>().ToList(); } }

        protected string SearchText { get { return Request != null ? Request.Params[SearchTextKey] : string.Empty; } }

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        public ActionResult GridView_Master()
        {
            return PartialView(Freights);
        }

        public ActionResult CardView_Master() {

            int searchId;
            if (SearchText == null)
            {
                searchId = 1;
            }
            else
            {
                searchId = int.Parse(SearchText);
            }
            
            var employees = _unitOfWork.GetObjects(_unitOfWork.GetClassInfo(typeof(Freight)), CriteriaOperator.Parse("Oid==?", searchId), null, 100, false, false);
            return PartialView(employees);
        }
        public ActionResult GridView_Detail()
        {
            return PartialView(SelectedFreight);
        }
        public ActionResult CardView_Detail()
        {
            return PartialView(SelectedFreight);
        }
        public ActionResult DetailCallbackPanel()
        {
            return PartialView();
        }
        //public ActionResult GridView_EvaluationsGridView()
        //{
        //    var freight = SelectedFreight;
        //    return PartialView(freight != null ? freight.ConactPersonName : null);
        //}

        public ActionResult Callback()
        {
            return View();
        }

    }
}