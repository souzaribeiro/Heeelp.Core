namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonContract")]
    public partial class PersonContract
    {
        public int PersonContractId { get; set; }

        public int PersonId { get; set; }

        public short ContractId { get; set; }

        public int UserId { get; set; }

        public DateTime AgreementDateUTC { get; set; }

        public bool Active { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual Person Person { get; set; }
    }
}
