using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;
using FreightExchangeDemo;

namespace CUFE.Models
{
    public class Order: BasePersistentObject
    {
        public Order(Session session): base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
        private int _MandantOid;
        public int MandantOid
        {
            get { return _MandantOid; }
            set { SetPropertyValue<int>("MandantOid", ref _MandantOid, value); }
        }

        private string _MandantName;
        [Size(80)]
        public string MandantName
        {
            get { return _MandantName; }
            set { SetPropertyValue<string>("MandantName", ref _MandantName, value); }
        }

        private int _OrderOid;
        public int OrderOid
        {
            get { return _OrderOid; }
            set { SetPropertyValue<int>("OrderOid", ref _OrderOid, value); }
        }

        private int _OrderNoCw; //AuftragsNrKW
        public int OrderNoCw
        {
            get { return _OrderNoCw; }
            set { SetPropertyValue<int>("OrderNoCw", ref _OrderNoCw, value); }
        }

        private DateTime _OrderDate;
        public DateTime OrderDate
        {
            get { return _OrderDate; }
            set { SetPropertyValue<DateTime>("OrderDate", ref _OrderDate, value); }
        }

        private string _CustomerName;
        [Size(80)]
        public string CustomerName
        {
            get { return _CustomerName; }
            set { SetPropertyValue<string>("CustomerName", ref _CustomerName, value); }
        }

        private DateTime _FromDate;
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { SetPropertyValue<DateTime>("FromDate", ref _FromDate, value); }
        }

        private string _FromNation;
        [Size(4)]
        public string FromNation
        {
            get { return _FromNation; }
            set { SetPropertyValue<string>("FromNation", ref _FromNation, value); }
        }

        private string _FromZip;
        [Size(10)]
        public string FromZip
        {
            get { return _FromZip; }
            set { SetPropertyValue<string>("FromZip", ref _FromZip, value); }
        }

        private string _FromCity;
        [Size(60)]
        public string FromCity
        {
            get { return _FromCity; }
            set { SetPropertyValue<string>("FromCity", ref _FromCity, value); }
        }

        private DateTime _ToDate;
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { SetPropertyValue<DateTime>("ToDate", ref _ToDate, value); }
        }

        private string _ToNation;
        [Size(4)]
        public string ToNation
        {
            get { return _ToNation; }
            set { SetPropertyValue<string>("ToNation", ref _ToNation, value); }
        }

        private string _ToZip;
        [Size(10)]
        public string ToZip
        {
            get { return _ToZip; }
            set { SetPropertyValue<string>("ToZip", ref _ToZip, value); }
        }

        private string _ToCity;
        [Size(60)]
        public string ToCity
        {
            get { return _ToCity; }
            set { SetPropertyValue<string>("ToCity", ref _ToCity, value); }
        }

        private decimal _Price;
        [Size(60)]
        public decimal Price
        {
            get { return _Price; }
            set { SetPropertyValue<decimal>("Price", ref _Price, value); }
        }

        private decimal _RecommandedPrice;
        [Size(60)]
        public decimal RecommandedPrice
        {
            get { return _RecommandedPrice; }
            set { SetPropertyValue<decimal>("RecommandedPrice", ref _RecommandedPrice, value); }
        }

        private string _Manager;
        [Size(120)]
        public string Manager
        {
            get { return _Manager; }
            set { SetPropertyValue<string>("Manager", ref _Manager, value); }
        }

        private string _ManagerPhone;
        [Size(60)]
        public string ManagerPhone
        {
            get { return _ManagerPhone; }
            set { SetPropertyValue<string>("ManagerPhone", ref _ManagerPhone, value); }
        }

        private string _ManagereMail;
        [Size(120)]
        public string ManagereMail
        {
            get { return _ManagereMail; }
            set { SetPropertyValue<string>("ManagereMail", ref _ManagereMail, value); }
        }

        private string _KMDistanz;
        [Size(60)]
        public string KMDistanz
        {
            get { return _KMDistanz; }
            set { SetPropertyValue<string>("KMDistanz", ref _KMDistanz, value); }
        }

        private string _freight;
        [Size(100)]
        public string freight
        {
            get { return _freight; }
            set { SetPropertyValue<string>("freight", ref _freight, value); }
        }

        private Enums.OrderFreightExchangeStatus _FreightExchangeStatus;
        public Enums.OrderFreightExchangeStatus FreightExchangeStatus
        {
            get { return _FreightExchangeStatus; }
            set { SetPropertyValue<Enums.OrderFreightExchangeStatus>("FreightExchangeStatus", ref _FreightExchangeStatus, value); }
        }

        private int _MandantTakeOid;
        public int MandantTakeOid
        {
            get { return _MandantTakeOid; }
            set { SetPropertyValue<int>("MandantTakeOid", ref _MandantTakeOid, value); }
        }

        private string _MandantTakeName;
        [Size(80)]
        public string MandantTakeName
        {
            get { return _MandantTakeName; }
            set { SetPropertyValue<string>("MandantTakeName", ref _MandantTakeName, value); }
        }

        private int _OrderTakeOid;
        public int OrderTakeOid
        {
            get { return _OrderTakeOid; }
            set { SetPropertyValue<int>("OrderTakeOid", ref _OrderTakeOid, value); }
        }

        private int _OrderTakeNoCw; //AuftragsNrKW
        public int OrderTakeNoCw
        {
            get { return _OrderTakeNoCw; }
            set { SetPropertyValue<int>("OrderTakeNoCw", ref _OrderTakeNoCw, value); }
        }

        private Enums.Priority _FreightExchangePriority;
        public Enums.Priority FreightExchangePriority
        {
            get { return _FreightExchangePriority; }
            set { SetPropertyValue<Enums.Priority>("FreightExchangePriority", ref _FreightExchangePriority, value); }
        }

        private string _comment;
        [Size(220)]
        public string comment
        {
            get { return _comment; }
            set { SetPropertyValue<string>("comment", ref _comment, value); }
        }

        private int _CountLoadingplace;
        public int CountLoadingplace
        {
            get { return _CountLoadingplace; }
            set { SetPropertyValue<int>("CountLoadingplace", ref _CountLoadingplace, value); }
        }

        private decimal _Loadingmeter;
        public decimal Loadingmeter
        {
            get { return _Loadingmeter; }
            set { SetPropertyValue<decimal>("Loadingmeter", ref _Loadingmeter, value); }
        }

        private string _Trailer;
        [Size(100)]
        public string Trailer
        {
            get { return _Trailer; }
            set { SetPropertyValue<string>("Trailer", ref _Trailer, value); }
        }
    }
}