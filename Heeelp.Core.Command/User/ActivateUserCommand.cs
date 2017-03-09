using System;
using System.Collections.Generic;

namespace Heeelp.Core.Command.User
{
    public class ActivateUserCommand : CommandBase
    {
        public ActivateUserCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid IntegrationCode { get; set; }
                                              
        public string Password { get; set; }

        public string PhoneNumber { get; set; }     

        public bool UserAgree { get; set; }

    }
}
