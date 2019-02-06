using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using DevExpress.Data.Filtering;
using Microsoft.AspNet.Identity;
using CUFE.Models;
using CUFE.Models.ChatModels;

namespace CUFE.Hubs
{
    public class GroupChatHub : Hub
    {
        public static Dictionary<string, string> lstAllConnections = new Dictionary<string, string>();
        public static Dictionary<string, string> groupNames = new Dictionary<string, string>();

        public static List<UserConnections> ConnectionList = new List<UserConnections>();
        public static List<ConnectionList> listAllConnections = new List<ConnectionList>();

        
        public override Task OnConnected()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var user = uow.FindObject<XpoApplicationUser>(CriteriaOperator.Parse("UserName==?", Context.User.Identity.Name));
                if (user != null)
                {
                    var con = new UserConnections
                    {
                        ConnectionId = Context.ConnectionId,
                        UserAgent = Context.Request.Headers["User-Agent"],
                        Connected = true,
                        UserId = user.Id
                    };
                    ConnectionList.Add(con);                    
                }
                return base.OnConnected();
            }
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var con = ConnectionList.Find(u => u.ConnectionId == Context.ConnectionId);
                if(con != null)
                {
                    ConnectionList.Remove(con);
                }
            }
            return base.OnDisconnected(stopCalled);
        }

       


        public Task SendPrivateMessage(string user, string message)
        {
            return Clients.User(user).SendAsync("ReceiveMessage", message);
        }


        //override functions
        public void SetConnectionName(string connectionname)
        {
            lstAllConnections[Context.ConnectionId] = connectionname;
            Clients.All.BroadcastConnections(lstAllConnections);
        }
        
       

        public void SendMessge(string message)
        {
            Clients.All.showBroadcastMessage(message);
        }
        

        public void JoinRoom(string roomName)
        {
            

            using (UnitOfWork uow = new UnitOfWork())
            {
                Groups.Add(Context.ConnectionId, roomName);
                Clients.Group(roomName).showBroadcastMessage(Context.User.Identity.Name + "Joined");

                var room = uow.FindObject<GroupChat>(CriteriaOperator.Parse("RoomName==?", roomName));
                if (room == null)
                {
                    var newRoom = new GroupChat(uow)
                    {
                        RoomName = roomName
                    };
                    var user = new ChatUser(uow)
                    {
                        UserName = Context.User.Identity.Name
                    };
                    newRoom.ChatUsers.Add(user);
                    Groups.Add(Context.ConnectionId, roomName);
                }
                else
                {
                    try
                    {
                        var user = room.ChatUsers.First(u => u.UserName == Context.User.Identity.Name);

                        if (user == null)
                        {
                            room.ChatUsers.Add(new ChatUser(uow)
                            {
                                UserName = Context.User.Identity.Name
                            });
                            Groups.Add(Context.ConnectionId, roomName);
                        }
                    }catch(Exception e)
                    {
                        room.ChatUsers.Add(new ChatUser(uow)
                        {
                            UserName = Context.User.Identity.Name
                        });
                        Groups.Add(Context.ConnectionId, roomName);
                    }
                    
                }
                uow.CommitChanges();
            }
        }

        public void LeaveRoom(string roomName)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Groups.Remove(Context.ConnectionId, roomName);
                var room = uow.FindObject<GroupChat>(CriteriaOperator.Parse("RoomName==?", roomName));
                if(room != null)
                {
                    var user = room.ChatUsers.FirstOrDefault(u => u.UserName == Context.User.Identity.Name);
                    if(user != null)
                    {
                        room.ChatUsers.Remove(user);
                        uow.CommitChanges();
                    }
                }
            }
            
        }

        public void GroupMessage(string groupName, string message)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Clients.Group(groupName).showBroadcastMessage(message);
                var room = uow.FindObject<GroupChat>(CriteriaOperator.Parse("RoomName==?", groupName));
                if(room != null)
                {
                    var msg = new GroupChatMessage(uow)
                    {
                        Message = message,
                        SenderUserName = Context.User.Identity.Name,
                        Timestamp = DateTime.Now
                    };

                    room.GroupChatMessages.Add(msg);
                    uow.CommitChanges();
                }
            }
        }
    }


    
    

}