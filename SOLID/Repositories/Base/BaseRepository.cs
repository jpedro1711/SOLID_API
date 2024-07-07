using SOLID.Data;
using SOLID.Models;

namespace SOLID.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IAppDbContext _context;

        public BaseRepository(IAppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return this._context.Set<T>().AsEnumerable();
        }

        public T Get(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

    }
}
