using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Exceuted_ReturnProjectId()
        {
            //Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();
            var createProjectCommand = new CreateProjectCommand()
            {
                Title = "Test",
                Description = "Test",
                IdClient = 1,
                IdFreelancer = 1,
                TotalCost = 10000,
            };

            var handler = new CreateProjectCommandHandler(projectRepositoryMock.Object);

            //Act
            var result = await handler.Handle(createProjectCommand, CancellationToken.None);

            //Assert
            Assert.True(result >= 0);
            projectRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Project>()), Times.Once);
        }
    }
}
