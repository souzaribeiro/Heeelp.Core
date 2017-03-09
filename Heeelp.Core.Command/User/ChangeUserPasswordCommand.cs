using Heeelp.Core.Common;
using System;
using System.Collections.Generic;

namespace Heeelp.Core.Command.User
{
    public class ChangeUserPasswordCommand : CommandBase
    {
        public ChangeUserPasswordCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid IntegrationCode { get; set; }
        public string Password { get; set; }


    }
}
