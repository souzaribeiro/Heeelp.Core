namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Database;
    using Infrastructure.Messaging;

    [Table("UserGroupMenu")]
    public partial class UserGroupMenu : IAggregateRoot, IEventPublisher
    {
        public UserGroupMenu(int userGroupMenuId, int userGroupId, int menuId, DateTime insertedDate)
        {
            this.UserGroupMenuId = userGroupMenuId;
            this.UserGroupId = userGroupId;
            this.MenuId = menuId;
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
        public int UserGroupMenuId { get; set; }

        public int UserGroupId { get; set; }

        public int MenuId { get; set; }

        public DateTime InsertedDate { get; set; }

        public virtual Menu Menu { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
