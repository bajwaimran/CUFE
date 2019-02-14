using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CUFE.Models;
using DevExpress.Xpo;
using DX.Data.Xpo.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CUFE.Helpers
{
    public static class DefaultData
    {
        public static void GenerateRoles()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var roles = unitOfWork.Query<CUFE.Models.XpoApplicationRole>();
                if (roles.Count() == 0)
                {
                    new CUFE.Models.XpoApplicationRole(unitOfWork)
                    {
                        Name = "SuperAdmin"
                    };
                    new CUFE.Models.XpoApplicationRole(unitOfWork)
                    {
                        Name = "Admin"
                    };
                    new CUFE.Models.XpoApplicationRole(unitOfWork)
                    {
                        Name = "User"
                    };
                    new CUFE.Models.XpoApplicationRole(unitOfWork)
                    {
                        Name = "Driver"
                    };
                    unitOfWork.CommitChanges();

                }
            }
        
         }
           
        public static void GenerateSuperUser()
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork())
            {
                var users = _unitOfWork.Query<CUFE.Models.XpoApplicationUser>();

                if (users == null)
                {
                    var user = new XpoApplicationUser(_unitOfWork)
                    {
                        UserName = "imran@abona.com",
                        Email = "imran@abona.com",
                        EmailConfirmed = true,
                        FirstName = "Imran",
                        LastName = "Bajwa"
                    };
                    _unitOfWork.CommitChanges();

                }
            }
            
        }
            
            
            
    }
}