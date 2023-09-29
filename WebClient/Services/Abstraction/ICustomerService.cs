using WebClient.Services.Contracts;

namespace WebClient.Services.Abstraction
{
    public interface ICustomerService
    {
        /// <summary>
        /// генерируем данные по пользователю для отправки на сервер
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        CustomerDto InitializeCustomerDto(string firstName,string lastName);
    }
}
