using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mi_Share.Data.Infrastructure;
using Mi_Share.Model;

namespace Mi_Share.Data.Repositories
{
    public class RequestRepository : RepositoryBase<Request>, IRequestRepository
    {
        public RequestRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }
    public interface IRequestRepository : IRepository<Request>
    {

    }
}