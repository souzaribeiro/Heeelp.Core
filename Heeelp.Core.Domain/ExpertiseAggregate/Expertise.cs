namespace Heeelp.Core.Domain
{
    using Common;
    using Infrastructure.Database;
    using Infrastructure.Messaging;
    using Process.Event.Expertise;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Expertise")]
    public partial class Expertise : IAggregateRoot, IEventPublisher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Expertise()
        {
            Expertise1 = new HashSet<Expertise>();
            ExpertiseHistory = new HashSet<ExpertiseHistory>();
            ExpertisePhoto = new HashSet<ExpertisePhoto>();
            PersonExpertise = new HashSet<PersonExpertise>();
            PersonInterest = new HashSet<PersonInterest>();
        }

        public Expertise(Guid id, int expertiseID, string name, int? expertiseFatherId, int createdBy, DateTime createdDateUtc, byte approvalStatusId, int approvedBy,
            DateTime approvedDate, string defaultDescription, bool isPriceDefinedEditorially, ApprovalStatus approvalStatus, ICollection<Expertise> expertise1,
            Expertise expertise2, User user, User user1, bool active)
        {
            this.Id = id;
            this.ExpertiseId = expertiseID;
            this.Name = name;
            this.ExpertiseFatherId = expertiseFatherId;
            this.CreatedBy = createdBy;
            this.CreatedDateUTC = createdDateUtc;
            this.ApprovalStatusId = approvalStatusId;
            this.ApprovedBy = approvedBy;
            this.ApprovedDate = approvedDate;
            this.DefaultDescription = defaultDescription;
            this.IsPriceDefinedEditorially = isPriceDefinedEditorially;
            this.ApprovalStatus = approvalStatus;
            this.Expertise1 = expertise1;
            this.Expertise2 = expertise2;
            this.User = user;
            this.User1 = user1;
            this.Active = active;
            // this.AddEvent(new ExpertiseAdded() { SourceId = id, ExpertiseId = expertiseID });
        }
        [NotMapped]
        public Guid Id { get; set; }



        public int ExpertiseId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool Active { get; set; }

        public int? ExpertiseFatherId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDateUTC { get; set; }

        public int ApprovalStatusId { get; set; }

        public int ApprovedBy { get; set; }

        public DateTime ApprovedDate { get; set; }

        [Required]
        [StringLength(150)]
        public string DefaultDescription { get; set; }

        public bool IsPriceDefinedEditorially { get; set; }

        public virtual ApprovalStatus ApprovalStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [NotMapped]
        public virtual ICollection<Expertise> Expertise1 { get; set; }

        [NotMapped]
        public virtual Expertise Expertise2 { get; set; }

        [NotMapped]
        public virtual User User { get; set; }

        [NotMapped]
        public virtual User User1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExpertiseHistory> ExpertiseHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExpertisePhoto> ExpertisePhoto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonExpertise> PersonExpertise { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonInterest> PersonInterest { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events
        {
            get
            {
                return this.events;
            }
        }


        public void Complete()
        {
            this.AddEvent(new ExpertiseAdded() { SourceId = this.Id, ExpertiseId = this.ExpertiseId, ExpertiseName = this.Name, RegisterUserId = this.CreatedBy });
        }
        public void CompleteSuccessFile(FIleServer fs)
        {
            this.AddEvent(new ExpertiseCreatedFIleEvent
            {
                SourceId = this.Id,
                ExpertiseId = this.ExpertiseId,
                FilePath = fs.FilePath,
                Width = fs.Width,
                Height = fs.Height,
                OriginalName = fs.OriginalName,
                FileTempId = fs.FileTempId,
                Description = "Usuário do Heeelp",
                FriendlyName = fs.FriendlyName,
                FileUtilizationId = fs.FileUtilizationId,
                Alt = fs.Alt,
                FileIntegrationCode = fs.FileIntegrationCode,
                Name = fs.Name,
                FileOriginTypeId = (int)GeneralEnumerators.EnumModules.Core_User,
                PersonId = fs.PersonId ,
                UploadedBy = fs.UploadedBy
            });
        }
        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }


        public void Cancel()
        {
        }
    }
}
