using Heeelp.Core.Common;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using Heeelp.Core.Process.Event.User;
using Heeelp.Core.Logging;
using System;
using System.Net.Http;

namespace Heeelp.Core.ProcessManager.EventHandlers.User
{
    public class UserCreatedContabEventHandler : IEventHandler<UserCreatedContabEvent>
    {
        public UserCreatedContabEventHandler()
        {

        }

        public void Handle(UserCreatedContabEvent @event)
        {
            //Call Account, create new user
            var _clientContab = new HttpClient();
            _clientContab.BaseAddress = new Uri(CustomConfiguration.WebApiContab);
            string uri = "api/User/AddUser";
            HttpResponseMessage responseAccount = _clientContab.PostAsJsonAsync(uri, @event).Result;

            if (!responseAccount.IsSuccessStatusCode)
            {
                LogManager.Error(string.Format("Call WebApiAccount new User Fail User: {0}", @event));
                throw new Exception();
            }
        }
    }
}
