using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Security.Principal;

namespace CUFE.Extensions
{
    public static class IdentityExtentions
    {
        public static string GetCompanyId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("CompanyId");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetCompanyName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("CompanyName");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Name");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUserRole(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserRole");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
