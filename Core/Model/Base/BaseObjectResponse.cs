namespace Core.Model.Base
{
    public class BaseObjectResponse<T>
    {
        public bool Status { get; set; }
        
        public string Message { get; set; }
        
        public T Data { get; set; }
    }
}