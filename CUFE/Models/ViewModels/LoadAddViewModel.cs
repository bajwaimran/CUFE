using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CUFE.Models.ViewModels
{
    public class LoadAddViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Start Location")]
        public string StartLocationName { get; set; }
        [Required]
        [Display(Name = "End Location")]
        public string EndLocationName { get; set; }

        public string StartLocationZip { get; set; }
        public string EndLocationZip { get; set; }

        [Required]
        [Display(Name = "Start Location Country")]
        public string StartLocationCountry { get; set; }
        [Required]
        [Display(Name = "End Location Country")]
        public string EndLocationCountry { get; set; }

        [Required]
        [Display(Name = "Load/Good Type")]
        public string Loadtype { get; set; }

        public int CompanyId { get; set; }
        public string CreatedbyUserId { get; set; }

        [Display(Name = "Offerred Price")]
        public decimal OfferredPrice { get; set; }
        [Display(Name = "Truck type Required")]
        public string TruckTypeNeeded { get; set; }
        [Display(Name = "Load Volume")]
        public decimal LoadVolume { get; set; }

        public double StartLat { get; set; }
        public double StartLon { get; set; }
        public double EndLat { get; set; }
        public double EndLon { get; set; }
        public string StartLocationCoordinates { get; set; }
        public string EndLocationCoordinates { get; set; }

        public float Height { get; set; }
        public float Width { get; set; }
        public string Notes { get; set; }

        public string CotactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}