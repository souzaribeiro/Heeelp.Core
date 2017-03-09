namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserGroupUser")]
    public partial class UserGroupUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserGroupUserId { get; set; }

        public int UserId { get; set; }

        public int UserGroupId { get; set; }

        public DateTime InsertedDate { get; set; }

        public virtual User User { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
