using DevFreela.Core.Dtos;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjecByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsDto>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjecByIdQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDetailsDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            return await _projectRepository.GetByIdAsync(request.Id);
        }
    }
}
