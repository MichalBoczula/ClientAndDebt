using Client.Domain.Enums;

namespace Client.Domain.Validation.Common
{
    public class ValidationError
    {
        public string Message { get; set; }
        public string RuleName { get; set; }
        public RuleSeverity Severity { get; set; }
    }
}
