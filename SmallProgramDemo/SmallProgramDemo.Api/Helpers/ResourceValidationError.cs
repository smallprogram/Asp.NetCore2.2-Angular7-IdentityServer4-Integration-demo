namespace SmallProgramDemo.Api.Helpers
{
    /// <summary>
    /// ResourceModel错误类型与错误信息类
    /// </summary>
    public class ResourceValidationError
    {
        public string ValidatorKey { get; private set; }
        public string Message { get; private set; }

        public ResourceValidationError(string message, string validatorKey = "")
        {
            ValidatorKey = validatorKey;
            Message = message;
        }
    }
}