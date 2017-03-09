using Heeelp.Core.Common;

namespace Heeelp.Core.Domain.ReadModel.DTO.Validation
{
    public class ErrorModel
    {
        public GeneralEnumerators.enumValidationErrorCode ErrorCode { get; set; }
        public string Field { get; set; }
        public string DeveloperMessage { get; set; }
        public string Documentation { get; set; }
        public string UserMessage { get; set; }
    }
}