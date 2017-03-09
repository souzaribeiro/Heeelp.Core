using System;

namespace Heeelp.Core.Command.Person
{
    public class AddPersonInterestCommand : CommandBase
    {
        public AddPersonInterestCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public int PersonInterestId { get; set; }

        public int PersonId { get; set; }

        public int ExpertiseId { get; set; }

        public int? DisplayOrder { get; set; }

        public DateTime InsertedDateUTC { get; set; }

        public int? InsertedBy { get; set; }

        public short ServerInstanceId { get; set; }

        public bool Active { get; set; }

    }
}
