using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class FileTempDao : IFileTempDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;

       

        public FileTempDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }


        public FileTemp Get(FileTemp t)
        {
            throw new NotImplementedException();
        }
        public FileTemp Get(int id)
        {
            var _context = _contextFactory();
            try
            {
                var a= _context.FileTemp.Where(x => x.FileTempId == id);

            }
            catch (Exception e)
            {

                throw;
            }
            return _context.FileTemp.Where(x => x.FileTempId == id).FirstOrDefault();
        }

        public IEnumerable<FileTemp> List()
        {
            var _context = _contextFactory();
            return _context.FileTemp.ToList();
        }
        public IEnumerable<FileTempListDTO> ListFileTemps()
        {
            var _context = _contextFactory();
            return _context.FileTemp.Select(x => new FileTempListDTO() { FileTempId = x.FileTempId, File = x.FilePath } );
        }

        public int SaveFileTemp(FileTemp ft) {

            var _context = _contextFactory();
            
            _context.FileTemp.Add(ft);
            _context.SaveChanges();
            return ft.FileTempId;
        }

        public void Delete(int id) {

            var _context = _contextFactory();
            var ft = new FileTemp { FileTempId = id };
            _context.FileTemp.Attach(ft);
            _context.FileTemp.Remove(ft);
            _context.SaveChanges();

        }
    }
}
