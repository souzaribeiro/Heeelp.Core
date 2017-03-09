using System;
using System.Collections.Generic;
using Heeelp.Core.Common;

namespace Heeelp.Core.Command.User
{
    public class AddUserCommand : CommandBase
    {
        public AddUserCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int UserId { get; set; }

        public Guid IntegrationCode { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string SecundaryEmail { get; set; }

        public string SmartPhoneNumber { get; set; }

        public int PersonId { get; set; }

        public bool IsDefaultUser { get; set; }

        public byte UserProfileId { get; set; }

        public byte AuthenticationModeId { get; set; }

        public string EnrollmentIP { get; set; }

        public int? CreatedBy { get; set; }

        public bool? IsPerpetual { get; set; }

        public string LoginPassword { get; set; }

        public GeneralEnumerators.EnumPersonProfile PersonProfile { get; set; }

        public byte SkinId  { get; set; }

    }
}
