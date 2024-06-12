using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastucture.Persistence.Repositories
{
    public class ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration) : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext = dbContext;

        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                 ?? throw new ArgumentNullException("Erro ao obter connection string para acesso ao banco de dados");

        public async Task<List<Project>> GetAllAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            var project = await _dbContext.Projects
               .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .SingleOrDefaultAsync(p => p.Id == id);

            return project;
        }

        public async Task AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddCommentAsync(ProjectComment comment)
        {
            await _dbContext.ProjectComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);

            project?.Cancel();

            await _dbContext.SaveChangesAsync();
        }

        public async Task StartAsync(Project project)
        {
            using var sqlConnection = new SqlConnection(_connectionString);

            sqlConnection.Open();

            var script = "UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";

            await sqlConnection.ExecuteAsync(script, new
            {
                status = project.Status,
                startedAt = project.StartedAt,
                id = project.Id,
            });
        }
        public async Task SaveChangesAsync()
           => await _dbContext.SaveChangesAsync();
    }
}
