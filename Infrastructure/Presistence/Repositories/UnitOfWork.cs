using System.Collections.Concurrent;

namespace Presistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;

        private ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new ();
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
            => _repositories.GetOrAdd(typeof(TEntity).Name,(_)=>new GenericRepository<TEntity, TKey>(_dbContext)) as IGenericRepository<TEntity, TKey>;


        //{
        //    //  return new GenericRepository<TEntity, TKey>(_dbContext); // Make one object for each repository

        //    // Dictionary ==> [Key,Value]
        //    // Key ==> NameOfEntity ==> String 
        //    // Value ==> Object of GenericRepository<TEntity, TKey>

        //    var Key = typeof(TEntity).Name; // Product as string
        //    if(!_repositories.ContainsKey(Key))
        //    {
        //        _repositories[Key] = new GenericRepository<TEntity, TKey>(_dbContext);
        //    }
        //    return _repositories[Key] as IGenericRepository<TEntity, TKey>;

        //}

        public async Task<int> SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
    }
}