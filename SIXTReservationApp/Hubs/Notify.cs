using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SIXTReservationApp.Hubs
{
    public class Notify : Hub
    {

        private static readonly Dictionary<int, List<string>> Connections;

        static Notify()
        {
            Connections = new Dictionary<int, List<string>>();
        }

        public override Task OnConnectedAsync()
        {
            var userId = GetLoggedUserId();
            if (userId != 0)
            {
                if (Connections.ContainsKey(userId))
                {
                    Connections[userId].Add(Context.ConnectionId);
                }
                else
                {
                    Connections[userId] = new List<string> { Context.ConnectionId };
                }
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userId = GetLoggedUserId();
            if (userId != 0)
            {
                if (Connections.ContainsKey(userId))
                {
                    Connections[userId]?.Remove(Context.ConnectionId);
                }
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task UpdateUnseenCount(int receiver, int count)
        {
             var clients = GetClientsByUserId(receiver);
            if (clients != null)
            {
                await clients.SendAsync("UpdateUnseenCount", count);
            }

        }

        public async Task SendNotification(int receiver, string message)
        {
            var clients = GetClientsByUserId(receiver);
            if (clients != null)
            {
                await clients.SendAsync("newNotification", message);
            }
        }

        private int GetLoggedUserId()
        {
            try
            {
                var identity = (ClaimsIdentity)Context.User.Identity;
                return Convert.ToInt32(identity.FindFirst("UserId")?.Value ?? "1");

            }
            catch
            {
                return 1;
            }
        }

        private IClientProxy GetClientsByUserId(int userId)
        {
            List<string> connectionIds;
            Connections.TryGetValue(userId, out connectionIds);
            if (connectionIds?.Count > 0)
            {
                return Clients.Clients(connectionIds);
            }
            else
            {
                return null;
            }
        }

    }
}
