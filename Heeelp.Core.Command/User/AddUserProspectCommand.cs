using System;
using System.Collections.Generic;

namespace Heeelp.Core.Command.User
{
    public class AddUserProspectCommand : CommandBase
    {
        public AddUserProspectCommand()
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

        public byte UserStatusId { get; set; }

        public byte AuthenticationModeId { get; set; }

        public byte? LanguageId { get; set; }

        public DateTime CreationDateUTC { get; set; }

        public DateTime? ValidationDateUTC { get; set; }

        public short? FormFillTime { get; set; }

        public string ValidationToken { get; set; }

        public bool? SecurityCheckNecessary { get; set; }

        public short? ValidationAttempts { get; set; }

        public short? LoginFailAttempts { get; set; }

        public DateTime? LastLoginFailUTC { get; set; }

        public bool Active { get; set; }

        public string EnrollmentIP { get; set; }

        public string ValidationIP { get; set; }

        public int? CreatedBy { get; set; }

        public short ServerInstanceId { get; set; }

        public bool? IsPerpetual { get; set; }

        public List<int> listFileTemp { get; set; }

        public string LoginPassword { get; set; }

        public Guid PersonIntegrationId { get; set; }
    }
}
