using System;

namespace Heeelp.Core.Command.User
{
    public class AddUserGroupUserCommand : CommandBase
    {
        public AddUserGroupUserCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int UserGroupUserId { get; set; }

        public int UserId { get; set; }

        public int UserGroupId { get; set; }

        public DateTime InsertedDate { get; set; }
    }
}
