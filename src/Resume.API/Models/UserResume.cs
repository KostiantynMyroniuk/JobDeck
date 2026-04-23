namespace Resume.API.Models
{
    public class UserResume
    {
        public Guid Id { get; private set; }
        public string OriginalName { get; private set; }
        public string FileName { get; private set; }
        public string ContentType { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Guid ApplicationId { get; private set; }

        public UserResume() { }

        public UserResume(
            string originalName,
            string fileName,
            string contentType,
            Guid applicationId
        )
        {
            OriginalName = originalName;
            FileName = fileName;
            ContentType = contentType;
            ApplicationId = applicationId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
