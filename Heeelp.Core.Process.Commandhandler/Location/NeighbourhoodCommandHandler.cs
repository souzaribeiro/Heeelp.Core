using Heeelp.Core.Command.Location;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Location
{
    public class NeighbourhoodCommandHandler :
        ICommandHandler<AddNeighbourhoodCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.Neighbourhood>> contextFactory;
        public NeighbourhoodCommandHandler(Func<IDataContext<Domain.Neighbourhood>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddNeighbourhoodCommand command)
        {
            var repository = this.contextFactory();


            var neighbourhood = new Domain.Neighbourhood(command.NeighbourhoodId, command.Name, 
                command.CityId, command.NeighbourhoodFatherId, command.Coordinates, command.Active, 
                command.InsertedDate, command.CityZoneId, command.PostCode);



            repository.Save(neighbourhood);
        }
    }
}
