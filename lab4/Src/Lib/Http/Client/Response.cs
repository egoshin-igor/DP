namespace Lib.Http.Client
{
    public class Response<R>
    {
        public int StatusCode { get; set; }
        public bool IsSuccessStatusCode {get; set;}
        public R Result {get; set;}
    }
}