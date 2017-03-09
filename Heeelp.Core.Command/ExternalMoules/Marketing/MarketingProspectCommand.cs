using Heeelp.Core.Common;
using System;
using System.Collections.Generic;

namespace Heeelp.Core.Command.ExternalModules
{
    public class MarketingProspectCommand : CommandBase
    {
        public MarketingProspectCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Message { get; set; }

        public string Address { get; set; }

        public int? RecommendedBy { get; set; }


    }
}
