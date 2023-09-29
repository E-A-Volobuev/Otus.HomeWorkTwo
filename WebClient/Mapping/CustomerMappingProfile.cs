using AutoMapper;
using WebClient.Domain.Entities;
using WebClient.Services.Contracts;

namespace WebClient.Mapping
{
    public class CustomerMappingProfile:Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
