namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Skin")]
    public partial class Skin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Skin()
        {   
        }          
        public byte SkinId { get; set; }       

        public string Name { get; set; }       
                                                           
        public bool Active { get; set; }                      

        public string CssFileSkin { get; set; }     
    }
}
