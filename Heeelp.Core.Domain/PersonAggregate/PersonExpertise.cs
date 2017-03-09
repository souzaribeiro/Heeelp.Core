namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Database;
    using Infrastructure.Messaging;


    [Table("PersonExpertise")]
    public partial class PersonExpertise : IAggregateRoot, IEventPublisher
    {
        public PersonExpertise(int personPageExpertiseId, int personId, int expertiseId, DateTime insertedDateUTC, int insertedBy, short serverInstanceId, long? customPhotoFileId, string customDescription, byte exhibitionOrder, bool active)
        {
            this.PersonPageExpertiseId = personPageExpertiseId;
            this.PersonId = personId;
            this.ExpertiseId = expertiseId;
            this.InsertedDateUTC = insertedDateUTC;
            this.InsertedBy = insertedBy;
            this.ServerInstanceId = serverInstanceId;
            this.CustomPhotoFileId = customPhotoFileId;
            this.CustomDescription = customDescription;
            this.ExhibitionOrder = exhibitionOrder;
            this.Active = active;
        }
        public PersonExpertise()
        { }
        [NotMapped]
        public Guid Id { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }
        [Key]
        public int PersonPageExpertiseId { get; set; }

        public int PersonId { get; set; }

        public int ExpertiseId { get; set; }

        public DateTime InsertedDateUTC { get; set; }

        public int InsertedBy { get; set; }

        public short ServerInstanceId { get; set; }

        public long? CustomPhotoFileId { get; set; }

        [StringLength(150)]
        public string CustomDescription { get; set; }

        public byte ExhibitionOrder { get; set; }

        public bool Active { get; set; }


        [NotMapped]
        public virtual Expertise Expertise { get; set; }
        [NotMapped]
        public virtual Person Person { get; set; }
        [NotMapped]
        public virtual ServerInstance ServerInstance { get; set; }
        [NotMapped]
        public virtual User User { get; set; }
        [NotMapped]
        public virtual ServerInstance ServerInstance1 { get; set; }
    }
}
