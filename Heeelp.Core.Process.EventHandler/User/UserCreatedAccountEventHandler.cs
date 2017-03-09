using Heeelp.Core.Common;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using Heeelp.Core.Process.Event.User;
using Heeelp.Core.Logging;
using System;
using System.Net.Http;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Command.Person;
using Heeelp.Core.Command.User;
using static Heeelp.Core.Domain.DomainEnumerators;

namespace Heeelp.Core.ProcessManager.EventHandlers.User
{
    public class UserCreatedAccountEventHandler : IEventHandler<UserCreatedAccountEvent>,
        IEventHandler<UserActivatedEvent>
    {

        private readonly ICommandBus _bus;
        private Func<IDataContext<Domain.Person>> contextFactory;
        private IFileTempDao _FileTemp;
        private IPersonDao _personDao;
        private IUserDao _userDao;
        private IContractDao _contractDao;

        public UserCreatedAccountEventHandler(Func<IDataContext<Domain.Person>> contextFactory, IFileTempDao contextFactoryFileTmp, ICommandBus bus, IPersonDao personDao, IUserDao userDao, IContractDao contractDao)
        {
            this.contextFactory = contextFactory;
            this._FileTemp = contextFactoryFileTmp;
            this._bus = bus;
            this._personDao = personDao;
            this._userDao = userDao;
            this._contractDao = contractDao;
        }


        public void Handle(UserCreatedAccountEvent @event)
        {
            //Call Account, create new user
            var _clientAccount = new HttpClient();
            _clientAccount.BaseAddress = new Uri(CustomConfiguration.WebApiAccount);
            string uri = "api/User/AddUser";
            HttpResponseMessage responseAccount = _clientAccount.PostAsJsonAsync(uri, @event).Result;

            if (!responseAccount.IsSuccessStatusCode)
            {
                LogManager.Error(string.Format("Call WebApiAccount new User Fail User: {0}", @event));
                throw new Exception();
            }
        }
    }
}
