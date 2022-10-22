namespace DatingApp.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string message =null, string details =null)// if we do not provide Messages or Details then both of these proberties is gonna be null 
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

    }
}
