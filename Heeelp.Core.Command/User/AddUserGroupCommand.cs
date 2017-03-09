using System;

namespace Heeelp.Core.Command.User
{
    public class AddUserGroupCommand : CommandBase
    {
        public AddUserGroupCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int UserGroupId { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }
    }
}
