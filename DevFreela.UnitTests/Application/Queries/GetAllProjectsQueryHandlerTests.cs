using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsQueryHandlerTests
    {
        [Fact]
        public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels()
        {
            //Arrange
            var projects = new List<Project>()
            {
                new("Nome Teste 1", "Descrição Teste 1", 1, 2, 10000),
                new("Nome Teste 2", "Descrição Teste 2", 1, 2, 20000),
                new("Nome Teste 3", "Descrição Teste 3", 1, 2, 3000),
            };

            var projectRepositoryMock = new Mock<IProjectRepository>();

            projectRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery("");
            var handler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            //Act
            var result = await handler.Handle(getAllProjectsQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(projects.Count, result.Count);

            projectRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
}
