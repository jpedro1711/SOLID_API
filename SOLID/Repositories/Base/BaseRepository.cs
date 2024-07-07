using SOLID.Data;
using SOLID.Models;
using SOLID.Transactions;

namespace SOLID.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IAppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public BaseRepository(IAppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
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

        public T Save(T entity)
        {
            _context.Set<T>().Add(entity);
            _unitOfWork.Commit();
            return entity;
        }

    }
}
