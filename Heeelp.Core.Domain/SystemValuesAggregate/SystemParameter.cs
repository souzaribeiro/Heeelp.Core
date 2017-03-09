namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("SystemParameter")]
    public partial class SystemParameter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SystemParameterId { get; set; }

        [StringLength(50)]
        public string ParamName { get; set; }

        [StringLength(250)]
        public string ParamValue { get; set; }

        public int? AdminUserId { get; set; }
    }
}
