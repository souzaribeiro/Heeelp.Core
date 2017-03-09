using System;
using System.Collections.Generic;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO;

namespace Heeelp.Core.Command.Person
{
    public class UploadLogoPersonCommand : CommandBase
    {
        public UploadLogoPersonCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int CreatedBy { get; set; }

        public Guid IntegrationCode { get; set; }

        public List<FileTempDTO> ListFileTemp { get; set; }

    }
}
