namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using static Common.GeneralEnumerators;
    [Table("User")]
    public partial class Authentication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Authentication()
        { }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string LoginPassword { get; set; }

        public byte UserProfileId { get; set; }

        public int Complete { get; set; }

        public int PersonId { get; set; }

        public long? InviteId { get; set; }

        public ICollection<PersonRules> PersonRules { get; set; }

        public List<int> ProfileClaims { get; set; }
        
        public byte PersonTypeId { get; internal set; }

        public Guid? PersonIntegrationCode { get; internal set; }

        public Guid? UserIntegrationCode { get; internal set; }

        public string ImgProfileLogo { get; set; }
    }
}
