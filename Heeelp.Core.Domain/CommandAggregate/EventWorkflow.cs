namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("EventWorkflow")]
    public partial class EventWorkflow
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long EventWorkflowId { get; set; }

        public Guid EventId { get; set; }

        public byte EventStatusId { get; set; }

        public DateTime DateStartUTC { get; set; }

        public DateTime DateEndUTC { get; set; }

        [Required]
        public string Details { get; set; }

        public virtual EventQuee EventQuee { get; set; }

        public virtual EventStatus EventStatus { get; set; }
    }
}
