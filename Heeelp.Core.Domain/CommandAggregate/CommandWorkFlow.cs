namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("CommandWorkFlow")]
    public partial class CommandWorkFlow
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CommandWorkFlowId { get; set; }

        public Guid CommandId { get; set; }

        public byte CommandStatusId { get; set; }

        public DateTime DateStartUTC { get; set; }

        public DateTime DateEndUTC { get; set; }

        [Required]
        public string Details { get; set; }

        public virtual CommandQuee CommandQuee { get; set; }

        public virtual CommandStatus CommandStatus { get; set; }
    }
}
