using Heeelp.Core.Command.Person;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Person
{
    public class PersonHistoricCommandHandler :
        ICommandHandler<AddPersonHistoricCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.PersonHistoric>> contextFactory;
        public PersonHistoricCommandHandler(Func<IDataContext<Domain.PersonHistoric>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddPersonHistoricCommand command)
        {
            var repository = this.contextFactory();


            var personHistoric = new Domain.PersonHistoric(command.PersonHistoricId, command.PersonId, 
                command.IntegrationCode, command.Name, command.FantasyName, command.NameFromSecurityCheck,
                command.SecuritySourceId, command.IsSafe, command.FriendlyNameURL, command.PersonOriginTypeId,
                command.PersonOriginDetails, command.CountryId, command.LanguageId, command.PersonTypeId,
                command.PersonProfileId, command.PersonStatusId, command.PersonalWebSite, command.CurrencyId,
                command.CreationDateUTC, command.ActivationCode, command.ActivationDateUTC, command.PhoneNumber,
                command.PersonFatherId, command.InviteId, command.ServerInstanceId, command.Active);





            repository.Save(personHistoric);
        }
    }
}
