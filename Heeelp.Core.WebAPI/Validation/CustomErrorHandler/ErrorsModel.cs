using System.Collections.Generic;

namespace Heeelp.Core.WebAPI.Validation
{
    public class ErrorsModel
    {
        public IEnumerable<ErrorModel> Errors { get; set; }
    }
}