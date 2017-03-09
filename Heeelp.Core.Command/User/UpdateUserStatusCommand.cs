using System;
using System.Collections.Generic;

namespace Heeelp.Core.Command.User
{
    public class UpdateUserStatusCommand : CommandBase
    {
        public UpdateUserStatusCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid IntegrationCode { get; set; }

        public int UserId { get; set; }
                                        
        public byte? UserStatusId { get; set; }
    }
}
