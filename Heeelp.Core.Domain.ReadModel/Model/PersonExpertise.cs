namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonExpertise")]
    public partial class PersonExpertise
    {
        [Key]
        public int PersonPageExpertiseId { get; set; }

        public int PersonId { get; set; }

        public int ExpertiseId { get; set; }

        public DateTime InsertedDateUTC { get; set; }

        public int InsertedBy { get; set; }

        public short ServerInstanceId { get; set; }

        public long? CustomPhotoFileId { get; set; }

        [StringLength(150)]
        public string CustomDescription { get; set; }

        public byte ExhibitionOrder { get; set; }

        public bool Active { get; set; }


        public virtual Expertise Expertise { get; set; }

        public virtual Person Person { get; set; }

        public virtual ServerInstance ServerInstance { get; set; }

        public virtual User User { get; set; }

        public virtual ServerInstance ServerInstance1 { get; set; }
    }
}
