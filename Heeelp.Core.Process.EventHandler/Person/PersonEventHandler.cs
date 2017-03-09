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
using Heeelp.Core.Process.Event;
using Heeelp.Core.Command.User;

namespace Heeelp.Core.ProcessManager.EventHandlers.Person
{
    public class PersonEventHandler :
        IEventHandler<PersonCompanyCreatedSucessEvent>,
        IEventHandler<PersonCompanyNotCreatedEvent>,
        IEventHandler<PersonEmployeeCreatedSucessEvent>,
        IEventHandler<PersonEmployeeNotCreatedEvent>,
        IEventHandler<PersonActivationUpdatedSucessEvent>,
        IEventHandler<PersonProspectFileAddEvent>,
        IEventHandler<PersonStatusUpdatedSucessEvent>,
        IEventHandler<AdminAllPersonSyncedSuccessEvent>
        

    {
        private IFileTempDao _FileTemp;
        private readonly ICommandBus bus;
        public PersonEventHandler(IFileTempDao contextFactoryFileTmp, ICommandBus bus)
        {
            this._FileTemp = contextFactoryFileTmp;
            this.bus = bus;
        }

        public void Handle(PersonCompanyCreatedSucessEvent @event)
        {
            //Cria command de sincronizacao
            SyncPersonInfoCommand syncInfo = new SyncPersonInfoCommand();
            syncInfo.Id = @event.Id;
            syncInfo.PersonId = @event.PersonId;
            syncInfo.SourceId = @event.SourceId;
            bus.Send(syncInfo);

            //Cria Command de disparo da ativacao da conta


        }

        public void Handle(PersonEmployeeCreatedSucessEvent @event)
        {
            //Cria command de sincronizacao
            SyncPersonInfoCommand syncInfo = new SyncPersonInfoCommand();
            syncInfo.Id = @event.Id;
            syncInfo.PersonId = @event.PersonId;
            syncInfo.SourceId = @event.SourceId;
            bus.Send(syncInfo);

            //Cria Command de disparo da ativacao da conta

        }


        public void Handle(PersonCompanyNotCreatedEvent @event)
        {
            //todo: Disparar o tratamento para caso de falha ao processar criacao
            throw new NotImplementedException();
        }

        public void Handle(PersonEmployeeNotCreatedEvent @event)
        {
            //todo: Disparar o tratamento para caso de falha ao processar criacao
            throw new NotImplementedException();

        }


        public void Handle(PersonProspectFileAddEvent @event)
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
                var personFileCommand = new AddPersonFileCommand()
                {
                    PersonId = @event.PersonId,
                    AssociatedDateUTC = DateTime.UtcNow,
                    FileId = ret,
                    Active = true,
                    AssocietedBy = Convert.ToInt32(@event.UploadedBy)
                };
                this.bus.Send(personFileCommand);

            }
            else
            {
                throw new Exception();
            }

        }

        // nao esta dsendo usado
        public void Handle(PersonActivationUpdatedSucessEvent @event)
        {
            //bus.Send(
            //           new SendWelcomeMessageCommand()
            //           {
            //               Id = @event.Id,
            //               PersonId = @event.PersonId,
            //               SourceId = @event.SourceId
            //           }
            //       );
        }

        public void Handle(PersonStatusUpdatedSucessEvent @event)
        {
            //quando ocorre atualizacao dos dados da pessoa eh necessario ressincronizar os dados com os demais modulos
            SyncPersonInfoCommand syncInfo = new SyncPersonInfoCommand();
            syncInfo.Id = @event.Id;
            syncInfo.PersonId = @event.PersonId;
            syncInfo.SourceId = @event.SourceId;
            bus.Send(syncInfo);
        }

        public void Handle(AdminAllPersonSyncedSuccessEvent @event)
        {

        }
    }
}