using AutoMapper;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebClient.Services.Abstraction;
using WebClient.Services.Exeptions;

namespace WebClient
{
    public class Start : BackgroundService
    {
        private readonly ICustomerService _customerService;
        private readonly IHttpService _httpService;
        private readonly IMapper _mapper;
        public delegate void AppEventHandler(string message);
        public event AppEventHandler Notify;

        public Start(ICustomerService customerService, IHttpService httpService, IMapper mapper)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Notify += DisplayMessage;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Menu();
        }

        #region private methods
        private async Task Menu()
        {
            var flag = true;
            while (flag)
            {
                StartMenuShowActions();
                string? input = Console.ReadLine();
                bool result = int.TryParse(input, out var number);
                if (result == true)
                    await StartMenuDoActions(number);
                else
                    Notify.Invoke("Введён неправильный символ");
            }
        }
        private void StartMenuShowActions()
        {
            Notify.Invoke("Выберите действие:");
            Notify.Invoke("1-Получить пользователя по Id");
            Notify.Invoke("2-Создать пользователя");
            Notify.Invoke("Для выхода из приложения нажмите-3");
            Notify.Invoke("Введите номер действия:");
        }
        private async Task StartMenuDoActions(int number)
        {
            switch (number)
            {
                case 1:
                    await GetByIdHelperAsync();
                    break;
                case 2:
                    await CreatedEntityAsync();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Notify.Invoke("Введён неправильный символ");
                    break;
            }
        }
        private async Task GetByIdHelperAsync()
        {
            Notify.Invoke("Введите Id:");
            string? input = Console.ReadLine();
            bool result = int.TryParse(input, out var id);
            if (result == true)
                Notify.Invoke(await GetCustomerByIdAsync(id));
            else
                Notify.Invoke("Введён неправильный символ");
        }
        private async Task<string> GetCustomerByIdAsync(long id)
        {
            try
            {
                var customer = await _httpService.GetCustomerByIdAsync(id);
                return "Пользователь:\n" + $"Id: {customer.Id}" + $" Имя: {customer.Firstname}" + $" Фамилия: {customer.Lastname}";
            }
            catch(NotFoundException ex)
            {
                return ex.Message;
            }
            catch (HttpException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task CreatedEntityAsync()
        {
            try
            {
                var dto = _customerService.InitializeCustomerDto("Петров", "Пётр");
                Notify.Invoke("Отправляем объект:\n" + $"Имя: {dto.Firstname}" + $" Фамилия: {dto.Lastname}");
                var entity = await _httpService.CreateAsync(dto);
                Notify.Invoke("Создан пользователь:\n" + $"Id: {entity.Id}" + $" Имя: {entity.Firstname}" + $" Фамилия: {entity.Lastname}");
            }
            catch (NotFoundException ex)
            {
                Notify.Invoke(ex.Message);
            }
            catch (HttpException ex)
            {
                Notify.Invoke(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
        #endregion     
    }
}
