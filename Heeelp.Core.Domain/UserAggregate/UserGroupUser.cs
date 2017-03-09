namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Messaging;
    using Infrastructure.Database;
    [Table("UserGroupUser")]
    public partial class UserGroupUser : IAggregateRoot, IEventPublisher
    {
        public UserGroupUser(int userGroupUserId, int userId, int userGroupId, DateTime insertedDate)
        {
            this.UserGroupUserId = userGroupUserId;
            this.UserId = userId;
            this.UserGroupId = userGroupId;
            this.InsertedDate = insertedDate;
        }
        public Guid Id { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserGroupUserId { get; set; }

        public int UserId { get; set; }

        public int UserGroupId { get; set; }

        public DateTime InsertedDate { get; set; }

        public virtual User User { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
