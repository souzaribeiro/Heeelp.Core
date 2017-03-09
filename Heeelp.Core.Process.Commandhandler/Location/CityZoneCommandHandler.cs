using Heeelp.Core.Command.Location;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Location
{
    public class CityZoneCommandHandler :
        ICommandHandler<AddCityZoneCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.CityZone>> contextFactory;
        public CityZoneCommandHandler(Func<IDataContext<Domain.CityZone>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddCityZoneCommand command)
        {
            var repository = this.contextFactory();


            var cityZone = new Domain.CityZone(command.CityZoneId, command.Name, 
                command.Code, command.CityId, command.InsertedDate, command.Active);


            repository.Save(cityZone);
        }
    }
}
