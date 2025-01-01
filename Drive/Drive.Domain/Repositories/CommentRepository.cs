using Drive.Data.Entities.Models.Users;
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
        public static bool CheckIfCommentExists(int fileId, int commentId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                return context.driveComments
                .Any(c => c.FileId == fileId && c.Id == commentId);
            }
        }
        public static void AddComment(User loggedUser,int fileId,string content)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var comment = new DriveComments()
                {
                    Content = content,
                    FileId = fileId,
                    UserId = loggedUser.Id
                };
                context.driveComments.Add(comment);
                context.SaveChanges();
            }
        }
        public static void EditComment(int fileId, int commentId, string content)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var comment = context.driveComments
                .FirstOrDefault(c => c.FileId == fileId && c.Id == commentId);
                if (comment != null)
                {
                    comment.Content = content;
                    context.SaveChanges();
                }
            }
        }
        public static void DeleteComments(int fileId, int commentId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var comment = context.driveComments
                .FirstOrDefault(c => c.FileId == fileId && c.Id == commentId);
                if (comment != null)
                {
                    context.driveComments.Remove(comment);
                    context.SaveChanges();
                }
            }
        }

    }
}
