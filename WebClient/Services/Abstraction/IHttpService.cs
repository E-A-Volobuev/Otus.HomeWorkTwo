using System.Threading.Tasks;
using WebClient.Domain.Entities;
using WebClient.Services.Contracts;

namespace WebClient.Services.Abstraction
{
    public interface IHttpService
    {
        /// <summary>
        /// получаем пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CustomerDto> GetCustomerByIdAsync(long id);       
        /// <summary>
        ///  Создаем пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<Customer> CreateAsync(CustomerDto dto);
    }
}
