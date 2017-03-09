using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.User
{
    public class AdminSyncAllUsersCommand : CommandBase
    {

        public AdminSyncAllUsersCommand()
        {
            this.Id = Guid.NewGuid();
        }

    }
}
