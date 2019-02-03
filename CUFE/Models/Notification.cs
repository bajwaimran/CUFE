using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models
{
    public class Notification: BasePersistentObject
    {
        public Notification(Session session): base(session) { }

        string notificationText;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string NotificationText
        {
            get => notificationText;
            set => SetPropertyValue(nameof(NotificationText), ref notificationText, value);
        }

        DateTime timestamp;
        public DateTime Timestamp
        {
            get => timestamp;
            set => SetPropertyValue(nameof(Timestamp), ref timestamp, value);
        }
    }
}