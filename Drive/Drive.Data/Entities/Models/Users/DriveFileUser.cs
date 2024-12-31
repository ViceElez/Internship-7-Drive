using Drive.Data.Entities.Models.Files;

namespace Drive.Data.Entities.Models.Users
{
    public class DriveFileUser
    {
        public int DriveFileId { get; set; }
        public DriveFile DriveFile { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DriveFileUser() { }
    }
}
