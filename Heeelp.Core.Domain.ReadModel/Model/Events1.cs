namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SqlBus.Events")]
    public partial class Events1
    {
        public long Id { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string CorrelationId { get; set; }
    }
}
