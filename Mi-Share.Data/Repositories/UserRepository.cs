using Mi_Share.Data.Infrastructure;
using Mi_Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_Share.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>,IUserRepository
    {
        public UserRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }
    public interface IUserRepository : IRepository<User>
    {

    }
}
