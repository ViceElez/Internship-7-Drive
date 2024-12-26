using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities.Models.Users;
using Drive.Data.Entities.Models.Folders;
using Drive.Data.Entities.Models.Files;

namespace Drive.Data.Seed
{
    public class DatabaseSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(new List<User>
            {
                new User
                {
                    Id = 1,
                    Email = "ivana@vip.com",
                    Password = "password123",
                },
                new User
                {
                    Id = 2,
                    Email = "josip@yahoo.pro",
                    Password = "password456",
                },
                new User
                {
                    Id = 3,
                    Email = "mario@abc.com",
                    Password = "password789",
                },
                new User
                {
                    Id = 4,
                    Email = "luka@gmail.com",
                    Password = "password000",
                },
                new User
                {
                    Id = 5,
                    Email = "ana@domain.com",
                    Password = "password111",
                },
                new User
                {
                    Id = 6,
                    Email = "nikola@company.com",
                    Password = "password222",
                }
            });

            builder.Entity<DriveFolder>().HasData(new List<DriveFolder>
            {
                new DriveFolder { Id = 1, Name = "Work", FolderUserId = 1, ParentFolderId = null }, 
                new DriveFolder { Id = 2, Name = "Work/Reports", FolderUserId = 1, ParentFolderId = 1 },
                new DriveFolder { Id = 3, Name = "Work/Presentations", FolderUserId = 1, ParentFolderId = 1 },

                new DriveFolder { Id = 4, Name = "Personal", FolderUserId = 1, ParentFolderId = null }, 
                new DriveFolder { Id = 5, Name = "Personal/Photos", FolderUserId = 1, ParentFolderId = 4 },
                new DriveFolder { Id = 6, Name = "Personal/Videos", FolderUserId = 1, ParentFolderId = 4 },

                new DriveFolder { Id = 7, Name = "Projects", FolderUserId = 2, ParentFolderId = null }, 
                new DriveFolder { Id = 8, Name = "Projects/Website", FolderUserId = 2, ParentFolderId = 7 },
                new DriveFolder { Id = 9, Name = "Projects/App", FolderUserId = 2, ParentFolderId = 7 },

                new DriveFolder { Id = 10, Name = "Finance", FolderUserId = 3, ParentFolderId = null }, 
                new DriveFolder { Id = 11, Name = "Finance/Invoices", FolderUserId = 3, ParentFolderId = 10 },
                new DriveFolder { Id = 12, Name = "Finance/Budgets", FolderUserId = 3, ParentFolderId = 10 },

                new DriveFolder { Id = 13, Name = "Designs", FolderUserId = 5, ParentFolderId = null }, 
                new DriveFolder { Id = 14, Name = "Designs/UI", FolderUserId = 5, ParentFolderId = 13 },
                new DriveFolder { Id = 15, Name = "Designs/UX", FolderUserId = 5, ParentFolderId = 13 },

                new DriveFolder { Id = 16, Name = "Development", FolderUserId = 6, ParentFolderId = null }, 
                new DriveFolder { Id = 17, Name = "Development/Backend", FolderUserId = 6, ParentFolderId = 16 },
                new DriveFolder { Id = 18, Name = "Development/Frontend", FolderUserId = 6, ParentFolderId = 16 }
            });

            builder.Entity<DriveFile>().HasData(new List<DriveFile>
            {
                new DriveFile { Id = 1, Name = "Resume.pdf", Text = "Resume content", FileUserId = 1, FolderId = null },
                new DriveFile { Id = 2, Name = "ProjectPlan.docx", Text = "Project Plan content", FileUserId = 1, FolderId = null },
                new DriveFile { Id = 3, Name = "Budget.xlsx", Text = "Budget content", FileUserId = 1, FolderId = null },
                new DriveFile { Id = 4, Name = "Hobbies.txt", Text = "Hobbies content", FileUserId = 2, FolderId = null },
                new DriveFile { Id = 5, Name = "ToDoList.docx", Text = "ToDo list content", FileUserId = 2, FolderId = null },
                new DriveFile { Id = 6, Name = "Notes.txt", Text = "Notes content", FileUserId = 3, FolderId = null },
                new DriveFile { Id = 7, Name = "Presentation.pptx", Text = "Presentation content", FileUserId = 3, FolderId = null },
                new DriveFile { Id = 8, Name = "Invoice.pdf", Text = "Invoice content", FileUserId = 4, FolderId = null },
                new DriveFile { Id = 9, Name = "MeetingNotes.doc", Text = "Meeting notes content", FileUserId = 5, FolderId = null },
                new DriveFile { Id = 10, Name = "ProductDesign.png", Text = "Product design content", FileUserId = 5, FolderId = null },
                new DriveFile { Id = 11, Name = "CodeSnippet.cs", Text = "Code snippet content", FileUserId = 6, FolderId = null },
                new DriveFile { Id = 12, Name = "SetupGuide.md", Text = "Setup guide content", FileUserId = 6, FolderId = null }
            });
        }
    }
}
