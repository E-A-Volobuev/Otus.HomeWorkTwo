using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebClient.Domain.Entities;
using WebClient.Services.Abstraction;
using WebClient.Services.Contracts;
using WebClient.Services.Exeptions;

namespace WebClient.Services
{
    public class HttpService : IHttpService
    {
        private static HttpClient _httpClient;
        private static readonly string _BASE_URL = "https://localhost:5001";
        private static readonly string _CUSTOMER_URL = "/customers";
        public HttpService()
        {
            _httpClient = new() { BaseAddress = new Uri(_BASE_URL) };
        }
        /// <summary>
        /// получаем пользователя по id с сервера
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CustomerDto> GetCustomerByIdAsync(long id)
        {
            string url = _BASE_URL + _CUSTOMER_URL + $"/{id}";
            using HttpResponseMessage response = await _httpClient.GetAsync(url);
            if ((int)response.StatusCode != 200 && (int)response.StatusCode != 404)
                throw new HttpException("Ошибка получения данных", (int)response.StatusCode);
            if ((int)response.StatusCode == 404)
                throw new NotFoundException("Пользователь с таким Id не найден", (int)response.StatusCode);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CustomerDto>(jsonResponse);
        }
        public async Task<Customer> CreateAsync(CustomerDto dto)
        {

            string url = _BASE_URL + _CUSTOMER_URL;
            var json = JsonConvert.SerializeObject(dto);
            using StringContent jsonContent = new(json, Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await _httpClient.PostAsync(url, jsonContent);
            if ((int)response.StatusCode != 200 && (int)response.StatusCode != 409)
                throw new HttpException("Ошибка получения данных", (int)response.StatusCode);
            if ((int)response.StatusCode == 409)
                throw new ExistException("Пользователь с таким Id уже существует", (int)response.StatusCode);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Customer>(jsonResponse);
        }
    }
}
