using Heeelp.Core.Command.User;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;
using System.Linq;
using Domain = Heeelp.Core.Domain.Expertise;

namespace Heeelp.Core.ProcessManager.CommandHandlers.User
{
    public class UserGroupUserCommandHandler :
        ICommandHandler<AddUserGroupUserCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.UserGroupUser>> contextFactory;
        public UserGroupUserCommandHandler(Func<IDataContext<Domain.UserGroupUser>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddUserGroupUserCommand command)
        {
            var repository = this.contextFactory();


            var userGroupUser = new Domain.UserGroupUser(command.UserGroupUserId, command.UserId, command.UserGroupId, command.InsertedDate);


            repository.Save(userGroupUser);
        }
    }
}
