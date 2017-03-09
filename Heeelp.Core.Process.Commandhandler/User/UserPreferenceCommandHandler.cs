using Heeelp.Core.Command.User;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;
using System.Linq;
using Domain = Heeelp.Core.Domain.Expertise;

namespace Heeelp.Core.ProcessManager.CommandHandlers.User
{
    public class UserPreferenceCommandHandler :
        ICommandHandler<AddUserPreferenceCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.UserPreference>> contextFactory;
        public UserPreferenceCommandHandler(Func<IDataContext<Domain.UserPreference>> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Handle(AddUserPreferenceCommand command)
        {
            var repository = this.contextFactory();


            var userPreference = new Domain.UserPreference(command.UserPreferenceId, command.UserId,
                command.CouponEstatisticModeId, command.CouponPeriodModeId, command.SaveRecentQueries,
                command.ShowRecentQueries, command.ShowRecentCoupons, command.ShowRecentReviews,
                command.ShowRecentCheckins, command.ShowPendentActions, command.AcceptReceiveAlertAboutInterests,
                command.AcceptReceiveAlertAboutContent, command.AcceptReceiveAlertAboutEvents,
                command.AcceptReceiveWizardSuggestions, command.AcceptReceiveNewsletterOffers,
                command.ShowFastProfileBar, command.ToggleLeftNavigationMenu, command.SelectedSkin,
                command.ConfigurationDateUTC, command.ShowFriendsActivity, command.ShareActivitiesWithFriends,
                command.SearchDistance, command.Active);



            repository.Save(userPreference);
        }
    }
}
