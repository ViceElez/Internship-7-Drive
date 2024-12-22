using Drive.Data.Entities.Models.Users;
using Drive.Data.Entities.Models.Folders;

namespace Drive.Data.Entities.Models.Files
{
    public class DriveFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int FileUserId { get; set; }
        public User FileUser { get; set; }
        public DateTime LastChanges { get; set; }
        public int FolderId { get; set; }
        public DriveFolder Folder { get; set; }

        public DriveFile(int ID, string name, string text)
        {
            Id = ID; //odi bi tribalo slat file.count++ 
            Name = name;
            Text = text;
        }

        public DriveFile()
        {
        }


    }
}
