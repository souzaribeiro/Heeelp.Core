using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO.Validation;


namespace Heeelp.Core.Domain.ReadModel.DTO
{
    

    public class PhotoLogoDTO
    {
        public Guid IntegrationId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

    }
    

}
