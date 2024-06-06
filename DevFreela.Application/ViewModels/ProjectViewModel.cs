namespace DevFreela.Application.ViewModels
{
    public class ProjectViewModel(int id, string title, DateTime createdAt)
    {
        public int Id { get; private set; }
        public string Title { get; set; } = title;
        public DateTime CreatedAt { get; set; } = createdAt;
    }
}
