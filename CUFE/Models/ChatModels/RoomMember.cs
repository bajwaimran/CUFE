using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models.ChatModels
{
    public class RoomMember : BasePersistentObject
    {
        public RoomMember(Session session) : base(session) { }
        Room room;

        [Association("Room-RoomMembers")]
        public Room Room
        {
            get => room;
            set => SetPropertyValue(nameof(Room), ref room, value);
        }

        string user;
        public string User
        {
            get => user;
            set => SetPropertyValue(nameof(User), ref user, value);

        }
    }
}