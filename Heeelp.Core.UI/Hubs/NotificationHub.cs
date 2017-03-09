using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Net.Http;
using Heeelp.Core.Common;
using System.Net;

namespace Heeelp.Core.UI.Hubs
{
    public class NotificationHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
        //private readonly static ConnectionMapping<string> _connections =
        //    new ConnectionMapping<string>();

        //public void SendChatMessage(string who, string message)
        //{
        //    string name = Context.User.Identity.Name;

        //    foreach (var connectionId in _connections.GetConnections(who))
        //    {
        //        Clients.Client(connectionId).addChatMessage(name + ": " + message);
        //    }
        //}
        public void SendClientNotification(string ClientID, string message)
        {
            Clients.Client(ClientID).addNewMessageToPage(message);
        }
        public void SendAllClientNotification(string userId, string message)
        {
            var UserSessionList = GetActiveUserSessions(userId);
            foreach (var item in UserSessionList)
            {
                SendClientNotification(item, message);
            }

        }


        public override Task OnConnected()
        {
            string UserSessionID = Context.User.Identity.Name;
            AddUserSessionClientID(UserSessionID, Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;
            DeactivateSession(name);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string UserSessionID = Context.User.Identity.Name;
            AddUserSessionClientID(UserSessionID, Context.ConnectionId);
            return base.OnReconnected();
        }

        public void AddUserSessionClientID(string UserSessionID, string ClientID)
        {
            string name = Context.User.Identity.Name;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(CustomConfiguration.WebApiContab);
            var userSessionClient = new
            {
                UserSessionId = UserSessionID,
                ClientApplicationId = (int)GeneralEnumerators.EnumClientApplication.WebBrowser,
                ClientID = ClientID
            };

            var response = client.PostAsJsonAsync("api/UserSession/SetUserSessionClientID", userSessionClient).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var res = response.Content.ReadAsAsync<dynamic>().Result;
            }

        }
        public void DeactivateSession(string UserSessionId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(CustomConfiguration.WebApiContab);
            var response = client.GetAsync(string.Format("api/UserSession/SetDeactiveClientId?userSessionId={0}", UserSessionId)).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var res = response.Content.ReadAsAsync<dynamic>().Result;
            }
        }

        // pega todas as sessoes abertas do usuario
        public List<string> GetActiveUserSessions(string UserID)
        {
            List<string> UserSessionList = new List<string>();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(CustomConfiguration.WebApiContab);
            var response = client.GetAsync(string.Format("api/UserSession/GetHubSessions?userID={0}", UserID)).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                dynamic res = response.Content.ReadAsAsync<dynamic>().Result;
                if (res != null)
                    foreach (dynamic child in res)
                    {
                        UserSessionList.Add(child.ClientId.ToString());
                    }
            }
            return UserSessionList;

        }

        //Pega uma sessao a partir do user session
        public void GetUserSessions(string UserSessionID)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(CustomConfiguration.WebApiContab);
            var response = client.PostAsJsonAsync("api/UserSession/GetUserSessions", new { UserSessionId = UserSessionID }).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var res = response.Content.ReadAsAsync<dynamic>().Result;
            }
        }


    }
}