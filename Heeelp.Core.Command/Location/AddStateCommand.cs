using System;

namespace Heeelp.Core.Command.Location
{
    public class AddStateCommand : CommandBase
    {
        public AddStateCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public int StateId { get; set; }

        
        public string Name { get; set; }

       
        public string Code { get; set; }

        public int? CountryRegionId { get; set; }

        public string Coordinates { get; set; }

        public bool? Active { get; set; }

    }
}
