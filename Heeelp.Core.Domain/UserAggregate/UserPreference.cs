namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Database;
    using Infrastructure.Messaging;

    [Table("UserPreference")]
    public partial class UserPreference : IAggregateRoot, IEventPublisher
    {
        public UserPreference(int userPreferenceId, int userId, byte? couponEstatisticModeId, byte? couponPeriodModeId, bool saveRecentQueries, byte? showRecentQueries, bool? showRecentCoupons, bool? showRecentReviews, bool? showRecentCheckins, bool? showPendentActions, bool acceptReceiveAlertAboutInterests, bool acceptReceiveAlertAboutContent, bool acceptReceiveAlertAboutEvents, bool acceptReceiveWizardSuggestions, bool acceptReceiveNewsletterOffers, bool showFastProfileBar, bool toggleLeftNavigationMenu, byte selectedSkin, DateTime configurationDateUTC, bool showFriendsActivity, bool shareActivitiesWithFriends, byte searchDistance, bool active)
        {
           this.UserPreferenceId = userPreferenceId;
           this.UserId = userId;
           this.CouponEstatisticModeId = couponEstatisticModeId;
           this.CouponPeriodModeId = couponPeriodModeId;
           this.SaveRecentQueries = saveRecentQueries;
           this.ShowRecentQueries = showRecentQueries;
           this.ShowRecentCoupons = showRecentCoupons;
           this.ShowRecentReviews = showRecentReviews;
           this.ShowRecentCheckins = showRecentCheckins;
           this.ShowPendentActions = showPendentActions;
           this.AcceptReceiveAlertAboutInterests = acceptReceiveAlertAboutInterests;
           this.AcceptReceiveAlertAboutContent = acceptReceiveAlertAboutContent;
           this.AcceptReceiveAlertAboutEvents = acceptReceiveAlertAboutEvents;
           this.AcceptReceiveWizardSuggestions = acceptReceiveWizardSuggestions;
           this.AcceptReceiveNewsletterOffers = acceptReceiveNewsletterOffers;
           this.ShowFastProfileBar = showFastProfileBar;
           this.ToggleLeftNavigationMenu = toggleLeftNavigationMenu;
           this.SelectedSkin = selectedSkin;
           this.ConfigurationDateUTC = configurationDateUTC;
           this.ShowFriendsActivity = showFriendsActivity;
           this.ShareActivitiesWithFriends = shareActivitiesWithFriends;
           this.SearchDistance = searchDistance;
           this.Active = Active;
        }
        public Guid Id { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }
        public int UserPreferenceId { get; set; }

        public int UserId { get; set; }

        public byte? CouponEstatisticModeId { get; set; }

        public byte? CouponPeriodModeId { get; set; }

        public bool SaveRecentQueries { get; set; }

        public byte? ShowRecentQueries { get; set; }

        public bool? ShowRecentCoupons { get; set; }

        public bool? ShowRecentReviews { get; set; }

        public bool? ShowRecentCheckins { get; set; }

        public bool? ShowPendentActions { get; set; }

        public bool AcceptReceiveAlertAboutInterests { get; set; }

        public bool AcceptReceiveAlertAboutContent { get; set; }

        public bool AcceptReceiveAlertAboutEvents { get; set; }

        public bool AcceptReceiveWizardSuggestions { get; set; }

        public bool AcceptReceiveNewsletterOffers { get; set; }

        public bool ShowFastProfileBar { get; set; }

        public bool ToggleLeftNavigationMenu { get; set; }

        public byte SelectedSkin { get; set; }

        public DateTime ConfigurationDateUTC { get; set; }

        public bool ShowFriendsActivity { get; set; }

        public bool ShareActivitiesWithFriends { get; set; }

        public byte SearchDistance { get; set; }

        public bool Active { get; set; }
        [NotMapped]
        public virtual CouponEstatisticMode CouponEstatisticMode { get; set; }
        [NotMapped]
        public virtual CouponPeriodMode CouponPeriodMode { get; set; }
        [NotMapped]
        public virtual User User { get; set; }
    }
}
