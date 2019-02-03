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

        public override Task OnConnected()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                string username = Context.User.Identity.Name;
                
                lstAllConnections.Add(Context.ConnectionId, username);
                Clients.All.BroadcastConnections(lstAllConnections);
                var user = uow.FindObject<ChatUser>(CriteriaOperator.Parse("UserName==?", username));
                if( user == null)
                {
                    user = new ChatUser(uow)
                    {
                        UserName = username
                    };
                    uow.CommitChanges();
                }
                else
                {
                    foreach(var item in user.Rooms)
                    {
                        Groups.Add(Context.ConnectionId, item.RoomName);
                    }
                }
                
            }
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                string usernameToRemove = Context.User.Identity.GetUserId();
                lstAllConnections.Remove(Context.ConnectionId);
                Clients.All.BroadcastConnections(lstAllConnections);
            }
            return base.OnDisconnected(stopCalled);
        }
        public void AddToRoom(string roomName)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var room = uow.FindObject<GroupChat>(CriteriaOperator.Parse("RoomName==?", roomName));
                if(room == null)
                {
                    var newRoom = new GroupChat(uow)
                    {
                        RoomName = roomName
                    };
                    var user = new ChatUser(uow)
                    {
                        UserName = Context.User.Identity.Name
                    };
                    newRoom.Users.Add(user);
                    uow.CommitChanges();
                    Groups.Add(Context.ConnectionId, roomName);
                }
            }
        }


        public void RemoveFromRoom(string roomName)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var room = uow.FindObject<GroupChat>(CriteriaOperator.Parse("RoomName==?", roomName));
                if(room != null)
                {
                    var userlist = room.Users.Where(u => u.UserName == Context.User.Identity.Name);
                    foreach(var item in userlist)
                    {
                        room.Users.Remove(item);
                    }

                    uow.CommitChanges();
                    Groups.Remove(Context.ConnectionId, roomName);
                }
            }
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
        
        public void AddUserToRoom(string roomName, string userId)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var room = uow.FindObject<GroupChat>(CriteriaOperator.Parse("RoomName==?", roomName));
                if(room != null){
                    var user = room.Users.FirstOrDefault(u => u.UserName == userId);
                    if(user == null)
                    {
                        var newuser = new ChatUser(uow)
                        {
                            UserName = userId
                        };
                        room.Users.Add(newuser);

                        uow.CommitChanges();
                        Clients.Caller.notifications("Added");
                    }
                    Clients.Caller.notifications("user is already a member of current group!");
                }
                //Clients.Caller.notifications("No Room found with given name!");

            }
        }
        
    }
}