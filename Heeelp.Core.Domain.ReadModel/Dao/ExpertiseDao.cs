using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class ExpertiseDao : IExpertiseDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public ExpertiseDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public Expertise Get(Expertise t)
        {
            throw new NotImplementedException();
        }
        public ExpertiseDTO Get(int id)
        {
            var _context = _contextFactory();
            return _context.Expertise.Where(x => x.ExpertiseId == id).Select(x =>
                new ExpertiseDTO()
                {
                    ExpertiseId = x.ExpertiseId,
                    Name = x.Name,
                    Active = x.Active,
                    ExpertiseFatherId = x.ExpertiseFatherId,
                    CreatedBy = x.CreatedBy,
                    CreatedDateUTC = x.CreatedDateUTC,
                    ApprovalStatusId = x.ApprovalStatusId,
                    ApprovedBy = x.ApprovedBy,
                    ApprovedDate = x.ApprovedDate,
                    DefaultDescription = x.DefaultDescription,
                    IsPriceDefinedEditorially = x.IsPriceDefinedEditorially
                }).First();
        }

        public ExpertisePhotoDTO GetExpetisePhoto(int id)
        {
            var _context = _contextFactory();
            var query = from e in _context.Expertise
                        join ep in _context.ExpertisePhoto on e.ExpertiseId equals ep.ExpertiseId
                        where ep.ExpertiseId == id
                        select new { e.ExpertiseId, e.Name, e.DefaultDescription, e.ExpertiseFatherId, ep.FileId };
            if (query.Count() > 0)
            {
                var expertise = query.First();
                ExpertisePhotoDTO expertPhoto = new ExpertisePhotoDTO();
                expertPhoto.ExpertiseId = expertise.ExpertiseId;
                expertPhoto.Name = expertise.Name;
                expertPhoto.Description = expertise.DefaultDescription;
                expertPhoto.ExpertiseFatherId = expertise.ExpertiseFatherId;
                expertPhoto.ImageListId = query.ToList().Select(x => x.FileId).ToList();
                return expertPhoto;
            }
            else
                return null;

        }
        public IEnumerable<Expertise> List()
        {
            var _context = _contextFactory();
            return _context.Expertise.Where(y => y.Active).ToList();
        }
        public IEnumerable<ExpertiseListDTO> List(int? experitseFatherId)
        {
            var _context = _contextFactory();

            return _context.Expertise.Where(y => y.Active && y.ExpertiseFatherId == experitseFatherId).Select(
            x => new ExpertiseListDTO()
            {
                ExpertiseId = x.ExpertiseId,
                Name = x.Name,
                Active = x.Active,
                DefaultDescription = x.DefaultDescription,
                CreatedDateUTC = x.CreatedDateUTC
            });
        }
        public IEnumerable<ExpertiseListDTO> ListExpertises()
        {
            var _context = _contextFactory();
            return _context.Expertise.Where(y => y.ExpertiseFatherId.Equals(null)).Select(
                x => new ExpertiseListDTO()
                {
                    ExpertiseId = x.ExpertiseId,
                    Name = x.Name,
                    Active = x.Active,
                    DefaultDescription = x.DefaultDescription,
                    CreatedDateUTC = x.CreatedDateUTC
                }
                );

        }

        public IEnumerable<ExpertiseListDTO> ListMainExpertises()
        {
            var _context = _contextFactory();
            // for while just services( id 2 ) as expertise father 
            return _context.Expertise.Where(y => y.ExpertiseFatherId == (int)Common.GeneralEnumerators.ExpertiseTypes.Services && y.Active).Select(x => new ExpertiseListDTO() { ExpertiseId = x.ExpertiseId, Name = x.Name });
        }

        public IEnumerable<ExpertiseListDTO> ListSubExpertises(int expertiseFatherId)
        {
            var _context = _contextFactory();
            return _context.Expertise.Where(y => y.ExpertiseFatherId == expertiseFatherId && y.Active).Select(x => new ExpertiseListDTO() { ExpertiseId = x.ExpertiseId, Name = x.Name });
        }
    }
}
