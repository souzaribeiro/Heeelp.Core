﻿using Heeelp.Core.Common;

namespace Heeelp.Core.Domain.ReadModel.DTO.Validation
{
    public class ErrorState
    {
        public GeneralEnumerators.enumValidationErrorCode ErrorCode { get; set; }
        public string DocumentationPath { get; set; }
        public string DeveloperMessageTemplate { get; set; }
        public string UserMessage { get; set; }
    }
}