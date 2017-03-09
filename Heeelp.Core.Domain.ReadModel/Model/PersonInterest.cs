namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonInterest")]
    public partial class PersonInterest
    {
        public int PersonInterestId { get; set; }

        public int PersonId { get; set; }

        public int ExpertiseId { get; set; }

        public int? DisplayOrder { get; set; }

        public DateTime InsertedDateUTC { get; set; }

        public int? InsertedBy { get; set; }

        public short ServerInstanceId { get; set; }

        public bool Active { get; set; }

        public virtual Expertise Expertise { get; set; }

        public virtual Person Person { get; set; }

        public virtual ServerInstance ServerInstance { get; set; }

        public virtual User User { get; set; }
    }
}
