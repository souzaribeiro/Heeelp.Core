using Heeelp.Core.Command.ExternalModules;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using Heeelp.Core.Logging;
using System;
using System.Net.Http;

namespace Heeelp.Core.Process.CommandHandler.ExternalModules
{
    public class NotificationGatewayCommandHandler :  ICommandHandler<SendExternalNotificationCommand>
    {

        private IPersonDao _personDao;
        private IUserDao _userDao;

        public NotificationGatewayCommandHandler(IPersonDao personDao, IUserDao UserDao)
        {
            this._personDao = personDao;
            this._userDao = UserDao;
        }

        public void Handle(SendExternalNotificationCommand command)
        {
            
            PersonDTO personFrom = _personDao.GetPerson(command.PersonFromId);
            PersonDTO personTo = _personDao.GetPerson(command.PersonToId);
            //Person company = _personDao.GetCompanyByEmployeePersonId((int)personTo.PersonFatherId);

            //byte companyProfile = 0;
            ////todo: implementar da forma correta o Get first or default
            //foreach (var pr in company.PersonRules)
            //{
            //    companyProfile = pr.PersonProfileId;
            //}           

            //todo: substituir as mensagens em hard code por arquivos de resources para permitir traducao para outros idiomas
            var message = new ExternalNotificationDTO(personFrom.PersonId,
                personTo.PersonId,
                personTo.Name,
                (int)GeneralEnumerators.EnumLanguage.Portuguese,
                command.MessageCodeType,
                command.Title,
                command.Body,
                personTo.CustomClubName,
                personTo.CustomHeeelpPersonDomain,
                personTo.CustomClubLogo);

            var _clientNotification = new HttpClient();
            _clientNotification.BaseAddress = new Uri(CustomConfiguration.WebApiNotification);
            var resultNotification = _clientNotification.PostAsJsonAsync("api/Communication/SendMessage", message).Result;
            if (!resultNotification.IsSuccessStatusCode)
            {
                LogManager.Info(string.Format("Erro ao enviar notificacao. personFrom: {0}, personTo: {1}, title: {2}", command.PersonFromId, command.PersonToId, command.Title));
            }

        }

    }
}
