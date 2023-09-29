using WebClient.Services.Abstraction;
using WebClient.Services.Contracts;

namespace WebClient.Services
{
    public class CustomerService:ICustomerService
    {
        public CustomerService() { }
        public CustomerDto InitializeCustomerDto(string firstName, string lastName)
        {
            CustomerDto dto = new() { Id=15,Firstname=firstName,Lastname=lastName};
            return dto;
        }
    }
}
