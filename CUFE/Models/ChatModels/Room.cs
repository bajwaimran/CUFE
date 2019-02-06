using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models.ChatModels
{
    public class Room : BasePersistentObject
    {
        public Room(Session session) : base(session) { }

        Company company;
        string roomName;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string RoomName
        {
            get => roomName;
            set => SetPropertyValue(nameof(RoomName), ref roomName, value);
        }


        [Association("Company-Rooms")]
        public Company Company
        {
            get => company;
            set => SetPropertyValue(nameof(Company), ref company, value);
        }

        XPCollection<RoomMember> members;
        [Association("Room-RoomMembers")]
        public XPCollection<RoomMember> Members
        {
            get
            {
                return GetCollection<RoomMember>("Members");
            }
            set
            {
                SetPropertyValue("Members", ref members, value);
            }
        }
    }
}