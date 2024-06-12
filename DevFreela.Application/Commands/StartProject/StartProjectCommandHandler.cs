using DevFreela.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectCommandHandler(IProjectRepository projectRepository, IConfiguration configuration) : IRequestHandler<StartProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            project.Start();

            await _projectRepository.StartAsync(project);

            return Unit.Value;
        }
    }
}
