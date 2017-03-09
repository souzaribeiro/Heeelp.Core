using Heeelp.Core.Command.Expertise;
using Heeelp.Core.Common;
using Heeelp.Core.Domain;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using Heeelp.Core.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain = Heeelp.Core.Domain.Expertise;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Expertise
{
    public class ExpertiseCommandHandler :
        ICommandHandler<AddExpertiseCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.Expertise>> contextFactory;
        private int fileTempId;
        private IFileTempDao _FileTemp;

        public ExpertiseCommandHandler(ICommandBus bus, Func<IDataContext<Domain.Expertise>> contextFactory, IFileTempDao contextFactoryFileTmp)
        {
            this.contextFactory = contextFactory;
            this._FileTemp = contextFactoryFileTmp;
            this.bus = bus;

        }

        public void Handle(AddExpertiseCommand command)
        {

            var repository = this.contextFactory();
            //command.CreatedBy = 0;
            var expertise = new Domain.Expertise(Guid.NewGuid(), command.ExpertiseId, command.Name, command.ExpertiseFatherId, command.CreatedBy, command.CreatedDateUTC, command.ApprovalStatusId,
                command.ApprovedBy, command.ApprovedDate, command.DefaultDescription, command.IsPriceDefinedEditorially, command.ApprovalStatus, command.Expertise1,
                command.Expertise2, command.User, command.User1, command.Active);

            repository.Save(expertise);


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
                fileTempId = item;
                fs.FileTempId = fileTempId;
                fs.Description = "Usuário do Heeelp";
                fs.FileUtilizationId = Convert.ToByte(GeneralEnumerators.EnumFileUtiliaztion.Album);
                fs.FriendlyName = command.Name;
                fs.Alt = command.Name;
                fs.Name = command.Name;
                fs.FileOriginTypeId = (int)GeneralEnumerators.EnumModules.Core_User;
                fs.PersonId = command.PersonId;
                fs.UploadedBy = command.CreatedBy;
                expertise.CompleteSuccessFile(fs);
                // repository.Save(expertise);
            }



            expertise.Complete();
            repository.Save(expertise);



        }
    }
}
