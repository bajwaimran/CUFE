using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models.ChatModels
{
    public class GroupChat : BasePersistentObject
    {
        public GroupChat(Session session) : base(session) { }
        string roomName;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string RoomName
        {
            get => roomName;
            set => SetPropertyValue(nameof(RoomName), ref roomName, value);
        }

        [Association("ChatUser-GroupChats")]
        public XPCollection<ChatUser> ChatUsers
        {
            get
            {
                return GetCollection<ChatUser>("ChatUsers");
            }
        }

        [Association("GroupChat-GroupChatMessages")]
        public XPCollection<GroupChatMessage> GroupChatMessages
        {
            get
            {
                return GetCollection<GroupChatMessage>("GroupChatMessages");
            }
        }
    }
}