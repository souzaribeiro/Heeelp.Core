using System;
using System.Collections.Generic;
using Heeelp.Core.Common;

namespace Heeelp.Core.Command.User
{
    public class AddSelfRegistrationCommand : CommandBase
    {
        public AddSelfRegistrationCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string EnrollmentIP { get; set; }
        public DateTime CreationDateUTC { get; set; }
        public string InviteCode { get; set; }
        public Guid PersonIntegrationId { get; set; }
    }
}
