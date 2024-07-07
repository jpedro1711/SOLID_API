using SOLID.Data;

namespace SOLID.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IAppDbContext _context;
        public UnitOfWork(IAppDbContext appDbContext) 
        {
            _context = appDbContext;        
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
