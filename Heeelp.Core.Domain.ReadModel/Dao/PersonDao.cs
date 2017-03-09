using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heeelp.Core.Common;
using static Heeelp.Core.Common.GeneralEnumerators;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Data.Entity;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class PersonDao : IPersonDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public PersonDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public Person Get(Person t)
        {
            throw new NotImplementedException();
        }

        public Person GetByPersonId(int personId)
        {
            var _context = _contextFactory();
            var ret = (from p in _context.Person
                       join pbc in _context.PersonBenefitClub on p.PersonBenefitClubId equals pbc.PersonBenefitClubId
                       where p.PersonId == personId
                       select new { p, pbc }).FirstOrDefault();

            var person = new Person();
            if (ret != null)
            {
                person = ret.p;
                person.PersonBenefitClub = ret.pbc;
            }
            return person;
        }
        public PersonDTO GetPerson(int id)
        {
            var _context = _contextFactory();
            var ret = (from p in _context.Person
                       join pbc in _context.PersonBenefitClub on p.PersonBenefitClubId equals pbc.PersonBenefitClubId
                       where p.PersonId == id
                       select new { p, pbc }).FirstOrDefault();
            if (ret != null)
            {
                return new PersonDTO()
                {
                    PersonId = ret.p.PersonId,
                    ActivationDateUTC = ret.p.ActivationDateUTC,
                    Name = ret.p.Name,
                    FantasyName = ret.p.FantasyName,
                    PhoneNumber = ret.p.PhoneNumber,
                    FriendlyNameURL = ret.p.FriendlyNameURL,
                    PersonalWebSite = ret.p.PersonalWebSite,
                    PersonStatusId = ret.p.PersonStatusId,
                    PersonFatherId = ret.p.PersonFatherId,
                    CustomClubName = ret.pbc.CustomClubName,
                    CustomClubLogo = ret.pbc.CustomClubLogo,
                    CustomHeeelpPersonDomain = ret.pbc.CustomHeeelpPersonDomain,
                    SkinId = ret.p.SkinId,
                    PersonTypeId = ret.p.PersonTypeId,
                    PersonRules = ret.p.PersonRules,
                    Active = ret.p.Active,
                    CountryId = ret.p.CountryId,
                    CreationDateUTC = ret.p.CreationDateUTC,
                    CurrencyId = ret.p.CurrencyId,
                    Description = ret.p.Description,
                    IntegrationCode = ret.p.IntegrationCode,
                    InviteId = ret.p.InviteId,
                    LanguageId = ret.p.LanguageId,
                    PersonContract = ret.p.PersonContract,
                    PersonOriginTypeId = ret.p.PersonOriginTypeId,
                    PersonType = ret.p.PersonType,
                    PersonStatus = ret.p.PersonStatus,
                    UrlOrigin = ret.p.UrlOrigin,
                    PersonFile = ret.p.PersonFile
                };
            }
            return null;

        }

        public PersonDTO GetPersonByEmail(string email)
        {

            var _context = _contextFactory();

            var ret = from p in _context.Person
                      join pbc in _context.PersonBenefitClub on p.PersonBenefitClubId equals pbc.PersonBenefitClubId
                      join u in _context.User on p.PersonId equals u.PersonId
                      where u.Email == email && p.PersonTypeId == (int)GeneralEnumerators.EnumPersonType.Natural_Person
                      select new PersonDTO()
                      {
                          PersonId = p.PersonId,
                          ActivationDateUTC = p.ActivationDateUTC,
                          Name = p.Name,
                          FantasyName = p.FantasyName,
                          PhoneNumber = p.PhoneNumber,
                          FriendlyNameURL = p.FriendlyNameURL,
                          PersonalWebSite = p.PersonalWebSite,
                          PersonStatusId = p.PersonStatusId,
                          PersonFatherId = p.PersonFatherId,
                          CustomClubName = pbc.CustomClubName,
                          CustomClubLogo = pbc.CustomClubLogo,
                          CustomHeeelpPersonDomain = pbc.CustomHeeelpPersonDomain,
                          SkinId = p.SkinId,
                          PersonTypeId = p.PersonTypeId,
                          PersonRules = p.PersonRules,
                          Active = p.Active,
                          CountryId = p.CountryId,
                          CreationDateUTC = p.CreationDateUTC,
                          CurrencyId = p.CurrencyId,
                          Description = p.Description,
                          IntegrationCode = p.IntegrationCode,
                          InviteId = p.InviteId,
                          LanguageId = p.LanguageId,
                          PersonContract = p.PersonContract,
                          PersonOriginTypeId = p.PersonOriginTypeId,
                          PersonType = p.PersonType,
                          PersonStatus = p.PersonStatus,
                          UrlOrigin = p.UrlOrigin,
                          PersonFile = p.PersonFile
                      };
            return ret.FirstOrDefault();


        }

        public PersonDTO GetPersonInviteCode(string inviteCode)
        {

            var _context = _contextFactory();

            var ret = from p in _context.Person
                      join pbc in _context.PersonBenefitClub on p.PersonBenefitClubId equals pbc.PersonBenefitClubId
                      join u in _context.User on p.PersonId equals u.PersonId
                      where p.InviteCode == inviteCode 
                      select new PersonDTO()
                      {
                          PersonId = p.PersonId,
                          ActivationDateUTC = p.ActivationDateUTC,
                          Name = p.Name,
                          FantasyName = p.FantasyName,
                          PhoneNumber = p.PhoneNumber,
                          FriendlyNameURL = p.FriendlyNameURL,
                          PersonalWebSite = p.PersonalWebSite,
                          PersonStatusId = p.PersonStatusId,
                          PersonFatherId = p.PersonFatherId,
                          CustomClubName = pbc.CustomClubName,
                          CustomClubLogo = pbc.CustomClubLogo,
                          CustomHeeelpPersonDomain = pbc.CustomHeeelpPersonDomain,
                          SkinId = p.SkinId,
                          PersonTypeId = p.PersonTypeId,
                          PersonRules = p.PersonRules,
                          Active = p.Active,
                          CountryId = p.CountryId,
                          CreationDateUTC = p.CreationDateUTC,
                          CurrencyId = p.CurrencyId,
                          Description = p.Description,
                          IntegrationCode = p.IntegrationCode,
                          InviteId = p.InviteId,
                          LanguageId = p.LanguageId,
                          PersonContract = p.PersonContract,
                          PersonOriginTypeId = p.PersonOriginTypeId,
                          PersonType = p.PersonType,
                          PersonStatus = p.PersonStatus,
                          UrlOrigin = p.UrlOrigin,
                          PersonFile = p.PersonFile,
                          InviteAvailable = p.InviteAvailable,
                      };

           
                return ret.FirstOrDefault();
           

        }

        public void UpdateInviteAvailable(int personId)
        {
            var _context = _contextFactory();
            var p = _context.Person.Where(x => x.PersonId == personId).FirstOrDefault();
            p.InviteAvailable = p.InviteAvailable - 1;

            _context.Person.Attach(p);
            _context.Entry(p).State = EntityState.Modified;
            _context.SaveChanges();


        }

        public IEnumerable<Person> List()
        {
            var _context = _contextFactory();
            return _context.Person.ToList();
        }
        public IEnumerable<PersonListDTO> ListPersons()
        {
            var _context = _contextFactory();
            return _context.Person.Select(x => new PersonListDTO() { PersonId = x.PersonId, Name = x.Name, IntegrationCode = x.IntegrationCode });
        }
        public IEnumerable<PersonListDTO> ListPersons(int personId)
        {
            var _context = _contextFactory();
            return _context.Person.Where(x => x.PersonFatherId == personId).Select(x => new PersonListDTO() { PersonId = x.PersonId, Name = x.Name, IntegrationCode = x.IntegrationCode });
        }


        public EmployeeDTO GetEmployeeByUserIntegrationCode(Guid UserIntegrationCode)
        {

            var _context = _contextFactory();
            var ret = from u in _context.User
                      join us in _context.UserStatus on u.UserStatusId equals us.UserStatusId
                      join up in _context.UserProfile on u.UserProfileId equals up.UserProfileId
                      where u.IntegrationCode == UserIntegrationCode
                      select (new EmployeeDTO()
                      {
                          EmployeeId = u.IntegrationCode,
                          Name = u.Name,
                          Email = u.Email,
                          SecundaryEmail = u.SecundaryEmail,
                          SmartPhoneNumber = u.SmartPhoneNumber,
                          UserProfileId = (GeneralEnumerators.EnumUserProfile)u.UserProfileId,
                          UserStatusId = (GeneralEnumerators.EnumUserStatus)u.UserStatusId,
                          UserStatusName = us.Name,
                          UserProfileName = up.Name,
                      });
            return ret.FirstOrDefault();
        }
        public EmployeeDTO GetEmployeeByUserIntegrationCode(int personId, Guid UserIntegrationCode)
        {

            var _context = _contextFactory();
            var ret = from u in _context.User
                      join p in _context.Person on u.PersonId equals p.PersonFatherId
                      join us in _context.UserStatus on u.UserStatusId equals us.UserStatusId
                      join up in _context.UserProfile on u.UserProfileId equals up.UserProfileId
                      where u.IntegrationCode == UserIntegrationCode && p.PersonFatherId == personId
                      select (new EmployeeDTO()
                      {
                          EmployeeId = u.IntegrationCode,
                          Name = u.Name,
                          Email = u.Email,
                          SmartPhoneNumber = u.SmartPhoneNumber,
                          UserProfileId = (GeneralEnumerators.EnumUserProfile)u.UserProfileId,
                          UserStatusId = (GeneralEnumerators.EnumUserStatus)u.UserStatusId,
                          UserStatusName = us.Name,
                          UserProfileName = up.Name,
                      });
            return ret.FirstOrDefault();
        }


        public EmployeeDTO GetEmployeeByPersonIntegrationCode(int personId, Guid PersonIntegrationCode)
        {

            var _context = _contextFactory();
            var ret = from u in _context.User
                      join p in _context.Person on u.PersonId equals p.PersonFatherId
                      join us in _context.UserStatus on u.UserStatusId equals us.UserStatusId
                      join up in _context.UserProfile on u.UserProfileId equals up.UserProfileId
                      where p.IntegrationCode == PersonIntegrationCode && p.PersonFatherId == personId
                      select (new EmployeeDTO()
                      {
                          EmployeeId = u.IntegrationCode,
                          Name = u.Name,
                          Email = u.Email,
                          SmartPhoneNumber = u.SmartPhoneNumber,
                          UserProfileId = (GeneralEnumerators.EnumUserProfile)u.UserProfileId,
                          UserStatusId = (GeneralEnumerators.EnumUserStatus)u.UserStatusId,
                          UserStatusName = us.Name,
                          UserProfileName = up.Name,
                      });
            return ret.FirstOrDefault();
        }


        public CompanyDTO GetCompany(int companyId)
        {
            var _context = _contextFactory();
            var ret = from p in _context.Person
                      join pbc in _context.PersonBenefitClub on p.PersonBenefitClubId equals pbc.PersonBenefitClubId
                      where p.PersonId == companyId
                      select new CompanyDTO()
                      {
                          PersonId = p.PersonId,
                          Name = p.Name,
                          FantasyName = p.FantasyName,
                          PhoneNumber = p.PhoneNumber,
                          FriendlyNameURL = p.FriendlyNameURL,
                          PersonalWebSite = p.PersonalWebSite,
                          PersonStatusId = p.PersonStatusId,
                          CompanyIntegrationCode = p.IntegrationCode,
                          PersonFatherId = p.PersonFatherId,
                          CustomClubName = pbc.CustomClubName,
                          CustomClubLogo = pbc.CustomClubLogo,
                          CustomHeeelpPersonDomain = pbc.CustomHeeelpPersonDomain,
                          SkinId = p.SkinId,
                          PersonTypeId = p.PersonTypeId,
                          Rules = p.PersonRules
                      };

            return ret.FirstOrDefault();

        }

        public CompanyDetailsDTO GetCompany(Guid integrationCode)
        {
            var _context = _contextFactory();
            var ret = from p in _context.Person
                      join pbc in _context.PersonBenefitClub on p.PersonBenefitClubId equals pbc.PersonBenefitClubId
                      join pa in _context.PersonAddress on p.PersonId equals pa.PersonId into g
                      from result in g.DefaultIfEmpty()
                      where p.IntegrationCode == integrationCode
                      select new CompanyDetailsDTO()
                      {
                          PersonId = p.PersonId,
                          Name = p.Name,
                          FantasyName = p.FantasyName,
                          PhoneNumber = p.PhoneNumber,
                          FriendlyNameURL = p.FriendlyNameURL,
                          PersonalWebSite = p.PersonalWebSite,
                          PersonStatusId = p.PersonStatusId,
                          CompanyIntegrationCode = p.IntegrationCode,
                          PersonFatherId = p.PersonFatherId,
                          CustomClubName = pbc.CustomClubName,
                          CustomClubLogo = pbc.CustomClubLogo,
                          CustomHeeelpPersonDomain = pbc.CustomHeeelpPersonDomain,
                          SkinId = p.SkinId,
                          PersonTypeId = p.PersonTypeId,
                          Rules = p.PersonRules,
                          UrlImageLogo = p.UrlImageLogo,
                          StreetName = result.StreetName,
                          Number = result.Number.ToString(),
                          Complement = result.Complement,
                          Neighbourhood = result.Neighbourhood,
                          PostCode = result.PostCode,
                          ContactPhoneNumber = result.ContactPhoneNumber,
                          ContactEMail = result.ContactEMail,
                          State = result.State,
                          City = result.City
                      };

            return ret.FirstOrDefault();

        }

        public ReadModel.Person GetCompanyByEmployeePersonId(int personId)
        {
            var _context = _contextFactory();
            var ret = from p in _context.Person
                      where p.PersonFatherId == personId
                      select p;

            return ret.FirstOrDefault();

        }

        public IEnumerable<CompanyDTO> ListCompanies(int companyFatherId)
        {
            var _context = _contextFactory();
            IEnumerable<CompanyDTO> ret;

            // validar se o usuario pode acessar a lista de empresas
            if ((int)GeneralEnumerators.EnumUserDefault.Heeelp == companyFatherId)
            {
                ret = from p in _context.Person
                      join ps in _context.PersonStatus on p.PersonStatusId equals ps.PersonStatusId
                      join pt in _context.PersonType on p.PersonTypeId equals pt.PersonTypeId
                      where p.PersonTypeId == (int)GeneralEnumerators.EnumPersonType.Legal_Person
                      && p.Active
                      &&
                      (p.PersonStatusId == (int)GeneralEnumerators.EnumPersonStatus.AguardandoAnálise ||
                      p.PersonStatusId == (int)GeneralEnumerators.EnumPersonStatus.AguardandoAtivação ||
                      p.PersonStatusId == (int)GeneralEnumerators.EnumPersonStatus.CadastroAprovado)
                      select new CompanyDTO()
                      {
                          PersonId = p.PersonId,
                          Name = p.Name,
                          FantasyName = p.FantasyName,
                          PhoneNumber = p.PhoneNumber,
                          FriendlyNameURL = p.FriendlyNameURL,
                          PersonalWebSite = p.PersonalWebSite,
                          PersonStatusId = p.PersonStatusId,
                          CompanyIntegrationCode = p.IntegrationCode,
                          PersonFatherId = p.PersonFatherId,
                          Rules = p.PersonRules,
                          PersonStatusName = ps.Name,
                          PersonTypeName = pt.Name
                      };
            }
            else
            {
                ret = from p in _context.Person
                      join ps in _context.PersonStatus on p.PersonStatusId equals ps.PersonStatusId
                      join pt in _context.PersonType on p.PersonTypeId equals pt.PersonTypeId
                      where p.PersonFatherId == companyFatherId && p.PersonTypeId == (int)GeneralEnumerators.EnumPersonType.Legal_Person
                      && p.Active
                      select new CompanyDTO()
                      {
                          PersonId = p.PersonId,
                          Name = p.Name,
                          FantasyName = p.FantasyName,
                          PhoneNumber = p.PhoneNumber,
                          FriendlyNameURL = p.FriendlyNameURL,
                          PersonalWebSite = p.PersonalWebSite,
                          PersonStatusId = p.PersonStatusId,
                          CompanyIntegrationCode = p.IntegrationCode,
                          PersonFatherId = p.PersonFatherId,
                          Rules = p.PersonRules,
                          PersonStatusName = ps.Name,
                          PersonTypeName = pt.Name
                      };
            }

            return ret.ToList();

        }

        public PersonDetailsDTO GetPersonDetails(int id)
        {
            var _context = _contextFactory();

            var ret = from p in _context.Person
                      join pa in _context.PersonAddress on p.PersonId equals pa.PersonId
                      join pe in _context.PersonExpertise on p.PersonId equals pe.PersonId
                      join pf in _context.PersonFile on p.PersonId equals pf.PersonId
                      join pd in _context.PersonDocument on p.PersonId equals pd.PersonId
                      where p.PersonId == id
                      select new PersonDetailsDTO()
                      {
                          Name = p.Name,
                          Description = p.Description,
                          Address = pa.StreetName,
                          Coordinates = pa.Coordinates,
                          CustomDescription = pe.CustomDescription,
                          FantasyName = p.FantasyName,
                          FileIdLogo = pf.FileId,
                          PhoneNumber = p.PhoneNumber,
                          Document = pd.Number,
                          PostCode = pa.PostCode,
                          WebSite = p.PersonalWebSite,
                          UrlImageLogo = p.UrlImageLogo
                      };

            return ret.FirstOrDefault();

        }


        public IEnumerable<EmployeeDTO> ListEmployes(int personId, int userId)
        {
            //todo: verificar propriedades faltando
            var _context = _contextFactory();
            var userMail = _context.User.Where(x => x.UserId == userId).Select(x => x.Email).First();
            var ret = from u in _context.User
                      join up in _context.UserProfile on u.UserProfileId equals up.UserProfileId
                      join us in _context.UserStatus on u.UserStatusId equals us.UserStatusId
                      where u.PersonId == personId
                      && u.Email != userMail
                      && u.Active
                      && (u.UserStatusId == (byte)GeneralEnumerators.EnumUserStatus.Ativo ||
                      u.UserStatusId == (byte)GeneralEnumerators.EnumUserStatus.AguardandoAtivacao)
                      select new EmployeeDTO()
                      {
                          EmployeeId = u.IntegrationCode,
                          Name = u.Name,
                          Email = u.Email,
                          SecundaryEmail = u.SecundaryEmail,
                          UserProfileId = (GeneralEnumerators.EnumUserProfile)u.UserProfileId,
                          SmartPhoneNumber = u.SmartPhoneNumber,
                          UserStatusId = (GeneralEnumerators.EnumUserStatus)u.UserStatusId,
                          UserProfileName = up.Name,
                          UserStatusName = us.Name
                      };

            return ret;

        }
        public IEnumerable<PersonListDTO> ListPersonsPromotion()
        {
            var _context = _contextFactory();

            return from p in _context.Person
                   join u in _context.User on p.PersonId equals u.PersonId
                   group p by p.PersonId into p
                   select new PersonListDTO() { PersonId = p.Select(m => m.PersonId).FirstOrDefault(), Name = p.Select(m => m.Name).FirstOrDefault(), IntegrationCode = p.Select(m => m.IntegrationCode).FirstOrDefault() };

        }
        public IEnumerable<PersonListDTO> ListPersonsNotAdrress()
        {
            var _context = _contextFactory();

            return _context.Person
                .Where(x => !_context.PersonAddress.Any(y => y.PersonId == x.PersonId))
                .Select(x => new PersonListDTO() { PersonId = x.PersonId, Name = x.Name, IntegrationCode = x.IntegrationCode });

        }


        public PersonClassifiedDTO GetPersonClassified(int id)
        {
            var _context = _contextFactory();

            return _context.Person.Where(x => x.PersonId.Equals(id)).Select(y => new PersonClassifiedDTO
            {
                PersonId = y.PersonId,
                IntegrationCode = y.IntegrationCode,
                CountryId = y.CountryId,
                LanguageId = y.LanguageId,
                PersonStatusId = y.PersonStatusId,
                CurrencyId = y.CurrencyId,
                Active = y.Active,
                PersonTypeId = y.PersonTypeId
            }).FirstOrDefault();
        }

        public PersonListDTO GetByPersonIntegrationId(Guid IntegrationCode)
        {
            var _context = _contextFactory();
            var person = _context.Person.Where(x => x.IntegrationCode == IntegrationCode);
            return person.ToList().Select(x => new PersonListDTO() { PersonId = x.PersonId, Name = x.Name }).FirstOrDefault();
        }

        public int GetAmountCostumersNearby(Guid providerPersonIntegrationCodeId, int ExpertiseId)
        {

            int radiusDistanceInMetters = 0;
            int AmountCostumersNearBy = 0;

            var _clientPromotion = new HttpClient();
            _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiMarketing);
            var resultTask = _clientPromotion.GetAsync("api/PricePolicies/GetDistanceExpertise/" + ExpertiseId).Result;
            if (resultTask.IsSuccessStatusCode)
            {
                radiusDistanceInMetters = resultTask.Content.ReadAsAsync<int>().Result;
            }
            else
            {
                radiusDistanceInMetters = 1000;
            }
            var _context = _contextFactory();
            using (SqlConnection conn = new SqlConnection(_context.Database.Connection.ConnectionString))
            {
                using (SqlCommand comm = new SqlCommand("[dbo].[sp_GetAmountCostumersNearby]", conn))
                {
                    comm.Parameters.Add("@providerPersonIntegrationCode", SqlDbType.UniqueIdentifier).Value = providerPersonIntegrationCodeId;
                    comm.Parameters.Add("@RadiusDistanceInMetters", SqlDbType.Int).Value = radiusDistanceInMetters;
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    var read = comm.ExecuteReader();

                    while (read.Read())
                    {
                        if (!string.IsNullOrEmpty(read[0].ToString()))
                            AmountCostumersNearBy = int.Parse(read[0].ToString());
                    }

                }
            }
            return AmountCostumersNearBy;
        }


        public PersonDTO GetByIntegrationCode(Guid IntegrationCode)
        {
            var _context = _contextFactory();
            var ret = from p in _context.Person
                      join pbc in _context.PersonBenefitClub on p.PersonBenefitClubId equals pbc.PersonBenefitClubId
                      where p.IntegrationCode == IntegrationCode
                      select new PersonDTO()
                      {
                          PersonId = p.PersonId,
                          ActivationDateUTC = p.ActivationDateUTC,
                          Name = p.Name,
                          FantasyName = p.FantasyName,
                          PhoneNumber = p.PhoneNumber,
                          FriendlyNameURL = p.FriendlyNameURL,
                          PersonalWebSite = p.PersonalWebSite,
                          PersonStatusId = p.PersonStatusId,
                          PersonFatherId = p.PersonFatherId,
                          CustomClubName = pbc.CustomClubName,
                          CustomClubLogo = pbc.CustomClubLogo,
                          CustomHeeelpPersonDomain = pbc.CustomHeeelpPersonDomain,
                          SkinId = p.SkinId,
                          PersonTypeId = p.PersonTypeId,
                          PersonRules = p.PersonRules,
                          Active = p.Active,
                          CountryId = p.CountryId,
                          CreationDateUTC = p.CreationDateUTC,
                          CurrencyId = p.CurrencyId,
                          Description = p.Description,
                          IntegrationCode = p.IntegrationCode,
                          InviteId = p.InviteId,
                          LanguageId = p.LanguageId,
                          PersonContract = p.PersonContract,
                          PersonOriginTypeId = p.PersonOriginTypeId,
                          PersonType = p.PersonType,
                          PersonStatus = p.PersonStatus,
                          UrlOrigin = p.UrlOrigin,
                          PersonFile = p.PersonFile
                      };
            return ret.FirstOrDefault();

        }


        public Person GetByUserIntegrationCode(Guid UserIntegrationCode)
        {
            var _context = _contextFactory();
            var person = (from p in _context.Person
                          join u in _context.User on p.PersonId equals u.PersonId
                          where u.IntegrationCode == UserIntegrationCode
                          select p);
            return person.FirstOrDefault();

        }

        public Domain.PersonBenefitClub GetBenefitClubInfo(int personBenefitClubId)
        {
            var _context = _contextFactory();
            var personBenefitClub = (from pbc in _context.PersonBenefitClub
                                     where pbc.PersonBenefitClubId == personBenefitClubId
                                     select new Domain.PersonBenefitClub()
                                     {
                                         Active = pbc.Active,
                                         CreatedBy = pbc.CreatedBy,
                                         CustomClubLogo = pbc.CustomClubLogo,
                                         CustomClubName = pbc.CustomClubName,
                                         CustomHeeelpPersonDomain = pbc.CustomHeeelpPersonDomain,
                                         PersonBenefitClubId = pbc.PersonBenefitClubId,
                                         PersonId = pbc.PersonId
                                     });
            return personBenefitClub.FirstOrDefault();

        }



        public bool ValidateTokenForgotPassword(Guid integrationCodde)
        {
            var _context = _contextFactory();

            var personToActivate = from p in _context.Person
                                   where p.IntegrationCode.Equals(integrationCodde)
                                   select new { p.PersonId };

            return personToActivate.Count() > 0;
        }

        #region Integracao Coworker
        public CompanyCoworkerDTO GetCompanyCoworker(Guid companyIntegrationid, int personId)
        {
            var _context = _contextFactory();
            var ret = from p in _context.Person
                      join s in _context.PersonStatus on p.PersonStatusId equals s.PersonStatusId
                      where p.IntegrationCode == companyIntegrationid
                      && p.PersonFatherId == personId
                      select new CompanyCoworkerDTO()
                      {
                          CompanyId = p.IntegrationCode,
                          CompanyName = p.Name,
                          PersonStatusName = s.Name,
                          FantasyName = p.FantasyName,
                          CompanyPhoneNumber = p.PhoneNumber,
                          FriendlyNameURL = p.FriendlyNameURL,
                          PersonalWebSite = p.PersonalWebSite,
                          PersonStatusId = p.PersonStatusId
                      };

            return ret.FirstOrDefault();

        }

        public IEnumerable<CompanyCoworkerOutDTO> ListCompanyCoworkers(int companyId)
        {
            var _context = _contextFactory();

            var ret = from p in _context.Person
                      join ps in _context.PersonStatus on p.PersonStatusId equals ps.PersonStatusId
                      where p.PersonFatherId == companyId
                      && p.Active
                      select new CompanyCoworkerOutDTO()
                      {
                          CompanyId = p.IntegrationCode,
                          CompanyName = p.Name,
                          FantasyName = p.FantasyName,
                          CompanyPhoneNumber = p.PhoneNumber,
                          FriendlyNameURL = p.FriendlyNameURL,
                          PersonalWebSite = p.PersonalWebSite,
                          PersonStatusId = p.PersonStatusId,
                          PersonStatusName = ps.Name
                      };

            return ret.ToList();

        }


        #endregion
        public List<EnumProfileClaims> GetPersonProfile(int userId)
        {
            var _context = _contextFactory();
            var userPerson = (from u in _context.User
                              join p in _context.Person on u.PersonId equals p.PersonId
                              where u.UserId.Equals(userId)
                              && u.Active && (u.UserStatusId == (byte)GeneralEnumerators.EnumUserStatus.Ativo)
                              select new { u.UserProfileId, p.PersonRules }).ToList();



            if (userPerson.Count == 0)
                return null;

            var auth = userPerson.Select(x => new Authentication
            {
                PersonRules = x.PersonRules,
                UserProfileId = x.UserProfileId

            }).ToList();

            var ret = new List<EnumProfileClaims>();

            auth.ForEach(x => ret = CustomProfile.ListProfilesClaims(x.UserProfileId, x.PersonRules.Select(y => y.PersonProfileId).ToList()));
            return ret;
        }

    }
}
