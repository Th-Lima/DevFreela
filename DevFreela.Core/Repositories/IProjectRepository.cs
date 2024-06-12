using DevFreela.Core.Dtos;
using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<ProjectDetailsDto> GetByIdAsync(int id);
    }
}
