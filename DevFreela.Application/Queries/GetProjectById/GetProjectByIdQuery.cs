using DevFreela.Core.Dtos;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdQuery(int id) : IRequest<ProjectDetailsDto>
    {
        public int Id { get; private set; } = id;
    }
}
