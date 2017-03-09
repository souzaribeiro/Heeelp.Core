namespace Heeelp.Core.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Database;
    using Infrastructure.Messaging;
    using System.Collections.Generic;

    [Table("PersonHistoric")]
    public partial class PersonHistoric
    {

        public PersonHistoric()
        {
        }

        public PersonHistoric(long personHistoricId, int personId, Guid integrationCode, string name,
            string fantasyName, string nameFromSecurityCheck, int? securitySourceId, bool? isSafe,
            string friendlyNameURL, byte personOriginTypeId, string personOriginDetails, byte countryId,
            byte languageId, byte personTypeId, byte personProfileId, byte personStatusId, string personalWebSite,
            byte currencyId, DateTime creationDateUTC, string activationCode, DateTime? activationDateUTC,
            string phoneNumber, int? personFatherId, long? inviteId, short serverInstanceId, bool active)
        {
            this.PersonHistoricId = personHistoricId;
            this.PersonId = personId;
            this.IntegrationCode = integrationCode;
            this.Name = name;
            this.FantasyName = fantasyName;
            this.NameFromSecurityCheck = nameFromSecurityCheck;
            this.SecuritySourceId = securitySourceId;
            this.IsSafe = isSafe;
            this.FriendlyNameURL = friendlyNameURL;
            this.PersonOriginTypeId = personOriginTypeId;
            this.PersonOriginDetails = personOriginDetails;
            this.CountryId = countryId;
            this.LanguageId = languageId;
            this.PersonTypeId = personTypeId;
            this.PersonProfileId = personProfileId;
            this.PersonStatusId = personStatusId;
            this.PersonalWebSite = personalWebSite;
            this.CurrencyId = currencyId;
            this.CreationDateUTC = creationDateUTC;
            this.ActivationCode = activationCode;
            this.ActivationDateUTC = activationDateUTC;
            this.PhoneNumber = phoneNumber;
            this.PersonFatherId = personFatherId;
            this.InviteId = inviteId;
            this.ServerInstanceId = serverInstanceId;
            this.Active = active;



        }

        

        
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

        public byte CountryId { get; set; }

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

        public DateTime UpdateDateUTC { get; set; }

        public virtual Person Person { get; set; }
    }
}
