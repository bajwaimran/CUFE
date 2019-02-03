using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace CUFE.Helpers
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            throw new NotImplementedException();
            //return request.User?.FindFirst(ClaimTypes.Email)?.Value:
        }
    }
}