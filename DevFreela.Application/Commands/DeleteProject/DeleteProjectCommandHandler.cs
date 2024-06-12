using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler(IProjectRepository projectRepository) : IRequestHandler<DeleteProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            await _projectRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
