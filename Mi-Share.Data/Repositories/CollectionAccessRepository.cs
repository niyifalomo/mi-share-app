using Mi_Share.Data.Infrastructure;
using Mi_Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mi_Share.Data.Repositories
{
    public class CollectionAccessRepository : RepositoryBase<CollectionAccess>, ICollectionAccessRepository
    {
        public CollectionAccessRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface ICollectionAccessRepository : IRepository<CollectionAccess>
    {

    }
}
