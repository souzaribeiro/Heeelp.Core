using Heeelp.Core.Command.User;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;
using System.Linq;
using Domain = Heeelp.Core.Domain.Expertise;

namespace Heeelp.Core.ProcessManager.CommandHandlers.User
{
    public class UserGroupCommandHandler :
        ICommandHandler<AddUserGroupCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.UserGroup>> contextFactory;
        public UserGroupCommandHandler(Func<IDataContext<Domain.UserGroup>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddUserGroupCommand command)
        {
            var repository = this.contextFactory();


            var userGroup = new Domain.UserGroup(command.UserGroupId, command.Name, command.Active);


            repository.Save(userGroup);
        }
    }
}
