namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonHistoric")]
    public partial class PersonHistoric
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PersonHistoricId { get; set; }

        public int PersonId { get; set; }

        public Guid IntegrationCode { get; set; }

        [Required]
        [StringLength(70)]
        public string Name { get; set; }

        [StringLength(50)]
        public string FantasyName { get; set; }

        [StringLength(50)]
        public string NameFromSecurityCheck { get; set; }

        public int? SecuritySourceId { get; set; }

        public bool? IsSafe { get; set; }

        [StringLength(50)]
        public string FriendlyNameURL { get; set; }

        public byte PersonOriginTypeId { get; set; }

        [StringLength(150)]
        public string PersonOriginDetails { get; set; }

        public byte? CountryId { get; set; }

        public byte LanguageId { get; set; }

        public byte PersonTypeId { get; set; }

        public byte PersonProfileId { get; set; }

        public byte PersonStatusId { get; set; }

        [StringLength(150)]
        public string PersonalWebSite { get; set; }

        public byte CurrencyId { get; set; }

        public DateTime CreationDateUTC { get; set; }

        [StringLength(50)]
        public string ActivationCode { get; set; }

        public DateTime? ActivationDateUTC { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        public int? PersonFatherId { get; set; }

        public long? InviteId { get; set; }

        public short ServerInstanceId { get; set; }

        public bool Active { get; set; }

        public virtual Person Person { get; set; }
    }
}
