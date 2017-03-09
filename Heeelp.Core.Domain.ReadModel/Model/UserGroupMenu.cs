namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserGroupMenu")]
    public partial class UserGroupMenu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserGroupMenuId { get; set; }

        public int UserGroupId { get; set; }

        public int MenuId { get; set; }

        public DateTime InsertedDate { get; set; }

        public virtual Menu Menu { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
