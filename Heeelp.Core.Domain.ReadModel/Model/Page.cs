namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Page")]
    public partial class Page
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Page()
        {
            Menu = new HashSet<Menu>();
            Page1 = new HashSet<Page>();
        }

        public int PageId { get; set; }

        [Required]
        [StringLength(250)]
        public string PageURL { get; set; }

        [Required]
        [StringLength(100)]
        public string PageTitle { get; set; }

        [StringLength(250)]
        public string PageDescription { get; set; }

        public byte PageTypeId { get; set; }

        public short NumberOfAdvertisingSpaces { get; set; }

        public int PageFatherId { get; set; }

        public int PageTemplateId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Menu> Menu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Page> Page1 { get; set; }

        public virtual Page Page2 { get; set; }

        public virtual PageType PageType { get; set; }
    }
}
