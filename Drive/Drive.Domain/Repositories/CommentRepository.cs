using Drive.Data.Entities.Models.Users;
using Drive.Data.Entities.Models;
using Drive.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities.Models.Comments;

namespace Drive.Domain.Repositories
{
    public class CommentRepository:BaseRepository
    {
        public CommentRepository(DriveDbContext dbContext) : base(dbContext) { }
        public static List<DriveComments> ListAllComments(int fileId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                return context.driveComments
                .Where(c => c.FileId == fileId)
                .OrderBy(c => c.CreatedAt) 
                .ToList();
            }
        }
    }
}
