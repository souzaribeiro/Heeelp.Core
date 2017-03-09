namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Database;
    using Infrastructure.Messaging;
    using Common;
    using Process.Event.Person;
    [Table("PersonDocument")]
    public partial class PersonDocument : IAggregateRoot, IEventPublisher
    {
        public PersonDocument()
        {

        }
        public PersonDocument(int personDocumentId, int personId, short documentTypeId,
            string number, string complement, DateTime? dateIssued,
            DateTime? dateValidUntil, DateTime? insertedDateUTC, long fileId, bool? active, int associatedBy)
        {
            this.PersonDocumentId = personDocumentId;
            this.PersonId = personId;
            this.DocumentTypeId = documentTypeId;
            this.Number = number;
            this.Complement = complement;
            this.DateIssued = dateIssued;
            this.DateValidUntil = dateValidUntil;
            this.InsertedDateUTC = insertedDateUTC;
            this.FileId = fileId;
            this.Active = active;
            this.AssociatedBy = associatedBy;
        }
       
        [NotMapped]
        public Guid Id { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }

        public void CompleteSuccessFile(FIleServer fs)
        {
            this.AddEvent(new PersonDocumentFileAddedEvent
            {
                SourceId = this.Id,
                PersonDocumentId = this.PersonDocumentId,
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
                PersonId = fs.PersonId,
                UploadedBy = fs.UploadedBy,
                Active = this.Active,
                InsertedDateUTC = this.InsertedDateUTC
            });
        }

        public int PersonDocumentId { get; set; }

        public int PersonId { get; set; }

        public short DocumentTypeId { get; set; }

        [StringLength(50)]
        public string Number { get; set; }

        [StringLength(50)]
        public string Complement { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateIssued { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateValidUntil { get; set; }

        public DateTime? InsertedDateUTC { get; set; }

        public long FileId { get; set; }

        public bool? Active { get; set; }

        public int AssociatedBy { get; set; }


        [NotMapped]
        public virtual DocumentType DocumentType { get; set; }
        [NotMapped]
        public virtual Person Person { get; set; }
    }
}
