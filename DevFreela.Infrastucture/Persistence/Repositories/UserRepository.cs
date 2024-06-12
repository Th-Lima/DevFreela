using DevFreela.Core.Dtos;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastucture.Persistence.Repositories
{
    public class UserRepository(DevFreelaDbContext dbContext) : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext = dbContext;

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

            return new UserDto
            {
                Email = user.Email,
                FullName = user.FullName
            };
        }
    }
}
