using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO.ExternalModules.Notification
{
    public class PersonCommunicationListDTO
    {
        public PersonCommunicationListDTO()
        {
            NotificationList = new List<PersonCommunicationDTO>();
        }
        public List<PersonCommunicationDTO> NotificationList { get; set; }

        public int NotificationsNotReadCount { get; set; }
    }
}
