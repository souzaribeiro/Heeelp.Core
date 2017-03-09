using Heeelp.Core.Command.Person;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Person
{
    public class PersonRulesCommandHandler :
        ICommandHandler<AddPersonRulesCommand>
    {
        private readonly ICommandBus _bus;
        private Func<IDataContext<Domain.PersonRules>> contextFactory;
        private IFileTempDao _FileTemp;
        private IPersonDao _personDao;

        public PersonRulesCommandHandler(Func<IDataContext<Domain.PersonRules>> contextFactory, ICommandBus bus, IPersonDao personDao)
        {
            this.contextFactory = contextFactory;
            this._bus = bus;
            this._personDao = personDao;
        }

        public void Handle(AddPersonRulesCommand command)
        {
            var repository = this.contextFactory();

            var personRules = new Domain.PersonRules(command.PersonRulesId, command.PersonId,
                command.PersonProfileId, command.DateUTC, command.Active, command.RulesStatusId);

            repository.Save(personRules);

        }

    }
}
