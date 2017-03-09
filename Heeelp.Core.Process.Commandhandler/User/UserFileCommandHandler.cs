using Heeelp.Core.Command.User;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;
using System.Linq;
using Domain = Heeelp.Core.Domain.Expertise;

namespace Heeelp.Core.ProcessManager.CommandHandlers.User
{
    public class UserFileCommandHandler :
        ICommandHandler<AddUserFileCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.UserFile>> contextFactory;
        public UserFileCommandHandler(Func<IDataContext<Domain.UserFile>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddUserFileCommand command)
        {
            var repository = this.contextFactory();

            var userFile = new Domain.UserFile(command.UserId, command.FileId
                                                    , command.AssociatedDateUTC, command.Active);

            repository.Save(userFile);
        }
    }
}
