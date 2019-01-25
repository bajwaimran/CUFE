using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models
{
    public class Country: XPObject
    {
        public Country(): base() { }
        public Country(Session session): base(session) { }

        public string CountryName { get; set; }
        public string CountryCode { get; set; }
    }
}