using Dapper;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsQueryHandler(IConfiguration configuration) : IRequestHandler<GetAllSkillsQuery, List<SkillViewModel>>
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                 ?? throw new ArgumentNullException("Erro ao obter connection string para acesso ao banco de dados");

        public async Task<List<SkillViewModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            using var sqlConnetion = new SqlConnection(_connectionString);

            sqlConnetion.Open();

            var script = "SELECT Id, Description FROM Skills";

            var skills = await sqlConnetion.QueryAsync<SkillViewModel>(script);

            return skills.ToList();
        }
    }
}
