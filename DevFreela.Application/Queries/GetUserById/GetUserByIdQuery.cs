using DevFreela.Application.ViewModels;
using DevFreela.Core.Dtos;
using MediatR;

namespace DevFreela.Application.Queries.GetUserById
{
    public class GetUserByIdQuery(int id) : IRequest<UserDto>
    {
        public int Id { get; set; } = id;
    }
}
