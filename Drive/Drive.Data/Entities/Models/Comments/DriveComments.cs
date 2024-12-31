using Drive.Data.Entities.Models.Files;
using Drive.Data.Entities.Models.Users;

namespace Drive.Data.Entities.Models.Comments
{
    public class DriveComments
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FileId { get; set; }
        public DriveFile File { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public DriveComments(int ID, string content, int fileId, int userId)
        {
            Id = ID;
            Content = content;
            FileId = fileId;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }

        public DriveComments() { }
    }
}
