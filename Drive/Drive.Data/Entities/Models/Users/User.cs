using Drive.Data.Entities.Models.Folders;
using Drive.Data.Entities.Models.Files;


namespace Drive.Data.Entities.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(int ID, string email, string password)
        {
            Id = ID;
            Email = email;
            Password = password;
        }
        public User()
        {
        }

        public ICollection<DriveFolder> Folders { get; set; } = new List<DriveFolder>();
        public ICollection<DriveFile> Files { get; set; } = new List<DriveFile>();
        public ICollection<DriveFolderUser> SharedFolders { get; set; } = new List<DriveFolderUser>();
        public ICollection<DriveFileUser> SharedFiles { get; set; } = new List<DriveFileUser>();

    }
}
