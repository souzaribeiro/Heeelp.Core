using System;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class CompanyReturnDTO
    {
        public Guid CompanyID { get; set; }
        public string ManagerHash { get; set; }
        public Guid ManagerEmployeeId { get; set; }
    }
}
