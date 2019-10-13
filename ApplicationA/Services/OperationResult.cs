namespace ApplicationA.Services
{
    public class OperationResult
    {
        public bool IsSucceed { get; private set; }
        public string ErrorMessage { get; private set; }

        public static OperationResult Failed(string message = null)
        {
            return new OperationResult
            {
                IsSucceed = false,
                ErrorMessage = message
            };
        }

        public static OperationResult Success()
        {
            return new OperationResult
            {
                IsSucceed = true
            };
        }
    }
}