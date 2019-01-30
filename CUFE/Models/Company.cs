using DevExpress.Xpo;

namespace CUFE.Models
{
    public class Company: BasePersistentObject
    {
        
        public Company (Session session): base(session) { }


        
        string phone;
        string email;
        string address;
        string vat;
        string companyName;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CompanyName
        {
            get => companyName;
            set => SetPropertyValue(nameof(CompanyName), ref companyName, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [DevExpress.Xpo.Indexed(Unique = true)]
        public string Vat
        {
            get => vat;
            set => SetPropertyValue(nameof(Vat), ref vat, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Address
        {
            get => address;
            set => SetPropertyValue(nameof(Address), ref address, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Email
        {
            get => email;
            set => SetPropertyValue(nameof(Email), ref email, value);
        }


        [Size(200)]
        public string Phone
        {
            get => phone;
            set => SetPropertyValue(nameof(Phone), ref phone, value);
        }


        [DevExpress.Xpo.Size(100)]
        public string Fax { get; set; }
        //[DevExpress.Xpo.Size(200)]
        //public string UserId { get; set; }
        //public ApplicationUser User { get; set; }


        [Association("Company-Trucks")]
        public XPCollection<Truck> Trucks
        {
            get
            {
                return GetCollection<Truck>("Trucks");
            }
        }
    }
}