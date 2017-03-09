namespace Heeelp.Core.Domain
{
    using Common;
    using Infrastructure.Database;
    using Infrastructure.Messaging;
    using Process.Event.User;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("SelfRegistration")]
    public partial class SelfRegistration : IAggregateRoot, IEventPublisher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SelfRegistration() { }


        [NotMapped]
        public Guid Id { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SelfRegistrationId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreationDateUTC { get; set; }
        public string InviteCode { get; set; }
        public Guid PersonIntegrationId { get; set; }
        public string EnrollmentIP { get; set; }

    }
}
