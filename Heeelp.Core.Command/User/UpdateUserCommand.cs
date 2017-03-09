using Heeelp.Core.Common;
using System;
using System.Collections.Generic;

namespace Heeelp.Core.Command.User
{
    public class UpdateUserCommand : CommandBase
    {
        public UpdateUserCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid UserIntegrationCode { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }      

        public string SmartPhoneNumber { get; set; }

        public GeneralEnumerators.EnumUserProfile UserProfileId { get; set; }

        public GeneralEnumerators.EnumUserStatus UserStatusId { get; set; }

        public string SecundaryEmail { get; set; }

        public int UpdatedBy { get; set; }
    }
}
