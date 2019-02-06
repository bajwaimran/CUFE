using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models.ChatModels
{
    public class GroupChatMessage : BasePersistentObject
    {
        public GroupChatMessage(Session session ): base(session) { }

        
        string senderUserName;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string SenderUserName
        {
            get => senderUserName;
            set => SetPropertyValue(nameof(SenderUserName), ref senderUserName, value);
        }

        string message;
        [Size(2000)]
        public string Message
        {
            get => message;
            set => SetPropertyValue(nameof(Message), ref message, value);
        }


        DateTime timestamp;
        public DateTime Timestamp
        {
            get => timestamp;
            set => SetPropertyValue(nameof(Timestamp), ref timestamp, value);
        }

        GroupChat groupChat;
        [Association("GroupChat-GroupChatMessages")]
        public GroupChat GroupChat
        {
            get => groupChat;
            set => SetPropertyValue(nameof(GroupChat), ref groupChat, value);
        }
    }
}