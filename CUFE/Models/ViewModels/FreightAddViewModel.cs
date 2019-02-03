using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CUFE.Models.ViewModels
{
    public class FreightAddViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Start Location")]
        public string StartLocationName { get; set; }
        [Required]
        [Display(Name = "End Location Name")]
        public string EndLocationName { get; set; }
        public string StartLocationZip { get; set; }
        public string EndLocationZip { get; set; }

        [Required]
        [Display(Name = "Start Location Country")]
        public string StartLocationCountry { get; set; }

        [Required]
        [Display(Name = "End Location Country")]
        public string EndLocationCountry { get; set; }


        [Display(Name = "Truck")]
        public int TruckId { get; set; }
        public int CompanyId { get; set; }
        public string CreatedbyUserId { get; set; }

        [Display(Name = "Offerred Price")]
        public decimal OfferredPrice { get; set; }

        public double StartLat { get; set; }
        public double StartLon { get; set; }
        public double EndLat { get; set; }
        public double EndLon { get; set; }
        public string StartLocationCoordinates { get; set; }
        public string EndLocationCoordinates { get; set; }

        public string ContactPersonName { get; set; }
        public string ContactPersonUserId { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonPhone { get; set; }

        public string Notes { get; set; }
    }
}