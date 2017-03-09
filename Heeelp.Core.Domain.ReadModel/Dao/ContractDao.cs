using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heeelp.Core.Common;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class ContractDao : IContractDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public ContractDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public Contract Get(Contract t)
        {
            var _context = _contextFactory();
            return _context.Contract.Where(x => x.ContractId == t.ContractId).First();
        }


        public IEnumerable<Contract> List()
        {
            var _context = _contextFactory();
            return _context.Contract.ToList();
        }

        public Contract GetLastestVersionContract(DomainEnumerators.enumContractType type)
        {
            var _context = _contextFactory();
            var ret = from c in _context.Contract
                      where c.ContractTypeId == (byte)type
                      orderby c.StartDate descending
                      select c;

            return ret.FirstOrDefault();

        }

        public IEnumerable<Contract> GetUserContracts(int userId)
        {
            var _context = _contextFactory();
            var ret = from u in _context.User
                      join p in _context.Person on u.PersonId equals p.PersonId
                      join pc in _context.PersonContract on p.PersonId equals pc.PersonId
                      join c in _context.Contract on pc.ContractId equals c.ContractId
                      where u.UserId == userId && pc.Active == true
                      orderby c.StartDate descending
                      select c;

            return ret;

        }

        public IEnumerable<Contract> GetPersonContracts(Guid PersonItegrationCode)
        {
            var _context = _contextFactory();
            var ret = from u in _context.User
                      join p in _context.Person on u.PersonId equals p.PersonId
                      join pc in _context.PersonContract on p.PersonId equals pc.PersonId
                      join c in _context.Contract on pc.ContractId equals c.ContractId
                      where p.IntegrationCode == PersonItegrationCode && pc.Active == true
                      orderby c.StartDate descending
                      select c;

            return ret;

        }

        public IEnumerable<ContractListDTO> ListUserContracts(Guid UserIntegrationCode)
        {

            List<ContractListDTO> contracts = new List<ContractListDTO>();
            List<Contract> companyTerms = new List<Contract>() ;
                                                                                                            

            contracts.Add(new ContractListDTO(DomainEnumerators.GetEnumDescription( DomainEnumerators.enumContractType.PoliticaDePrivacidade), GetLastestVersionContract(DomainEnumerators.enumContractType.PoliticaDePrivacidade).FileId));
                                                                                                       
            contracts.Add(new ContractListDTO(DomainEnumerators.GetEnumDescription(DomainEnumerators.enumContractType.TermosDeUsoColaborador), GetLastestVersionContract(DomainEnumerators.enumContractType.TermosDeUsoColaborador).FileId)); 

            var _context = _contextFactory();
            var userPerson = (from u in _context.User
                              join p in _context.Person on u.PersonId equals p.PersonId
                              where u.IntegrationCode == UserIntegrationCode && p.Active == true
                              select   p ).First();

            if (userPerson != null)
            {
                var personRules = (from pr in _context.PersonRules
                                   where pr.PersonId == userPerson.PersonId && pr.Active == true
                                   select pr).ToList();



                foreach (var rule in personRules)
                {
                    switch ((Domain.DomainEnumerators.enumPersonProfile)rule.PersonProfileId)
                    {
                        case Domain.DomainEnumerators.enumPersonProfile.ServiceProvider:
                            contracts.Add(new ContractListDTO(DomainEnumerators.GetEnumDescription(DomainEnumerators.enumContractType.TermosDeUsoPrestadorDeServico ), GetLastestVersionContract(Domain.DomainEnumerators.enumContractType.TermosDeUsoPrestadorDeServico).FileId));
                             
                            break;
                        case Domain.DomainEnumerators.enumPersonProfile.CompanyPartner:
                            contracts.Add(new ContractListDTO(DomainEnumerators.GetEnumDescription(DomainEnumerators.enumContractType.TermosDeUsoEmpresaParceiraRH ), GetLastestVersionContract(Domain.DomainEnumerators.enumContractType.TermosDeUsoEmpresaParceiraRH).FileId));    
                            break;
                        case Domain.DomainEnumerators.enumPersonProfile.Coworking:
                            contracts.Add(new ContractListDTO(DomainEnumerators.GetEnumDescription(DomainEnumerators.enumContractType.TermosDeUsoCoworking ), GetLastestVersionContract(Domain.DomainEnumerators.enumContractType.TermosDeUsoCoworking).FileId));
                            break;
                        case Domain.DomainEnumerators.enumPersonProfile.EducationalCenter:
                            contracts.Add(new ContractListDTO(DomainEnumerators.GetEnumDescription(DomainEnumerators.enumContractType.TermosDeUsoCentroEducacaional ), GetLastestVersionContract(Domain.DomainEnumerators.enumContractType.TermosDeUsoCentroEducacaional).FileId));
                             break;
                        case Domain.DomainEnumerators.enumPersonProfile.Condominium:
                            contracts.Add(new ContractListDTO(DomainEnumerators.GetEnumDescription(DomainEnumerators.enumContractType.TermosDeUsoCondominio ), GetLastestVersionContract(Domain.DomainEnumerators.enumContractType.TermosDeUsoCondominio).FileId)); 
                            break;
                    }
                }
            }
                   
            return contracts;
        }
    }
}
