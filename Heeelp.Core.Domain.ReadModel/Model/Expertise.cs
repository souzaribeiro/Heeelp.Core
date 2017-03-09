namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Expertise")]
    public partial class Expertise
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Expertise()
        {
            Expertise1 = new HashSet<Expertise>();
            ExpertiseHistory = new HashSet<ExpertiseHistory>();
            ExpertisePhoto = new HashSet<ExpertisePhoto>();
            PersonExpertise = new HashSet<PersonExpertise>();
            PersonInterest = new HashSet<PersonInterest>();
        }

        public int ExpertiseId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool Active { get; set; }

        public int? ExpertiseFatherId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDateUTC { get; set; }

        public int ApprovalStatusId { get; set; }

        public int ApprovedBy { get; set; }

        public DateTime ApprovedDate { get; set; }

        [Required]
        [StringLength(150)]
        public string DefaultDescription { get; set; }

        public bool IsPriceDefinedEditorially { get; set; }

        public virtual ApprovalStatus ApprovalStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expertise> Expertise1 { get; set; }

        public virtual Expertise Expertise2 { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExpertiseHistory> ExpertiseHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExpertisePhoto> ExpertisePhoto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonExpertise> PersonExpertise { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonInterest> PersonInterest { get; set; }
    }
}
