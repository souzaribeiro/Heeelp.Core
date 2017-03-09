namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("AdminUser")]
    public partial class AdminUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AdminUserId { get; set; }

        [StringLength(10)]
        public string User { get; set; }
    }
}
