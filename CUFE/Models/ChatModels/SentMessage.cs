﻿using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models.ChatModels
{
    public class SentMessage : BasePersistentObject
    {
   
        public SentMessage(Session session): base(session) { }
        
        string text;
        string receiverId;
        DateTime timestamp;
        

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ReceiverId
        {
            get => receiverId;
            set => SetPropertyValue(nameof(ReceiverId), ref receiverId, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Text
        {
            get => text;
            set => SetPropertyValue(nameof(Text), ref text, value);
        }

        public DateTime Timestamp
        {
            get => timestamp;
            set => SetPropertyValue<DateTime>(nameof(Timestamp), ref timestamp, value);
        }

        string user;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string User
        {
            get => user;
            set => SetPropertyValue(nameof(User), ref user, value);
        }


        XpoApplicationUser applicationUser;
        [Association("XpoApplicationUser-SentMessages")]
        public XpoApplicationUser ApplicationUser
        {
            get => applicationUser;
            set => SetPropertyValue(nameof(ApplicationUser), ref applicationUser, value);
        }

    }
}