using System;

namespace Heeelp.Core.Command.User
{
    public class AddUserFileCommand : CommandBase
    {
        public AddUserFileCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int UserFileId { get; set; }

        public int UserId { get; set; }

        public long FileId { get; set; }

        public DateTime AssociatedDateUTC { get; set; }

        public bool Active { get; set; }
    }
}
