using Drive.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Drive.Domain.Factories
{
    public class DbContextFactory
    {

        public static DriveDbContext GetDriveDbContext()
        {
            var options = new DbContextOptionsBuilder<DriveDbContext>()
                 .UseNpgsql(ConfigurationManager.ConnectionStrings["Drive"].ConnectionString)
                 .Options;

            return new DriveDbContext(options);
        }
    }
}
