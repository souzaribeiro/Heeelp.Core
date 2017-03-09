namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommandType")]
    public partial class CommandType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CommandType()
        {
            CommandQuee = new HashSet<CommandQuee>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CommandTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public byte Priority { get; set; }

        public bool Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommandQuee> CommandQuee { get; set; }
    }
}
