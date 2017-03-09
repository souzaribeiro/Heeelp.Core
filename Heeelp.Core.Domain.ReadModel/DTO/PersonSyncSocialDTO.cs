using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class PersonSyncSocialDTO: PersonSyncDTO
    {
        public string Name { get; set; }// o nome precisa ser enviado para o modulo Social. Para os demais modulos, nao deve ser enviado        
    }
}
