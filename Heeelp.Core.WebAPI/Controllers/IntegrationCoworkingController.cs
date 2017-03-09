using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Heeelp.Core.Command.Person;
using Heeelp.Core.Command.User;
using Heeelp.Core.Common;
using Heeelp.Core.Domain;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Logging;
using Heeelp.Core.Storage;
using Swashbuckle.Swagger;
using Heeelp.Core.WebAPI.Validation;

namespace Heeelp.Core.WebAPI.Controllers
{


    public class IntegrationCoworkingController : BaseController
    {
        private readonly ICommandBus bus;
        private long fileId;
        private List<int> _listFileTemp;
        private List<FIleServer> fs;
        private IStorage _storage;
        private string _containerName;
        private readonly IFileTempDao _FileTemp;
        private IPersonDao _person;

        public IntegrationCoworkingController(ICommandBus bus, IFileTempDao fileTemp, IPersonDao person) : base()
        {
            this.bus = bus;
            this._FileTemp = fileTemp;
            this._listFileTemp = new List<int>();
            this._person = person;
            _storage = new StorageClient(CustomConfiguration.Storage);
            _containerName = CustomConfiguration.ContainerName;
        }


        #region Add

        /// <summary>
        /// Adiciona Nova Empresa
        /// </summary>
        /// <param name="company">company</param>
        /// <remarks>Add nova Empresa</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(NewCompanyDTO))]
        [Authorize(Roles = "GestorCoWorking,AdminHeeelp")] 
        [Route("api/integration/AddCoworkerCompany")]
        public HttpResponseMessage AddCoworkerCompany(NewCompanyDTO company)
        {

            Claims claims = new Claims().Values();
            Guid personIntegrationCode = Guid.NewGuid();

            try
            {
                if (ModelState.IsValid)
                {
                    var command = new AddPersonCompanyPartnerCommand();

                    command.IntegrationCode = Guid.NewGuid();
                    command.PersonIntegrationID = personIntegrationCode;
                    command.CompanyName = company.CompanyName;
                    command.CompanyPhoneNumber = company.CompanyPhoneNumber;
                    command.ManagerName = company.ManagerName;
                    command.ManagerPhoneNumber = company.ManagerSmartPhoneNumber;
                    command.UserSystemId = claims.userSystemId;
                    command.ManagerEmail = company.ManagerEmail;
                    command.CreatedBy = claims.userSystemId;
                    command.EnrollmentIP = HttpContext.Current.Request.UserHostAddress;
                    command.PersonOriginType = GeneralEnumerators.EnumPersonOriginType.IntegracaoAPIExterna;
                    command.SkinId = 3;
                    command.PersonIntegrationFatherId = company.PersonIntegrationFatherId;
                    this.bus.Send(command);
                }

                return Request.CreateResponse(HttpStatusCode.OK, personIntegrationCode);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add Empresa Coworker", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

        }


        /// <summary>
        /// Adiciona Novo Colaborador na Empresa Associada
        /// </summary>
        /// <param name="person">user</param>
        /// <remarks> Adiciona Novo Membro Empresa Associada</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(NewEmployeeDTO))]
        [Authorize(Roles = "GestorCoWorking,AdminHeeelp")]
        [Route("api/integration/AddCoworkerEmployee")]
        public HttpResponseMessage AddCoworkerEmployee(NewEmployeeDTO person)
        {

            Claims claims = new Claims().Values();
            Guid personIntegrationCode = Guid.NewGuid();

            try
            {
                if (ModelState.IsValid)
                {
                    var command = new AddPersonEmployeeCommand();
                    var employeeId = Guid.NewGuid();
                    command.IntegrationCode = employeeId;
                    command.Name = person.Name;
                    command.PhoneNumber = person.SmartPhoneNumber;
                    command.CreatedBy = claims.userSystemId;
                    command.Email = person.Email;
                    command.SecundaryEmail = person.Email;
                    command.EnrollmentIP = HttpContext.Current.Request.UserHostAddress;
                    command.PersonIntegrationFatherId = person.CompanyIntegrationCode; // todo: eliminar o campo CompanyIntegrationCode e usar apenas o IntegrationCode
                    command.UserProfileId = (GeneralEnumerators.EnumUserProfile)person.UserProfileId;
                    command.PersonOriginType = GeneralEnumerators.EnumPersonOriginType.IntegracaoAPIExterna;
                    command.SkinId = 3;                    
                    this.bus.Send(command);
                    
                }

                return Request.CreateResponse(HttpStatusCode.OK, personIntegrationCode);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add Empresa Coworker", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }


        }

        #endregion


        #region Select
        /// <summary>
        /// Get Colaboradores da Empresa
        /// </summary>
        /// <remarks>Get Colaboradores da Empresa</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize(Roles = "GestorCoWorking,AdminHeeelp")]
        [Route("api/integration/GetEmployes")]
        public HttpResponseMessage GetEmployes()
        {
            try
            {
                Claims claims = new Claims().Values();

                IEnumerable<EmployeeDTO> user = _person.ListEmployes(claims.Values().personFatherId, claims.Values().userSystemId);

                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao listar colaboradores", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Get Colaboradores da Empresa
        /// </summary>
        /// <param name="employeeId">employeeId</param>
        /// <remarks>Get Colaboradores da Empresa</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [ResponseType(typeof(Guid))]
        [Authorize(Roles = "GestorCoWorking,AdminHeeelp")]
        [Route("api/integration/GetEmployee/{employeeId}")]
        public HttpResponseMessage GetEmployee(Guid employeeId)
        {

            //executa validacao sobre tipos basicos
            if (employeeId == Guid.Empty) {
                var error = new ErrorState()
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidGuid,
                    DeveloperMessageTemplate = "employeeId inválido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O Id informado para o colaborador não é válido."
                };

                this.ThrowFormattedApiResponse(error,  "EmployeeId");
            }

            try
            {
                Claims claims = new Claims().Values();

                EmployeeDTO employee = _person.GetEmployee(claims.Values().personFatherId, employeeId);

                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar Colaborador", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Get Empresa coworker
        /// </summary>
        /// <param name="CompanyId">CompanyId</param>
        /// <remarks>Get Membros Empresa coworker</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [HttpPost]
        [ResponseType(typeof(Guid))]
        [Authorize(Roles = "GestorCoWorking,AdminHeeelp")]
        [Route("api/integration/GetCompanyCoWorker/{CompanyId}")]
        public HttpResponseMessage GetCompanyCoWorker(Guid CompanyId)
        {

            //executa validacao sobre tipos basicos
            if (CompanyId == Guid.Empty)
            {
                var error = new ErrorState()
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidGuid,
                    DeveloperMessageTemplate = "CompanyId inválido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O Id informado para o Coworker não é válido."
                };

                this.ThrowFormattedApiResponse(error, "CompanyId");
            }

            try
            {
                Claims claims = new Claims().Values();
                
                CompanyCoworkerDTO coworker = _person.GetCompanyCoworker(CompanyId, claims.Values().personFatherId);

                return Request.CreateResponse(HttpStatusCode.OK, coworker);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar empresa coworker", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Lista Empresa coworkers
        /// </summary>
        /// <remarks>Lista Empresa coworkers</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [HttpPost]
        [Authorize(Roles = "GestorCoWorking,AdminHeeelp")]
        [Route("api/integration/ListCompanyCoWorker")]
        public HttpResponseMessage ListCompanyCoWorker()
        {
            try
            {
                Claims claims = new Claims().Values();

                IEnumerable<CompanyCoworkerDTO> coworker = _person.ListCompanyCoworkers(claims.Values().personFatherId);

                return Request.CreateResponse(HttpStatusCode.OK, coworker);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao listar empresas coworkers", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        #endregion


        #region Edit

        /// <summary>
        /// Editar colaborador
        /// </summary>
        /// <param name="employee">employee</param>
        /// <remarks>Editar colaborador</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(EmployeeDTO))]
        [Authorize(Roles = "GestorCoWorking,AdminHeeelp")]
        [Route("api/integration/EditEmployee")]
        public HttpResponseMessage EditEmployee(EmployeeDTO employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Claims claims = new Claims().Values();

                    var commandUpdateUser = new UpdateUserCommand
                    {
                        UserIntegrationCode = employee.EmployeeId,
                        Name = employee.Name,
                        SmartPhoneNumber = employee.SmartPhoneNumber,
                        Email = employee.Email,
                        UserProfileId = byte.Parse(employee.UserProfileId),
                        UserStatusId = byte.Parse(employee.UserStatusId),
                        UpdatedBy = claims.Values().userSystemId
                    };

                    bus.Send(commandUpdateUser);
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
        /// Editar empresa Coworker
        /// </summary>
        /// <param name="company">company</param>
        /// <remarks>Editar empresa coworker</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(CompanyCoworkerDTO))]
        [Authorize(Roles = "GestorCoWorking,AdminHeeelp")]
        [Route("api/integration/EditCoworkerCompany")]
        public HttpResponseMessage EditCoworkerCompany(CompanyCoworkerDTO company)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Claims claims = new Claims().Values();

                    var command = new UpdatePersonCompanyCommand()
                    {
                        PersonIntegrationId = company.CompanyId.Value,
                        FantasyName = company.FantasyName,
                        FriendlyNameURL = company.FriendlyNameURL,
                        Name = company.ManagerName,
                        PersonalWebSite = company.PersonalWebSite,
                        PersonStatusId = company.PersonStatusId,
                        PhoneNumber = company.ManagerSmartPhoneNumber,
                        UpdatedBy = claims.Values().userSystemId
                    };

                    bus.Send(command);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao Add UserAdd", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        #endregion


        #region Delete



        /// <summary>
        /// Deleta usuario associado
        /// </summary>
        /// <param name="userid">userid</param>
        /// <remarks>Deleta uma lista de ususarios associados</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpDelete]
        [ResponseType(typeof(int))]
        [Authorize(Roles = "GestorCoWorking,AdminHeeelp")]
        [Route("api/integration/DeleteEmployee/{employeeId}")]
        public HttpResponseMessage DeleteEmployee(List<Guid> employeeId)
        {

            //executa validacao sobre tipos basicos
            if (employeeId == null)
            {
                var error = new ErrorState()
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidGuid,
                    DeveloperMessageTemplate = "employeeId inválido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O Id informado para o colaborador não é válido."
                };

                this.ThrowFormattedApiResponse(error, "EmployeeId");
            }

            try
            {
                Claims claims = new Claims().Values();

                var command = new InactiveEmployeeCommand();
                command.EmployeeListId = employeeId;
                command.DeletedBy = claims.Values().userSystemId;
                this.bus.Send(command);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar Colaborador", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        /// <summary>
        /// Deleta Empresa
        /// </summary>
        /// <param name="company">Guid PersonIntegrationID</param>
        /// <remarks>Deleta Empresa</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [ResponseType(typeof(Guid))]
        [Authorize(Roles = "GestorCoWorking,AdminHeeelp")]
        [Route("api/integration/DeleteCompany/{companyId}")]
        public HttpResponseMessage DeleteCompany(DeleteCompanyDTO deleteCompaniesDTO)
        {

            //executa validacao sobre tipos basicos
            if (deleteCompaniesDTO == null )
            {
                var error = new ErrorState()
                {
                    ErrorCode = GeneralEnumerators.enumValidationErrorCode.InvalidGuid,
                    DeveloperMessageTemplate = "companyId inválido",
                    DocumentationPath = "/Usernames",
                    UserMessage = "O Id informado para a empresa não é válido."
                };

                this.ThrowFormattedApiResponse(error, "EmployeeId");
            }

            try
            {
                Claims claims = new Claims().Values();

                var command = new InactivePersonCoWorkerCommand();
                command.CompanyListId = deleteCompaniesDTO.CompanyListId;
                command.DeletedBy = claims.Values().userSystemId;
                this.bus.Send(command);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                LogManager.Error("Erro ao consultar Colaborador", e);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }


        }


        #endregion

    }

}
