using System;

namespace Heeelp.Core.Command.User
{
    public class AddUserModuleCommand : CommandBase
    {
        public AddUserModuleCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public long UserModuleId { get; set; }

        public int UserId { get; set; }

        public short ModuleId { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool Active { get; set; }
    }
}
