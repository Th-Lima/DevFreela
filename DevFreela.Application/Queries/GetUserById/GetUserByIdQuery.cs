using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.Queries.GetUserById
{
    public class GetUserByIdQuery(int id) : IRequest<UserViewModel>
    {
        public int Id { get; set; } = id;
    }
}
