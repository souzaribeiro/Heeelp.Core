using System;
using System.Collections.Generic;

namespace Heeelp.Core.Command.User
{
    public class InactiveEmployeeCommand : CommandBase
    {
        public InactiveEmployeeCommand()
        {
            this.Id = Guid.NewGuid();
        }
        
        public List<Guid> EmployeeListId { get; set; }
        public Guid IntegrationCode { get; set; }
        public int DeletedBy { get; set; }

    }
}
