using Microsoft.EntityFrameworkCore.Storage;
using SOLID.Data;

namespace SOLID.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IAppDbContext _context;
        private IDbContextTransaction _transcation;
        public UnitOfWork(IAppDbContext appDbContext) 
        {
            _context = appDbContext;        
        }

        public void CreateTransaction()
        {
            _transcation = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transcation.Commit();
        }

        public void Rollback()
        {
            _transcation.Rollback();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _transcation?.Dispose();
        }
    }
}
