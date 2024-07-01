using DevFreela.Core.Dtos;

namespace DevFreela.Core.Services
{
    public interface IPaymentService
    {
        Task ProcessPayment(PaymentInfoDto paymentInfoDto);
    }
}
