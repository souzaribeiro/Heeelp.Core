using System;

namespace Heeelp.Core.Command.Location
{
    public class AddNeighbourhoodCommand : CommandBase
    {
        public AddNeighbourhoodCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public int NeighbourhoodId { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public int? NeighbourhoodFatherId { get; set; }

        public string Coordinates { get; set; }

        public bool Active { get; set; }

        public DateTime InsertedDate { get; set; }

        public int? CityZoneId { get; set; }

        public string PostCode { get; set; }
    }
}
