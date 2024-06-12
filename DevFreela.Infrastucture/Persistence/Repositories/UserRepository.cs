using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastucture.Persistence.Repositories
{
    public class UserRepository(DevFreelaDbContext dbContext) : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext = dbContext;

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
