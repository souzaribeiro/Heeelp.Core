using System;

namespace Heeelp.Core.Command.Location
{
    public class AddCityZoneCommand : CommandBase
    {
        public AddCityZoneCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public int CityZoneId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int CityId { get; set; }

        public DateTime InsertedDate { get; set; }

        public bool Active { get; set; }
    }
}
