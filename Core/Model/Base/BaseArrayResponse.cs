using System.Collections.Generic;

namespace Core.Model.Base
{
    public class BaseArrayResponse
    {
        public bool Status { get; private set; }
        
        public string Message { get; private set; }
        
        public IEnumerable<object> Data { get; private set; }
        public BaseArrayResponse(bool status, string message, IEnumerable<object> data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}