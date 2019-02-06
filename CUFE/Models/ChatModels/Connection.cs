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
        
        string connectionId;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ConnectionId
        {
            get => connectionId;
            set => SetPropertyValue(nameof(ConnectionId), ref connectionId, value);
        }

        string userAgent;
        [Size(2000)]
        public string UserAgent
        {
            get => userAgent;
            set => SetPropertyValue(nameof(UserAgent), ref userAgent, value);
        }


        bool connected;
        public bool Connected
        {
            get => connected;
            set => SetPropertyValue<bool>(nameof(Connected), ref connected, value);
        }

        string userId;
        public string UserId
        {
            get => userId;
            set => SetPropertyValue(nameof(UserId), ref userId, value);
        }
        //ChatUser chatUser;
        //[Association("ChatUser-Connections")]       
        //public ChatUser ChatUser
        //{
        //    get => chatUser;
        //    set => SetPropertyValue(nameof(ChatUser), ref chatUser, value);
        //}

        //XpoApplicationUser user;
        //[Association("XpoApplicationUser-Connections")]
        //public XpoApplicationUser User
        //{
        //    get => user;
        //    set => SetPropertyValue<XpoApplicationUser>(nameof(User), ref user, value);
        //}
    }
}