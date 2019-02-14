using DevExpress.Xpo;


namespace CUFE.Models
{
    public class Country: BasePersistentObject
    {       
        public Country(Session session): base(session) { }

        public override void AfterConstruction()
        {           
            
            base.AfterConstruction();
        }
        
        [DevExpress.Xpo.Indexed(Unique = true)]
        public string CountryName { get; set; }
        [DevExpress.Xpo.Indexed(Unique = true)]
        public string CountryCode { get; set; }
    }
}