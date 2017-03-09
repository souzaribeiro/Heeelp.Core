using Heeelp.Core.Domain;
using Heeelp.Core.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain = Heeelp.Core.Domain;

namespace Heeelp.Core.Command.Expertise
{
    public class AddExpertisePhotoCommand : CommandBase
    {
        public AddExpertisePhotoCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int ExpertisePhotoId { get; set; }

        public int ExpertiseId { get; set; }

        public long FileId { get; set; }

        public bool IsDefault { get; set; }


    }


}
