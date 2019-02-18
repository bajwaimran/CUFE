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

        string regNo;
        [Indexed(Unique = true)]
        public string RegNo {
            get => regNo;
            set => SetPropertyValue(nameof(RegNo), ref regNo, value);
        }        
        public decimal Capacity { get; set; }

        bool? isFrigo;
     
        public bool? IsFrigo {
            get => isFrigo;
            set => SetPropertyValue(nameof(IsFrigo), ref isFrigo, value);
        }

        bool? isisoThermal;
        public bool? IsisoThermal {
            get => isisoThermal;
            set => SetPropertyValue(nameof(IsisoThermal), ref isisoThermal, value);
        }
        bool? isAdr;
        public bool? IsAdr {
            get => isAdr;
            set  => SetPropertyValue(nameof(IsAdr), ref isAdr, value);
        }
        bool? isLift;
        public bool? IsLift {
            get => isLift;
            set => SetPropertyValue(nameof(IsLift), ref isLift, value);
        }
        bool? isGps;
        public bool? IsGps {
            get => isGps;
            set => SetPropertyValue(nameof(IsGps), ref isGps, value);
        }

        public int TruckType {
            get;
            set;
        }

        decimal width;
        decimal height;
        decimal length;
        public decimal Width
        {
            get => width;
            set => SetPropertyValue(nameof(Width), ref width, value);
        }
        public decimal Height
        {
            get => height;
            set => SetPropertyValue(nameof(Height), ref height, value);
        }
        public decimal Length
        {
            get => length;
            set => SetPropertyValue(nameof(Length), ref length, value);
        }
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