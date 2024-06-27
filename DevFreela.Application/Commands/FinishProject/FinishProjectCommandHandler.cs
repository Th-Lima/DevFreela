using DevFreela.Core.Dtos;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommandHandler(IProjectRepository projectRepository, IPaymentService paymentService) : IRequestHandler<FinishProjectCommand, bool>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly IPaymentService _paymentService = paymentService;

        public async Task<bool> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            project.Finish();

            var paymentInfoDto = new PaymentInfoDto(request.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.FullName);

            var result = await _paymentService.ProcessPayment(paymentInfoDto);

            if (!result)
                project.SetPaymentPending();

            await _projectRepository.SaveChangesAsync();

            return result;
        }
    }
}
