namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserFile")]
    public partial class UserFile
    {
        public int UserFileId { get; set; }

        public int UserId { get; set; }

        public long FileId { get; set; }

        public DateTime AssociatedDateUTC { get; set; }

        public bool Active { get; set; }

        public virtual User User { get; set; }
    }
}
