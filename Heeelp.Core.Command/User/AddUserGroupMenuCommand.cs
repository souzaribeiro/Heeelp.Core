using System;

namespace Heeelp.Core.Command.User
{
    public class AddUserGroupMenuCommand : CommandBase
    {
        public AddUserGroupMenuCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int UserGroupMenuId { get; set; }

        public int UserGroupId { get; set; }

        public int MenuId { get; set; }

        public DateTime InsertedDate { get; set; }
    }
}
