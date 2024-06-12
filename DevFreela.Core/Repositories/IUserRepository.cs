using DevFreela.Core.Dtos;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
        Task<UserDto> GetByIdAsync(int id);
    } 
}
