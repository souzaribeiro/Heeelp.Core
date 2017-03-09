using System;
using System.Collections.Generic;
using Heeelp.Core.Domain;
using Heeelp.Core.Command.Person;
using Heeelp.Core.Common;
using Heeelp.Core.Event;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using Heeelp.Core.Process.Event.User;
using System.Net.Http;
using Heeelp.Core.Logging;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Common.CustomException;
using Heeelp.Core.Process.Event.Person;
using System.Net.Http.Headers;
using System.Data.Entity.Spatial;
using Heeelp.Core.Command.ExternalModules;

namespace Heeelp.Core.ProcessManager.CommandHandlers.ExternalModules
{
    public class MarketingCommandHandler :
        ICommandHandler<MarketingProspectCommand>

    {

        public MarketingCommandHandler()
        {
        }


        #region Send Prospect

        public void Handle(MarketingProspectCommand command)
        {
            try
            {
                //Call Account, create new user
                var _clientAccount = new HttpClient();
                _clientAccount.BaseAddress = new Uri(CustomConfiguration.WebApiMarketing);
                HttpResponseMessage responseAccount = _clientAccount.PostAsJsonAsync("api/Prospect/ProspectAdd", command).Result;
                if (!responseAccount.IsSuccessStatusCode)
                {
                    LogManager.Error(string.Format("Call WebApiMarketing new Prospect Fail, Prospect: {0}", command));
                    throw new HeeelpSyncException();
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }


        #endregion



    }
}
