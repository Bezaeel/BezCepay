namespace BezCepay.Service.Communication
{
    public enum ErrorCodes
    {
        Success = 00,
        Exception = 99,
        Error = -11

    }
    public class ServiceResponse
    {
        public bool IsSuccess { get; set; }
        public ErrorCodes Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ServiceResponse()
        {

        }
        public ServiceResponse(bool success, string message)
        {
            IsSuccess = success;
            Message = message;
        }
        
        public ServiceResponse(bool success, string message, object data)
        {
            IsSuccess = success;
            Message = message;
            Data = data;
        }
    }
}
