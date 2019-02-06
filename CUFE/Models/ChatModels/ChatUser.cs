using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models.ChatModels
{
    public class ChatUser : BasePersistentObject
    {
        public ChatUser(Session session): base(session) { }
        
        
        string userName;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string UserName
        {
            get => userName;
            set => SetPropertyValue(nameof(UserName), ref userName, value);
        }


        [Association("ChatUser-GroupChats")]
        public XPCollection<GroupChat> Rooms
        {
            get
            {
                return GetCollection<GroupChat>("Rooms");
            }
        }





    }
}