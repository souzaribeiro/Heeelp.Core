namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Events.Events")]
    public partial class Events
    {
        [Key]
        [Column(Order = 0)]
        public Guid AggregateId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string AggregateType { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Version { get; set; }

        public string Payload { get; set; }

        public string CorrelationId { get; set; }
    }
}
