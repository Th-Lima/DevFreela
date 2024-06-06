namespace DevFreela.Application.ViewModels
{
    public class ProjectDetailsViewModel(int id,
        string title,
        string description,
        decimal totalCost, 
        DateTime? startedAt,
        DateTime? finishedAt)
    {
        public int Id { get; set; } = id;
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public decimal TotalCost { get; set; } = totalCost;
        public DateTime? StartedAt { get; set; } = startedAt;
        public DateTime? FinishedAt { get; set; } = finishedAt;
    }
}
