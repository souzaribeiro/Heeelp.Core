namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminUser")]
    public partial class AdminUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AdminUserId { get; set; }

        [StringLength(10)]
        public string User { get; set; }
    }
}
