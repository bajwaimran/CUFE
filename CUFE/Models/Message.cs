using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models
{
    public class Message : BasePersistentObject
    {
        public Message(Session session): base(session) { }


        string text;
        string email;
        string name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Email
        {
            get => email;
            set => SetPropertyValue(nameof(Email), ref email, value);
        }

        string subject;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Subject
        {
            get => subject;
            set => SetPropertyValue(nameof(Subject), ref subject, value);
        }

        
        public string Text
        {
            get => text;
            set => SetPropertyValue(nameof(Text), ref text, value);
        }
        

        DateTime timestamp;
        public DateTime Timestamp
        {
            get => timestamp;
            set => SetPropertyValue(nameof(Timestamp), ref timestamp, value);
        }
    }
}