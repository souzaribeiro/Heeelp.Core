namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SqlBus.Commands")]
    public partial class Commands
    {
        public long Id { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string CorrelationId { get; set; }
    }
}
