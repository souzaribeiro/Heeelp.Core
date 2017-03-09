using Heeelp.Core.Common;
using Heeelp.Core.Event;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using Heeelp.Core.Process.Event.User;
using Heeelp.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Heeelp.Core.Process.Event.Person;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Command.Person;

namespace Heeelp.Core.ProcessManager.EventHandlers.Person
{
    public class PersonDocumentCreatedEventHandler : IEventHandler<PersonDocumentFileAddedEvent>, IEventHandler<PersonDocumentAdded>
    {
        private IFileTempDao _FileTemp;
        private readonly ICommandBus bus;
        public PersonDocumentCreatedEventHandler(IFileTempDao contextFactoryFileTmp, ICommandBus bus)
        {
            this._FileTemp = contextFactoryFileTmp;
            this.bus = bus;
        }

        public void Handle(PersonDocumentFileAddedEvent @event)
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
                var personFileCommand = new UpdatePersonDocument()
                {
                    PersonDocumentId = @event.PersonDocumentId,
                    PersonId = @event.PersonId,
                    DocumentTypeId = @event.DocumentTypeId,
                    InsertedDateUTC = @event.InsertedDateUTC,
                    FileId = ret,
                    Active = @event.Active,
                    UserSystemId = Convert.ToInt32(@event.UploadedBy)

                };
                this.bus.Send(personFileCommand);

            }
            else
            {
                throw new Exception();
            }

        }

        public void Handle(PersonDocumentAdded @event)
        {
            var command = new AddPersonDocumentNoFileCommand();
            command.DocumentTypeId = @event.DocumentTypeId;
            command.Number = @event.Number;
            command.Active = @event.Active;
            command.PersonId = @event.PersonId;
            command.UserSystemId = @event.UserSystemId;

            this.bus.Send(command);
        }
    }
}