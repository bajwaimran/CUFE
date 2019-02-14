using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using CUFE.Models;
using Newtonsoft.Json;

namespace CUFE.Controllers
{
    public class GeoCoordinatesController : Controller
    {
        UnitOfWork _unitOfWork = new UnitOfWork();

        public List<GeoCoordinate> AllCoordinates()
        {
            var model = _unitOfWork.Query<GeoCoordinate>();
            return model.ToList();
        }
        public ActionResult GetCoordinate(string country, string zip)
        {
            var model = _unitOfWork.FindObject<GeoCoordinate>(CriteriaOperator.Parse("Zip==?",zip));
            if (model != null)
            {
                var obj = new
                {
                    Latitude = model.Latitude,
                    Longitude = model.Longitude
                };
                return Json(obj);
            }
                
            return null;
        }

        [HttpPost]
        public bool Add(string country, string zip, string latitude, string longitude)
        {
            var model = _unitOfWork.FindObject<GeoCoordinate>(CriteriaOperator.Parse("Zip==?", zip));
            if (model == null)
            {
                new GeoCoordinate(_unitOfWork)
                {
                    CountryName = country,
                    Zip = zip,
                    Latitude = latitude,
                    Longitude = longitude,
                    Timestamp = DateTime.Now
                };
                _unitOfWork.CommitChanges();
                return true;
            }
            return false;
        }

    }
}