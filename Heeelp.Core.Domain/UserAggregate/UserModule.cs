namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Database;
    using Infrastructure.Messaging;

    [Table("UserModule")]
    public partial class UserModule : IAggregateRoot, IEventPublisher
    {
        public UserModule(long userModuleId, int userId, short moduleId, int displayOrder, DateTime startDate, DateTime? endDate, bool active)
        {
            this.UserModuleId = UserModuleId;
            this.UserId = UserId;
            this.ModuleId = ModuleId;
            this.DisplayOrder = DisplayOrder;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.Active = Active;
        }
        public Guid Id { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }
        public long UserModuleId { get; set; }

        public int UserId { get; set; }

        public short ModuleId { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool Active { get; set; }
        [NotMapped]
        public virtual Module Module { get; set; }
        [NotMapped]
        public virtual User User { get; set; }
    }
}
