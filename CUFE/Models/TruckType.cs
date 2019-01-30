using System;
using DevExpress.Xpo;

namespace CUFE.Models
{

    public class TruckType : BasePersistentObject
    {
        

        public TruckType(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
        private string _Type;
        public string Type
        {
            get { return _Type; }
            set { SetPropertyValue("Type", ref _Type, value); }
        }

        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { SetPropertyValue("IsActive", ref _IsActive, value); }
        }

        
        //[Association("TruckType-Trucks")]
        //public XPCollection<Truck> Trucks
        //{
        //    get
        //    {
        //        return GetCollection<Truck>("Trucks");
        //    }
        //}
    }

}