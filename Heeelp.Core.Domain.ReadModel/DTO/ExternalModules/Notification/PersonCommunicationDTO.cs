using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO.ExternalModules.Notification
{
    public class PersonCommunicationDTO
    {
        public long CommunicationId { get; set; }
        public string MessageType { get; set; }
        public string TitleText { get; set; }
        public string CommunicationMessageText { get; set; }
        public string DateUTC { get; set; }
        public bool HasRead { get; set; }
    }
}
