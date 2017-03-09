using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class PersonSkinDTO
    {
        public Guid PersonIntegrationCodeId { get; set; }
        public string SkinName { get; set; }
        public string CustomClubName { get; set; }
        public string CssFileSkin { get; set; }
        public string UrlImageLogo { get; set; }
        public string CustomClubLogo { get; set; }     
        public string CustomImageTheme { get; set; }
        public string CustomPresentationText { get; set; }
        public string DiagramView { get; set; }
    }
}
