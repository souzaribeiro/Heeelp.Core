using Heeelp.Core.Command.Location;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Location
{
    public class StateCommandHandler :
        ICommandHandler<AddStateCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.State>> contextFactory;
        public StateCommandHandler(Func<IDataContext<Domain.State>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddStateCommand command)
        {
            var repository = this.contextFactory();


            var state = new Domain.State(command.StateId, command.Name, command.Code, command.CountryRegionId, 
                command.Coordinates, command.Active);
            repository.Save(state);
        }
    }
}
