using Drive.Data.Entities.Models.Users;
using Drive.Data.Entities.Models.Files;


namespace Drive.Data.Entities.Models.Folders
{
    public class DriveFolder
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FolderUserId { get; set; }
        public User FolderUser { get; set; }
        public int? ParentFolderId { get; set; }
        public DriveFolder ParentFolder { get; set; }

        public DriveFolder(int ID, string name, int FolderUserID,int FolderParentID)
        {
            Id = ID;
            Name = name;
            CreatedAt = DateTime.Now;
            FolderUserId = FolderUserID;
            ParentFolderId = FolderParentID;
        }
        public DriveFolder()
        {
        }
        public ICollection<DriveFolder> SubFolders { get; set; } = new List<DriveFolder>();
        public ICollection<DriveFile> Files { get; set; } = new List<DriveFile>();

    }
}
