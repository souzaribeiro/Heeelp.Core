namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FIleTemp")]
    public partial class FIleTemp
    {
        public int FileTempId { get; set; }

        [Required]
        [StringLength(200)]
        public string FilePath { get; set; }

        [Required]
        [StringLength(50)]
        public string Width { get; set; }

        [Required]
        [StringLength(50)]
        public string Height { get; set; }

        [Required]
        [StringLength(200)]
        public string OriginalName { get; set; }

        public int? ShowOrder { get; set; }

        public int? IsDefault { get; set; }

        public Guid? FileIntegrationCode { get; set; }

        public int? UploadedBy { get; set; }
    }
}
