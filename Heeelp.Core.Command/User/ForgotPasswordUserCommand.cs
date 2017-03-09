using Heeelp.Core.Common;
using System;
using System.Collections.Generic;

namespace Heeelp.Core.Command.User
{
    public class ForgotPasswordUserCommand : CommandBase
    {
        public ForgotPasswordUserCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public string Email { get; set; }


    }
}
