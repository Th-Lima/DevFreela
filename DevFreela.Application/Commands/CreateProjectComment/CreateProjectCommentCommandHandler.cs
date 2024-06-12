using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateProjectComment
{
    public class CreateProjectCommentCommandHandler(IProjectRepository projectRepository) : IRequestHandler<CreateProjectCommentCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<Unit> Handle(CreateProjectCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);

            await _projectRepository.AddCommentAsync(comment);

            return Unit.Value;
        }
    }
}
