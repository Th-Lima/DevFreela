using DevFreela.Core.Dtos;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastucture.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _paymentsBaseUrl;

        public PaymentService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _paymentsBaseUrl = configuration.GetSection("Services:Payments").Value;
        }

        public async Task<bool> ProcessPayment(PaymentInfoDto paymentInfoDto)
        {
            var url = $"{_paymentsBaseUrl}/api/Payments";

            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDto);

            var paymentInfoContent = new StringContent(paymentInfoJson, Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient("payments");

            var response = await httpClient.PostAsync(url, paymentInfoContent);

            return response.IsSuccessStatusCode;
        }
    }
}
