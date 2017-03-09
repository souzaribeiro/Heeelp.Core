using Heeelp.Core.Command.Person;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Person
{
    public class PersonInterestCommandHandler :
        ICommandHandler<AddPersonInterestCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.PersonInterest>> contextFactory;
        public PersonInterestCommandHandler(Func<IDataContext<Domain.PersonInterest>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddPersonInterestCommand command)
        {
            var repository = this.contextFactory();


            var personInterest = new Domain.PersonInterest(command.PersonInterestId, command.PersonId, 
                command.ExpertiseId, command.DisplayOrder, command.InsertedDateUTC, command.InsertedBy,
                command.ServerInstanceId, command.Active);



            repository.Save(personInterest);
        }
    }
}
