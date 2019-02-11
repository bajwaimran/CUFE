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
        public static List<UserConnections> connectionList = new List<UserConnections>();
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
                    connectionList.Add(con);
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
                    var con = connectionList.Find(u => u.ConnectionId == Context.ConnectionId);
                    if(con != null)
                    {
                        connectionList.Remove(con);
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
                    var connections = connectionList.FindAll(u => u.UserId == receiver.Id);
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

        public void SendMessageByEmailId(string email, string message)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var user = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("UserName==?", Context.User.Identity.Name));
                var receiver = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("UserName==?", email));
                if (receiver != null)
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
                    catch (Exception e)
                    {
                        var msfg = e.Message;
                    }
                    var connections = connectionList.FindAll(u => u.UserId == receiver.Id);
                    if (connections != null)
                    {
                        foreach (var con in connections)
                        {
                            Clients.Client(con.ConnectionId).addChatMessage(user.Id, message);
                            Clients.Client(con.ConnectionId).Notify(string.Format("<a href='/Client/Chat'>Message received form {0}</a>", user.FirstName));
                        }
                    }
                    Clients.Client(Context.ConnectionId).Notify("Your Message has been sent!");
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