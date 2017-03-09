using System.Collections.Generic;

namespace Heeelp.Core.Domain.ReadModel.DTO.Validation
{
    public class ErrorsModel
    {
        public IEnumerable<ErrorModel> Errors { get; set; }
    }
}