using CUFE.Models;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using CUFE.Models.ChatModels;

namespace CUFE.Helpers
{
    public static class FastController
    {
         
        public static List<CUFE.Models.ChatModels.SentMessage> GetAllSentMessagesByUserId(string userId)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {               
                var user = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("Id==?", userId));
                //var sentMessages = user.SentMessages.ToList();
                return null;
            }
            
        }

        public static List<CUFE.Models.ChatModels.SentMessage> GetAllReceivedMessagesByUserId(string userId)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                //var receivedMessages = db.SentMessages.Where(m => m.ReceiverId == userId).ToList();
                var receivedMessages = uow.Query<SentMessage>().Where(u => u.ReceiverId == userId).ToList();
                return receivedMessages;
            }
            
        }

        public static List<TruckType> GetAllTruckTypes()
        {
            UnitOfWork uow = new UnitOfWork();
            
            return uow.Query<TruckType>().ToList();
            
        }

        public static List<LoadType> GetAllLoadTypes()
        {
            UnitOfWork uow = new UnitOfWork();

            return uow.Query<LoadType>().ToList();

        }

        public static List<Truck> GetTrucksByCompanyId(int companyId)
        {
            UnitOfWork uow = new UnitOfWork();
            var company = uow.FindObject<Company>(CriteriaOperator.Parse("Oid==?", companyId));
            return company.Trucks.ToList();

        }


    }
}