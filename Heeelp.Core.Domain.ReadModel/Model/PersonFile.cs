namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonFile")]
    public partial class PersonFile
    {
        public int PersonFileId { get; set; }

        public int PersonId { get; set; }

        public long FileId { get; set; }

        public DateTime AssociatedDateUTC { get; set; }

        public int? AssocietedBy { get; set; }

        public bool Active { get; set; }

        public virtual Person Person { get; set; }

        public virtual User User { get; set; }
    }
}
