namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            Menu1 = new HashSet<Menu>();
            UserGroupMenu = new HashSet<UserGroupMenu>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MenuId { get; set; }

        [Required]
        [StringLength(50)]
        public string MenuText { get; set; }

        public int PageId { get; set; }

        [Required]
        [StringLength(250)]
        public string MenuAlt { get; set; }

        [Required]
        [StringLength(250)]
        public string MenuIcon { get; set; }

        public int? MenuFatherId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Menu> Menu1 { get; set; }

        public virtual Menu Menu2 { get; set; }

        public virtual Page Page { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserGroupMenu> UserGroupMenu { get; set; }
    }
}
