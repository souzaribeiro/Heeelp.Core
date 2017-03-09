using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Command.Person
{
    public class UpdatePersonDocument : CommandBase
    {
        public UpdatePersonDocument()
        {
            this.Id = Guid.NewGuid();
        }
        public int PersonDocumentId { get; set; }

        public int PersonId { get; set; }

        public short DocumentTypeId { get; set; }

        public string Number { get; set; }

        public string Complement { get; set; }

        public DateTime? DateIssued { get; set; }

        public DateTime? DateValidUntil { get; set; }

        public DateTime? InsertedDateUTC { get; set; }

        public long FileId { get; set; }

        public bool? Active { get; set; }

        public int AssociatedBy { get; set; }

        public int UserSystemId { get; set; }
    }
}
