using System;
using System.ComponentModel.DataAnnotations.Schema;
using CUFE.Models;
using DevExpress.Xpo;

namespace CUFE.Models
{

    public class Truck : BasePersistentObject
    {
        public Truck(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
        
        [Index(IsUnique = true)]
        public string RegNo { get; set; }        
        public decimal Capacity { get; set; }
        public bool IsFrigo { get; set; }
        public bool IsisoThermal { get; set; }
        public bool IsAdr { get; set; }
        public bool IsLift { get; set; }
        public bool IsGps { get; set; }
        public int TruckType { get; set; }
        //TruckType truckType;
        //[Association("TruckType-Trucks")]
        //public TruckType TruckType
        //{
        //    get => truckType;
        //    set => SetPropertyValue(nameof(TruckType), ref truckType, value);
        //}

        
        Company company;

        [Association("Company-Trucks")]
        public Company Company
        {
            get => company;
            set => SetPropertyValue(nameof(Company), ref company, value);
        }

    }

}