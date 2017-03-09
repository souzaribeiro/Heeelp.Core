namespace Heeelp.Core.Domain
{
    using Infrastructure.Database;
    using Infrastructure.Messaging;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("PersonBenefitClub")]
    public class PersonBenefitClub
    {
        [NotMapped]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonBenefitClubId { get; set; }
        public int PersonId { get; set; }
        public string CustomHeeelpPersonDomain { get; set; }    
        public string CustomClubName { get; set; }
        public string CustomClubLogo { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public bool Active { get; set; }
        [NotMapped]
        public virtual Person Person { get; set; }
    }
}
