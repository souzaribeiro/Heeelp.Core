using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
   public class FileTempDTO
    {
        public string FilePath { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }

        public string OriginalName { get; set; }

        public Guid FileIntegrationCode { get; set; }

        public int? UploadedBy { get; set; }
    }
}
