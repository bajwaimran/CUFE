using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models
{
    public class Lager : BasePersistentObject
    {
        public Lager(Session session): base(session) { }

        int length;
        int height;
        int width;
        double capacity;
        string address;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Address
        {
            get => address;
            set => SetPropertyValue(nameof(Address), ref address, value);
        }

        public double Capacity
        {
            get => capacity;
            set => SetPropertyValue(nameof(Capacity), ref capacity, value);
        }


        public int Width
        {
            get => width;
            set => SetPropertyValue(nameof(Width), ref width, value);
        }



        public int Height
        {
            get => height;
            set => SetPropertyValue(nameof(Height), ref height, value);
        }

        
        public int Length
        {
            get => length;
            set => SetPropertyValue(nameof(Length), ref length, value);
        }

        bool active;
        public bool Active
        {
            get => active;
            set => SetPropertyValue(nameof(Active), ref active, value);
        }

    }
}