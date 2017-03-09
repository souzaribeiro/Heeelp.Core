using Heeelp.Core.Command.User;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;
using System.Linq;
using Domain = Heeelp.Core.Domain.Expertise;

namespace Heeelp.Core.ProcessManager.CommandHandlers.User
{
    public class UserModuleCommandHandler :
        ICommandHandler<AddUserModuleCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.UserModule>> contextFactory;
        public UserModuleCommandHandler(Func<IDataContext<Domain.UserModule>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddUserModuleCommand command)
        {
            var repository = this.contextFactory();


            var user = new Domain.UserModule(command.UserModuleId, command.UserId, command.ModuleId, 
                command.DisplayOrder, command.StartDate, command.EndDate, command.Active);


            repository.Save(user);
        }
    }
}
