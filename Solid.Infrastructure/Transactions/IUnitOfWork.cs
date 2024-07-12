namespace SOLID.Transactions
{
    public interface IUnitOfWork
    {
        void Commit();
        void CreateTransaction();
        void Rollback();
        void Save();
        void Dispose();
    }
}
