namespace TTB.Assignment.API.Model
{
    public class ResponseModel<T> (int statusCode,string message)
    {
        public int StatusCode { get; set; } = statusCode;
        public string Message { get; set; } = message;
        public T? Data { get; set; }

        public ResponseModel(int statusCode,string message, T data) : this(statusCode,message)
        {
            Data = data;
        }
    }
}
