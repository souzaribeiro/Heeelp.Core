namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FileTemp")]
    public partial class FileTemp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FileTemp()
        {
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FileTempId { get; set; }

        [Required]
        public string FilePath { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }

        public string OriginalName { get; set; }

        public Guid FileIntegrationCode { get; set; }

        public int? UploadedBy { get; set; }


    }
}
