using Heeelp.Core.Common;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using Heeelp.Core.Process.Event.User;
using Heeelp.Core.Logging;
using System;
using System.Net.Http;

namespace Heeelp.Core.ProcessManager.EventHandlers.User
{
    public class UserCreatedNotificationEventHandler : IEventHandler<UserCreatedNotificationEvent>
    {
        public UserCreatedNotificationEventHandler()
        {

        }

        public void Handle(UserCreatedNotificationEvent @event)
        {
            //Call Notification, create new user
            var _clientNotification = new HttpClient();
            _clientNotification.BaseAddress = new Uri(CustomConfiguration.WebApiNotification);
            var uriNotification = "api/User/AddUser";
            HttpResponseMessage responseUser = _clientNotification.PostAsJsonAsync(uriNotification, @event).Result;

            if (!responseUser.IsSuccessStatusCode)
            {
                LogManager.Error(string.Format("Call WebApiNotification new User Fail User: {0}", @event));
                throw new Exception();
            }
        }
    }
}
