using System;

namespace Heeelp.Core.Command.Person
{
    public class AddPersonFileCommand : CommandBase
    {
        public AddPersonFileCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int PersonFileId { get; set; }

        public int PersonId { get; set; }

        public long FileId { get; set; }

        public DateTime AssociatedDateUTC { get; set; }

        public int? AssocietedBy { get; set; }

        public bool Active { get; set; }
    }
}
