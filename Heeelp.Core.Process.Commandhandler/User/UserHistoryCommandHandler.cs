using Heeelp.Core.Command.User;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;
using System.Linq;
using Domain = Heeelp.Core.Domain.Expertise;

namespace Heeelp.Core.ProcessManager.CommandHandlers.User
{
    public class UserHistoryCommandHandler :
        ICommandHandler<AddUserHistoryCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.UserHistory>> contextFactory;
        public UserHistoryCommandHandler(Func<IDataContext<Domain.UserHistory>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddUserHistoryCommand command)
        {
            var repository = this.contextFactory();


            var userHistory = new Domain.UserHistory(command.UserHistoryId, command.UserId, command.IntegrationCode
                                                , command.Name, command.Email, command.SecundaryEmail, command.SmartPhoneNumber
                                                , command.PersonId, command.IsDefaultUser, command.UserProfileId, command.UserStatusId
                                                , command.AuthenticationModeId, command.LanguageId, command.CreationDateUTC
                                                , command.ValidationDateUTC, command.FormFillTime, command.ValidationToken
                                                , command.SecurityCheckNecessary, command.ValidationAttempts, command.LoginFailAttempts
                                                , command.LastLoginFailUTC, command.Active, command.EnrollmentIP
                                                , command.ValidationIP, command.CreatedBy, command.ServerInstanceId, command.IsPerpetual);
            repository.Save(userHistory);
        }
    }
}
