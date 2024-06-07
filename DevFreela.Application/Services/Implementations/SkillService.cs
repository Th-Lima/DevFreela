using Dapper;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastucture.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Services.Implementations
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public SkillService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new ArgumentNullException("Erro ao obter connection string para acesso ao banco de dados");
        }

        public List<SkillViewModel> GetAll()
        {
            using(var sqlConnetion = new SqlConnection(_connectionString)) 
            {
                sqlConnetion.Open();

                var script = "SELECT Id, Description FROM Skills";

                return sqlConnetion.Query<SkillViewModel>(script).ToList();
            }
        }
    }
}

