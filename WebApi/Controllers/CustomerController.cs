using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Abstractions;
using WebApi.Dto;
using WebApi.Exceptions;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new System.ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerAsync([FromRoute] long id)
        {
            try
            {
                var isExist = await _customerRepository.IsExistByIdAsync(id);
                if (isExist)
                {
                    var customer = await _customerRepository.GetByIdAsync(id);
                    return Ok(_mapper.Map<CustomerDto>(customer));
                }
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                throw new DataOperationException("Ошибка добавления данных", ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomerAsync([FromBody] CustomerDto dto)
        {
            try
            {
                if (dto != null)
                {
                    var isExist = await _customerRepository.IsExistByIdAsync(dto.Id);
                    if (!isExist)
                    {
                        var customer = await _customerRepository.CreateAsync(_mapper.Map<Customer>(dto));
                        return Ok(_mapper.Map<CustomerDto>(customer));
                    }
                    return new StatusCodeResult(409);
                }
                return BadRequest("Объект пустой");
            }
            catch (Exception ex)
            {
                throw new DataOperationException("Ошибка добавления данных", ex);
            }
        }
    }
}