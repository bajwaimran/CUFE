using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
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

        public override void Assign(object source, int loadingFlags)
        {
            base.Assign(source, loadingFlags);
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
                // etc.				
            }
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