using Drive.Data.Entities;
using Drive.Domain.Enums;


namespace Drive.Domain.Repositories
{
    public class BaseRepository
    {
        protected readonly DriveDbContext DbContext;

        protected BaseRepository(DriveDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected ResponseResult SaveChanges()
        {
            var hasChanges = DbContext.SaveChanges() > 0;
            if (hasChanges)
                return ResponseResult.Success;

            return ResponseResult.NoChanges;
        }
    }
}
