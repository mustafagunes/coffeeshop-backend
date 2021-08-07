namespace Core.Model.Base
{
    public class BaseObjectResponse
    {
        public bool Status { get; private set; }
        
        public string Message { get; private set; }
        
        public object Data { get; private set; }
        
        public BaseObjectResponse(bool status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}