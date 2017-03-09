using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Heeelp.Core.Command.Person;
using Heeelp.Core.Command.User;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Logging;
using Heeelp.Core.Storage;

namespace Heeelp.Core.WebAPI.Controllers
{


    public class ExternalController : BaseController
    {
        private readonly ICommandBus bus;
        private long fileId;
        private List<int> _listFileTemp;
        private List<FIleServer> fs;
        private IStorage _storage;
        private string _containerName;
        private readonly IFileTempDao _FileTemp;
        private IPersonDao _person;

        public ExternalController(ICommandBus bus, IFileTempDao fileTemp, IPersonDao person) : base()
        {
            this.bus = bus;
            this._FileTemp = fileTemp;
            this._listFileTemp = new List<int>();
            this._person = person;
            _storage = new StorageClient(CustomConfiguration.Storage);
            _containerName = CustomConfiguration.ContainerName;
        }


        /// <summary>
        /// Adiciona Nova Empresa
        /// </summary>
        /// <param name="company">company</param>
        /// <remarks>Add nova Empresa</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(NewCompanyDTO))]
        [Authorize]
        public HttpResponseMessage AddCompany(NewCompanyDTO company)
        {

            Claims claims = new Claims().Values();
            Guid personIntegrationCode = Guid.NewGuid();

            try
            {
                //se estao validos, cria o cammand
                var command = new AddPersonCompanyPartnerCommand();

                if (ModelState.IsValid)
                {
                    command.IntegrationCode = personIntegrationCode;
                    command.PersonIntegrationID = Guid.NewGuid();
                    command.CompanyName = company.CompanyName;
                    command.CompanyPhoneNumber = company.CompanyPhoneNumber;
                    command.ManagerName = company.ManagerName;
                    command.ManagerPhoneNumber = company.ManagerSmartPhoneNumber;
                    command.UserSystemId = claims.userSystemId;
                    command.ManagerEmail = company.ManagerEmail;
                    command.CreatedBy = claims.userSystemId;
                    command.EnrollmentIP = "Colocar aqui o IP";
                    command.PersonOriginType = GeneralEnumerators.EnumPersonOriginType.IntegracaoAPIExterna;
                    this.bus.Send(command);
                }

                return Request.CreateResponse(HttpStatusCode.OK, personIntegrationCode);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add UserAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        /// <summary>
        /// Adiciona Novo Coworker
        /// </summary>
        /// <param name="coworker">coworker</param>
        /// <remarks>Add novo coworker</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(NewCompanyDTO))]
        [Authorize]
        public async Task<HttpResponseMessage> AddCoworking(NewCompanyDTO coworking)
        {
            Guid personIntegrationCode = Guid.NewGuid();
            Claims claims = new Claims().Values();

            try
            {

                var command = new AddPersonCoworkingCommand();

                if (ModelState.IsValid)
                {
                    command.IntegrationCode = personIntegrationCode;
                    command.PersonIntegrationID = Guid.NewGuid();
                    command.CompanyName = coworking.CompanyName;
                    command.CompanyPhoneNumber = coworking.CompanyPhoneNumber;
                    command.ManagerName = coworking.ManagerName;
                    command.ManagerPhoneNumber = coworking.ManagerSmartPhoneNumber;
                    command.UserSystemId = claims.userSystemId;
                    command.ManagerEmail = coworking.ManagerEmail;
                    command.CreatedBy = claims.userSystemId;
                    command.EnrollmentIP = "Colocar aqui o IP";
                    command.PersonOriginType = GeneralEnumerators.EnumPersonOriginType.IntegracaoAPIExterna;
                    this.bus.Send(command);
                }

                this.bus.Send(command);

                return Request.CreateResponse(HttpStatusCode.OK, personIntegrationCode);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add Coworking", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Adiciona Nova Empresa
        /// </summary>
        /// <param name="company">company</param>
        /// <remarks>Add nova Empresa</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(NewCompanyDTO))]
        [Authorize]
        public HttpResponseMessage AddServiceProvider(NewCompanyDTO company)
        {

            Claims claims = new Claims().Values();
            Guid personIntegrationCode = Guid.NewGuid();

            try
            {

                #region PersonCommand Pessoa Juridica
                var CommandCompanyServiceProvider = new AddPersonServiceProviderCommand();

                CommandCompanyServiceProvider.IntegrationCode = personIntegrationCode;
                CommandCompanyServiceProvider.PersonIntegrationID = Guid.NewGuid();
                CommandCompanyServiceProvider.CompanyName = company.CompanyName;
                CommandCompanyServiceProvider.CompanyPhoneNumber = company.CompanyPhoneNumber;
                CommandCompanyServiceProvider.ManagerName = company.ManagerName;
                CommandCompanyServiceProvider.ManagerPhoneNumber = company.ManagerSmartPhoneNumber;
                CommandCompanyServiceProvider.UserSystemId = claims.userSystemId;
                CommandCompanyServiceProvider.ManagerEmail = company.ManagerEmail;
                CommandCompanyServiceProvider.CreatedBy = claims.userSystemId;
                CommandCompanyServiceProvider.EnrollmentIP = "Colocar aqui o IP";
                CommandCompanyServiceProvider.PersonOriginType = GeneralEnumerators.EnumPersonOriginType.IntegracaoAPIExterna;
                this.bus.Send(CommandCompanyServiceProvider);

                #endregion
                return Request.CreateResponse(HttpStatusCode.OK, personIntegrationCode);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add UserAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }



        /// <summary>
        /// Adiciona Novo Colaborador na Empresa Associada
        /// </summary>
        /// <param name="user">user</param>
        /// <remarks> Adiciona Novo Membro Empresa Associada</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(AssociateCompanyUserDTO))]
        [Authorize]
        public async Task<HttpResponseMessage> AddAssociateCompanyUser(NewCompanyDTO company)
        {

            try
            {
                Claims claims = new Claims().Values();
                Guid personIntegrationCode = Guid.NewGuid();
                var commandCoWorking = new AddPersonCoworkingCommand();

                commandCoWorking.IntegrationCode = Guid.NewGuid();
                commandCoWorking.CompanyName = company.CompanyName;
                commandCoWorking.CompanyPhoneNumber = company.CompanyPhoneNumber;
                commandCoWorking.ManagerName = company.ManagerName;
                commandCoWorking.ManagerPhoneNumber = company.ManagerSmartPhoneNumber;
                commandCoWorking.UserSystemId = claims.userSystemId;
                commandCoWorking.ManagerEmail = company.ManagerEmail;
                commandCoWorking.CreatedBy = claims.userSystemId;
                commandCoWorking.EnrollmentIP = "Colocar aqui o IP";

                this.bus.Send(commandCoWorking);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add Coworking", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }



        ///// <summary>
        ///// Get Membros Empresa Associada
        ///// </summary>
        ///// <param name="personId">personId</param>
        ///// <param name="userId">userId</param>
        ///// <remarks>Get Membros Empresa Associada</remarks>
        ///// <returns></returns>
        ///// <response code="200"></response>
        //[HttpGet]
        //[ResponseType(typeof(int))]
        //[Authorize]
        //public async Task<HttpResponseMessage> GetAssociateComapnyUsers(int personId, int userId)
        //{
        //    //try
        //    //{
        //    //    Claims claims = new Claims().Values();
        //    //    IEnumerable<UserListAssociateDTO> user = _person.ListCompanyCoworkers(claims.personFatherId, claims.userSystemId);

        //    //    IEnumerable<EmployeeDTO> user = _person.ListEmployes(personId, userId);

        //    //    return Request.CreateResponse(HttpStatusCode.OK, user);
        //    //}
        //    //catch (System.Exception e)
        //    //{
        //    //    LogManager.Error("Erro ao Add UserAdd", e);
        //    //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        //    //}
        //}


        /// <summary>
        /// Editar Membro Empresa Associada
        /// </summary>
        /// <param name="user">user</param>
        /// <remarks>Editar Membro Empresa Associada</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        //[HttpPost]
        //[ResponseType(typeof(UserUpdateDTO))]
        //[Authorize]
        //public async Task<HttpResponseMessage> EditAssociateComapnyUsers(UserUpdateDTO user)
        //{
        //    try
        //    {
        //        if (user.UserId != 0)
        //        {
        //            var commandUpdateUser = new UpdateUserCommand
        //            {
        //                UserId = user.UserId,
        //                Name = user.Name,
        //                SmartPhoneNumber = user.SmartPhoneNumber,
        //                SecundaryEmail = user.SecundaryEmail,
        //                Email = user.Email,
        //                UserProfileId = user.UserProfileId,
        //                UserStatusId = user.UserStatusId
        //            };

        //            bus.Send(commandUpdateUser);
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed, "invalid UserId");
        //        }

        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch (System.Exception e)
        //    {
        //        LogManager.Error("Erro ao Add UserAdd", e);
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        //    }
        //}


        /// <summary>
        /// Get Empresa Associada ao coworker
        /// </summary>
        /// <param name="personId">personId</param>
        /// <remarks>Get Membros Empresa Associada ao coworker</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [ResponseType(typeof(int))]
        [Authorize]
        [Route("external/GetCompanyCoWorker/{personId}")]
        public async Task<HttpResponseMessage> GetCompanyCoWorker(int personId)
        {
            try
            {
                Claims claims = new Claims().Values();

                IEnumerable<PersonListDTO> person = _person.ListPersons(personId);

                return Request.CreateResponse(HttpStatusCode.OK, person);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add UserAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        /// <summary>
        /// Editar Empresa Associada CoWorker
        /// </summary>
        /// <param name="person">person</param>
        /// <remarks>Editar Empresa Associada CoWorker</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(PersonCoworkerUpdateDTO))]
        [Authorize]
        public async Task<HttpResponseMessage> EditCoWorkerComapny(PersonCoworkerUpdateDTO person)
        {
            try
            {
                if (person.PersonId == 0)
                {
                    var commandUpdatePersonCoWorker = new UpdatePersonCompanyCommand
                    {
                        Name = person.Name,
                        FantasyName = person.FantasyName,
                        FriendlyNameURL = person.FriendlyNameURL,
                        PersonTypeId = person.PersonTypeId,
                        PersonProfileId = person.PersonProfileId,
                        PersonStatusId = person.PersonStatusId,
                        PersonalWebSite = person.PersonalWebSite,
                        PhoneNumber = person.PhoneNumber
                    };

                    bus.Send(commandUpdatePersonCoWorker);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed, "invalid UserId");
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add UserAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Deleta usuario associado
        /// </summary>
        /// <param name="userid">userid</param>
        /// <remarks>Deleta usuario associado</remarks>
        /// <returns></returns>
        /// <response code="200"></response>  
        [HttpGet]
        [HttpPost]
        [ResponseType(typeof(int))]
        [Authorize]
        [Route("external/DeleteAssociateCompanyUser/{EmployeeId}")]
        public HttpResponseMessage DeleteAssociateCompanyUser(Guid EmployeeId)
        {
            #region UserCommand
            try
            {
                var commandInactiveAssociateCompanyUser = new InactiveEmployeeCommand();
                commandInactiveAssociateCompanyUser.IntegrationCode = EmployeeId;
                this.bus.Send(commandInactiveAssociateCompanyUser);

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK);
                LogManager.Error("Error to delete User");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error to delete User");

            }
            catch (Exception e)
            {
                LogManager.Error("Error to delete User");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);

            }
        }


        ///// <summary>
        ///// Deleta Empresa
        ///// </summary>
        ///// <param name="company">Guid PersonIntegrationID</param>
        ///// <remarks>Deleta Empresa</remarks>
        ///// <returns></returns>
        ///// <response code="200"></response>
        //[HttpPost]
        //[ResponseType(typeof(DeleteCompanyDTO))]
        //[Authorize]
        //public HttpResponseMessage DeleteCompany(DeleteCompanyDTO company)
        //{
        //    #region PersonCommand

        //    var commandInactivePersonLegal = new InactivePersonCoWorkerCommand();

        //    commandInactivePersonLegal.PersonIntegrationID = company.PersonIntegrationID;
        //    this.bus.Send(commandInactivePersonLegal);

        //    #endregion

        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}


    }

}
