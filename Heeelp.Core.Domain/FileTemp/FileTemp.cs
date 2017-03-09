namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Database;
    using Infrastructure.Messaging;

    [Table("FileTemp")]
    public partial class FileTemp : IAggregateRoot, IEventPublisher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FileTemp()
        {
        }
        public Guid Id { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FileTempId { get; set; }

        [Required]
        public byte[] Bytes { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }
        public string OriginalName { get; set; }



    }
}
