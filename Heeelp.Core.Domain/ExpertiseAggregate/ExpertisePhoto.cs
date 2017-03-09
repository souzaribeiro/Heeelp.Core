namespace Heeelp.Core.Domain
{
    using Infrastructure.Database;
    using Infrastructure.Messaging;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("ExpertisePhoto")]
    public partial class ExpertisePhoto : IAggregateRoot, IEventPublisher
    {
        public ExpertisePhoto()
        {

        }
        public ExpertisePhoto(int expertisePhotoId, int expertiseId, long fileId, bool isDefault)
        {
            this.ExpertisePhotoId = expertisePhotoId;
            this.ExpertiseId = expertiseId;
            this.FileId = fileId;
            this.IsDefault = isDefault;
        }

        [NotMapped]
        public Guid Id { get; set; }

        private List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        public int ExpertisePhotoId { get; set; }

        public int ExpertiseId { get; set; }

        public long FileId { get; set; }

        public bool IsDefault { get; set; }

        public virtual Expertise Expertise { get; set; }
    }
}
