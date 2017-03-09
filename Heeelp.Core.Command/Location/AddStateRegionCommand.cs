using System;

namespace Heeelp.Core.Command.Location
{
    public class AddStateRegionCommand : CommandBase
    {
        public AddStateRegionCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public int StateRegionId { get; set; }
        
        public string Name { get; set; }

        public int StateId { get; set; }

        public string Coordinates { get; set; }

        public bool? Active { get; set; }

        public DateTime? InsertedDate { get; set; }
    }
}
