using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models.ChatModels
{
    public class OnlineUser: BasePersistentObject
    {
        public OnlineUser(Session session): base(session) { }
        string username;
        string connectionId;
        string user;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string User
        {
            get => user;
            set => SetPropertyValue(nameof(User), ref user, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ConnectionId
        {
            get => connectionId;
            set => SetPropertyValue(nameof(ConnectionId), ref connectionId, value);
        }

        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Username
        {
            get => username;
            set => SetPropertyValue(nameof(Username), ref username, value);
        }

    }
}