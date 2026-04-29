namespace JobApplication.API.Models
{
    public class ApplicationDto
    {
        public Guid Id { get; init; }
        public string CompanyName { get; init; }
        public string Position { get; init; }
        public string? JobUrl { get; init; }
        public string? Notes { get; init; }
        public ApplicationStatus Status { get; init; }
        public DateTime AppliedAt { get; init; }
        public DateTime? InterviewDate { get; init; }
    }
}
