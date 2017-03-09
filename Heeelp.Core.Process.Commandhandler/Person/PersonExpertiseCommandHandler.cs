using Heeelp.Core.Command.Person;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Person
{
    public class PersonExpertiseCommandHandler :
        ICommandHandler<AddPersonExpertiseCommand>
    {
        private readonly ICommandBus bus;
        private readonly IPersonDao _personDao;
        private Func<IDataContext<Domain.PersonExpertise>> contextFactory;

        public PersonExpertiseCommandHandler(Func<IDataContext<Domain.PersonExpertise>> contextFactory, IPersonDao personDao)
        {
            this.contextFactory = contextFactory;
            _personDao = personDao;
        }

        public void Handle(AddPersonExpertiseCommand command)
        {
            var repository = this.contextFactory();
            var person = _personDao.GetByPersonIntegrationId(command.PersonIntegrationId);
            if(person == null) { throw new Exception(string.Format("Person não encontrado, PersonIntegrationId: {0}", command.PersonIntegrationId)); }
            foreach (var expertiseId in command.ExpertiseListId)
            {

                var personExpertise = new Domain.PersonExpertise(command.PersonPageExpertiseId, person.PersonId,
                    expertiseId, command.InsertedDateUTC, command.InsertedBy, command.ServerInstanceId,
                    command.CustomPhotoFileId, command.CustomDescription, command.ExhibitionOrder, command.Active);

                repository.Save(personExpertise);

            }
        }
    }
}
