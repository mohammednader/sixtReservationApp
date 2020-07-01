using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SIXTReservationApp.SignalR
{
    public class NotificationHub : Hub
    {
        private readonly static Dictionary<int, List<string>> Connections;

        static NotificationHub()
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

        private int GetLoggedUserId()
        {
            try
            {
                return Convert.ToInt32(((ClaimsIdentity)Context.User.Identity).FindFirst("UserId")?.Value ?? "1");
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

        public async Task SendNotification(int clientId, string message = "You have new notification")
        {
            var clients = GetClientsByUserId(clientId);
            await clients.SendAsync("newNotification", message);
        }

    }
}
