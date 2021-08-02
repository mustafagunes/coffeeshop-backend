using System.Collections.Generic;

namespace Core.Model.Base
{
    public class BaseArrayResponse<T>
    {
        public string Code { get; set; }
        
        public string Message { get; set; }
        
        public IEnumerable<T> Data { get; set; }
    }
}