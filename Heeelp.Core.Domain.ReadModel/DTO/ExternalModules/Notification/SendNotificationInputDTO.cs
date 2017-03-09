using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class SendNotificationInputDTO
    {
        public int PersonFromId { get; set; }

        public int PersonToId { get; set; }

        public string MessageCodeType { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

    }
}
