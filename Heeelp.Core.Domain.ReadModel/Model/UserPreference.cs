namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserPreference")]
    public partial class UserPreference
    {
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

        public virtual CouponEstatisticMode CouponEstatisticMode { get; set; }

        public virtual CouponPeriodMode CouponPeriodMode { get; set; }

        public virtual User User { get; set; }
    }
}
