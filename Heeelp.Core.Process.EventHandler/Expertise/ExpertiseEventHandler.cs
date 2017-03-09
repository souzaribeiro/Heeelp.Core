using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging.Handling;
//using Heeelp.Core.NotificationService;
using Heeelp.Core.Process.Event.Expertise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heeelp.Core.Domain;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Configuration;
using System.Net.Http.Headers;
using Heeelp.Core.Common;
using Heeelp.Core.Logging;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Command.Expertise;
using Heeelp.Core.Infrastructure.Messaging;

namespace Heeelp.Core.ProcessManager.EventHandlers.Expertise
{
    public class ExpertiseEventHandler : IEventHandler<ExpertiseAdded>,
        IEventHandler<ExpertiseCreatedFIleEvent>,
        IEventHandler<ExpertiseMarketingAdd>
    {
        private Func<IDataContext<Domain.Expertise>> contextFactory;
        private IFileTempDao _FileTemp;
        static HttpClient client = new HttpClient();
        private readonly ICommandBus bus;
        public ExpertiseEventHandler(Func<IDataContext<Domain.Expertise>> contextFactory, IFileTempDao contextFactoryFileTmp, ICommandBus bus)
        {
            this.contextFactory = contextFactory;
            client.BaseAddress = new Uri(CustomConfiguration.WebApiNotification);
            this._FileTemp = contextFactoryFileTmp;
            this.bus = bus;
        }


        public void Handle(ExpertiseAdded @event)
        {
            var context = contextFactory();

            Dictionary<string, string> listKeys = new Dictionary<string, string>();
            listKeys.Add("ExpertiseName", @event.ExpertiseName);
            var message = new { UserFromId = @event.RegisterUserId, UserToId = @event.RegisterUserId, LanguageId = 1, MessageTypeID = 1, ListKeys = listKeys };
            var resultTask = client.PostAsJsonAsync("api/Communication/SendMessage", message).Result;
            if (!resultTask.IsSuccessStatusCode)
            {
                throw new Exception();
            }

        }

        public void Handle(ExpertiseMarketingAdd @event)
        {
            //TODO devera enviar a expertise para o modulo marketing
        }
        public void Handle(ExpertiseCreatedFIleEvent @event)
        {
            FIleServer fs = new FIleServer();
            fs.FilePath = @event.FilePath;
            fs.Width = @event.Width;
            fs.Height = @event.Height;
            fs.OriginalName = @event.OriginalName;
            fs.FileTempId = @event.FileTempId;
            fs.Description = @event.Description;
            fs.FriendlyName = @event.FriendlyName;
            fs.Alt = @event.Alt;
            fs.Name = @event.Name;
            fs.FileOriginTypeId = @event.FileOriginTypeId;
            fs.PersonId = @event.PersonId;
            fs.FileUtilizationId = @event.FileUtilizationId;
            fs.UploadedBy = @event.UploadedBy;

            var ret = fs.SendFilePath(fs);

            if (ret > 0)
            {
                _FileTemp.Delete(fs.FileTempId);
                var expertisePhoto = new AddExpertisePhotoCommand()
                {
                    ExpertiseId = @event.ExpertiseId,
                    ExpertisePhotoId = 1,
                    FileId = ret,
                    IsDefault = true
                };
                this.bus.Send(expertisePhoto);

            }
            else
            {
                throw new Exception();
            }
        }
    }
}
