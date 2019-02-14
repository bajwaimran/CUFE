using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models
{
    public class GeoCoordinate : BasePersistentObject
    {
        public GeoCoordinate(Session session) : base(session) { }

        string longitude;
        string latitude;
        string zip;
        string countryName;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CountryName
        {
            get => countryName;
            set => SetPropertyValue(nameof(CountryName), ref countryName, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Zip
        {
            get => zip;
            set => SetPropertyValue(nameof(Zip), ref zip, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Latitude
        {
            get => latitude;
            set => SetPropertyValue(nameof(Latitude), ref latitude, value);
        }

        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Longitude
        {
            get => longitude;
            set => SetPropertyValue(nameof(Longitude), ref longitude, value);
        }

        DateTime timestamp;
        public DateTime Timestamp
        {
            get => timestamp;
            set => SetPropertyValue(nameof(Timestamp), ref timestamp, value);
        }
    }
}