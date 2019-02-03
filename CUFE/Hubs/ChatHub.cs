using CUFE.Models;
using CUFE.Models.ChatModels;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CUFE.Hubs
{
    public class ChatHub : Hub
    {
        public static Dictionary<string, string> lstAllConnections = new Dictionary<string, string>();
        public static List<ConnectionModel> listAllConnections = new List<ConnectionModel>();
        public void SendChatMessage(string who, string message)
        {
            var name = Context.User.Identity.Name;
            var id = Context.User.Identity.GetUserId();
            using (UnitOfWork uow = new UnitOfWork())
            {
                //var user = db.Users.Find(who);
                var user = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("Id==?", id));
                if (user == null)
                {
                    Clients.Caller.showErrorMessage("Could not find that user.");
                }
                else
                {
                    //db.Entry(user)
                    //    .Collection(u => u.Connections)
                    //    .Query()
                    //    .Where(c => c.Connected == true)
                    //    .Load();




                    if (user.Connections == null)
                    {
                        Clients.Caller.showErrorMessage("The user is no longer connected.");
                    }
                    else
                    {
                        SendMessage(who, message);
                        foreach (var connection in user.Connections)
                        {
                            // sending chat messsage with email id
                            //Clients.Client(connection.ConnectionID)
                            //    .addChatMessage(id, name + ": " + message);

                            //sending chat message without email id
                            Clients.Client(connection.ConnectionId)
                                .addChatMessage(id,  message);

                        }
                    }
                }
            }
        }

        public void SendGroupChatMessage(string who, string message)
        {

        }
        public override Task OnConnected()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                //var x = db.Users.Find(Context.User.Identity.GetUserId());
                var x = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("Id==?", Context.User.Identity.GetUserId()));
                if (x != null)
                {
                    var newOnlineUser = new ConnectionModel
                    {
                        ConnectionId = Context.ConnectionId,
                        UserId = x.Id,
                        Username = x.FirstName
                    };
                    var usr = listAllConnections.Find(u => u.UserId == x.Id);
                    if (usr != null)
                    {
                        listAllConnections.Remove(usr);
                        listAllConnections.Add(newOnlineUser);
                    }
                    else
                    {
                        listAllConnections.Add(newOnlineUser);
                    }
                }

            }

            //string username = Context.User.Identity.GetUserName();
            string username = Context.User.Identity.GetUserName();
            lstAllConnections.Add(Context.ConnectionId, username);
            //Clients.All.BroadcastConnections(lstAllConnections);
            Clients.All.BroadcastConnections(listAllConnections);
            //return base.OnConnected();

            var name = Context.User.Identity.Name;
            using (UnitOfWork uow = new UnitOfWork())
            {
                //var user = db.Users
                //    .Include(u => u.Connections)
                //    .SingleOrDefault(u => u.UserName == name);

                var user = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("UserName==?", name));

                if (user == null)
                {
                    //all users must be registered users
                    //user = new ApplicationUser
                    //{
                    //    UserName = name,
                    //    Connections = new List<Connection>()
                    //};
                    //db.Users.Add(user);
                }
                else
                {
                    var newConnection = new Connection(uow)
                    {
                        ConnectionId = Context.ConnectionId,
                        UserAgent = Context.Request.Headers["User-Agent"],
                        Connected = true
                    };
                    user.Connections.Add(newConnection);

                    uow.CommitChanges();
                }


            }
            return base.OnConnected();
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            var x = listAllConnections.Where(u => u.ConnectionId == Context.ConnectionId).FirstOrDefault();
            if (x != null)
            {
                listAllConnections.Remove(x);
            }
            //listAllConnections.Remove()
            lstAllConnections.Remove(Context.ConnectionId);
            //Clients.All.BroadcastConnections(lstAllConnections);
            Clients.All.BroadcastConnections(listAllConnections);
            using (UnitOfWork uow = new UnitOfWork())
            {
                var connection = uow.FindObject<Connection>(CriteriaOperator.Parse("ConnectionId==?", Context.ConnectionId));
                connection.Connected = false;
                connection.Save();
            }
            return base.OnDisconnected(stopCalled);
        }

        public void SetConnectionName(string connectionname)
        {
            lstAllConnections[Context.ConnectionId] = connectionname;
            Clients.All.BroadcastConnections(lstAllConnections);
        }
        //Broadcast Message from Admin
        public void BroadcastMessage(string message)
        {

            Clients.All.showBroadcastMessage(message);
            using (UnitOfWork uow = new UnitOfWork())
            {
                var notification = new Notification(uow)
                {
                    NotificationText = message,
                    Timestamp = DateTime.Now
                };
                uow.CommitChanges();

            }
        }


        // custome function 
        public void SendMessage(string receiverId, string message)
        {
            string senderId = Context.User.Identity.Name;
            DateTime currentTime = DateTime.Now;
            using (UnitOfWork uow = new UnitOfWork())
            {
                //var sender = db.Users
                //    .Include(u => u.SentMessages)
                //    .Include(u => u.ReceivedMessages)
                //    .SingleOrDefault(u => u.UserName == senderId);

                var sender = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("UserName==?", senderId));

                //var receiver = db.Users
                //    .Include(u => u.SentMessages)
                //    .Include(u => u.ReceivedMessages)
                //    .SingleOrDefault(u => u.Id == receiverId);

                var receiver = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("UserName==?", receiverId));

                if (sender != null && receiver != null)
                {
                    try
                    {
                        var nmsg = new SentMessage(uow)
                        {

                            ReceiverId = receiver.Id,
                            Text = message,
                            Timestamp = currentTime,
                            User = sender.Id
                        };
                        sender.SentMessages.Add(nmsg);
                        uow.CommitChanges();
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.Message);
                    }
                    
                }
            }

            
        }

        //create new group
        public async Task CreateGroup(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);
            Clients.All.showBroadcastMessage("{groupName} is added!");
            //await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }    

        
    }
   
   

}