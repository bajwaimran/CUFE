using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUFE.Models
{
    public class Blog :BasePersistentObject
    {
        public Blog(Session session): base(session) { }

        string title;

        [Size(200)]
        public string Title
        {
            get => title;
            set => SetPropertyValue(nameof(Title), ref title, value);
        }


        string shortDescription;

        [Size(400)]
        public string ShortDescription {
            get => shortDescription;
            set => SetPropertyValue(nameof(ShortDescription), ref shortDescription, value);
        }

        [Size(3000)]
        string article;
        public string Article {
            get => article;
            set => SetPropertyValue(nameof(Article), ref article, value);
        }

        string articleImage;
        [Size(300)]
        public string ArticleImage {
            get => articleImage;
            set => SetPropertyValue(nameof(ArticleImage), ref articleImage, value);
        }
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        string createdBy;
        public string CreatedBy {
            get => createdBy;
            set => SetPropertyValue(nameof(CreatedBy), ref createdBy, value);
        }

        DateTime createdOn;
        public DateTime CreatedOn {
            get => createdOn;
            set => SetPropertyValue(nameof(CreatedOn), ref createdOn, value);
        }
    }
}