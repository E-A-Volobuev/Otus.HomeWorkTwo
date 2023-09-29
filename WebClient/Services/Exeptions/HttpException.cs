using System;

namespace WebClient.Services.Exeptions
{
    public class HttpException:Exception
    {
        public int ErrorCode {  get; set; }
        public HttpException(string message, int errorCode): base(message)
        {
            ErrorCode = errorCode;
        }
    }
    public class NotFoundException : HttpException
    {
        public NotFoundException(string message,int errorCode) : base(message, errorCode)
        {

        }
    }
    public class ExistException : HttpException
    {
        public ExistException(string message, int errorCode) : base(message, errorCode)
        {

        }
    }
}
