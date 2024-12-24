using Drive.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Domain.Factories
{
    public class RepositoryFactory
    {
        public static TRepository Create<TRepository>() where TRepository : BaseRepository
        {
            var dbContext = DbContextFactory.GetDriveDbContext();
            var repository = Activator.CreateInstance(typeof(TRepository), dbContext) as TRepository;

            return repository;
        }
    }
}
