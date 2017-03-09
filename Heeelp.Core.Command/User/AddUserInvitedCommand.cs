using System;
using System.Collections.Generic;
using Heeelp.Core.Common;

namespace Heeelp.Core.Command.User
{
    public class AddUserInvitedCommand : CommandBase
    {
        public AddUserInvitedCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public string Email { get; set; }
        public string InviteCode { get; set; }
        public string Name { get; set; }
        public string EnrollmentIP { get; set; }
    }
}
