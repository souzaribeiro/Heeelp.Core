using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;


namespace Heeelp.Core.Domain.ReadModel.DTO
{                                           
    public class ContractListDTO
    {
        public ContractListDTO(string name , string url)
        {
            this.Name = name;
            this.Url = url;
        }
        public ContractListDTO(string name, long FileId)
        {
            this.Name = name;
            this.FileId = FileId;
        }
        public long FileId { get; set; }
        public int ContractId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }


    }          
}
