using Heeelp.Core.Command.Location;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Location
{
    public class CountryCommandHandler :
        ICommandHandler<AddCountryCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.Country>> contextFactory;
        public CountryCommandHandler(Func<IDataContext<Domain.Country>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddCountryCommand command)
        {
            var repository = this.contextFactory();


            var country = new Domain.Country(command.CountryId, command.Name, 
                command.Code, command.PhoneCode, command.LanguageId, command.CurrencyId);


            repository.Save(country);
        }
    }
}
