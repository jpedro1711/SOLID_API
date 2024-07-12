using SOLID.Data;
using SOLID.Models;
using SOLID.Repositories.Base;
using SOLID.Repositories.Interfaces;
using SOLID.Transactions;

namespace SOLID.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(IAppDbContext appDbContext, IUnitOfWork unitOfWork) : base(appDbContext, unitOfWork)
        {
        }
    }
}
