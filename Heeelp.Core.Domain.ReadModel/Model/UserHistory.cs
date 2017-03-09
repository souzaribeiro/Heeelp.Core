namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserHistory")]
    public partial class UserHistory
    {
        public long UserHistoryId { get; set; }

        public int UserId { get; set; }

        public Guid IntegrationCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string SecundaryEmail { get; set; }

        [StringLength(20)]
        public string SmartPhoneNumber { get; set; }

        public int PersonId { get; set; }

        public bool IsDefaultUser { get; set; }

        public int UserProfileId { get; set; }

        public int UserStatusId { get; set; }

        public int AuthenticationModeId { get; set; }

        public byte? LanguageId { get; set; }

        public DateTime CreationDateUTC { get; set; }

        public DateTime? ValidationDateUTC { get; set; }

        public short? FormFillTime { get; set; }

        [StringLength(15)]
        public string ValidationToken { get; set; }

        public bool? SecurityCheckNecessary { get; set; }

        public short? ValidationAttempts { get; set; }

        public short? LoginFailAttempts { get; set; }

        public DateTime? LastLoginFailUTC { get; set; }

        public bool Active { get; set; }

        [Required]
        [StringLength(20)]
        public string EnrollmentIP { get; set; }

        [StringLength(20)]
        public string ValidationIP { get; set; }

        public int? CreatedBy { get; set; }

        public short ServerInstanceId { get; set; }

        public bool? IsPerpetual { get; set; }

        public virtual User User { get; set; }
    }
}
