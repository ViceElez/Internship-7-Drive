using Drive.Data.Entities.Models.Folders;


namespace Drive.Data.Entities.Models.Users
{
    public class DriveFolderUser
    {
        public int DriveFolderId { get; set; }
        public DriveFolder DriveFolder { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DriveFolderUser() { }
    }
}
