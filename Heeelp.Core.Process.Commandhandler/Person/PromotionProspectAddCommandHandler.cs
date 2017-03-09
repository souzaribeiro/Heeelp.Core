using Heeelp.Core.Command.Person;
using Heeelp.Core.Command.RegisterPromotion;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using Heeelp.Core.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Person
{
    public class PromotionProspectAddCommandHandler :
        ICommandHandler<AddPromotionProspectCommand>
    {
        private readonly ICommandBus bus;
        private readonly IPersonDao _PersonDao;
        private IFileTempDao _FileTemp;
        public PromotionProspectAddCommandHandler(IPersonDao personDao, IFileTempDao contextFactoryFileTmp)
        {
            _PersonDao = personDao;
            this._FileTemp = contextFactoryFileTmp;
        }

        public void Handle(AddPromotionProspectCommand command)
        {
            var person = _PersonDao.GetByPersonIntegrationId(command.PersonIntegrationID);
            command.PersonId = person.PersonId;
            List<long> listImgIds = new List<long>();

            if (command.listFileTemp != null)
            {
                foreach (var item in command.listFileTemp)
                {
                    FIleServer fs = new FIleServer();

                    Domain.ReadModel.FileTemp fileTmp = new Domain.ReadModel.FileTemp();
                    fileTmp = _FileTemp.Get(item);
                    fs.FilePath = fileTmp.FilePath;
                    fs.Width = fileTmp.Width;
                    fs.Height = fileTmp.Height;
                    fs.OriginalName = fileTmp.OriginalName.Replace(".jpg.jpeg", ".jpg");
                    fs.FileIntegrationCode = fileTmp.FileIntegrationCode;
                    fs.FileTempId = item;
                    fs.Description = "Usuário do Heeelp";
                    fs.FileUtilizationId = Convert.ToByte(GeneralEnumerators.EnumFileUtiliaztion.Album);
                    fs.FriendlyName = fileTmp.OriginalName;
                    fs.Alt = fileTmp.OriginalName;
                    fs.Name = fileTmp.OriginalName;
                    fs.FileOriginTypeId = (int)GeneralEnumerators.EnumModules.Core_User;
                    fs.PersonId = command.PersonId;
                    fs.UploadedBy = Convert.ToInt32(command.UserSystemId);
                    var ret = fs.SendFilePath(fs);
                    if (ret > 0)
                    {
                        listImgIds.Add(ret);
                        _FileTemp.Delete(fs.FileTempId);
                    }
                    else
                    {
                        throw new Exception();
                    }

                }
            }                                                                                                     
            command.FilesIdList = listImgIds;



            string url = string.Empty;
            switch ((GeneralEnumerators.enumPromotionType)command.PromotionTypeId)
            {
                case GeneralEnumerators.enumPromotionType.Discount:
                    url = "api/Promotion/AddDiscountPromotion";
                    break;
                case GeneralEnumerators.enumPromotionType.Award:
                    url = "api/Promotion/AddAwardPromotion";
                    break;
                case GeneralEnumerators.enumPromotionType.Gift:
                    url = "api/Promotion/AddGiftPromotion";
                    break;
            }


            var _clientPromotion = new HttpClient();
            _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
            var resultTask = _clientPromotion.PostAsJsonAsync(url, command).Result;
            if (!resultTask.IsSuccessStatusCode)
            {
                LogManager.Error(url +" Handler: Erro ao enviar web.api promotion:  status: " + resultTask.StatusCode);
            }
            else {                 
                 resultTask = _clientPromotion.PostAsJsonAsync("api/Promotion/UploadPromotionPhoto", command).Result;
                if (!resultTask.IsSuccessStatusCode)
                {
                    LogManager.Error("UploadPromotionPhoto Handler: Erro ao enviar web.api promotion:  status: " + resultTask.StatusCode);
                }
            }
        }
    }
}
