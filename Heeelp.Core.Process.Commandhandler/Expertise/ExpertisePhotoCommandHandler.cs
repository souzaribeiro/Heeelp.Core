using Heeelp.Core.Command.Expertise;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain = Heeelp.Core.Domain.Expertise;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Expertise
{
    public class ExpertisePhotoCommandHandler :
        ICommandHandler<AddExpertisePhotoCommand>
    {
        private Func<IDataContext<Domain.ExpertisePhoto>> contextFactory;
        public ExpertisePhotoCommandHandler(Func<IDataContext<Domain.ExpertisePhoto>> contextFactory)
        {
            this.contextFactory = contextFactory;

        }

        public void Handle(AddExpertisePhotoCommand command)
        {
            var repository = this.contextFactory();


            var expertisePhoto = new Domain.ExpertisePhoto(
                command.ExpertisePhotoId, 
                command.ExpertiseId, 
                command.FileId, 
                command.IsDefault);

            repository.Save(expertisePhoto);
        }
    }
}
