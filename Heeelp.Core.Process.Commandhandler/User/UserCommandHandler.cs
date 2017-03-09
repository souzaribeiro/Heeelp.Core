
using System;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Heeelp.Core.Domain;
using Heeelp.Core.Command.Person;
using Heeelp.Core.Common;
using Heeelp.Core.Common.CustomException;
using Heeelp.Core.Event;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using Heeelp.Core.Process.Event;
using Heeelp.Core.Command.User;
using Heeelp.Core.Process.Event.User;
using Heeelp.Core.Logging;
using Heeelp.Core.Domain.ReadModel.DTO;
using static Heeelp.Core.Domain.DomainEnumerators;
using System.Linq;

namespace Heeelp.Core.ProcessManager.CommandHandlers.User
{
    public class UserCommandHandler :
        ICommandHandler<AddUserCommand>,
        //ICommandHandler<AddUserProspectCommand>,
        ICommandHandler<UpdateUserCommand>,
        ICommandHandler<UpdateUserByPersonIntegrationCodeCommand>,
        ICommandHandler<InactiveEmployeeCommand>,
        ICommandHandler<CreateActivationRequestCommand>,
        ICommandHandler<ActivateUserCommand>,
        ICommandHandler<UpdateUserStatusCommand>,
        ICommandHandler<SyncUserInfoCommand>,
        ICommandHandler<ForgotPasswordUserCommand>,
        ICommandHandler<ChangeUserPasswordCommand>,
        ICommandHandler<UploadPhotoUserCommand>,
        ICommandHandler<AdminSyncAllUsersCommand>,
        ICommandHandler<AddUserInvitedCommand>,
        ICommandHandler<AddSelfRegistrationCommand>,
        IEventPublisher
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.User>> contextFactory;
        private Func<IDataContext<Domain.SelfRegistration>> contextFactorySelfRegistration;
        private IFileTempDao _FileTemp;
        private AddUserFileCommand userFileCommand;
        private readonly IPersonDao _personDao;
        private IUserDao _userDao;
        private HttpClient _clientNotification;
        public List<IEvent> events = new List<IEvent>();
        private Func<IDataContext<SelfRegistration>> _contextFactorySelfRegistration;

        public UserCommandHandler(ICommandBus bus, Func<IDataContext<Domain.User>> contextFactory, Func<IDataContext<Domain.SelfRegistration>> contextFactorySelfRegistration, IFileTempDao contextFactoryFileTmp, IPersonDao personDao, IUserDao userDao)
        {
            this._userDao = userDao;
            this.bus = bus;
            this.contextFactory = contextFactory;
            this._FileTemp = contextFactoryFileTmp;
            this._personDao = personDao;
            this._contextFactorySelfRegistration = contextFactorySelfRegistration;
        }


        public IEnumerable<IEvent> Events { get { return this.events; } }

        public void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }


        public void Handle(AddUserCommand command)
        {
            //userFileCommand = new AddUserFileCommand();
            var repository = this.contextFactory();
            var user = new Domain.User(command.IntegrationCode,
                                        command.Name, command.Email
                                        , command.SecundaryEmail, command.SmartPhoneNumber,
                                        command.PersonId, command.IsDefaultUser,
                                        command.UserProfileId,
                                        command.AuthenticationModeId,
                                        DateTime.UtcNow,
                                        true,
                                        command.EnrollmentIP,
                                        command.CreatedBy,
                                        command.IsPerpetual,
                                        command.LoginPassword,
                                        (byte)GeneralEnumerators.EnumLanguage.Portuguese,
                                        (byte)GeneralEnumerators.EnumUserStatus.AguardandoAtivacao,
                                        Convert.ToInt16(GeneralEnumerators.EnumServerInstance.Server1)
                        );

            user.UserHistory.Add(this.CreateUserHistory(user));

            try
            {
                repository.Save(user);
                this.UserCreatedSucess(user, this.ActivationMessageTypeSelector(command.SkinId, (GeneralEnumerators.EnumUserProfile)command.UserProfileId, command.PersonProfile));
                repository.AddEventToBus(user);

            }
            catch (Exception ex)
            {
                //Dispara os eventos de sucesso na criacao do usuario
                this.UserCreatedFailure(user);

                throw;
            }

        }



        //public void Handle(AddUserProspectCommand command)
        //{
        //    userFileCommand = new AddUserFileCommand();
        //    var repository = this.contextFactory();

        //    var person = _personDao.GetPersonIntegrationId(command.PersonIntegrationId);
        //    var user = new Domain.User(command.IntegrationCode, command.Name, command.Email
        //                   , command.SmartPhoneNumber, command.PersonId, command.IsDefaultUser,
        //                   command.UserProfileId, command.AuthenticationModeId,
        //                   DateTime.UtcNow, true, command.EnrollmentIP,
        //                   command.CreatedBy, command.IsPerpetual,
        //                   command.LoginPassword, (byte)GeneralEnumerators.EnumLanguage.Portuguese, (byte)GeneralEnumerators.EnumUserStatus.AguardandoAtivacao
        //                   , Convert.ToInt16(GeneralEnumerators.EnumServerInstance.Server1)
        //               );
        //    repository.Save(user);
        //    this.UserCreatedSucess(user, MessageTypeSelector(command.Skin, (GeneralEnumerators.EnumUserProfile)command.UserProfileId, command.PersonProfile));

        //}

        public void Handle(UpdateUserCommand command)
        {
            var repository = this.contextFactory();

            var updatedBy = _userDao.Get(new Domain.ReadModel.User() { UserId = command.UpdatedBy });
            var ProfileList = _personDao.GetPersonProfile(updatedBy.UserId);
            bool updatedByIsManager = Constants.IsManager(ProfileList);




            var u = _userDao.GetUserByIntegrationCode(command.UserIntegrationCode);
            IEnumerable<Domain.User> users = repository.List(x => x.Email.Equals(u.Email));

            var f = users.Select(x => x.UserId).ToList();
            var userIds = users.Select(x => x.UserId).ToList();
            var personIds = users.Select(x => x.PersonId).ToList();
            var sameCompany = personIds.Contains(updatedBy.PersonId);
            var updatingOwnProfile = userIds.Contains(updatedBy.UserId);

            //caso esteja alterando o proprio perfil ou seja gestor e os usuarios sao da mesma compania
            if (updatingOwnProfile || (updatedByIsManager && sameCompany))

                foreach (var user in users.ToList())
                {
                    user.SecundaryEmail = string.IsNullOrEmpty(command.SecundaryEmail) ? user.SecundaryEmail : command.SecundaryEmail;
                    user.SmartPhoneNumber = string.IsNullOrEmpty(command.SmartPhoneNumber) ? user.SmartPhoneNumber : command.SmartPhoneNumber;
                    user.Name = string.IsNullOrEmpty(command.Name) ? user.Name : command.Name;
                    //user.Email = string.IsNullOrEmpty(command.Email) ? user.Email : command.Email;

                    // considerando que o usuario nao pode alterar seu proprio perfil
                    if (!updatingOwnProfile && (updatedByIsManager && sameCompany))
                    {
                        user.UserProfileId = command.UserProfileId == null ? user.UserProfileId : Convert.ToByte(command.UserProfileId);
                        //user.UserStatusId = command.UserStatusId == null ? user.UserStatusId : Convert.ToByte(command.UserStatusId);
                    }
                    user.UserHistory.Add(this.CreateUserHistory(user));
                    repository.Update(user);
                }
            //todo: disparar evento de usuario atualizado com sucesso ou falha

        }


        public void Handle(UpdateUserByPersonIntegrationCodeCommand command)
        {

            //identifica a person - employee
            var person = this._personDao.GetByIntegrationCode(command.PersonIntegrationCode);

            //identifica o email associado
            var repository = this.contextFactory();
            Domain.User singleUser = repository.Get(x => x.PersonId.Equals(person.PersonId));

            //consulta os usuarios vinculados a PF e PJ
            IEnumerable<Domain.User> users = repository.List(x => x.Email.Equals(singleUser.Email));

            foreach (var user in users.ToList())
            {
                user.SecundaryEmail = string.IsNullOrEmpty(command.SecundaryEmail) ? user.SecundaryEmail : command.SecundaryEmail;
                user.SmartPhoneNumber = string.IsNullOrEmpty(command.SmartPhoneNumber) ? user.SmartPhoneNumber : command.SmartPhoneNumber;
                user.Name = string.IsNullOrEmpty(command.Name) ? user.Name : command.Name;
                //user.Email = string.IsNullOrEmpty(command.Email) ? user.Email : command.Email;

                // considerando que o usuario nao pode alterar seu proprio perfil
                if (command.UpdatedBy != user.UserId)
                {
                    user.UserProfileId = (byte)command.UserProfileId;
                    user.UserStatusId = (byte)command.UserStatusId;
                }
                user.UserHistory.Add(this.CreateUserHistory(user));
                repository.Update(user);
            }
            //todo: disparar evento de usuario atualizado com sucesso ou falha

        }



        public void Handle(InactiveEmployeeCommand command)
        {
            var repository = this.contextFactory();
            foreach (var EmployeeId in command.EmployeeListId)
            {
                Domain.User user = repository.Get(x => x.IntegrationCode == EmployeeId);
                user.UserStatusId = (byte)GeneralEnumerators.EnumUserStatus.Cancelado;
                //todo: necessario atualizar eventos de workflow e registrar quem executou a operacao

                repository.Update(user);
            }

        }

        public void Handle(SyncUserInfoCommand command)
        {
            try
            {
                var repository = this.contextFactory();
                Domain.User user = repository.Get(x => x.UserId == command.UserId);


                var syncInfo = new UserSyncDTO()
                {
                    Active = user.Active,
                    IntegrationCode = user.IntegrationCode,
                    IsDefaultUser = user.IsDefaultUser,
                    LanguageId = user.LanguageId,
                    PersonId = user.PersonId,
                    Name = user.Name,
                    SourceId = command.SourceId,
                    UserId = user.UserId,
                    UserStatusId = user.UserStatusId,
                    Email = user.Email,
                    UserProfileId = user.UserProfileId
                };


                //Call Notification, create new user
                var _clientNotification = new HttpClient();
                _clientNotification.BaseAddress = new Uri(CustomConfiguration.WebApiNotification);
                HttpResponseMessage responseUser = _clientNotification.PostAsJsonAsync("api/User/AddUser", syncInfo).Result;
                if (!responseUser.IsSuccessStatusCode)
                {
                    LogManager.Error(string.Format("Call WebApiNotification new User Fail User: {0}", syncInfo));
                    throw new HeeelpSyncException();
                }

                //Call Account, create new user
                var _clientAccount = new HttpClient();
                _clientAccount.BaseAddress = new Uri(CustomConfiguration.WebApiAccount);
                HttpResponseMessage responseAccount = _clientAccount.PostAsJsonAsync("api/User/AddUser", syncInfo).Result;
                if (!responseAccount.IsSuccessStatusCode)
                {
                    LogManager.Error(string.Format("Call WebApiAccount new User Fail User: {0}", syncInfo));
                    throw new HeeelpSyncException();
                }

                //Call Contab, create new user
                var _clientContab = new HttpClient();
                _clientContab.BaseAddress = new Uri(CustomConfiguration.WebApiContab);
                HttpResponseMessage responseContab = _clientContab.PostAsJsonAsync("api/User/AddUser", syncInfo).Result;

                if (!responseAccount.IsSuccessStatusCode)
                {
                    LogManager.Error(string.Format("Call WebApiContab new User Fail User: {0}", syncInfo));
                    throw new HeeelpSyncException();
                }

                //Call Promotion, create new user
                var _clientPromotion = new HttpClient();
                _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                HttpResponseMessage responsePromotion = _clientPromotion.PostAsJsonAsync("api/User/AddUser", syncInfo).Result;
                if (!responseUser.IsSuccessStatusCode)
                {
                    LogManager.Error(string.Format("Call WebApiNotification new User Fail User: {0}", syncInfo));
                    throw new HeeelpSyncException();
                }

                //Call Social, create new user
                var _clientSocial = new HttpClient();
                _clientSocial.BaseAddress = new Uri(CustomConfiguration.WebApiSocial);
                HttpResponseMessage responseSocial = _clientSocial.PostAsJsonAsync("api/Sync/AddUser", syncInfo).Result;
                if (!responseUser.IsSuccessStatusCode)
                {
                    LogManager.Error(string.Format("Call WebApiSocial new User Fail User: {0}", syncInfo));
                    throw new HeeelpSyncException();
                }


                //disparar o evento de sincronizacao concluida com sucesso! O eventHandler posteriormente vai disparar o comando de envio de email de ativacao
                this.AddEvent(new UserSyncedSuccessEvent()
                {
                    SourceId = Guid.NewGuid(),
                    UserId = command.UserId,
                    IntegrationCode = command.IntegrationCode,
                    PersonId = command.PersonId
                });

            }
            catch (Exception ex)
            {
                //disparar o evento de sincronizacao concluida com sucesso! O eventHandler posteriormente vai disparar o comando de envio de email de ativacao
                this.AddEvent(new UserNotSyncedEvent()
                {
                    SourceId = Guid.NewGuid(),
                    UserId = command.UserId,
                    IntegrationCode = command.IntegrationCode,
                    PersonId = command.PersonId
                });

                throw;
            }

        }

        private void UserCreatedSucess(Domain.User user, GeneralEnumerators.EnumEmailAcvtiveType messageCodeType)
        {

            //disparar o evento de usuario criado com sucesso para sincronizacao dos dados de usuario nos demais modulos
            user.AddEvent(new UserCreatedSuccessEvent
            {
                //SourceId = user.Id,
                UserId = user.UserId,
                IntegrationCode = user.IntegrationCode,
                PersonId = user.PersonId,
                IsDefaultUser = user.IsDefaultUser,
                UserStatusId = user.UserStatusId,
                LanguageId = user.LanguageId,
                Active = user.Active,
                Email = user.Email
            });


            var person = user.Person;
            var employee = _personDao.GetCompany(user.PersonId);

            if (employee.PersonTypeId == (byte)GeneralEnumerators.EnumPersonType.Natural_Person)
            {
                var company = _personDao.GetCompany((int)employee.PersonFatherId);

                //disparar o comando para criacao do email de ativacao
                CreateActivationRequestCommand command = new CreateActivationRequestCommand();
                command.SourceId = Guid.NewGuid();
                command.PersonToId = user.PersonId;
                command.MessageCodeType = messageCodeType;
                command.UserIntegrationCode = user.IntegrationCode;
                this.bus.Send(command);

            }
        }

        public void Handle(CreateActivationRequestCommand command)
        {

            var employee = _personDao.GetPerson(command.PersonToId);
            var company = _personDao.GetCompanyByEmployeePersonId((int)employee.PersonFatherId);
            long fileid = 0;
            foreach (var pf in employee.PersonFile)
            {
                fileid = pf.FileId;
            }

            _clientNotification = new HttpClient();
            _clientNotification.BaseAddress = new Uri(CustomConfiguration.WebApiNotification);
            //todo: substituir as mensagens em hard code por arquivos de resources para permitir traducao para outros idiomas
            string title = String.Format("Ola {0}, boas vindas ao {1}", employee.Name, employee.CustomClubName);
            var message = new NotificationDTO(1,
                                                employee.PersonId,
                                                employee.Name,
                                                (int)GeneralEnumerators.EnumLanguage.Portuguese,
                                                command.MessageCodeType.ToString(),
                                                title,
                                                employee.CustomClubName,
                                                employee.CustomHeeelpPersonDomain,
                                                employee.CustomClubLogo);
            message.AddParameter("LinkDeAtivacao", employee.CustomHeeelpPersonDomain + "/login/firstaccess/" + command.UserIntegrationCode.ToString());

            var resultNotification = _clientNotification.PostAsJsonAsync("api/Communication/SendMessage", message).Result;
            if (!resultNotification.IsSuccessStatusCode)
            {
                LogManager.Info(string.Format("Erro ao enviar msg de ativação para a pessoa  personId: {0}", command.PersonToId));

            }
        }



        public void UserCreatedFailure(Domain.User user)
        {
            //disparar os eventos de tratamento de erro
            user.AddEvent(new UserNotCreatedEvent
            {
                SourceId = user.Id,
                UserId = user.UserId,
                IntegrationCode = user.IntegrationCode,
                PersonId = user.PersonId,
                IsDefaultUser = user.IsDefaultUser,
                UserStatusId = user.UserStatusId,
                LanguageId = user.LanguageId,
                Active = user.Active
            });

        }

        //private string GetLogoTipo(int PersonId)
        //{
        //    //todo: implementar a logica que dado um personId de uma pessoa fisica faz a consulta de quem eh a empresa empregadora (personFatherId), o FileId e com base nisso, consulta no FileServer a URL do logotipo

        //    return "";
        //}

        public void Handle(UploadPhotoUserCommand command)
        {
            var repository = this.contextFactory();
            var user = repository.Get(x => x.IntegrationCode == command.IntegrationCode);

            foreach (var item in command.ListFileTemp)
            {
                FIleServer fs = new FIleServer();

                fs.FilePath = item.FilePath;
                fs.Width = item.Width;
                fs.Height = item.Height;
                fs.OriginalName = item.OriginalName;
                fs.FileIntegrationCode = item.FileIntegrationCode;
                //fs.FileTempId = item.FIl;
                fs.Description = "Photo Profile User";
                fs.FileUtilizationId = Convert.ToByte(GeneralEnumerators.EnumFileUtiliaztion.Album);
                fs.FriendlyName = user.Name;
                fs.Alt = user.Name;
                fs.Name = user.Name;
                fs.FileOriginTypeId = (int)GeneralEnumerators.EnumModules.Core_User;
                fs.PersonId = user.PersonId;
                fs.UploadedBy = Convert.ToInt32(command.CreatedBy);


                var ret = fs.SendFilePath(fs);

                if (ret > 0)
                {
                    UserFile userFile = new UserFile();
                    userFile.UserId = user.UserId;
                    userFile.FileId = ret;
                    userFile.AssociatedDateUTC = DateTime.UtcNow;
                    userFile.Active = true;
                    //aggregateroot
                    user.UserFile.Add(userFile);
                    user.UrlImagemLogo = item.FilePath;
                    repository.Save(user);
                }
                else
                {
                    throw new Exception();
                }
            }
        }


        #region PassWord

        public void Handle(ForgotPasswordUserCommand command)
        {
            var repository = this.contextFactory();
            var person = _personDao.GetPersonByEmail(command.Email);
            var userIntegrationCode = _userDao.GetUserIntegrationCodeUserPF(command.Email);

            string messageCode = person.SkinId == 1 ?
                GeneralEnumerators.EnumEmailForgotPassWord.RecuperarSenha.ToString() :
                GeneralEnumerators.EnumEmailForgotPassWord.RecupSenhaWL.ToString();


            _clientNotification = new HttpClient();
            _clientNotification.BaseAddress = new Uri(CustomConfiguration.WebApiNotification);
            //todo: substituir as mensagens em hard code por arquivos de resources para permitir traducao para outros idiomas
            string title = String.Format("Ola {0}, Seu pedido de recuperar sua senha", person.Name);
            var message = new NotificationDTO(1,
                                                person.PersonId,
                                                person.Name,
                                                (int)GeneralEnumerators.EnumLanguage.Portuguese,
                                                messageCode,
                                                title,
                                                person.CustomClubName,
                                                person.CustomHeeelpPersonDomain,
                                                person.CustomClubLogo);
            message.AddParameter("LinkDeAtivacao", person.CustomHeeelpPersonDomain + "/Login/RecoveryPassword/" + userIntegrationCode.ToString());

            var resultNotification = _clientNotification.PostAsJsonAsync("api/Communication/SendMessage", message).Result;
            if (!resultNotification.IsSuccessStatusCode)
            {
                LogManager.Info(string.Format("Erro ao enviar msg de recuperacao de sennha para a pessoa  personId: {0}", person.PersonId));

            }

        }

        public void Handle(ChangeUserPasswordCommand command)
        {

            List<Domain.User> users = _userDao.GetUsersByPersonIntegrationCode(command.IntegrationCode);
            foreach (var item in users)
            {
                var repository = this.contextFactory();
                Domain.User user = repository.Get(x => x.UserId == item.UserId);
                user.LoginPassword = command.Password;
                user.UserHistory.Add(this.CreateUserHistory(user));
                repository.Update(user);
            }
        }
        #endregion




        private GeneralEnumerators.EnumEmailAcvtiveType ActivationMessageTypeSelector(int skin, GeneralEnumerators.EnumUserProfile userProfileId, GeneralEnumerators.EnumPersonProfile personProfileId)
        {

            GeneralEnumerators.EnumEmailAcvtiveType messageType = GeneralEnumerators.EnumEmailAcvtiveType.AtivarContaCola;
            if (skin > 1)
                messageType = GeneralEnumerators.EnumEmailAcvtiveType.AtivContColabWL;
            switch (personProfileId)
            {
                case GeneralEnumerators.EnumPersonProfile.PrestadorServiços:
                    if (userProfileId == GeneralEnumerators.EnumUserProfile.Administrador)
                        messageType = GeneralEnumerators.EnumEmailAcvtiveType.AtivContPresSer;
                    break;
                case GeneralEnumerators.EnumPersonProfile.EmpresaAssociadaClubedeBeneficios:
                    if (userProfileId == GeneralEnumerators.EnumUserProfile.Administrador)
                    {
                        if (skin > 1)
                            messageType = GeneralEnumerators.EnumEmailAcvtiveType.AtivContGesRHWL;
                        else
                            messageType = GeneralEnumerators.EnumEmailAcvtiveType.AtivContGestRH;
                    }
                    break;
                case GeneralEnumerators.EnumPersonProfile.AdminstradorSistema:
                    if (userProfileId == GeneralEnumerators.EnumUserProfile.Administrador)
                        messageType = GeneralEnumerators.EnumEmailAcvtiveType.AtivContAdmHeeelp;
                    break;
                case GeneralEnumerators.EnumPersonProfile.EmpresaCoWorking:
                    if (userProfileId == GeneralEnumerators.EnumUserProfile.Administrador)
                        messageType = GeneralEnumerators.EnumEmailAcvtiveType.AtivContGestCWL;

                    break;

                default:
                    break;
            }
            return messageType;
        }

        public void Handle(ActivateUserCommand command)
        {

            var repository = this.contextFactory();
            // a partir da pessoa fisica que recebeu o email de ativacao                             
            Domain.User userPF = repository.Get(x => x.IntegrationCode == command.IntegrationCode);

            try
            {
                if (userPF.UserStatusId != (byte)GeneralEnumerators.EnumUserStatus.Ativo)
                {
                    //atualiza os dados do usuario vinculado a pessoa fisica
                    userPF.SmartPhoneNumber = command.PhoneNumber;
                    userPF.LoginPassword = command.Password;
                    userPF.UserStatusId = (byte)GeneralEnumerators.EnumUserStatus.Ativo;
                    userPF.UserHistory.Add(this.CreateUserHistory(userPF));


                    repository.Update(userPF);
                    userPF.AddEvent(new UserActivatedEvent()
                    {
                        UserPFId = userPF.UserId
                    });

                    repository.AddEventToBus(userPF);
                    //atualiza o Status da Pessoa Fisica
                    this.bus.Send(new UpdatePersonStatusCommand() { PersonId = userPF.PersonId, Status = GeneralEnumerators.EnumPersonStatus.CadastroAprovado });
                    //enviar o email de boas vindas 
                    this.bus.Send(new SendWelcomeMessageCommand() { UserIntegrationCode = userPF.IntegrationCode });

                }

                var person = _personDao.GetByPersonId(userPF.PersonId);
                //agora, atualiza tambem os dados do usuario vinculado com a pessoa juridica. Considerar que antes de aceitar um usuario, eh necessario validar se o email ainda nao existe no banco.
                Domain.User userPJ = repository.Get(x => x.PersonId == person.PersonFatherId && x.Email == userPF.Email);
                if (userPJ.UserStatusId != (byte)GeneralEnumerators.EnumUserStatus.Ativo)
                {
                    userPJ.SmartPhoneNumber = command.PhoneNumber;
                    userPJ.LoginPassword = command.Password;
                    userPJ.UserStatusId = (byte)GeneralEnumerators.EnumUserStatus.Ativo;
                    userPJ.UserHistory.Add(this.CreateUserHistory(userPJ));
                    repository.Update(userPJ);
                    //atualiza o Status da Pessoa Fisica
                    this.bus.Send(new UpdatePersonStatusCommand() { PersonId = userPJ.PersonId, Status = GeneralEnumerators.EnumPersonStatus.CadastroAprovado });
                }


            }
            catch (Exception ex)
            {
                NotActivatedNewUserEvent passwordNotChangedEvent = new NotActivatedNewUserEvent()
                {
                    IntegrationCode = command.IntegrationCode
                };
                userPF.AddEvent(passwordNotChangedEvent);
                repository.AddEventToBus(userPF);
                throw;
            }

        }

        public void Handle(UpdateUserStatusCommand command)
        {
            var repository = this.contextFactory();
            Domain.User user = repository.Get(x => x.UserId == command.UserId);
            user.UserStatusId = command.UserStatusId.Value;
            user.UserHistory.Add(this.CreateUserHistory(user));
            repository.Save(user);

        }

        public void Handle(AdminSyncAllUsersCommand command)
        {
            try
            {
                var repository = this.contextFactory();

                var userList = repository.List(x => x.Active.Equals(true));

                foreach (var user in userList)
                {
                    SyncUserInfoCommand syncPersonInfoCommand = new SyncUserInfoCommand()
                    {
                        UserId = user.UserId
                    };
                    this.bus.Send(syncPersonInfoCommand);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        private Domain.UserHistory CreateUserHistory(Domain.User user)
        {
            Domain.UserHistory userHistory = new Domain.UserHistory();
            userHistory.UserId = user.UserId;
            userHistory.IntegrationCode = user.IntegrationCode;
            userHistory.Name = user.Name;
            userHistory.Email = user.Email;
            userHistory.SecundaryEmail = user.SecundaryEmail;
            userHistory.SmartPhoneNumber = user.SmartPhoneNumber;
            userHistory.PersonId = user.PersonId;
            userHistory.IsDefaultUser = user.IsDefaultUser;
            userHistory.UserProfileId = user.UserProfileId;
            userHistory.UserStatusId = user.UserStatusId;
            userHistory.AuthenticationModeId = user.AuthenticationModeId;
            userHistory.LanguageId = user.LanguageId;
            userHistory.CreationDateUTC = user.CreationDateUTC;
            userHistory.ValidationDateUTC = user.ValidationDateUTC;
            userHistory.FormFillTime = user.FormFillTime;
            userHistory.ValidationToken = user.ValidationToken;
            userHistory.SecurityCheckNecessary = user.SecurityCheckNecessary;
            userHistory.ValidationAttempts = user.ValidationAttempts;
            userHistory.LoginFailAttempts = user.LoginFailAttempts;
            userHistory.LastLoginFailUTC = user.LastLoginFailUTC;
            userHistory.Active = user.Active;
            userHistory.EnrollmentIP = user.EnrollmentIP;
            userHistory.ValidationIP = user.ValidationIP;
            userHistory.CreatedBy = user.CreatedBy;
            userHistory.ServerInstanceId = user.ServerInstanceId;
            userHistory.IsPerpetual = user.IsPerpetual;

            return userHistory;
        }


        #region Invited
        public void Handle(AddUserInvitedCommand command)
        {
            try
            {
                if (command.InviteCode != null)
                {
                    var person = _personDao.GetPersonInviteCode(command.InviteCode);

                    if (person != null)
                    {
                        if (person.InviteAvailable < 1)
                        {
                            //sem convites disponiveis
                            var c = new AddSelfRegistrationCommand();
                            c.Name = command.Name;
                            c.Email = command.Email;
                            c.EnrollmentIP = command.EnrollmentIP;
                            c.InviteCode = command.InviteCode;
                            this.bus.Send(c);
                        }
                        else
                        {
                            //convite disponivel
                            //cadastra a pessoa
                            var c = new AddPersonEmployeeCommand();
                            c.IntegrationCode = Guid.NewGuid();
                            c.Name = command.Name;
                            //c.PhoneNumber = employee.SmartPhoneNumber;
                            c.CreatedBy = 1;
                            c.Email = command.Email;
                            c.SecundaryEmail = command.Email;
                            c.EnrollmentIP = command.EnrollmentIP;
                            c.PersonIntegrationFatherId = person.IntegrationCode;
                            c.UserProfileId = GeneralEnumerators.EnumUserProfile.SemAcesso;
                            c.PersonOriginType = GeneralEnumerators.EnumPersonOriginType.PainelAdministrativoGestor;
                            this.bus.Send(c);

                            _personDao.UpdateInviteAvailable(person.PersonId);
                        }
                    }
                    else
                    {
                        //codigo nao encontrado
                        var c = new AddSelfRegistrationCommand();
                        c.Name = command.Name;
                        c.Email = command.Email;
                        c.EnrollmentIP = command.EnrollmentIP;
                        this.bus.Send(c);
                    }
                }
                else
                {
                    //codigo nao encontrado
                    var c = new AddSelfRegistrationCommand();
                    c.Name = command.Name;
                    c.Email = command.Email;
                    c.EnrollmentIP = command.EnrollmentIP;
                    this.bus.Send(c);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void Handle(AddSelfRegistrationCommand command)
        {
            var repository = this._contextFactorySelfRegistration();
            SelfRegistration self = new SelfRegistration();
            self.Email = command.Email;
            self.Name = command.Name;
            self.EnrollmentIP = command.EnrollmentIP;
            self.InviteCode = command.InviteCode;
            self.PersonIntegrationId = command.PersonIntegrationId;
            self.CreationDateUTC = DateTime.UtcNow;

            repository.Save(self);
            //todo: criar um meio pra enviar email pra pessoas que não fazem parte do heeelp
            //todo: enviar email para pessoa que ainda não faz parte do heeelp
            
        }

        

        #endregion

    }
}
