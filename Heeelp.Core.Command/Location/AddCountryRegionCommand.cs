using System;

namespace Heeelp.Core.Command.Location
{
    public class AddCountryRegionCommand : CommandBase
    {
        public AddCountryRegionCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public int CountryRegionId { get; set; }

        public string Name { get; set; }

        public byte CountryId { get; set; }
    }
}
