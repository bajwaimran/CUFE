using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace CUFE.Hubs
{
    public class NotificationHub : Hub
    {
        public override Task OnConnected()
        {
            //Clients.All.Notify("Welcome to Freight Exchange notification server");
            return base.OnConnected();
        }
        public void SendNotfication(string message)
        {
            Clients.All.Notify(message);
        }
    }
}