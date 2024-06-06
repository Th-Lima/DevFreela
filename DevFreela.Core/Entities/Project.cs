using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities
{
    public class Project(string title,
        string description,
        int idClient,
        int idFreelancer,
        decimal totalCost) : BaseEntity
    {
        public string Title { get; private set; } = title;
        public string Description { get; private set; } = description;
        public int IdClient { get; private set; } = idClient;
        public User Client { get; private set; }
        public int IdFreelancer { get; private set; } = idFreelancer;
        public User Freelancer { get; private set; }
        public decimal TotalCost { get; private set; } = totalCost;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime? StartedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public ProjectStatusEnum Status { get; private set; } = ProjectStatusEnum.Created;
        public List<ProjectComment> Comments { get; private set; } = new List<ProjectComment>();

        public void Cancel()
        {
            if(Status == ProjectStatusEnum.InProgress || Status == ProjectStatusEnum.Suspended)
                Status = ProjectStatusEnum.Cancelled;
        }

        public void Start()
        {
            if(Status == ProjectStatusEnum.Created)
            {
                Status = ProjectStatusEnum.InProgress;
                CreatedAt = DateTime.Now;
            }
        }

        public void Finish()
        {
            if(Status == ProjectStatusEnum.InProgress)
            {
                Status = ProjectStatusEnum.Finished;
                FinishedAt = DateTime.Now;
            }
        }

        public void Update(string title, string description, decimal totalCost)
        {
            Title = title;
            Description = description;
            TotalCost = totalCost;
        }
    }
}
