namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserModule")]
    public partial class UserModule
    {
        public long UserModuleId { get; set; }

        public int UserId { get; set; }

        public short ModuleId { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool Active { get; set; }

        public virtual Module Module { get; set; }

        public virtual User User { get; set; }
    }
}
