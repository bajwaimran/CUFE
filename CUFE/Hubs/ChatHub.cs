using DevExpress.Xpo;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using DevExpress.Data.Filtering;
using CUFE.Models;
using CUFE.Models.ChatModels;
using System.Collections.Generic;
using System;
using CUFE.Controllers;

namespace CUFE.Hubs
{
    public class ChatHub : Hub
    {
        public static List<ConnectionList> listAllConnections = new List<ConnectionList>();
        public static List<UserConnections> ConnectionList = new List<UserConnections>();
        public override Task OnConnected()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var user = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("UserName==?", Context.User.Identity.Name));
                if(user != null)
                {
                    var con = new UserConnections
                    {
                        ConnectionId = Context.ConnectionId,
                        UserAgent = Context.Request.Headers["User-Agent"],
                        Connected = true,
                        UserId = user.Id
                    };
                    ConnectionList.Add(con);
                    var userInList = listAllConnections.Find(u => u.UserId == user.Id);
                    if(userInList == null)
                    {
                        listAllConnections.Add(new ConnectionList
                        {
                            UserId = user.Id,
                            Username = user.UserName
                        });
                    }
                    Clients.All.BroadcastConnections(listAllConnections);
                }                
                return base.OnConnected();
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var user = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("UserName==?", Context.User.Identity.Name));
                var connection = uow.FindObject<Connection>(CriteriaOperator.Parse("ConnectionId==?", Context.ConnectionId));
                if(connection != null)
                {
                    uow.Delete(connection);
                    uow.CommitChanges();
                    var userInList = listAllConnections.Find(u => u.UserId == user.Id);
                    if (userInList != null)
                    {
                        listAllConnections.Remove(userInList);
                    }
                    var con = ConnectionList.Find(u => u.ConnectionId == Context.ConnectionId);
                    if(con != null)
                    {
                        ConnectionList.Remove(con);
                    }
                    Clients.All.BroadcastConnections(listAllConnections);
                }
                return base.OnDisconnected(stopCalled);
            }
            
        }

        public void SendChatMessage(string who, string message)
        {
            
            using (UnitOfWork uow = new UnitOfWork())
            {
                var user = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("UserName==?", Context.User.Identity.Name));
                var receiver = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("Id==?", who));
                if(receiver != null)
                {
                    var msg = new SentMessage(uow)
                    {
                        Text = message,
                        ReceiverId = receiver.Id,
                        User = user.Id,                        
                        Timestamp = DateTime.Now
                    };
                    user.SentMessages.Add(msg);
                    try
                    {
                        uow.CommitChanges();
                    }
                    catch(Exception e)
                    {
                        var msfg  = e.Message;
                    }
                    var connections = ConnectionList.FindAll(u => u.UserId == receiver.Id);
                    if(connections != null)
                    {
                        foreach (var con in connections)
                        {
                            Clients.Client(con.ConnectionId).addChatMessage(user.Id, message);
                        }
                    }
                }
                
                
            }
        }
    }


    public class ConnectionList
    {        
        public string UserId { get; set; }
        public string Username { get; set; }
    }


    public class UserConnections
    {
        public string ConnectionId { get; set; }
        public string UserId { get; set; }
        public string UserAgent { get; set; }
        public bool Connected { get; set; }
    }
}