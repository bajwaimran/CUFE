using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using DevExpress.Xpo.Metadata;
using CUFE.Models;
using DevExpress.Xpo.DB;

namespace CUFE.Helpers
{
    public class XpoHelper
    {
        static readonly Type[] entityTypes = new Type[] {
          typeof(XPObjectType),
          typeof(Company),
          typeof(RegistrationMessage),
          typeof(Truck),
          typeof(Country),
        };

        private const string ConnectionStringName = "FreightExchangeConnectionXPO";
        //string connectionString = ConfigurationSettings.AppSettings.Get("FreightExchangeConnectionXPO");
        private static string pooledConnectionString = XpoDefault.GetConnectionPoolString(ConnectionStringName);
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
                            fDataLayer = GetDataLayer(pooledConnectionString);
                        }
                    }
                }
                return fDataLayer;
            }
        }

        private static ThreadSafeDataLayer GetDataLayer(string connectionString)
        {
            var dictionary = PrepareDictionary();

            using (var updateDataLayer = XpoDefault.GetDataLayer(connectionString, dictionary, AutoCreateOption.DatabaseAndSchema))
            {
                updateDataLayer.UpdateSchema(false, dictionary.CollectClassInfos(entityTypes));
            }

            string pooledConnectionString = XpoDefault.GetConnectionPoolString(connectionString);
            var dataStore = XpoDefault.GetConnectionProvider(pooledConnectionString, AutoCreateOption.SchemaAlreadyExists);
            var dataLayer = new ThreadSafeDataLayer(dictionary, dataStore); ;
            return dataLayer;
        }
        static XPDictionary PrepareDictionary()
        {
            var dict = new ReflectionDictionary();
            dict.GetDataStoreSchema(entityTypes);
            return dict;
        }

    }
}