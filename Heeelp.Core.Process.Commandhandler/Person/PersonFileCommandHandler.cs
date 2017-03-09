using Heeelp.Core.Command.Person;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Person
{
    public class PersonFileCommandHandler :
        ICommandHandler<AddPersonFileCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.PersonFile>> contextFactory;
        public PersonFileCommandHandler(Func<IDataContext<Domain.PersonFile>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddPersonFileCommand command)
        {
            var repository = this.contextFactory();


            var personFile = new Domain.PersonFile(command.PersonFileId, command.PersonId, 
                command.FileId, command.AssociatedDateUTC, command.AssocietedBy, command.Active);



            repository.Save(personFile);
        }
    }
}
