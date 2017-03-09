namespace Heeelp.Core.Domain
{
    using Infrastructure.Database;
    using Infrastructure.Messaging;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("PersonContract")]
    public class PersonContract 
    {
        [NotMapped]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonContractId { get; set; }

        public int PersonId { get; set; }

        public short ContractId { get; set; }

        public int UserId { get; set; }

        public long FileId { get; set; }

        public DateTime AgreementDateUTC { get; set; }
        
        public bool Active { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual Person Person { get; set; }
    }
}
