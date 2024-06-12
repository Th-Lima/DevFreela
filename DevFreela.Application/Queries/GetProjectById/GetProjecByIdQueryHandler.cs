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
            var project = await _projectRepository.GetByIdAsync(request.Id);

            var projectDetailsDto = new ProjectDetailsDto()
            {
                Id = project.Id,
                ClientFullName = project.Client.FullName,
                Description = project.Description,
                FinishedAt = project.FinishedAt,
                FreelancerFullName = project.Freelancer.FullName,
                StartedAt = project.StartedAt,
                Title = project.Title,
                TotalCost = project.TotalCost,
            };

            return projectDetailsDto;
        }
    }
}
