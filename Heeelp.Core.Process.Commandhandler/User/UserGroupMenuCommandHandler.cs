using Heeelp.Core.Command.User;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;
using System.Linq;
using Domain = Heeelp.Core.Domain.Expertise;

namespace Heeelp.Core.ProcessManager.CommandHandlers.User
{
    public class UserGroupMenuCommandHandler :
        ICommandHandler<AddUserGroupMenuCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.UserGroupMenu>> contextFactory;
        public UserGroupMenuCommandHandler(Func<IDataContext<Domain.UserGroupMenu>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddUserGroupMenuCommand command)
        {
            var repository = this.contextFactory();


            var userGroupMenu = new Domain.UserGroupMenu(command.UserGroupMenuId, command.UserGroupId, command.MenuId, command.InsertedDate);


            repository.Save(userGroupMenu);
        }
    }
}
