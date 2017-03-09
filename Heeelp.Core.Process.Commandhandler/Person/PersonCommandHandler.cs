using System;
using System.Collections.Generic;
using Heeelp.Core.Domain;
using Heeelp.Core.Command.Person;
using Heeelp.Core.Common;
using Heeelp.Core.Event;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using Heeelp.Core.Process.Event.User;
using System.Net.Http;
using Heeelp.Core.Logging;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Common.CustomException;
using Heeelp.Core.Process.Event.Person;
using System.Net.Http.Headers;
using System.Data.Entity.Spatial;
using Newtonsoft.Json;
using Heeelp.Core.Command.User;
using System.Linq;
namespace Heeelp.Core.ProcessManager.CommandHandlers.Person
{
    public class PersonCommandHandler :
        ICommandHandler<AddPersonCompanyPartnerCommand>,
        ICommandHandler<AddPersonCoworkingCommand>,
        ICommandHandler<AddPersonEmployeeCommand>,
        ICommandHandler<InactivePersonCoWorkerCommand>,
        ICommandHandler<UpdatePersonCompanyCommand>,
        ICommandHandler<SyncPersonInfoCommand>,
        ICommandHandler<SendWelcomeMessageCommand>,
        ICommandHandler<AddPersonContractCommand>,
        ICommandHandler<UpdatePersonStatusCommand>,
        ICommandHandler<AddCompanyInformationCommand>,
        ICommandHandler<UploadFilePersonCommand>,
        ICommandHandler<AddPersonServiceProviderCommand>,
        ICommandHandler<UploadLogoPersonCommand>,
        ICommandHandler<AddPersonCompanyExpertiseCommand>,
        ICommandHandler<AdminSyncAllPersonsCommand>
    //ICommandHandler<UpdatePersonAddressCompleteRegistrationCommand>,


    {
        private readonly ICommandBus _bus;
        private Func<IDataContext<Domain.Person>> contextFactory;
        private IFileTempDao _FileTemp;
        private IPersonDao _personDao;
        private IUserDao _userDao;
        private Func<IDataContext<Domain.Expertise>> contextFactoryExpertise;

        public PersonCommandHandler(Func<IDataContext<Domain.Person>> contextFactory, Func<IDataContext<Domain.Expertise>> contextFactoryExpertise, IFileTempDao contextFactoryFileTmp, ICommandBus bus, IPersonDao personDao, IUserDao UserDao)
        {
            this.contextFactoryExpertise = contextFactoryExpertise;
            this.contextFactory = contextFactory;
            this._FileTemp = contextFactoryFileTmp;
            this._bus = bus;
            this._personDao = personDao;
            this._userDao = UserDao;
        }
        public Guid Id { get; set; }

        #region Create Person

        public void Handle(AddPersonCompanyPartnerCommand command)
        {
            try
            {
                #region Cadastra Pessoa Juridica Associada ao Heeelp para o clube de beneficios - RH

                var repository = this.contextFactory();
                Domain.Person personFather;
                if (command.PersonIntegrationFatherId == Guid.Empty)
                {
                    //empresas - e apenas empresas! - sem um personFatherId definidos sao associados diretamente ao Heeelp
                    personFather = repository.Get(x => x.PersonId.Equals(1));
                }
                else
                {
                    personFather = repository.Get(x => x.IntegrationCode.Equals(command.PersonIntegrationFatherId));
                }

                var company = new Domain.Person();
                company.IntegrationCode = command.IntegrationCode;
                company.Name = command.CompanyName;
                company.FantasyName = command.CompanyFantasyName;
                company.PersonOriginTypeId = (byte)command.PersonOriginType;
                company.PhoneNumber = command.CompanyPhoneNumber;
                company.ServerInstanceId = (int)GeneralEnumerators.EnumServerInstance.Server1;
                company.Active = true;
                company.PersonFatherId = personFather.PersonId;
                company.SkinId = personFather.SkinId;

                //se a empresa esta associada ao coworking, ela ingressa no clube de beneficios do coworking. 
                //Se ela eh vinculada diretamente ao Heeelp, ela ingressa no clube de beneficios Heeelp!
                //se a empresa deseja criar o proprio clube de beneficios, o processo deve ser:
                //1-cria a emrpesa ainda vinculada ao plano Heeelp!.  
                //2 - Criar um novo clube de beneficios para a empresa. 
                //3 - Atualizar o company.PersonBenefitClubId apontando para o novo clube de beneficios
                //company.PersonBenefitClubId = personFather.PersonBenefitClubId;
                company.PersonBenefitClubId = personFather.PersonBenefitClubId;

                CreateCompany(company, GeneralEnumerators.EnumPersonProfile.EmpresaAssociadaClubedeBeneficios);

                #endregion

                #region Cadastra Pessoa Fisica - Gestor da Empresa

                //primeiro a pessoa
                var manager = new Domain.Person();


                manager.IntegrationCode = command.ManagerEmployeeId != Guid.Empty ? command.ManagerEmployeeId : Guid.NewGuid();
                manager.Name = command.ManagerName;
                manager.PersonOriginTypeId = (byte)command.PersonOriginType;
                manager.PersonTypeId = (byte)GeneralEnumerators.EnumPersonType.Natural_Person;
                manager.PhoneNumber = command.CompanyPhoneNumber;
                manager.ServerInstanceId = (int)GeneralEnumerators.EnumServerInstance.Server1;
                manager.Active = true;
                manager.PersonFatherId = company.PersonId;
                manager.SkinId = company.SkinId;
                manager.PersonBenefitClubId = company.PersonBenefitClubId;

                //segundo, o usuario da pessoa
                var user = new Domain.User();
                user.Email = command.ManagerEmail;
                user.EnrollmentIP = command.EnrollmentIP;
                user.IsDefaultUser = true;
                user.Name = command.ManagerName;
                user.PersonId = manager.PersonId; //a conta PF vincula da a pessoa fisica. a Conta PJ estara vinculada a PersonFatherId
                user.SmartPhoneNumber = command.ManagerPhoneNumber;
                user.UserProfileId = (byte)GeneralEnumerators.EnumUserProfile.Administrador;
                user.CreatedBy = command.CreatedBy;
                user.IsDefaultUser = true;
                user.IsPerpetual = true;

                CreateEmployee(manager, user, GeneralEnumerators.EnumPersonProfile.EmpresaAssociadaClubedeBeneficios, command.SkinId);

                #endregion

            }
            catch (Exception ex)
            {
                //todo: aplicar o tratamento de erro
                throw;
            }

        }

        public void Handle(AddPersonCoworkingCommand command)
        {

            try
            {
                #region Cadastra Pessoa Juridica Associada ao Heeelp para o clube de beneficios - Coworking

                var repository = this.contextFactory();


                //todo:necessario incluir coluna createdBy na tabela PErson para saber quem foi o usuario responsavel pela criacao do registro
                var company = new Domain.Person();
                company.IntegrationCode = command.IntegrationCode;
                company.Name = command.CompanyName;
                company.FantasyName = command.CompanyFantasyName;
                company.PersonOriginTypeId = (byte)command.PersonOriginType;
                company.PersonTypeId = (byte)GeneralEnumerators.EnumPersonType.Legal_Person;
                company.PhoneNumber = command.CompanyPhoneNumber;
                company.ServerInstanceId = (int)GeneralEnumerators.EnumServerInstance.Server1;
                company.Active = true;
                company.PersonFatherId = 1;//o coworking tem sempre o Heeelp como personFather
                company.SkinId = command.SkinId;

                //se a empresa deseja criar o proprio clube de beneficios, o processo deve ser:
                //1-cria a emrpesa ainda vinculada ao plano Heeelp!.  
                //2 - Criar um novo clube de beneficios para a empresa. 
                //3 - Atualizar o company.PersonBenefitClubId apontando para o novo clube de beneficios
                company.PersonBenefitClubId = (int)GeneralEnumerators.EnumPersonBenefitClub.GoPlus;
                CreateCompany(company, GeneralEnumerators.EnumPersonProfile.EmpresaCoWorking);

                #endregion

                #region Cadastra Pessoa Fisica - Gestor da Empresa

                //primeiro a pessoa
                var manager = new Domain.Person();
                manager.IntegrationCode = Guid.NewGuid();
                manager.Name = command.ManagerName;
                manager.PersonOriginTypeId = (byte)command.PersonOriginType;
                manager.PersonTypeId = (byte)GeneralEnumerators.EnumPersonType.Natural_Person;
                manager.PhoneNumber = command.CompanyPhoneNumber;
                manager.ServerInstanceId = (int)GeneralEnumerators.EnumServerInstance.Server1;
                manager.Active = true;
                manager.PersonFatherId = company.PersonId;
                manager.SkinId = company.SkinId;
                manager.PersonBenefitClubId = company.PersonBenefitClubId;


                //segundo, o usuario da pessoa
                var user = new Domain.User();
                user.Email = command.ManagerEmail;
                user.EnrollmentIP = command.EnrollmentIP;
                user.IsDefaultUser = true;
                user.Name = command.ManagerName;
                user.PersonId = manager.PersonId; //a conta PF vincula da a pessoa fisica. a Conta PJ estara vinculada a PersonFatherId
                user.SmartPhoneNumber = command.ManagerPhoneNumber;
                user.UserProfileId = (byte)GeneralEnumerators.EnumUserProfile.Administrador;
                user.CreatedBy = command.CreatedBy;
                user.IsDefaultUser = true;
                user.IsPerpetual = true;

                CreateEmployee(manager, user, GeneralEnumerators.EnumPersonProfile.EmpresaCoWorking, command.SkinId);

                #endregion

            }
            catch (Exception)
            {
                //todo: aplicar o tratamento de erro
                throw;
            }

        }

        public void Handle(AddPersonServiceProviderCommand command)
        {

            try
            {
                #region Cadastra Pessoa Juridica Prestador de Servico

                var repository = this.contextFactory();
                Domain.Person personFather;
                if (command.PersonIntegrationFatherId == Guid.Empty)
                {
                    //empresas - e apenas empresas! - sem um personFatherId definidos sao associados diretamente ao Heeelp
                    personFather = repository.Get(x => x.PersonId.Equals(1));
                }
                else
                {
                    personFather = repository.Get(x => x.IntegrationCode.Equals(command.PersonIntegrationFatherId));
                }


                var company = new Domain.Person();
                company.IntegrationCode = command.IntegrationCode;
                company.Name = command.CompanyName;
                company.FantasyName = command.CompanyFantasyName;
                company.PersonOriginTypeId = (byte)command.PersonOriginType;
                company.PersonTypeId = (byte)GeneralEnumerators.EnumPersonType.Legal_Person;
                company.PhoneNumber = command.CompanyPhoneNumber;
                company.ServerInstanceId = (int)GeneralEnumerators.EnumServerInstance.Server1;
                company.Active = true;
                company.PersonFatherId = personFather.PersonId;
                company.SkinId = personFather.SkinId;

                //se a empresa esta associada ao coworking, ela ingressa no clube de beneficios do coworking. 
                //Se ela eh vinculada diretamente ao Heeelp, ela ingressa no clube de beneficios Heeelp!
                //se a empresa deseja criar o proprio clube de beneficios, o processo deve ser:
                //1-cria a emrpesa ainda vinculada ao plano Heeelp!.  
                //2 - Criar um novo clube de beneficios para a empresa. 
                //3 - Atualizar o company.PersonBenefitClubId apontando para o novo clube de beneficios
                //company.PersonBenefitClubId = personFather.PersonBenefitClubId;
                company.PersonBenefitClubId = (int)GeneralEnumerators.EnumPersonBenefitClub.Heeelp;

                CreateCompany(company, GeneralEnumerators.EnumPersonProfile.PrestadorServiços);

                #endregion

                #region Cadastra Pessoa Fisica - Gestor da Empresa

                //primeiro a pessoa
                var manager = new Domain.Person();
                manager.IntegrationCode = Guid.NewGuid();
                manager.Name = command.ManagerName;
                manager.PersonOriginTypeId = (byte)command.PersonOriginType;
                manager.PersonTypeId = (byte)GeneralEnumerators.EnumPersonType.Natural_Person;
                manager.PhoneNumber = command.CompanyPhoneNumber;
                manager.ServerInstanceId = (int)GeneralEnumerators.EnumServerInstance.Server1;
                manager.Active = true;
                manager.PersonFatherId = company.PersonId;
                manager.SkinId = company.SkinId;
                manager.PersonBenefitClubId = company.PersonBenefitClubId;

                //segundo, o usuario da pessoa
                var user = new Domain.User();
                user.Email = command.ManagerEmail;
                user.EnrollmentIP = command.EnrollmentIP;
                user.IsDefaultUser = true;
                user.Name = command.ManagerName;
                user.PersonId = manager.PersonId; //a conta PF vincula da a pessoa fisica. a Conta PJ estara vinculada a PersonFatherId
                user.SmartPhoneNumber = command.ManagerPhoneNumber;
                user.UserProfileId = (byte)GeneralEnumerators.EnumUserProfile.Administrador;
                user.CreatedBy = command.CreatedBy;
                user.IsDefaultUser = true;
                user.IsPerpetual = true;

                CreateEmployee(manager, user, GeneralEnumerators.EnumPersonProfile.PrestadorServiços, command.SkinId);

                #endregion

            }
            catch (Exception)
            {
                //todo: aplicar o tratamento de erro
                throw;
            }

        }

        public void Handle(AddPersonEmployeeCommand command)
        {
            var repository = this.contextFactory();
            var personFather = repository.Get(x => x.IntegrationCode.Equals(command.PersonIntegrationFatherId));

            try
            {
                //todo: Precisamos eliminar um dos 2 codigos IntegrationCode ou PersonIntegrationId, esta crianco confusao e ambos tem a mesma finalidade
                var employee = new Domain.Person();
                employee.IntegrationCode = Guid.NewGuid();
                employee.Name = command.Name;
                employee.PersonOriginTypeId = (byte)command.PersonOriginType;
                employee.PersonTypeId = (byte)GeneralEnumerators.EnumPersonType.Natural_Person;
                employee.PhoneNumber = command.PhoneNumber;
                employee.ServerInstanceId = (int)GeneralEnumerators.EnumServerInstance.Server1;
                employee.Active = true;
                employee.PersonFatherId = personFather.PersonId;
                employee.SkinId = personFather.SkinId;
                employee.PersonBenefitClubId = personFather.PersonBenefitClubId;

                //segundo, o usuario da pessoa
                var user = new Domain.User();
                user.Email = command.Email;
                user.SecundaryEmail = command.SecundaryEmail;
                user.EnrollmentIP = command.EnrollmentIP;
                user.IsDefaultUser = true;
                user.Name = employee.Name;
                user.PersonId = employee.PersonId; //a conta PJ vincula da a pessoa juridica
                user.SmartPhoneNumber = command.PhoneNumber;
                user.UserProfileId = (byte)command.UserProfileId;
                user.CreatedBy = command.CreatedBy;
                user.IsDefaultUser = false;// na perspectiva em relacao a PJ
                user.IsPerpetual = false;// na perspectiva em relacao a PJ

                CreateEmployee(employee, user, GeneralEnumerators.EnumPersonProfile.Colaborador, command.SkinId);

            }
            catch (Exception)
            {
                //todo: aplicar o tratamento de erro
                throw;
            }
        }

        public void Handle(SyncPersonInfoCommand command)
        {

            var repository = this.contextFactory();
            Domain.Person person = repository.Get(x => x.PersonId == command.PersonId);

            try
            {
                var PersonInfo = new PersonSyncDTO()
                {
                    Active = true,
                    CurrencyId = person.CurrencyId,
                    Id = person.Id,
                    CountryId = person.CountryId,
                    IntegrationCode = person.IntegrationCode,
                    LanguageId = person.LanguageId,
                    PersonId = person.PersonId,
                    PersonStatusId = person.PersonStatusId,
                    PersonTypeId = person.PersonTypeId,
                    SourceId = command.SourceId,
                    SkinId = person.SkinId,
                    CustomClubName = person.PersonBenefitClub.CustomClubName,
                    CustomHeeelpPersonDomain = person.PersonBenefitClub.CustomHeeelpPersonDomain
                };

                //Call Contab, create new Person
                var _clientContab = new HttpClient();
                _clientContab.BaseAddress = new Uri(CustomConfiguration.WebApiContab);
                HttpResponseMessage responseContab = _clientContab.PostAsJsonAsync("api/Person/AddPerson", PersonInfo).Result;
                if (!responseContab.IsSuccessStatusCode)
                {
                    LogManager.Error(string.Format("Fail posting to WebApiContab AddPerson: {0}", PersonInfo));
                    //throw new HeeelpSyncException();
                }

                //Call Account, create new Person
                var _clientAccount = new HttpClient();
                _clientAccount.BaseAddress = new Uri(CustomConfiguration.WebApiAccount);
                HttpResponseMessage responseAccount = _clientAccount.PostAsJsonAsync("api/Person/AddPerson", PersonInfo).Result;
                if (!responseAccount.IsSuccessStatusCode)
                {
                    LogManager.Error(string.Format("Fail posting to WebApiAccount AddPerson: {0}", PersonInfo));
                    throw new HeeelpSyncException();
                }

                //Call Promotion, create new Person
                var _clientPromotion = new HttpClient();
                _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                HttpResponseMessage responsePromotion = _clientPromotion.PostAsJsonAsync("api/Person/AddPerson", PersonInfo).Result;

                if (!responsePromotion.IsSuccessStatusCode)
                {
                    LogManager.Error(string.Format("Fail posting to WebApiPromotion AddPerson: {0}", PersonInfo));
                    throw new HeeelpSyncException();
                }

                //Call Notification, create new Person
                var _clientNotification = new HttpClient();
                _clientNotification.BaseAddress = new Uri(CustomConfiguration.WebApiNotification);
                HttpResponseMessage responseNotification = _clientNotification.PostAsJsonAsync("api/Person/AddPerson", PersonInfo).Result;

                if (!responseNotification.IsSuccessStatusCode)
                {
                    LogManager.Error(string.Format("Fail posting to WebApiNotification AddPerson: {0}", PersonInfo));
                    throw new HeeelpSyncException();
                }


                //Call Classified, create new Person
                var _clientClassified = new HttpClient();
                _clientClassified.BaseAddress = new Uri(CustomConfiguration.WebApiClassified);
                HttpResponseMessage responseClassified = _clientClassified.PostAsJsonAsync("api/Sync/AddPerson", PersonInfo).Result;

                if (!responseClassified.IsSuccessStatusCode)
                {
                    LogManager.Error(string.Format("Fail posting to WebApiClassified AddPerson: {0}", PersonInfo));
                    throw new HeeelpSyncException();
                }


                //a sincronizacao com o modulo social envolve o envio do nome. Para evitar que o nome seja trafegado na sincronizacao nos demais modulos eh necessario um DTO especializado para o social
                var PersonInfoSocial = new PersonSyncSocialDTO()
                {
                    Active = true,
                    CurrencyId = PersonInfo.CurrencyId,
                    Id = PersonInfo.Id,
                    CountryId = PersonInfo.CountryId,
                    IntegrationCode = PersonInfo.IntegrationCode,
                    LanguageId = PersonInfo.LanguageId,
                    PersonId = PersonInfo.PersonId,
                    PersonStatusId = PersonInfo.PersonStatusId,
                    PersonTypeId = PersonInfo.PersonTypeId,
                    SourceId = PersonInfo.SourceId,
                    SkinId = PersonInfo.SkinId,
                    CustomClubName = PersonInfo.CustomClubName,
                    CustomHeeelpPersonDomain = PersonInfo.CustomHeeelpPersonDomain,
                    Name = person.Name //campo adicional a ser trafegado apenas na sincronizacao com o Social
                };

                //Call Social, create new Person
                var _clientSocial = new HttpClient();
                _clientSocial.BaseAddress = new Uri(CustomConfiguration.WebApiSocial);
                HttpResponseMessage responseSocial = _clientSocial.PostAsJsonAsync("api/Sync/AddPerson", PersonInfoSocial).Result;

                if (!responseSocial.IsSuccessStatusCode)
                {
                    LogManager.Error(string.Format("Call WebApiNotification new User Fail Person: {0}", PersonInfoSocial));
                    throw new HeeelpSyncException();
                }

                //disparar o evento de sincronizacao concluida com sucesso! 
                person.AddEvent(new PersonSyncedSuccessEvent()
                {
                    SourceId = Guid.NewGuid(),
                    PersonId = command.PersonId
                });
                repository.AddEventToBus(person);

            }
            catch (Exception ex)
            {
                //disparar o evento de sincronizacao concluida com sucesso! 
                person.AddEvent(new PersonNotSyncedEvent()
                {
                    SourceId = Guid.NewGuid(),
                    PersonId = command.PersonId
                });
                repository.AddEventToBus(person);
                throw;
            }

        }

        private void CreateCompany(Domain.Person person, GeneralEnumerators.EnumPersonProfile profileId)
        {
            var repository = this.contextFactory();

            person.CountryId = (int)GeneralEnumerators.EnumCountry.Brazil;
            person.LanguageId = (int)GeneralEnumerators.EnumLanguage.Portuguese;
            person.PersonStatusId = (byte)GeneralEnumerators.EnumPersonStatus.AguardandoAtivação;
            person.CurrencyId = (int)GeneralEnumerators.EnumCurrency.Real;
            person.PersonTypeId = (byte)GeneralEnumerators.EnumPersonType.Legal_Person;

            //acrescenta o perfil da pessoa as suas Rules
            Domain.PersonRules personRules = new PersonRules();
            personRules.PersonId = person.PersonId;
            personRules.PersonProfileId = Convert.ToByte(profileId);
            personRules.RulesStatusId = Convert.ToByte(GeneralEnumerators.EnumRulesStatus.Ativo);
            personRules.Active = true;
            personRules.DateUTC = DateTime.UtcNow;
            person.CreationDateUTC = DateTime.UtcNow;

            person.PersonRules.Add(personRules);



            try
            {
                //salva todo o agreggate na mesma unit of work
                repository.Save(person);

                person.PersonBenefitClub = this._personDao.GetBenefitClubInfo((int)person.PersonBenefitClubId);
                //dispara a criacao dos eventos em caso de sucesso - essa logica fica na CommandHandler, nao na entidade
                this.PersonCreatedSuccess(person);
                repository.AddEventToBus(person);


            }
            catch (Exception ex)
            {
                //dispara a criacao dos eventos em caso de falha e da um thow em uma classe especializada de tratamento de erro. ex: Heeelp.Core.CustomException(Exception e)
                throw;
            }

        }


        private void CreateEmployee(Domain.Person person, Domain.User user, GeneralEnumerators.EnumPersonProfile companyProfile, byte skin)
        {
            var repository = this.contextFactory();

            try
            {

                person.CountryId = (int)GeneralEnumerators.EnumCountry.Brazil;
                person.LanguageId = (int)GeneralEnumerators.EnumLanguage.Portuguese;
                person.PersonStatusId = (byte)GeneralEnumerators.EnumPersonStatus.AguardandoAtivação;
                person.CurrencyId = (int)GeneralEnumerators.EnumCurrency.Real;
                person.CreationDateUTC = DateTime.UtcNow;

                //acrescenta o perfil da pessoa as suas Rules
                Domain.PersonRules personRules = new PersonRules();
                personRules.PersonId = person.PersonId;
                personRules.PersonProfileId = Convert.ToByte(GeneralEnumerators.EnumPersonProfile.Colaborador);
                personRules.RulesStatusId = Convert.ToByte(GeneralEnumerators.EnumRulesStatus.Ativo);
                personRules.DateUTC = DateTime.UtcNow;
                person.CreationDateUTC = DateTime.UtcNow;

                person.PersonRules.Add(personRules);


                //salva todo o agreggate na mesma unit of work
                repository.Save(person);
                //carrega as informacoes detalhadas do clube de beneficio
                person.PersonBenefitClub = this._personDao.GetBenefitClubInfo((int)person.PersonBenefitClubId);
                this.PersonCreatedSuccess(person);
                repository.AddEventToBus(person);


                #region Cria o command para usuario Vinculado a PF

                var addUserPFCommand = new Command.User.AddUserCommand();
                addUserPFCommand.Email = user.Email;
                addUserPFCommand.SecundaryEmail = user.SecundaryEmail;
                addUserPFCommand.EnrollmentIP = user.EnrollmentIP;
                addUserPFCommand.IsDefaultUser = true;
                addUserPFCommand.Name = user.Name;
                addUserPFCommand.PersonId = person.PersonId; // a conta PF vinculada a pessoa fisica
                addUserPFCommand.SmartPhoneNumber = person.PhoneNumber;
                addUserPFCommand.UserProfileId = (byte)GeneralEnumerators.EnumUserProfile.Administrador;//sempre eh o administrador da sua conta PF
                addUserPFCommand.CreatedBy = user.CreatedBy;
                addUserPFCommand.PersonProfile = companyProfile;
                addUserPFCommand.SkinId = skin;
                addUserPFCommand.IntegrationCode = Guid.NewGuid();
                addUserPFCommand.IsPerpetual = user.IsPerpetual;
                addUserPFCommand.IsDefaultUser = true;


                this._bus.Send(addUserPFCommand);

                #endregion

                #region Cria o command para o usuario Vinculado a PJ
                var addUserPJCommand = new Command.User.AddUserCommand();
                addUserPJCommand.Email = user.Email;
                addUserPJCommand.SecundaryEmail = user.SecundaryEmail;
                addUserPJCommand.EnrollmentIP = user.EnrollmentIP;
                addUserPJCommand.IsDefaultUser = true;
                addUserPJCommand.Name = user.Name;
                addUserPJCommand.PersonId = (int)person.PersonFatherId; // a conta PJ vinculada a pessoa fisica
                addUserPJCommand.SmartPhoneNumber = person.PhoneNumber;
                addUserPJCommand.UserProfileId = user.UserProfileId; //pode variar
                addUserPJCommand.CreatedBy = user.CreatedBy;
                addUserPJCommand.PersonProfile = companyProfile;
                addUserPJCommand.SkinId = skin;
                addUserPJCommand.IntegrationCode = Guid.NewGuid();
                addUserPJCommand.IsPerpetual = user.IsPerpetual;
                addUserPJCommand.IsDefaultUser = user.IsDefaultUser;
                this._bus.Send(addUserPJCommand);

                #endregion

            }
            catch (Exception ex)
            {

                throw;
            }
        }




        #endregion


        #region Updates
        //public void Handle(UpdatePersonCompleteRegistrationCommand command)
        //{

        //    try
        //    {
        //        var repository = this.contextFactory();                
        //        Domain.Person person = this.GetPersonByPersonId(command.PersonId);

        //        foreach (var user in person.Users)
        //        {
        //            user.LoginPassword = command.Password;
        //            user.SmartPhoneNumber = command.PhoneNumber;

        //        }

        //        Domain.PersonContract termoDeUso = new PersonContract() { PersonId = command.PersonId, ContractId = command.ContractId, AgreementDateUTC = DateTime.UtcNow };
        //        Domain.PersonContract politicaDePrivacidade = new PersonContract() { PersonId = command.PersonId, ContractId = command.PrivacyPolicyId, AgreementDateUTC = DateTime.UtcNow };
        //        person.PersonContract.Add(termoDeUso);
        //        person.PersonContract.Add(politicaDePrivacidade);

        //        repository.Save(person);

        //        person.AddEvent(new PersonActivationUpdatedSucessEvent
        //        {
        //            SourceId = person.Id,
        //            PersonId = person.PersonId
        //        });
        //        repository.AddEventToBus(person);

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }


        //}

        public void Handle(InactivePersonCoWorkerCommand command)
        {
            var repository = this.contextFactory();

            var personList = repository.List(x => command.CompanyListId.Contains(x.IntegrationCode));
            if (personList != null)
            {
                var list = personList.ToList();
                foreach (var item in list)
                {

                    item.Active = false;
                    repository.Update(item);
                }
            }

        }

        public void Handle(UpdatePersonCompanyCommand command)
        {

            var repository = this.contextFactory();
            Domain.Person person = repository.Get(x => x.IntegrationCode.Equals(command.IntegrationCode));

            person.Name = string.IsNullOrEmpty(command.Name) ? person.Name : command.Name;
            person.FantasyName = string.IsNullOrEmpty(command.FantasyName) ? person.FantasyName : command.FantasyName;
            person.FriendlyNameURL = string.IsNullOrEmpty(command.FriendlyNameURL) ? person.FriendlyNameURL : command.FriendlyNameURL;
            person.PersonTypeId = command.PersonTypeId == null ? person.PersonTypeId : Convert.ToByte(command.PersonTypeId);
            person.PersonStatusId = command.PersonStatusId == null ? person.PersonStatusId : Convert.ToByte(command.PersonStatusId);
            person.PersonalWebSite = string.IsNullOrEmpty(command.PersonalWebSite) ? person.PersonalWebSite : command.PersonalWebSite;
            person.PhoneNumber = string.IsNullOrEmpty(command.PhoneNumber) ? person.PhoneNumber : command.PhoneNumber;

            //todo: adicionar o PersonProfile no PersonRules
            repository.Update(person);
        }

        public void Handle(SendWelcomeMessageCommand command)
        {

            var user = this._userDao.Get(command.UserIntegrationCode);
            //var userPf = _userDao.GetUserFullInfoUserPF(user.Email);
            var employee = _personDao.GetPerson(user.PersonId);
            var company = _personDao.GetCompanyByEmployeePersonId((int)employee.PersonFatherId);

            var repository = this.contextFactory();

            #region variaveis
            Domain.PersonFile personFile = new PersonFile();
            byte companyProfile = 0;
            //todo: implementar da forma correta o Get first or default
            foreach (var pr in company.PersonRules)
            {
                companyProfile = pr.PersonProfileId;
            }


            string title = String.Format("Ola {0}, tudo pronto para usar o {1}", employee.Name, company.PersonBenefitClub.CustomClubName);
            string messageCodeType = WelcomeMessageTypeSelector(employee.SkinId, (GeneralEnumerators.EnumUserProfile)user.UserProfileId, (GeneralEnumerators.EnumPersonProfile)companyProfile).ToString();

            //todo: substituir as mensagens em hard code por arquivos de resources para permitir traducao para outros idiomas
            #endregion

            var message = new NotificationDTO(1,
                employee.PersonId,
                user.Name,
                (int)GeneralEnumerators.EnumLanguage.Portuguese,
                messageCodeType,
                title,
                company.PersonBenefitClub.CustomClubName,
                company.PersonBenefitClub.CustomHeeelpPersonDomain,
                company.PersonBenefitClub.CustomClubLogo);

            var _clientNotification = new HttpClient();
            _clientNotification.BaseAddress = new Uri(CustomConfiguration.WebApiNotification);
            var resultNotification = _clientNotification.PostAsJsonAsync("api/Communication/SendMessage", message).Result;
            if (!resultNotification.IsSuccessStatusCode)
            {
                LogManager.Info(string.Format("Erro ao enviar msg de boas vindas para o userId: {0}, user: {1}, email: {2}", user.UserId, user.Name, user.Email));
            }


            //if (messageCodeType != GeneralEnumerators.EnumEmailWelcomeType.BoasVindColab.ToString())
            //{
            //    messageCodeType = GeneralEnumerators.EnumEmailWelcomeType.BoasVindColabWeb.ToString();


            //    #endregion


            //    message = new NotificationDTO(1,
            //      userPf.PersonId,
            //      userPf.Name,
            //      (int)GeneralEnumerators.EnumLanguage.Portuguese,
            //      messageCodeType,
            //      title,
            //      company.PersonBenefitClub.CustomClubName,
            //      company.PersonBenefitClub.CustomHeeelpPersonDomain,
            //      company.PersonBenefitClub.CustomClubLogo);

            //    _clientNotification = new HttpClient();
            //    _clientNotification.BaseAddress = new Uri(CustomConfiguration.WebApiNotification);
            //    resultNotification = _clientNotification.PostAsJsonAsync("api/Communication/SendMessage", message).Result;
            //    if (!resultNotification.IsSuccessStatusCode)
            //    {
            //        LogManager.Info(string.Format("Erro ao enviar msg de boas vindas para o userId: {0}, user: {1}, email: {2}", user.UserId, employee.Name, user.Email));
            //    }
            //}

        }


        public void Handle(AddPersonContractCommand command)
        {
            try
            {
                var repository = this.contextFactory();
                Domain.Person person = repository.Get(x => x.PersonId.Equals(command.PersonId));
                person.PersonContract.Add(new Domain.PersonContract()
                {
                    PersonId = command.PersonId,
                    ContractId = command.ContractId,
                    UserId = command.UserId,
                    FileId = command.FileId,
                    AgreementDateUTC = DateTime.UtcNow
                });

                repository.Save(person);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void Handle(UpdatePersonStatusCommand command)
        {
            var repository = this.contextFactory();

            Domain.Person person = repository.Get(x => x.PersonId.Equals(command.PersonId));
            person.PersonStatusId = (byte)command.Status;
            person.PersonHistoric.Add(CreatePersonHistory(person));
            try
            {
                repository.Update(person);
                person.AddEvent(new PersonStatusUpdatedSucessEvent() { PersonId = person.PersonId, PersonStatusId = (GeneralEnumerators.EnumPersonStatus)person.PersonStatusId });
                repository.AddEventToBus(person);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region Events
        private void PersonCreatedSuccess(Domain.Person person)
        {

            switch (person.PersonTypeId)
            {
                case (byte)GeneralEnumerators.EnumPersonType.Natural_Person:
                    person.AddEvent(new PersonEmployeeCreatedSucessEvent
                    {
                        SourceId = person.Id,
                        PersonId = person.PersonId,
                        IntegrationCode = person.IntegrationCode,
                        CountryId = person.CountryId,
                        LanguageId = person.LanguageId,
                        PersonStatusId = person.PersonStatusId,
                        CurrencyId = person.CurrencyId,
                        Active = person.Active,
                        PersonTypeId = person.PersonTypeId,
                        SkinId = person.SkinId,
                        CustomClubName = person.PersonBenefitClub.CustomClubName,
                        CustomHeeelpPersonDomain = person.PersonBenefitClub.CustomHeeelpPersonDomain
                    });
                    break;

                case (byte)GeneralEnumerators.EnumPersonType.Legal_Person:
                    person.AddEvent(new PersonCompanyCreatedSucessEvent
                    {
                        SourceId = person.Id,
                        PersonId = person.PersonId,
                        IntegrationCode = person.IntegrationCode,
                        CountryId = person.CountryId,
                        LanguageId = person.LanguageId,
                        PersonStatusId = person.PersonStatusId,
                        CurrencyId = person.CurrencyId,
                        Active = person.Active,
                        PersonTypeId = person.PersonTypeId,
                        SkinId = person.SkinId,
                        CustomClubName = person.PersonBenefitClub.CustomClubName,
                        CustomHeeelpPersonDomain = person.PersonBenefitClub.CustomHeeelpPersonDomain
                    });
                    break;
            }

        }

        private void PersonCreatedFailure(Domain.Person person)
        {
            switch (person.PersonTypeId)
            {
                case (byte)GeneralEnumerators.EnumPersonType.Natural_Person:
                    person.AddEvent(new PersonEmployeeNotCreatedEvent
                    {
                        SourceId = person.Id,
                        PersonId = person.PersonId,
                        IntegrationCode = person.IntegrationCode,
                        CountryId = person.CountryId,
                        LanguageId = person.LanguageId,
                        PersonStatusId = person.PersonStatusId,
                        CurrencyId = person.CurrencyId,
                        Active = person.Active,
                        PersonTypeId = person.PersonTypeId
                    });
                    break;

                case (byte)GeneralEnumerators.EnumPersonType.Legal_Person:
                    person.AddEvent(new PersonCompanyNotCreatedEvent
                    {
                        SourceId = person.Id,
                        PersonId = person.PersonId,
                        IntegrationCode = person.IntegrationCode,
                        CountryId = person.CountryId,
                        LanguageId = person.LanguageId,
                        PersonStatusId = person.PersonStatusId,
                        CurrencyId = person.CurrencyId,
                        Active = person.Active,
                        PersonTypeId = person.PersonTypeId
                    });
                    break;
            }

        }

        private void CompleteSuccessFile(FIleServer fs)
        {
            //todo: esse metodo nao pode ficar na entidade, necessario achar outro lugar para ele, se possivel um command a ser acionado pela Controller
            //this.AddEvent(new PersonProspectFileAddEvent
            //{
            //    SourceId = this.Id,
            //    FilePath = fs.FilePath,
            //    Width = fs.Width,
            //    Height = fs.Height,
            //    OriginalName = fs.OriginalName,
            //    FileTempId = fs.FileTempId,
            //    Description = "Usuário do Heeelp",
            //    FriendlyName = fs.FriendlyName,
            //    FileUtilizationId = fs.FileUtilizationId,
            //    Alt = fs.Alt,
            //    FileIntegrationCode = fs.FileIntegrationCode,
            //    Name = fs.Name,
            //    FileOriginTypeId = (int)GeneralEnumerators.EnumModules.Core_User,
            //    PersonId = this.PersonId,
            //    UploadedBy = fs.UploadedBy
            //});
        }

        //todo: esse metodo nao pode ficar na entidade, necessario achar outro lugar para ele, se possivel um command a ser acionado pela Controller
        private void CompleteSendDocumentSuccess(string number, int userSystem, short docType)
        {
            //this.AddEvent(new PersonDocumentAdded
            //{
            //    SourceId = this.Id,
            //    PersonId = this.PersonId,
            //    IntegrationCode = this.IntegrationCode,
            //    DocumentTypeId = docType,
            //    Number = number,
            //    Active = true,
            //    UserSystemId = userSystem,

            //});
        }

        private GeneralEnumerators.EnumEmailWelcomeType WelcomeMessageTypeSelector(int skin, GeneralEnumerators.EnumUserProfile userProfileId, GeneralEnumerators.EnumPersonProfile personProfileId)
        {

            GeneralEnumerators.EnumEmailWelcomeType messageType = GeneralEnumerators.EnumEmailWelcomeType.BoasVindColab;
            if (skin > 1)
                messageType = GeneralEnumerators.EnumEmailWelcomeType.BoasVindColbWL;
            switch (personProfileId)
            {
                case GeneralEnumerators.EnumPersonProfile.PrestadorServiços:
                    if (userProfileId == GeneralEnumerators.EnumUserProfile.Administrador)
                        messageType = GeneralEnumerators.EnumEmailWelcomeType.BoasVindPresSer;
                    break;
                case GeneralEnumerators.EnumPersonProfile.EmpresaAssociadaClubedeBeneficios:
                    if (userProfileId == GeneralEnumerators.EnumUserProfile.Administrador)
                    {
                        if (skin > 1)
                            messageType = GeneralEnumerators.EnumEmailWelcomeType.BoasVindGestCW;
                        else
                            messageType = GeneralEnumerators.EnumEmailWelcomeType.BoasVindGestRH;
                    }
                    break;
                case GeneralEnumerators.EnumPersonProfile.AdminstradorSistema:
                    if (userProfileId == GeneralEnumerators.EnumUserProfile.Administrador)
                        messageType = GeneralEnumerators.EnumEmailWelcomeType.BoasVindColab;
                    break;
                case GeneralEnumerators.EnumPersonProfile.EmpresaCoWorking:
                    if (userProfileId == GeneralEnumerators.EnumUserProfile.Administrador)
                        messageType = GeneralEnumerators.EnumEmailWelcomeType.BoasVindGestCW;
                    break;

                default:
                    break;
            }
            return messageType;
        }

        #endregion

        #region PersonInformation


        public void Handle(AddCompanyInformationCommand command)
        {
            var repository = this.contextFactory();

            Domain.Person person = this.GetPersonByPersonIntegrationCode(repository, command.IntegrationCode);

            //add address
            person.PersonAddress.Add(CreateAddress(person, command.StreetName, command.Number, GeneralEnumerators.EnumAddressType.Comercial, command.PostCode, command.CreatedBy, command.ContactEmail, command.Neighbourhood, command.StateId, command.CityId, command.Complement));
            //add documento
            if (!string.IsNullOrEmpty(command.DocumentNumber))
                person.PersonDocument.Add(CreateDocument(person, GeneralEnumerators.EnumDocumentType.CNPJ, command.DocumentNumber, command.CreatedBy));

            repository.Save(person);
        }

        public void Handle(UploadFilePersonCommand command)
        {

            var repository = this.contextFactory();
            var person = repository.Get(x => x.IntegrationCode == command.IntegrationCode);

            foreach (var item in command.ListFileTemp)
            {
                FIleServer fs = new FIleServer();

                fs.FilePath = item.FilePath;
                fs.Width = item.Width;
                fs.Height = item.Height;
                fs.OriginalName = item.OriginalName;
                fs.FileIntegrationCode = item.FileIntegrationCode;
                //fs.FileTempId = item.FIl;
                fs.Description = "File Person";
                fs.FileUtilizationId = Convert.ToByte(GeneralEnumerators.EnumFileUtiliaztion.Album);
                fs.FriendlyName = person.Name;
                fs.Alt = person.Name;
                fs.Name = person.Name;
                fs.FileOriginTypeId = (int)GeneralEnumerators.EnumModules.Core_User;
                fs.PersonId = person.PersonId;
                fs.UploadedBy = Convert.ToInt32(command.CreatedBy);

                person.UrlImageLogo = item.FilePath;
                repository.Save(person);
                try
                {
                    var ret = fs.SendFilePath(fs);
                    if (ret > 0)
                    {
                        PersonFile personFile = new PersonFile();
                        personFile.PersonId = person.PersonId;
                        personFile.FileId = ret;
                        personFile.AssociatedDateUTC = DateTime.UtcNow;
                        personFile.Active = true;
                        //aggregateroot
                        person.PersonFile.Add(personFile);
                        repository.Save(person);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Falha ao enviar Arquivo para o FileServer");
                }
            }

        }


        public void Handle(UploadLogoPersonCommand command)
        {

            var repository = this.contextFactory();
            var person = repository.Get(x => x.IntegrationCode == command.IntegrationCode);

            foreach (var item in command.ListFileTemp)
            {
                FIleServer fs = new FIleServer();

                fs.FilePath = item.FilePath;
                fs.Width = item.Width;
                fs.Height = item.Height;
                fs.OriginalName = item.OriginalName;
                fs.FileIntegrationCode = item.FileIntegrationCode;
                //fs.FileTempId = item.FIl;
                fs.Description = "Logo Person";
                fs.FileUtilizationId = Convert.ToByte(GeneralEnumerators.EnumFileUtiliaztion.Album);
                fs.FriendlyName = person.Name;
                fs.Alt = person.Name;
                fs.Name = person.Name;
                fs.FileOriginTypeId = (int)GeneralEnumerators.EnumModules.Core_User;
                fs.PersonId = person.PersonId;
                fs.UploadedBy = Convert.ToInt32(command.CreatedBy);


                var ret = fs.SendFilePath(fs);

                if (ret > 0)
                {
                    PersonFile personFile = new PersonFile();
                    personFile.PersonId = person.PersonId;
                    personFile.FileId = ret;
                    personFile.AssociatedDateUTC = DateTime.UtcNow;
                    personFile.Active = true;
                    //aggregateroot
                    person.PersonFile.Add(personFile);
                    person.UrlImageLogo = fs.FilePath;
                    repository.Save(person);
                }
                else
                {
                    throw new Exception();
                }
            }

        }


        #endregion

        #region Utils
        private Domain.Person GetPersonByPersonId(int personId)
        {

            var repository = this.contextFactory();
            return repository.Get(x => x.PersonId.Equals(personId));

        }

        private Domain.Person GetPersonByPersonIntegrationCode(IDataContext<Domain.Person> repository, Guid integrationCode)
        {
            return repository.Get(x => x.IntegrationCode.Equals(integrationCode));
        }

        private Domain.PersonHistoric CreatePersonHistory(Domain.Person person)
        {
            Domain.PersonHistoric personHistory = new Domain.PersonHistoric();
            personHistory.ActivationCode = person.ActivationCode;
            personHistory.ActivationDateUTC = person.ActivationDateUTC;
            personHistory.Active = person.Active;
            personHistory.CountryId = (byte)person.CountryId;
            personHistory.CreationDateUTC = person.CreationDateUTC;
            personHistory.CurrencyId = person.CurrencyId;
            personHistory.FantasyName = person.FantasyName;
            personHistory.FriendlyNameURL = person.FriendlyNameURL;
            personHistory.IntegrationCode = person.IntegrationCode;
            personHistory.InviteId = person.InviteId;
            personHistory.IsSafe = person.IsSafe;
            personHistory.LanguageId = person.LanguageId;
            personHistory.Name = person.Name;
            personHistory.NameFromSecurityCheck = person.NameFromSecurityCheck;
            personHistory.PersonalWebSite = person.PersonalWebSite;
            personHistory.PersonFatherId = person.PersonFatherId;
            personHistory.PersonId = person.PersonId;
            personHistory.PersonOriginDetails = person.PersonOriginDetails;
            personHistory.PersonOriginTypeId = person.PersonOriginTypeId;
            personHistory.PersonStatusId = person.PersonStatusId;
            personHistory.PersonTypeId = person.PersonTypeId;
            personHistory.PhoneNumber = person.PhoneNumber;
            personHistory.SecuritySourceId = person.SecuritySourceId;
            personHistory.ServerInstanceId = person.ServerInstanceId;
            personHistory.UpdateDateUTC = DateTime.UtcNow;

            return personHistory;
        }

        private Domain.PersonAddress CreateAddress(Domain.Person person, string streetName, string number, GeneralEnumerators.EnumAddressType addressType, string postCode, int createdBy, string contactEmail, string neighbourhood, int stateId, int cityId, string complement)
        {
            var address = new Domain.PersonAddress();
            var _client = new HttpClient();
            _client.BaseAddress = new Uri("http://maps.google.com");
            string uri = string.Concat("/maps/api/geocode/json?address=", streetName, " ", number, " ", neighbourhood, " ", GeneralEnumerators.GetEnumDescription((GeneralEnumerators.EnumCity)cityId), "-", GeneralEnumerators.GetEnumDescription((GeneralEnumerators.EnumState)stateId), " ", postCode);
            HttpResponseMessage response = _client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                var ret = response.Content.ReadAsStringAsync().Result;
                var geocode = JsonConvert.DeserializeObject<dynamic>(ret);
                var pointString = string.Format("POINT({0} {1})", geocode.results[0].geometry.location.lat, geocode.results[0].geometry.location.lng);
                var point = DbGeography.PointFromText(pointString.Replace(",", "."), 4326);
                //var neighbourhood = geocode.results[0].address_components[2].long_name;
                streetName = streetName + ", " + number + " - " + neighbourhood + ", " + GeneralEnumerators.GetEnumDescription((GeneralEnumerators.EnumCity)cityId) + " - " + GeneralEnumerators.GetEnumDescription((GeneralEnumerators.EnumState)stateId) + ", " + GeneralEnumerators.EnumCountry.Brazil.ToString();

                address.PersonId = person.PersonId;
                address.AddressTypeId = Convert.ToByte(addressType);
                address.StartDateUTC = DateTime.UtcNow;
                address.StreetName = streetName;
                address.Complement = complement;
                address.Number = number;
                address.Neighbourhood = neighbourhood;
                address.Country = GeneralEnumerators.GetEnumDescription(GeneralEnumerators.EnumCountry.Brazil);
                address.State = GeneralEnumerators.GetEnumDescription((GeneralEnumerators.EnumState)stateId);
                address.City = GeneralEnumerators.GetEnumDescription((GeneralEnumerators.EnumCity)cityId);
                address.PostCode = postCode;
                address.Coordinates = point;
                address.ContactPhoneNumber = person.PhoneNumber;
                address.ServerInstanceId = 1;
                address.CreatedBy = createdBy;
                address.ContactEmail = contactEmail;
                address.Active = true;
            }
            return address;
        }

        private Domain.PersonDocument CreateDocument(Domain.Person person, GeneralEnumerators.EnumDocumentType documentType, string documentNumber, int createdBy)
        {
            var document = new Domain.PersonDocument();
            document.PersonId = person.PersonId;
            document.DocumentTypeId = (short)documentType;
            document.Number = documentNumber;
            document.Active = true;
            document.AssociatedBy = createdBy;
            document.InsertedDateUTC = DateTime.UtcNow;
            return document;
        }

        #endregion

        #region Expertise

        public void Handle(AddPersonCompanyExpertiseCommand command)
        {
            try
            {
                var repository = this.contextFactory();
                var repositoryExpertise = this.contextFactoryExpertise();
                Domain.Person person = repository.Get(x => x.IntegrationCode.Equals(command.IntegrationCode));
                Domain.Expertise expertise = repositoryExpertise.Get(x => x.ExpertiseId == command.ExpertiseId);
                person.PersonExpertise.Add(new PersonExpertise()
                {
                    PersonId = person.PersonId,
                    ExpertiseId = expertise.ExpertiseId,
                    InsertedBy = command.CreatedBy,
                    ServerInstanceId = (int)GeneralEnumerators.EnumServerInstance.Server1,
                    CustomDescription = expertise.Name,
                    InsertedDateUTC = DateTime.UtcNow,
                    ExhibitionOrder = 1,
                    Active = true
                });

                repository.Save(person);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion


        #region AdminFeatures
        public void Handle(AdminSyncAllPersonsCommand command)
        {
            try
            {
                var repository = this.contextFactory();

                var personList = repository.List(x => x.Active.Equals(true));

                foreach (var person in personList)
                {
                    SyncPersonInfoCommand syncPersonInfoCommand = new SyncPersonInfoCommand()
                    {
                        PersonId = person.PersonId
                    };
                    this._bus.Send(syncPersonInfoCommand);
                }

                this._bus.Send(new AdminSyncAllUsersCommand());

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

    }
}
