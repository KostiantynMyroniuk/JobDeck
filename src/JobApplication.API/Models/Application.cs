namespace JobApplication.API.Models
{
    public class Application
    {
        public Guid Id { get; private set; }
        public string CompanyName { get; private set; }
        public string Position { get; private set; }
        public string? JobUrl { get; private set; }
        public string? Notes { get; private set; }
        public ApplicationStatus Status { get; private set; }
        public DateTime AppliedAt { get; private set; }
        public DateTime? InterviewDate { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Application() { }

        public Application(
            string companyName,
            string position,
            string? jobUrl = null,
            string? notes = null)
        {
            Id = Guid.NewGuid();
            CompanyName = companyName;
            Position = position;
            JobUrl = jobUrl;
            Notes = notes;
            Status = ApplicationStatus.Applied;

            AppliedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStatus(ApplicationStatus newStatus, DateTime? interviewDate = null)
        {
            Status = newStatus;
            UpdatedAt = DateTime.UtcNow;
        }

        public void InterviewScheduled(DateTime interviewDate)
        {
            InterviewDate = interviewDate;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
