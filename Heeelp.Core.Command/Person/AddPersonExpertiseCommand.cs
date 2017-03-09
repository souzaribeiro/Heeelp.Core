using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.Person
{
    public class AddPersonExpertiseCommand : CommandBase
    {
        public AddPersonExpertiseCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int PersonPageExpertiseId { get; set; }

        public int PersonId { get; set; }

        public List<int> ExpertiseListId { get; set; }

        public DateTime InsertedDateUTC { get; set; }

        public int InsertedBy { get; set; }

        public short ServerInstanceId { get; set; }

        public long? CustomPhotoFileId { get; set; }

        public string CustomDescription { get; set; }

        public byte ExhibitionOrder { get; set; }

        public bool Active { get; set; }

        public Guid PersonIntegrationId { get; set; }
    }
}
