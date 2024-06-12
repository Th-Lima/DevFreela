using DevFreela.Core.Entities;
using DevFreela.Infrastucture.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProjectComment
{
    public class CreateProjectCommentCommandHandler(DevFreelaDbContext dbContext) : IRequestHandler<CreateProjectCommentCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext = dbContext;

        public async Task<Unit> Handle(CreateProjectCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);

            await _dbContext.ProjectComments.AddAsync(comment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
