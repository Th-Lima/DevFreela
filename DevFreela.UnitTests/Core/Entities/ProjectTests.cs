using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Core.Entities
{
    public class ProjectTests
    {
        [Fact]
        public void TestIfProjectStartWorks()
        {
            var project = new Project("Nome de teste", "descriçao teste", 1, 2, 10000);

            project.Start();

            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.Equal(DateTime.Now.Date, project.StartedAt.Value.Date);
            Assert.NotNull(project.StartedAt);
        }
    }
}
