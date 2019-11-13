using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using CUFE.Models;

public static class XpoHelper
{
    public static Session GetNewSession()
    {
        return new Session(DataLayer);
    }

    public static UnitOfWork GetNewUnitOfWork()
    {
        return new UnitOfWork(DataLayer);
    }

    private readonly static object lockObject = new object();

    static volatile IDataLayer fDataLayer;
    static IDataLayer DataLayer
    {
        get
        {
            if (fDataLayer == null)
            {
                lock (lockObject)
                {
                    if (fDataLayer == null)
                    {
                        fDataLayer = GetDataLayer();
                    }
                }
            }
            return fDataLayer;
        }
    }

    public static IDataLayer GetDataLayer()
    {
        XpoDefault.Session = null;
        //string conn = MySqlConnectionProvider.GetConnectionString("localhost", "root", "", "CUFE");
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        XPDictionary dict = new ReflectionDictionary();
        IDataStore store = XpoDefault.GetConnectionProvider(conn, AutoCreateOption.DatabaseAndSchema);
        DevExpress.Xpo.Metadata.ReflectionClassInfo.SuppressSuspiciousMemberInheritanceCheck = true;
        //dict.GetDataStoreSchema(typeof(Inscripcion).Assembly);
        IDataLayer dl = new ThreadSafeDataLayer(dict, store);
        //XpoDefault.DataLayer = XpoDefault.GetDataLayer(conn, AutoCreateOption.DatabaseAndSchema);
        //using (UnitOfWork uow = new UnitOfWork(dl))
        //{
        //    int cnt = (int)uow.Evaluate<CUFE.Models.XpoApplicationRole>(CriteriaOperator.Parse("count"), null);
        //    if (cnt == 0)
        //    {
        //        new XpoApplicationRole(uow)
        //        {
        //            Name = "SuperAdmin"
        //        };
        //        new XpoApplicationRole(uow)
        //        {
        //            Name = "Admin"
        //        };
        //        new XpoApplicationRole(uow)
        //        {
        //            Name = "User"
        //        };
        //        uow.CommitChanges();
        //    }
        //    cnt = (int)uow.Evaluate<CUFE.Models.XpoApplicationUser>(CriteriaOperator.Parse("count"), null);
        //    if (cnt == 0)
        //    {
        //        var user = new XpoApplicationUser(uow)
        //        {
        //            Email = "i.munawer@abona-erp.com",
        //            EmailConfirmed = true
        //        };
        //        uow.CommitChanges();
        //    }
        //}
        return dl;
    }
}