namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommandQuee")]
    public partial class CommandQuee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CommandQuee()
        {
            CommandWorkFlow = new HashSet<CommandWorkFlow>();
            EventQuee = new HashSet<EventQuee>();
        }

        [Key]
        public Guid CommandId { get; set; }

        [Required]
        public string CommandPayload { get; set; }

        public byte CommandStatusId { get; set; }

        public DateTime CreatedDateUTC { get; set; }

        public short CommandTypeId { get; set; }

        public byte CommandOriginId { get; set; }

        public byte ProcessAttempts { get; set; }

        public virtual CommandOrigin CommandOrigin { get; set; }

        public virtual CommandStatus CommandStatus { get; set; }

        public virtual CommandType CommandType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommandWorkFlow> CommandWorkFlow { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventQuee> EventQuee { get; set; }
    }
}
