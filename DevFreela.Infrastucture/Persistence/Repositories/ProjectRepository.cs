using DevFreela.Core.Dtos;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastucture.Persistence.Repositories
{
    public class ProjectRepository(DevFreelaDbContext dbContext) : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext = dbContext;

        public async Task<List<Project>> GetAllAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<ProjectDetailsDto> GetByIdAsync(int id)
        {
            var project = await _dbContext.Projects
               .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .SingleOrDefaultAsync(p => p.Id == id);

            var projectDetailsDto = new ProjectDetailsDto()
            {
                Id = id,
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
