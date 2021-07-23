namespace Core.Model.BaseReponseModel
{
    public class BaseObjectResponse<T>
    {
        public string Code { get; set; }
        
        public string Message { get; set; }
        
        public T Data { get; set; }
    }
}