using Heeelp.Core.Domain.ReadModel.
    DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class PersonAddressDao : IPersonAddressDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public PersonAddressDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public PersonAddress Get(PersonAddress t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonAddress> List()
        {
            var _context = _contextFactory();
            return _context.PersonAddress.ToList();
        }
        public PersonAddressClassifiedDTO GetPersonAddressClassified(int id)
        {
            var _context = _contextFactory();
            try
            {

                //var ret = (from p in _context.Person
                //        join pa in _context.PersonAddress on p.PersonId equals pa.PersonId
                //        join n in _context.Neighbourhood on pa.NeighbourhoodId equals n.NeighbourhoodId
                //        join city in _context.City on n.CityId equals city.CityId
                //        join sr in _context.StateRegion on city.StateRegionId equals sr.StateRegionId
                //        join s in _context.State on sr.StateId equals s.StateId
                //        join cr in _context.CountryRegion on s.CountryRegionId equals cr.CountryRegionId
                //        join c in _context.Country on cr.CountryId equals c.CountryId
                //        join pf in _context.PersonFile on p.PersonId equals pf.PersonId into file
                //        from f in file.DefaultIfEmpty()
                //        where p.PersonId == id
                var ret = (from pa in _context.PersonAddress
                           join p in _context.Person on pa.PersonId equals p.PersonId
                           join pf in _context.PersonFile on p.PersonId equals pf.PersonId
                           join pd in _context.PersonDocument on p.PersonId equals pd.PersonId into 
                           tempPd from lpd in tempPd.DefaultIfEmpty()
                           where p.PersonId.Equals(id)
                           select new PersonAddressClassifiedDTO()
                           {

                               PersonAddressId = pa.PersonAddressId,
                               FantasyName = p.FantasyName,
                               //CountryId = c.CountryId,
                               CountryName = pa.Country,
                               PersonId = p.PersonId,
                               //StateId = s.StateId,
                               StateName = pa.State,
                               //CityId = city.CityId,
                               CityName = pa.City,
                               //NeighbourhoodId = n.NeighbourhoodId,
                               PersonDocumentNumber = lpd.Number,
                               PhoneNumber = p.PhoneNumber,
                               NeighbourhoodName = pa.Neighbourhood,
                               Coordinates = pa.Coordinates,
                               PersonLogo = pf.FileId,
                               Address = pa.StreetName,
                               UrlImageLogo = p.UrlImageLogo                               
                           }).FirstOrDefault();
                return ret;
            }
            catch (Exception ex)
            {

                throw;
            }



        }

        public int GetPersonNeighbourhoodId(Guid PersonIntegrationCode)
        {
            var _context = _contextFactory();
            var PostCode = (from p in _context.Person
                            join pa in _context.PersonAddress on p.PersonId equals pa.PersonId
                            where p.IntegrationCode == PersonIntegrationCode
                            select pa.PostCode).FirstOrDefault();
            if (PostCode !=null)
            {
                return  (from n in _context.Neighbourhood where n.PostCode == PostCode select n.NeighbourhoodId).FirstOrDefault();                
            }
            return 0;
        }
    }
}
