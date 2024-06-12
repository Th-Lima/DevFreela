using Dapper;
using DevFreela.Infrastucture.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectCommandHandler(DevFreelaDbContext dbContext, IConfiguration configuration) : IRequestHandler<StartProjectCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext = dbContext;

        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                 ?? throw new ArgumentNullException("Erro ao obter connection string para acesso ao banco de dados");

        public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == request.Id);

            project.Start();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new
                {
                    status = project.Status,
                    startedAt = project.StartedAt,
                    id = project.Id
                });
            }

            return Unit.Value;
        }
    }
}
