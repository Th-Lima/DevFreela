using Dapper;
using DevFreela.Core.Dtos;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastucture.Persistence.Repositories
{
    public class SkillRepository(IConfiguration configuration) : ISkillRepository
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                 ?? throw new ArgumentNullException("Erro ao obter connection string para acesso ao banco de dados");

        public async Task<List<SkillDto>> GetAllAsync()
        {
            using var sqlConnetion = new SqlConnection(_connectionString);

            sqlConnetion.Open();

            var script = "SELECT Id, Description FROM Skills";

            var skills = await sqlConnetion.QueryAsync<SkillDto>(script);
              
            return skills.ToList();
        }
    }
}
