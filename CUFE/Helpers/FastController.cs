using CUFE.Models;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using CUFE.Models.ChatModels;
using DX.Data.Xpo.Identity;
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
        public static List<XpoApplicationUser> AllUsers()
        {
            UnitOfWork uow = new UnitOfWork();
            return uow.Query<XpoApplicationUser>().ToList();
        }


        public static List<XpoApplicationRole> AllRoles()
        {
            UnitOfWork uow = new UnitOfWork();
            var model = from u in uow.Query<XpoApplicationRole>()
                        where u.Name != "SuperAdmin"
                        select u;
                       
            return model.ToList();
        }

        public static List<Country> AllCountries()
        {
            UnitOfWork uow = new UnitOfWork();
            return uow.Query<Country>().ToList();
        }

        public static List<GroupChat> MyGroups(string username)
        {
            UnitOfWork uow = new UnitOfWork();
            var user = uow.FindObject<ChatUser>(CriteriaOperator.Parse("UserName==?",username));
            if (user != null)
                return user.Rooms.ToList();
            else return null;
        }

        public static Truck TruckInfo(int oid)
        {
            UnitOfWork uow = new UnitOfWork();
            return uow.FindObject<Truck>(CriteriaOperator.Parse("Oid==?", oid));
        }

        
    }
}