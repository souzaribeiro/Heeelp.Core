namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Infrastructure.Database;
    using Infrastructure.Messaging;
    using System.Data.Entity.Spatial;

    [Table("PersonAddress")]
    public partial class PersonAddress : IAggregateRoot, IEventPublisher
    {
        public PersonAddress()
        {

        }
        public PersonAddress( int personId, byte addressTypeId,
            string streetName, string number,
            string neighbourhood,string country,
            string state, string city, string postCode, 
            DbGeography coordinates, string contactPhoneNumber,
            int createdBy, string contactEmail)
        {
            this.PersonId = personId;
            this.AddressTypeId = addressTypeId;
            this.StartDateUTC = DateTime.UtcNow;
            this.StreetName = streetName;
            this.Number = number;
            this.Neighbourhood = neighbourhood;
            this.Country = country;
            this.State = state;
            this.City = city;  
            this.PostCode = postCode;
            this.Coordinates = coordinates;
            this.ContactPhoneNumber = contactPhoneNumber;
            this.ServerInstanceId = 1;
            this.CreatedBy = createdBy;
            this.ContactEmail = contactEmail;
            this.Active = true;
            
        }


        public PersonAddress(int personAddressId, int personId, byte addressTypeId,
           DateTime startDateUTC, string streetName, string number,
           string neighbourhood, string country, string state, string city, string postCode, DbGeography coordinates, string contactPhoneNumber,
           short serverInstanceId, int createdBy, string contactEMail, bool active)
        {
            this.PersonAddressId = personAddressId;
            this.PersonId = personId;
            this.AddressTypeId = addressTypeId;
            this.StartDateUTC = startDateUTC;
            this.StreetName = streetName;
            this.Number = number;
            this.Neighbourhood = neighbourhood;
            this.Country = country;
            this.State = state;
            this.City = city;
            this.PostCode = postCode;
            this.Coordinates = coordinates;
            this.ContactPhoneNumber = contactPhoneNumber;
            this.ServerInstanceId = serverInstanceId;
            this.CreatedBy = createdBy;
            this.ContactEmail = contactEMail;
            this.Active = active;

        }


        public PersonAddress(int personAddressId, int personId, byte addressTypeId,
            DateTime startDateUTC, string streetName, string number, int neighbourhoodId,
            string neighbourhood, string country, string state, string city, string postCode, DbGeography coordinates, string contactPhoneNumber,
            short serverInstanceId, int createdBy, string contactEMail, bool active)
        {
            this.PersonAddressId = personAddressId;
            this.PersonId = personId;
            this.AddressTypeId = addressTypeId;
            this.StartDateUTC = startDateUTC;
            this.StreetName = streetName;
            this.Number = number;
            this.NeighbourhoodId = neighbourhoodId;
            this.Neighbourhood = neighbourhood;
            this.Country = country;
            this.State = state;
            this.City = city;
            this.PostCode = postCode;
            this.Coordinates = coordinates;
            this.ContactPhoneNumber = contactPhoneNumber;
            this.ServerInstanceId = serverInstanceId;
            this.CreatedBy = createdBy;
            this.ContactEmail = contactEMail;
            this.Active = active;

        }
        [NotMapped]
        public Guid Id { get; set; }

        [NotMapped]
        private List<IEvent> events = new List<IEvent>();

        [NotMapped]
        public IEnumerable<IEvent> Events { get { return this.events; } }

        protected void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }
        public int PersonAddressId { get; set; }

        public int PersonId { get; set; }

        public byte AddressTypeId { get; set; }
        public string Complement { get; set; }



        public DateTime StartDateUTC { get; set; }

        [StringLength(150)]
        public string StreetName { get; set; }

        public string Number { get; set; }

        public int NeighbourhoodId { get; set; }

        public string Neighbourhood { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        [Required]
        [StringLength(15)]
        public string PostCode { get; set; }

        public DbGeography Coordinates { get; set; }

        
        [StringLength(15)]
        public string ContactPhoneNumber { get; set; }

        public short ServerInstanceId { get; set; }

        public int CreatedBy { get; set; }

        [StringLength(50)]
        public string ContactEmail { get; set; }

        public bool Active { get; set; }

       

        [NotMapped]
        public virtual AddressType AddressType { get; set; }

        [NotMapped]
        public virtual Neighbourhood Neighbourhoods { get; set; }

        [NotMapped]
        public virtual Person Person { get; set; }

        [NotMapped]
        public virtual User User { get; set; }

    }
}
