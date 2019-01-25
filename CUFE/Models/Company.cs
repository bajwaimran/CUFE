using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models
{
    public class Company: XPObject
    {
        public Company(): base() { }
        public Company (Session session): base(session) { }

        public string CompanyName { get; set; }
        [DevExpress.Xpo.Indexed(Unique =true)]
        public string Vat { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        [DevExpress.Xpo.Size(200)]
        public string Phone { get; set; }
        [DevExpress.Xpo.Size(100)]
        public string Fax { get; set; }
        [DevExpress.Xpo.Size(200)]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}