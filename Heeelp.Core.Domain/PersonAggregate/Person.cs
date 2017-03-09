namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Database;
    using Infrastructure.Messaging;
    using Event;
    using Common;
    using Process.Event.Person;
    [Table("Person")]
    public partial class Person : IAggregateRoot, IEventPublisher
    {
        public Person(Guid integrationCode, string name, string fantasyName, string nameFromSecurityCheck, int? securitySourceId, bool? isSafe, string friendlyNameURL, byte personOriginTypeId, string personOriginDetails, long? campaignId, byte countryId, byte languageId, byte personTypeId, byte personStatusId, string personalWebSite, byte currencyId, DateTime creationDateUTC, string activationCode, DateTime? activationDateUTC, string phoneNumber, int? personFatherId, long? inviteId, short serverInstanceId, bool active)
        {

            this.IntegrationCode = integrationCode;
            this.Name = name;
            this.FantasyName = fantasyName;
            this.NameFromSecurityCheck = nameFromSecurityCheck;
            this.SecuritySourceId = securitySourceId;
            this.IsSafe = isSafe;
            this.FriendlyNameURL = friendlyNameURL;
            this.PersonOriginTypeId = personOriginTypeId;
            this.PersonOriginDetails = personOriginDetails;
            this.CampaignId = campaignId;
            this.CountryId = countryId;
            this.LanguageId = languageId;
            this.PersonTypeId = personTypeId;
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

            PersonFile = new HashSet<PersonFile>();
            PersonHistoric = new HashSet<PersonHistoric>();
            PersonRules = new HashSet<PersonRules>();
            PersonBenefitClub1 = new HashSet<PersonBenefitClub>();

        }

        public Person(Guid integrationCode, string name, string fantasyName, byte personOriginTypeId, byte countryId, byte languageId, byte personTypeId, byte personStatusId, byte currencyId, DateTime creationDateUTC, string phoneNumber, short serverInstanceId, bool active, int? personFatherId)
        {
            this.IntegrationCode = integrationCode;
            this.Name = name;
            this.FantasyName = fantasyName;
            this.PersonOriginTypeId = personOriginTypeId;
            this.CountryId = countryId;
            this.LanguageId = languageId;
            this.PersonTypeId = personTypeId;
            this.PersonStatusId = personStatusId;
            this.CurrencyId = currencyId;
            this.CreationDateUTC = creationDateUTC;
            this.PhoneNumber = phoneNumber;
            this.ServerInstanceId = serverInstanceId;
            this.Active = active;
            this.PersonFatherId = personFatherId;

            PersonFile = new HashSet<PersonFile>();
            PersonHistoric = new HashSet<PersonHistoric>();
            PersonRules = new HashSet<PersonRules>();
            PersonBenefitClub1 = new HashSet<PersonBenefitClub>();
        }

        public Person(Guid integrationCode, string name, string fantasyName, string friendlyNameURL, byte personOriginTypeId, byte countryId, byte languageId, byte personTypeId, byte personStatusId, string personalWebSite, byte currencyId, DateTime creationDateUTC, string phoneNumber, long? inviteId, short serverInstanceId, bool active)
        {
            this.IntegrationCode = integrationCode;
            this.Name = name;
            this.FantasyName = fantasyName;
            this.FriendlyNameURL = friendlyNameURL;
            this.PersonOriginTypeId = personOriginTypeId;
            this.CountryId = countryId;
            this.LanguageId = languageId;
            this.PersonTypeId = personTypeId;
            this.PersonStatusId = personStatusId;
            this.PersonalWebSite = personalWebSite;
            this.CurrencyId = currencyId;
            this.CreationDateUTC = creationDateUTC;
            this.PhoneNumber = phoneNumber;
            this.InviteId = inviteId;
            this.ServerInstanceId = serverInstanceId;
            this.Active = active;
            

            PersonFile = new HashSet<PersonFile>();
            PersonHistoric = new HashSet<PersonHistoric>();
            PersonRules = new HashSet<PersonRules>();
            PersonBenefitClub1 = new HashSet<PersonBenefitClub>();

        }

        public List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        public void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }

        [NotMapped]
        public Guid Id { get; set; }

        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {            
            PersonAddress = new HashSet<PersonAddress>();
            PersonContract = new HashSet<PersonContract>();
            PersonDocument = new HashSet<PersonDocument>();
            PersonExpertise = new HashSet<PersonExpertise>();
            PersonFile = new HashSet<PersonFile>();
            PersonHistoric = new HashSet<PersonHistoric>();
            PersonInterest = new HashSet<PersonInterest>();
            PersonRules = new HashSet<PersonRules>();
            Users = new HashSet<User>();
            PersonBenefitClub1 = new HashSet<PersonBenefitClub>();

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }

        public Guid IntegrationCode { get; set; }

        public string Name { get; set; }

        public string FantasyName { get; set; }

        public string NameFromSecurityCheck { get; set; }

        public int? SecuritySourceId { get; set; }

        public bool? IsSafe { get; set; }

        public string FriendlyNameURL { get; set; }

        public byte PersonOriginTypeId { get; set; }

        public string PersonOriginDetails { get; set; }

        public long? CampaignId { get; set; }

        public byte? CountryId { get; set; }

        public byte LanguageId { get; set; }

        public byte PersonTypeId { get; set; }

        public byte PersonStatusId { get; set; }

        public string PersonalWebSite { get; set; }

        public byte CurrencyId { get; set; }

        public byte SkinId { get; set; }

        public int? PersonBenefitClubId { get; set; }

        public DateTime CreationDateUTC { get; set; }

        public string ActivationCode { get; set; }

        public DateTime? ActivationDateUTC { get; set; }

        public string PhoneNumber { get; set; }

        public int? PersonFatherId { get; set; }

        public long? InviteId { get; set; }

        public short ServerInstanceId { get; set; }

        public bool Active { get; set; }

        public string UrlOrigin { get; set; }

        public string UrlImageLogo { get; set; }
        public string InviteCode { get; set; }
        public int? InviteAvailable { get; set; }

        public virtual Country Country { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Language Language { get; set; }

        [NotMapped]
        public virtual Person PersonFather { get; set; }

        [NotMapped]
        public virtual PersonOriginType PersonOriginType { get; set; }
        [NotMapped]
        public virtual PersonStatus PersonStatus { get; set; }
        [NotMapped]
        public virtual PersonType PersonType { get; set; }
        [NotMapped]
        public virtual SecuritySource SecuritySource { get; set; }
        [NotMapped]
        public virtual ServerInstance ServerInstance { get; set; }
        public virtual PersonBenefitClub PersonBenefitClub { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<PersonAddress> PersonAddress { get; set; }

        [NotMapped]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonBenefitClub> PersonBenefitClub1 { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]        
        public virtual ICollection<PersonContract> PersonContract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonFile> PersonFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonDocument> PersonDocument { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        
        public virtual ICollection<PersonExpertise> PersonExpertise { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonRules> PersonRules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]        
        public virtual ICollection<PersonHistoric> PersonHistoric { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [NotMapped]
        public virtual ICollection<PersonInterest> PersonInterest { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [NotMapped]
        public virtual ICollection<User> Users { get; set; }
    }
}
