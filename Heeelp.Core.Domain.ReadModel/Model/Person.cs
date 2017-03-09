namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person")]
    public partial class Person
    {
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
            //PersonBenefitClub1 = new HashSet<PersonBenefitClub>();
            User = new HashSet<User>();

        }

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

        public string Description { get; set; }

        public byte PersonStatusId { get; set; }


        public string PersonalWebSite { get; set; }

        public int? PersonBenefitClubId { get; set; }

        public byte CurrencyId { get; set; }

        public byte SkinId { get; set; }


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

        public virtual PersonOriginType PersonOriginType { get; set; }

        public virtual PersonStatus PersonStatus { get; set; }

        public virtual PersonType PersonType { get; set; }

        public virtual PersonBenefitClub PersonBenefitClub { get; set; }

        public virtual SecuritySource SecuritySource { get; set; }

        public virtual ServerInstance ServerInstance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonAddress> PersonAddress { get; set; }


        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<PersonBenefitClub> PersonBenefitClub1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonContract> PersonContract { get; set; }
        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonDocument> PersonDocument { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonExpertise> PersonExpertise { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonFile> PersonFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonHistoric> PersonHistoric { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonInterest> PersonInterest { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonRules> PersonRules { get; set; }
    }
}
