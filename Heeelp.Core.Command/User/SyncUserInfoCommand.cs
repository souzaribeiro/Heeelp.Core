using System;
using System.Collections.Generic;
using Heeelp.Core.Common;

namespace Heeelp.Core.Command.User
{
    public class SyncUserInfoCommand : CommandBase
    {
        public SyncUserInfoCommand()
        {
            this.Id = Guid.NewGuid();
        }
        
        public Guid SourceId { get; set; }
        public Guid IntegrationCode { get; set; }
        public int UserId { get; set; }
        public int PersonId { get; set; }

    }

}
