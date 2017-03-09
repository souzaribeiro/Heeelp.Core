namespace Heeelp.Core.Domain
{
    using Infrastructure.Database;
    using Infrastructure.Messaging;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("UserFile")]
    public partial class UserFile : IAggregateRoot, IEventPublisher
    {
        public UserFile()
        {

        }
        public UserFile(int userId, long fileId, DateTime associatedDateUTC, bool active)
        {
            this.UserId = userId;
            this.FileId = fileId;
            this.AssociatedDateUTC = associatedDateUTC;
            this.Active = active;
        }
        [NotMapped]
        public Guid Id { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserFileId { get; set; }

        public int UserId { get; set; }

        public long FileId { get; set; }

        public DateTime AssociatedDateUTC { get; set; }

        public bool Active { get; set; }

        [NotMapped]
        public virtual User User { get; set; }
    }
}
