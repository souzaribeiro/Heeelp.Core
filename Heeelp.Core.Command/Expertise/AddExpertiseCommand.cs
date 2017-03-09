using Heeelp.Core.Common;
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
    public class AddExpertiseCommand : CommandBase
    {
        public AddExpertiseCommand()
        {
            this.Id = Guid.NewGuid();
        }

        public int ExpertiseId { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public int? ExpertiseFatherId { get; set; }

        public int CreatedBy { get; set; }

        public int PersonId { get; set; }

        public DateTime CreatedDateUTC { get; set; }

        public byte ApprovalStatusId { get; set; }

        public DateTime ApprovedDate { get; set; }

        public int ApprovedBy { get; set; }

        public string DefaultDescription { get; set; }

        public bool IsPriceDefinedEditorially { get; set; }

        public Domain.ApprovalStatus ApprovalStatus { get; set; }

        public ICollection<Domain.Expertise> Expertise1 { get; set; }

        public Domain.Expertise Expertise2 { get; set; }

        public Domain.User User { get; set; }

        public Domain.User User1 { get; set; }

        public ICollection<ExpertiseHistory> ExpertiseHistory { get; set; }

        public ICollection<ExpertisePhoto> ExpertisePhoto { get; set; }

        public ICollection<PersonExpertise> PersonExpertise { get; set; }

        public ICollection<PersonInterest> PersonInterest { get; set; }

        public List<FIleServer> FileTmp { get; set; }
        public List<int> listFileTemp { get; set; }

    }


}
