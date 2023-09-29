using AutoMapper;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Mapping
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
