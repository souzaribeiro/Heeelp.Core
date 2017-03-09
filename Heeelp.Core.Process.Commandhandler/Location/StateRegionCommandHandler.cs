using Heeelp.Core.Command.Location;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Location
{
    public class StateRegionCommandHandler :
        ICommandHandler<AddStateRegionCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.StateRegion>> contextFactory;
        public StateRegionCommandHandler(Func<IDataContext<Domain.StateRegion>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddStateRegionCommand command)
        {
            var repository = this.contextFactory();


            var stateRegion = new Domain.StateRegion(command.StateRegionId, command.Name,
                command.StateId, command.Coordinates, command.Active, command.InsertedDate);




            repository.Save(stateRegion);
        }
    }
}
