using Heeelp.Core.Domain.ReadModel.DTO;
using System;
using System.Collections.Generic;
using static Heeelp.Core.Common.GeneralEnumerators;

namespace Heeelp.Core.Domain.ReadModel.Interfaces
{
    public interface IPersonDao :IReadBase<Person>
    {

        IEnumerable<PersonListDTO> ListPersons();

        IEnumerable<PersonListDTO> ListPersons(int personId);

        #region Integracao Coworker
        CompanyCoworkerDTO GetCompanyCoworker(Guid companyIntegrationid, int personId);

        IEnumerable<CompanyDTO> ListCompanies(int companyFatherId);

        IEnumerable<CompanyCoworkerOutDTO> ListCompanyCoworkers(int companyId);
        #endregion
        PersonDetailsDTO GetPersonDetails(int id);

        CompanyDTO GetCompany(int companyId);

        Person GetCompanyByEmployeePersonId(int personId);

        PersonClassifiedDTO GetPersonClassified(int id);

        //todo:deveria retornar um objeto List mesmo? consultando a pessoa pelo codigo de integracao em tese so deveria retornar 1 resultado
        PersonListDTO GetByPersonIntegrationId(Guid PersonIntegrationId);

        PersonDTO GetByIntegrationCode(Guid IntegrationCode);

        List<EnumProfileClaims> GetPersonProfile(int UserId);

        IEnumerable<PersonListDTO> ListPersonsNotAdrress();

        IEnumerable<PersonListDTO> ListPersonsPromotion();

        IEnumerable<EmployeeDTO> ListEmployes(int personId, int userId);

        EmployeeDTO GetEmployeeByUserIntegrationCode(int personId, Guid UserIntegrationCode);

        EmployeeDTO GetEmployeeByUserIntegrationCode( Guid UserIntegrationCode);

        EmployeeDTO GetEmployeeByPersonIntegrationCode(int personId, Guid UserIntegrationCode);

        Person GetByUserIntegrationCode(Guid UserIntegrationCode);

        Person GetByPersonId(int personId);

        PersonDTO GetPerson(int id);

        PersonDTO GetPersonByEmail(string email);
        Domain.PersonBenefitClub GetBenefitClubInfo(int personBenefitClubId);
        bool ValidateTokenForgotPassword(Guid integrationCodde);

        int GetAmountCostumersNearby(Guid providerPersonIntegrationCodeId, int ExpertiseId);

        CompanyDetailsDTO GetCompany(Guid integrationCode);
        PersonDTO GetPersonInviteCode(string inviteCode);

        void UpdateInviteAvailable(int personId);
    }
}
