using Heeelp.Core.Command.Person;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Person
{
    public class PersonContractCommandHandler :
        ICommandHandler<AddPersonContractCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.PersonContract>> contextFactory;
        public PersonContractCommandHandler(Func<IDataContext<Domain.PersonContract>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddPersonContractCommand command)
        {
            var repository = this.contextFactory();


            repository.Save(new Domain.PersonContract()
                                {
                                    PersonId = command.PersonId,
                                    ContractId = command.ContractId,
                                    UserId = command.UserId,
                                    AgreementDateUTC = DateTime.UtcNow
                                }
             
 
 
 
                                                        

            );
        }
    }
}
