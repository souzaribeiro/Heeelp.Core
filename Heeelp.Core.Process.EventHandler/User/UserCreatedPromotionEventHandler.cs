using Heeelp.Core.Common;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using Heeelp.Core.Process.Event.User;
using Heeelp.Core.Logging;
using System;
using System.Net.Http;

namespace Heeelp.Core.ProcessManager.EventHandlers.User
{
    public class UserCreatedPromotionEventHandler : IEventHandler<UserCreatedPromotionEvent>
    {
        public UserCreatedPromotionEventHandler()
        {

        }

        public void Handle(UserCreatedPromotionEvent @event)
        {
            //Call Promotion, create new user
            var _clientPromotion = new HttpClient();
            _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
            var uriPromotion = "api/User/AddUser";
            HttpResponseMessage responsePromotion = _clientPromotion.PostAsJsonAsync(uriPromotion, @event).Result;

            if (!responsePromotion.IsSuccessStatusCode)
            {
                LogManager.Error(string.Format("Call WebApiNotification new User Fail User: {0}", @event));
                throw new Exception();
            }
        }
    }
}
