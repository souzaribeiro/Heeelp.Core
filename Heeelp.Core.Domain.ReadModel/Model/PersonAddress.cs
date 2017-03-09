namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonAddress")]
    public partial class PersonAddress
    {
        public int PersonAddressId { get; set; }

        public int PersonId { get; set; }

        public byte AddressTypeId { get; set; }

        public DateTime StartDateUTC { get; set; }

        [StringLength(150)]
        public string StreetName { get; set; }

        public int? Number { get; set; }

        public int NeighbourhoodId { get; set; }
        public string Complement { get; set; }

        public string Neighbourhood { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        [Required]
        [StringLength(15)]
        public string PostCode { get; set; }

        public DbGeography Coordinates { get; set; }

        [Required]
        [StringLength(15)]
        public string ContactPhoneNumber { get; set; }

        public short ServerInstanceId { get; set; }

        public int CreatedBy { get; set; }

        [StringLength(50)]
        public string ContactEMail { get; set; }

        public bool Active { get; set; }

        public virtual AddressType AddressType { get; set; }

        public virtual Person Person { get; set; }

        public virtual User User { get; set; }
    }
}
