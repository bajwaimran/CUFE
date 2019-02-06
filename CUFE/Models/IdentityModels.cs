using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using CUFE.Models.ChatModels;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DX.Data.Xpo.Identity;
using DX.Data.Xpo.Identity.Persistent;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CUFE.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : XPIdentityUser<string, XpoApplicationUser>
    {
        public ApplicationUser() :
        base()
        {
        }
        public ApplicationUser(XpoApplicationUser source) :
        base(source)
        {
        }

        public ApplicationUser(XpoApplicationUser source, int loadingFlags) :
            base(source, loadingFlags)
        {
        }



        //custom properties for ApplicationUser
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public DateTime Birthdate { get; set; }
        public string Languages { get; set; }
        public string Photo { get; set; }
        public string Profile { get; set; }
        public string Notes { get; set; }
        public string Role { get; set; }


        //public XPCollection<Connection> Connections { get; set; }
        public XPCollection<SentMessage> SentMessages { get; set; }
        public override void Assign(object source, int loadingFlags)
        {
            
            XpoApplicationUser src = source as XpoApplicationUser;
            if (src != null)
            {
                // additional properties here
                this.CompanyId = src.CompanyId;
                this.FirstName = src.FirstName;
                this.LastName = src.LastName;

                this.Address1 = src.Address1;
                this.Address2 = src.Address2;
                this.City = src.City;
                this.Province = src.Province;
                this.Country = src.Country;
                this.Birthdate = src.Birthdate;
                this.Languages = src.Languages;
                this.Photo = src.Photo;
                this.Profile = src.Profile;
                this.Notes = src.Notes;
                this.Role = src.Role;
                //this.Connections = src.Connections;
                //this.SentMessages = src.SentMessages;
                // etc.				
            }
            base.Assign(source, loadingFlags);
        }

        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            using (UnitOfWork uow = new UnitOfWork())
            {
                var usr = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("Id ==?", this.Id));
                userIdentity.AddClaim(new Claim("CompanyId", usr.CompanyId.ToString()));
            }
            return userIdentity;
        }
    }

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection", throwIfV1Schema: false)
    //    {
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}

    public class ApplicationDbContext
    {
        public static DX.Data.Xpo.XpoDatabase Create()
        {
            return new DX.Data.Xpo.XpoDatabase("DefaultConnection");
        }
    }

    //new code for xpo identity

    [MapInheritance(MapInheritanceType.ParentTable)]
    public class XpoApplicationUser : XpoDxUser
    {
        public XpoApplicationUser(Session session) : base(session)
        {
        }

        //custom properties for ApplicationUser


        int companyId;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public int CompanyId
        {
            get => companyId;
            set => SetPropertyValue(nameof(CompanyId), ref companyId, value);
        }

        string firstName;
        public string FirstName
        {
            get => firstName;
            set => SetPropertyValue(nameof(FirstName), ref firstName, value);
        }

        string lastName;
        public string LastName
        {
            get => lastName;
            set => SetPropertyValue(nameof(LastName), ref lastName, value);
        }

        string adrress1;
        public string Address1
        {
            get => adrress1;
            set => SetPropertyValue(nameof(Address1), ref adrress1, value);
        }

        string adrress2;
        public string Address2
        {
            get => adrress2;
            set => SetPropertyValue(nameof(Address2), ref adrress2, value);
        }
        
        string city;
        public string City
        {
            get => city;
            set => SetPropertyValue(nameof(City), ref city, value);
        }

        
        string province;
        public string Province
        {
            get => province;
            set => SetPropertyValue(nameof(Province), ref province, value);
        }
        //public string Country { get; set; }
        string country;
        public string Country
        {
            get => country;
            set => SetPropertyValue(nameof(Country), ref country, value);
        }
        //public DateTime Birthdate { get; set; }
        DateTime birthdate;
        public DateTime Birthdate
        {
            get => birthdate;
            set => SetPropertyValue(nameof(Birthdate), ref birthdate, value);
        }
        //public string Languages { get; set; }
        string languages;
        public string Languages
        {
            get => languages;
            set => SetPropertyValue(nameof(Languages), ref languages, value);
        }
        //public string Photo { get; set; }
        string photo;
        public string Photo
        {
            get => photo;
            set => SetPropertyValue(nameof(Photo), ref photo, value);
        }
        //public string Profile { get; set; }
        string profile;
        public string Profile
        {
            get => profile;
            set => SetPropertyValue(nameof(Profile), ref profile, value);
        }
        //public string Notes { get; set; }
        string notes;
        public string Notes
        {
            get => notes;
            set => SetPropertyValue(nameof(Notes), ref notes, value);
        }


        string role;
        public string Role
        {
            get => role;
            set => SetPropertyValue(nameof(Role), ref role, value);
        }


        //XPCollection<Connection> connections;

        //[Association("XpoApplicationUser-Connections")]
        //public XPCollection<Connection> Connections
        //{
        //    get
        //    {
        //        return GetCollection<Connection>("Connections");
        //    }
        //   set => SetPropertyValue(nameof(Connections), ref connections, value);
        //}



        XPCollection<SentMessage> sentMessages;
        [Association("XpoApplicationUser-SentMessages")]
        public XPCollection<SentMessage> SentMessages
        {
            get
            {
                return GetCollection<SentMessage>("SentMessages");
            }
            set => SetPropertyValue(nameof(SentMessage), ref sentMessages, value);


        }
        public override void Assign(object source, int loadingFlags)
        {
            base.Assign(source, loadingFlags);
            ApplicationUser src = source as ApplicationUser;
            if (src != null)
            {
                // additional properties here
                this.CompanyId = src.CompanyId;
                this.FirstName = src.FirstName;
                this.LastName = src.LastName;

                this.Address1 = src.Address1;
                this.Address2 = src.Address2;
                this.City = src.City;
                this.Province = src.Province;
                this.Country = src.Country;
                this.Birthdate = src.Birthdate;
                this.Languages = src.Languages;
                this.Photo = src.Photo;
                this.Profile = src.Profile;
                this.Notes = src.Notes;
                this.Role = src.Role;
                //this.Connections = src.Connections;
                this.SentMessages = src.SentMessages;
                this.Notes = src.Notes;
            }
        }
    }

    public class ApplicationRole : XPIdentityRole<XpoApplicationRole>
    {
        public ApplicationRole(XpoApplicationRole source, int loadingFlags) : base(source, loadingFlags)
        { }

        public ApplicationRole(XpoApplicationRole source) : base(source)
        { }

        public ApplicationRole()
        { }
        public override void Assign(object source, int loadingFlags)
        {
            base.Assign(source, loadingFlags);
            //XpoApplicationRole src = source as XpoApplicationRole;
            //if (src != null)
            //{
            //  // additional properties here
            //  this.PropertyA = src.PropertyA;
            //  // etc.             
            //}
        }
    }


    [MapInheritance(MapInheritanceType.ParentTable)]
    public class XpoApplicationRole : XpoDxRole
    {
        public XpoApplicationRole(Session session) : base(session)
        {
        }
        public override void Assign(object source, int loadingFlags)
        {
            base.Assign(source, loadingFlags);
            //ApplicationUser src = source as ApplicationUser;
            //if (src != null)
            //{
            //  // additional properties here
            //  this.PropertyA = src.PropertyA;
            //  // etc.             
            //}
        }

        public static void AllRoles()
        {

        }
    }


}