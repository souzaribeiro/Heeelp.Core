using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class AssociateCompanyUserDTO
    {


        public string Name { get; set; }
        public string Email { get; set; }
        public string SecundaryEmail { get; set; }
        public string SmartPhoneNumber { get; set; }
        public Guid CompanyId { get; set; }
        public byte UserProfileId { get; set; }
        //public byte UserStatusId { get; set; }
        //public int UserSystemId { get; set; }
        //aaa


    }
}
