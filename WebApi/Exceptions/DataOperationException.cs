using System;

namespace WebApi.Exceptions
{
    /// <summary>
    /// Класс исключений для информирования об ошибке при работе с данными 
    /// </summary>
    public class DataOperationException : Exception
    {
        public DataOperationException(string message, Exception innerException): base(message, innerException)
        {
        }
    }
}
