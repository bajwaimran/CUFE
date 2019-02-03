using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models.ChatModels
{
    public class Connection: BasePersistentObject
    {
        public Connection(Session session): base(session) { }
        bool connected;
        string userAgent;
        string connectionId;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ConnectionId
        {
            get => connectionId;
            set => SetPropertyValue(nameof(ConnectionId), ref connectionId, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string UserAgent
        {
            get => userAgent;
            set => SetPropertyValue(nameof(UserAgent), ref userAgent, value);
        }

        public bool Connected
        {
            get => connected;
            set => SetPropertyValue(nameof(Connected), ref connected, value);
        }

        ChatUser chatUser;
        [Association("ChatUser-Connections")]       
        public ChatUser ChatUser
        {
            get => chatUser;
            set => SetPropertyValue(nameof(ChatUser), ref chatUser, value);
        }

        XpoApplicationUser user;
        [Association("XpoApplicationUser-Connections")]
        public XpoApplicationUser User
        {
            get => user;
            set => SetPropertyValue(nameof(User), ref user, value);
        }
    }
}