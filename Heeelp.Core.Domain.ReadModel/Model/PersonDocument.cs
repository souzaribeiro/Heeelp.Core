namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonDocument")]
    public partial class PersonDocument
    {
        public int PersonDocumentId { get; set; }

        public int PersonId { get; set; }

        public short DocumentTypeId { get; set; }

        [StringLength(50)]
        public string Number { get; set; }

        [StringLength(50)]
        public string Complement { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateIssued { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateValidUntil { get; set; }

        public DateTime? InsertedDateUTC { get; set; }

        public long FileId { get; set; }

        public bool? Active { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual Person Person { get; set; }
    }
}
