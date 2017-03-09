namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("ExpertiseHistory")]
    public partial class ExpertiseHistory
    {
        public int ExpertiseHistoryId { get; set; }

        public int ExpertiseId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool Active { get; set; }

        public int? ExpertiseFatherId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDateUTC { get; set; }

        public byte ApprovalStatusId { get; set; }

        public int ApprovedBy { get; set; }

        public DateTime ApprovedDate { get; set; }

        public long DefaultIconFileId { get; set; }

        public long DefaultPhotoFileId { get; set; }

        [Required]
        [StringLength(150)]
        public string DefaultDescription { get; set; }

        public virtual Expertise Expertise { get; set; }
    }
}
