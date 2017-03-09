using Heeelp.Core.Common;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using Heeelp.Core.Process.Event.User;
using Heeelp.Core.Logging;
using System;
using System.Net.Http;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Command.User;
using System.Collections.Generic;
using Heeelp.Core.Command.Person;

namespace Heeelp.Core.ProcessManager.EventHandlers.User
{
    public class UserEventHandler : 
        IEventHandler<UserCreatedSuccessEvent>,
        IEventHandler<UserNotCreatedEvent>,
        IEventHandler<UserCreatedFIleEvent>,
        IEventHandler<UserSyncedSuccessEvent>,
        IEventHandler<UserNotSyncedEvent>,
        IEventHandler<NewUserWelcomeMessageEvent>,
        IEventHandler<UserActivatedEvent>

    {
        private IFileTempDao _FileTemp;
        private readonly ICommandBus bus;
        private HttpClient _clientNotification;
        private IPersonDao _personDao;
        private IUserDao _userDao;
        private IContractDao _contractDao;

        public UserEventHandler(IFileTempDao contextFactoryFileTmp, ICommandBus bus,  IPersonDao personDao, IUserDao userDao, IContractDao contractDao)
        {
            this._FileTemp = contextFactoryFileTmp;
            this.bus = bus;
            this._personDao = personDao;
            this._userDao = userDao;
            this._contractDao = contractDao;
        }

        public void Handle(UserCreatedSuccessEvent @event)
        {
            //cria o comando de sincronizacao dos dados de usuario nos demais modulos
            bus.Send(
                    new SyncUserInfoCommand()
                    {
                        Id = @event.Id,
                        IntegrationCode = @event.IntegrationCode,
                        PersonId = @event.PersonId,
                        SourceId = @event.SourceId,
                        UserId = @event.UserId,
                    }                
                );            
        }

        public void Handle(UserNotCreatedEvent @event)
        {
            throw new NotImplementedException();
        }

        public void Handle(UserSyncedSuccessEvent @event)
        {
            ;// throw new NotImplementedException();
        }

        public void Handle(UserNotSyncedEvent @event)
        {
            throw new NotImplementedException();
        }

        public void Handle(UserActivatedEvent @event)
        {
            try
            {
                var userPF = _userDao.GetUserFullInfoUserPF(@event.UserPFId);

                var userPJ = _userDao.GetUserFullInfoUserPJ(userPF.Email);
                    
                //o usuario vai estar sempre atrelado a uma pessoa fisica. Necessario dar o aceite aos termos de uso e politica de privacidade associado a pessoa fisica
                //adicionalmente, essa pessoa fisica pode ser o usuario gestor da empresa. Nesse caso, ele aceita tambem os termos pela sua companhia.
                //nesse caso, necessario dar aceite aos termos de uso e politica de privacidade para a pesoa juridica
                var terms = _contractDao.GetLastestVersionContract(Domain.DomainEnumerators.enumContractType.TermosDeUsoColaborador);
                this.bus.Send(new AddPersonContractCommand()
                {
                    UserId = userPF.UserId,
                    PersonId = userPF.PersonId,
                    ContractId = terms.ContractId,
                    FileId = terms.FileId
                });

                var privacyPolicy = _contractDao.GetLastestVersionContract(Domain.DomainEnumerators.enumContractType.PoliticaDePrivacidade);
                this.bus.Send(new AddPersonContractCommand()
                {
                    UserId = userPF.UserId,
                    PersonId = userPF.PersonId,
                    ContractId = privacyPolicy.ContractId,
                    FileId = privacyPolicy.FileId
                });


                //se o usuario é o gestor princial de uma empresa, ele aceita tambem os termos em nome da empresa
                if (userPJ.UserProfileId == (byte)GeneralEnumerators.EnumUserProfile.Administrador && userPJ.IsDefaultUser == true)
                {                    
                    Domain.ReadModel.Contract companyTerms = new Domain.ReadModel.Contract();
                    foreach (var rule in userPJ.Person.PersonRules)
                    {
                        switch ((Domain.DomainEnumerators.enumPersonProfile)rule.PersonProfileId)
                        {
                            case Domain.DomainEnumerators.enumPersonProfile.ServiceProvider:
                                companyTerms = _contractDao.GetLastestVersionContract(Domain.DomainEnumerators.enumContractType.TermosDeUsoPrestadorDeServico);
                                break;
                            case Domain.DomainEnumerators.enumPersonProfile.CompanyPartner:
                                companyTerms = _contractDao.GetLastestVersionContract(Domain.DomainEnumerators.enumContractType.TermosDeUsoEmpresaParceiraRH);
                                break;
                            case Domain.DomainEnumerators.enumPersonProfile.Coworking:
                                companyTerms = _contractDao.GetLastestVersionContract(Domain.DomainEnumerators.enumContractType.TermosDeUsoCoworking);
                                break;
                            case Domain.DomainEnumerators.enumPersonProfile.EducationalCenter:
                                companyTerms = _contractDao.GetLastestVersionContract(Domain.DomainEnumerators.enumContractType.TermosDeUsoCentroEducacaional);
                                break;
                            case Domain.DomainEnumerators.enumPersonProfile.Condominium:
                                companyTerms = _contractDao.GetLastestVersionContract(Domain.DomainEnumerators.enumContractType.TermosDeUsoCondominio);
                                break;
                        }

                        this.bus.Send(new AddPersonContractCommand()
                        {
                            UserId = userPJ.UserId,
                            PersonId = userPJ.Person.PersonId,
                            ContractId = companyTerms.ContractId,
                            FileId = companyTerms.FileId
                        });
                    }

                    this.bus.Send(new AddPersonContractCommand()
                    {
                        UserId = userPJ.UserId,
                        PersonId = userPJ.Person.PersonId,
                        ContractId = privacyPolicy.ContractId,
                        FileId = privacyPolicy.FileId
                    });

                    //com a ativacao, o status do usuario sofreu alteracao. precisamos resincronizar essa alteracao nos demais modulos
                    this.bus.Send(new SyncUserInfoCommand() { PersonId = userPF.PersonId, UserId = userPF.UserId });
                    this.bus.Send(new SyncUserInfoCommand() { PersonId = userPJ.PersonId, UserId = userPJ.UserId });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public void Handle(UserCreatedFIleEvent @event)
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
                var userFileCommand = new AddUserFileCommand()
                {
                    FileId = ret,
                    AssociatedDateUTC = DateTime.UtcNow,
                    Active = true,
                    UserId = @event.UserId
                };
                this.bus.Send(userFileCommand);

            }
            else
            {
                throw new Exception();
            }
        }

        public void Handle(NewUserWelcomeMessageEvent @event)
        {

            _clientNotification = new HttpClient();
            _clientNotification.BaseAddress = new Uri(CustomConfiguration.WebApiNotification);

            #region variaveis
            var messageCodeType = ((GeneralEnumerators.EnumEmailWelcomeType)@event.MessageCodeType).ToString();
            Dictionary<string, string> listKeys = new Dictionary<string, string>();
            listKeys.Add("NomePessoa", @event.Name);
            if (!string.IsNullOrEmpty(@event.ClubBenefits))
                listKeys.Add("NomeClubeBeneficio", @event.ClubBenefits);
            if (!string.IsNullOrEmpty(@event.LogoTipo))
                listKeys.Add("LogoTipo", @event.LogoTipo);
            #endregion

            var message = new { UserFromId = @event.UserId, UserToId = @event.UserId, LanguageId = (int)GeneralEnumerators.EnumLanguage.Portuguese, MessageCodeType = messageCodeType, ListKeys = listKeys };
            var resultNotification = _clientNotification.PostAsJsonAsync("api/Communication/SendMessage", message).Result;
            if (!resultNotification.IsSuccessStatusCode)
            {
                LogManager.Info(string.Format("Erro ao enviar msg de boas vindas para o userId: {0}, user: {1}, email: {2}", @event.UserId, @event.Name, @event.Email));

            }
        }
    }
}
