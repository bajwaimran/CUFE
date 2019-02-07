using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models
{
    public class Load: BasePersistentObject
    {
        public Load(Session session): base(session) { }

        
        
        

        string startLocationName;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string StartLocationName
        {
            get => startLocationName;
            set => SetPropertyValue(nameof(StartLocationName), ref startLocationName, value);
        }
        DateTime startDate;
        public DateTime StartDate
        {
            get => startDate;
            set => SetPropertyValue(nameof(StartDate), ref startDate, value);
        }
        DateTime endDate;
        public DateTime EndDate
        {
            get => endDate;
            set => SetPropertyValue(nameof(EndDate), ref endDate, value);
        }


        
        
        

        //public string StartCity { get; set; }
        string startCitz;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string StartCity
        {
            get => startCitz;
            set => SetPropertyValue(nameof(StartCity), ref startCitz, value);
        }


        //public string StartLocationZip { get; set; }
        string startLocationZip;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string StartLocationZip
        {
            get => startLocationZip;
            set => SetPropertyValue(nameof(StartLocationZip), ref startLocationZip, value);
        }
        //public string StartLocationCountry { get; set; }
        string startLocationCountry;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string StartLocationCountry
        {
            get => startLocationCountry;
            set => SetPropertyValue(nameof(StartLocationCountry), ref startLocationCountry, value);
        }
        //public double StartLat { get; set; }
        double startLat;        
        public double StartLat
        {
            get => startLat;
            set => SetPropertyValue(nameof(StartLat), ref startLat, value);
        }
        //public double StartLon { get; set; }
        double startLon;        
        public double StartLon
        {
            get => startLon;
            set => SetPropertyValue(nameof(StartLon), ref startLon, value);
        }
        //public string StartLocationCoordinates { get; set; }
        string startLocationCoordinates;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string StartLocationCoordinates
        {
           get => startLocationCoordinates;
            set => SetPropertyValue(nameof(StartLocationCoordinates), ref startLocationCoordinates, value);
        }

        //public string EndLocationName { get; set; }
        string endLocationName;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string EndLocationName
        {
            get => endLocationName;
            set => SetPropertyValue(nameof(EndLocationName), ref endLocationName, value);
        }
        string endCity;
        public string EndCity {
            get => endCity;
            set => SetPropertyValue(nameof(EndCity), ref endCity, value);
        }
        string endLocationZip;
        public string EndLocationZip {
            get => endLocationZip;
            set => SetPropertyValue(nameof(EndLocationZip), ref endLocationZip, value);
        }
        string endLocationCountry;
        public string EndLocationCountry {
            get => endLocationCountry;
            set => SetPropertyValue(nameof(EndLocationCountry), ref endLocationCountry, value);
        }

        double endLat;
        public double EndLat {
            get => endLat;
            set => SetPropertyValue(nameof(EndLat), ref endLat, value);

        }

        double endLon;
        public double EndLon {
            get => endLon;
            set => SetPropertyValue(nameof(EndLon), ref endLon, value);
        }

        string endLocationCoordinates;
        public string EndLocationCoordinates {
            get => endLocationCoordinates;
            set => SetPropertyValue(nameof(EndLocationCoordinates), ref endLocationCoordinates, value);
        }
        decimal offerredPrice;
        public decimal OfferredPrice
        {
            get => offerredPrice;
            set => SetPropertyValue(nameof(OfferredPrice), ref offerredPrice, value);
        }

        string truckTypeNeeded;
        public string TruckTypeNeeded
        {
            get => truckTypeNeeded;
            set => SetPropertyValue(nameof(TruckTypeNeeded), ref truckTypeNeeded, value);
        }
        decimal loadVolume;
        public decimal LoadVolume {
            get => loadVolume;
            set => SetPropertyValue(nameof(LoadVolume), ref loadVolume, value);
        }
        float height;
        public float Height {
            get => height;
            set => SetPropertyValue(nameof(Height), ref height, value);
        }
        float width;
        public float Width {
            get => width;
            set => SetPropertyValue(nameof(Width), ref width, value);
        }
        int loadTypeId;
        public int LoadTypeId {
            get => loadTypeId;
            set => SetPropertyValue(nameof(LoadTypeId), ref loadTypeId, value);
        }
        string loadType;
        public string LoadType
        {
            get => loadType;
            set => SetPropertyValue(nameof(LoadType), ref loadType, value);
        }

        string conactPersonName;
        public string ConactPersonName
        {
            get => conactPersonName;
            set => SetPropertyValue(nameof(ConactPersonName), ref conactPersonName, value);
        }
        string contactPersonEmail;
        public string ContactPersonEmail {
            get => contactPersonEmail;
            set => SetPropertyValue(nameof(ContactPersonEmail), ref contactPersonEmail, value);
        }
        string contactPersonPhone;
        public string ContactPersonPhone {
            get => contactPersonPhone;
            set => SetPropertyValue(nameof(ContactPersonPhone), ref contactPersonPhone, value);
        }
        string notes;
        public string Notes {
            get => notes;
            set => SetPropertyValue(nameof(Notes), ref notes, value);
        }

        string userId;
        public string UserId {
            get => userId;
            set => SetPropertyValue(nameof(UserId), ref userId, value);
        }
        DateTime createdOn;
        public DateTime CreatedOn {
            get => createdOn;
            set => SetPropertyValue(nameof(CreatedOn), ref createdOn, value);
        }
        public int CompanyId { get; set; }

        Company company;

        [Association("Company-Loads")]
        public Company Company
        {
            get => company;
            set => SetPropertyValue(nameof(Company), ref company, value);
        }
    }
}