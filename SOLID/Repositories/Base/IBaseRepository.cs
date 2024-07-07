namespace SOLID.Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        IEnumerable<T> Get(Func<T, bool> predicate);
        T Save(T entity);
    }
}
