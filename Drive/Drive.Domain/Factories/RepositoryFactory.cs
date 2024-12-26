﻿using Drive.Domain.Repositories;


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
