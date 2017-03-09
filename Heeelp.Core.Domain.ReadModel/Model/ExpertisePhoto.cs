namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExpertisePhoto")]
    public partial class ExpertisePhoto
    {
        public int ExpertisePhotoId { get; set; }

        public int ExpertiseId { get; set; }

        public long FileId { get; set; }

        public bool IsDefault { get; set; }

        public virtual Expertise Expertise { get; set; }
    }
}
