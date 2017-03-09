using Heeelp.Core.Command.Location;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Location
{
    public class CountryRegionCommandHandler :
        ICommandHandler<AddCountryRegionCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.CountryRegion>> contextFactory;
        public CountryRegionCommandHandler(Func<IDataContext<Domain.CountryRegion>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddCountryRegionCommand command)
        {
            var repository = this.contextFactory();


            var countryRegion = new Domain.CountryRegion(command.CountryRegionId, command.Name, command.CountryId);
            repository.Save(countryRegion);
        }
    }
}
