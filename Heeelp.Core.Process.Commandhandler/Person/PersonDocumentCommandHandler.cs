using Heeelp.Core.Command.Person;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;
using System.IO;
using System.Net.Http;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Person
{
    public class PersonDocumentCommandHandler :
        ICommandHandler<AddPersonDocumentCommand>, ICommandHandler<UpdatePersonDocument>, ICommandHandler<AddPersonDocumentNoFileCommand>
    {
        private readonly ICommandBus _bus;
        private IFileTempDao _FileTemp;
        private readonly IPersonDao _PersonDao;
        private Func<IDataContext<Domain.PersonDocument>> contextFactory;
        public PersonDocumentCommandHandler(ICommandBus bus, Func<IDataContext<Domain.PersonDocument>> contextFactory, IFileTempDao contextFactoryFileTmp, IPersonDao personDao)
        {
            this._FileTemp = contextFactoryFileTmp;
            this.contextFactory = contextFactory;
            _PersonDao = personDao;
            this._bus = bus;
        }

        public void Handle(AddPersonDocumentCommand command)
        {
            var repository = this.contextFactory();

            var person = _PersonDao.GetByPersonIntegrationId(command.PersonIntegrationId);

            var personDocument = new Domain.PersonDocument(command.PersonDocumentId, person.PersonId,
                command.DocumentTypeId, command.Number, command.Complement, command.DateIssued,
                command.DateValidUntil, DateTime.UtcNow, command.FileId, command.Active, command.UserSystemId);

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
                    fs.OriginalName = fileTmp.OriginalName;
                    fs.FileIntegrationCode = fileTmp.FileIntegrationCode;
                    fs.FileTempId = item;
                    fs.Description = "Usuário do Heeelp";
                    fs.FileUtilizationId = Convert.ToByte(GeneralEnumerators.EnumFileUtiliaztion.Album);
                    fs.FriendlyName = person.Name;
                    fs.Alt = person.Name;
                    fs.Name = person.Name;
                    fs.FileOriginTypeId = (int)GeneralEnumerators.EnumModules.Core_User;
                    fs.PersonId = command.PersonId;
                    fs.UploadedBy = command.UserSystemId;

                    var ret = fs.SendFilePath(fs);

                    if (ret > 0)
                    {
                        _FileTemp.Delete(fs.FileTempId);
                        personDocument.FileId = ret;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }

            repository.Save(personDocument);
        }

        public void Handle(UpdatePersonDocument command)
        {
            var repository = this.contextFactory();
            var personDocument = new Domain.PersonDocument(command.PersonDocumentId, command.PersonId,
                command.DocumentTypeId, command.Number, command.Complement, command.DateIssued,
                command.DateValidUntil, command.InsertedDateUTC, command.FileId, command.Active, command.UserSystemId);

            repository.Update(personDocument);
        }


        public void Handle(AddPersonDocumentNoFileCommand command)
        {
            var repository = this.contextFactory();
            var personDocument = new Domain.PersonDocument(command.PersonDocumentId, command.PersonId,
                command.DocumentTypeId, command.Number, command.Complement, command.DateIssued,
                command.DateValidUntil, command.InsertedDateUTC, command.FileId, command.Active, command.UserSystemId);
            repository.Save(personDocument);
        }
    }
}
