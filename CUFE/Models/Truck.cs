using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models
{
    public class Truck :XPObject
    {
        public Truck() : base() { }
        public Truck(Session session) : base(session) { }

        public string RegistrationNo { get; set; }
    }
}