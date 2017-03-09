using Heeelp.Core.Command.Location;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Location
{
    public class CityCommandHandler :
        ICommandHandler<AddCityCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.City>> contextFactory;
        public CityCommandHandler(Func<IDataContext<Domain.City>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddCityCommand command)
        {
            var repository = this.contextFactory();


            var city = new Domain.City(
                    command.CityId
                    , command.Name
                    , command.StateRegionId
                    , command.Coordinates
                    , command.PostCode
                    , command.Active
                    , command.InsertedDate
                    , command.PhoneCode
            );



            repository.Save(city);
        }
    }
}
